using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands
{
    public class BaseComMotorCommands : SerialCom
    {
        public int MotorAddresse
        {
            get;
            set;
        }

        private string ParseResponse(string fullCommand, string response)
        {
            /* Parse the response from the translation table controller. 
             * 
             * The format of full command sent to the controller is "#(motor address)(command)\r".
             * The translation table controller gives response to all the commands.
             * For commands without return value, response is simply the echo of the sent full command 
             * without #.
             * For commands with return value, response is the echo of the sent full command without #
             * with numeric return value appended at the end. 
             * 
             * ex. Command: "Zs"
             *     Full command: "#(motor address)Zs\r"
             *     Response: "(motor address)Zs+20\r"
             *     
             *     Command "s+20"
             *     Full command: "#(motor address)s+20\r"
             *     Response: "(motor address)s+20\r" */

            int comLength = fullCommand.Length;
            int responseIndex = response.IndexOf(fullCommand);

            if (responseIndex == -1)
            {
                // Case when command echo cannot be found from the controller response.
                ErrorMessage = "Invalid response from the controller (command echo not found in response).";

                return "Invalid response";
            }
            else if (response.Length == comLength)
            {
                // Case when there is no return value from the controller.
                return "";
            }
            else
            {
                // Only return the value after the fullCommand.
                return response.Substring(response.IndexOf(fullCommand) + comLength);
            }
        }

        public string MotorCommand(string command)
        {
            string fullCommand = MotorAddresse + command;
            string response = SendCommand(String.Format("#{0}\r", fullCommand));

            // In case of read time out, response is null
            if (response == null)
            {
                return null;
            }

            // When command is unrecognized by the controller, the command is return with '?' at the end.
            if (response[response.Length - 1] == '?')
            {
                ErrorMessage += "Command not recognized by the controller.";
                return null;
            }
            else
            {
                return ParseResponse(fullCommand, response);
            }
        }

        public bool MotorNoResponse(string command)
        {
            return (MotorCommand(command) == "");
        }

        public int MotorIntResponse(string command)
        {
            int response;
            if (int.TryParse(MotorCommand(command), out response))
            {
                return response;
            }
            else
            {
                ErrorMessage += "Invalid response from the controller (integer response expected and not received).";

                return 0;
            }
        }
    }
}
