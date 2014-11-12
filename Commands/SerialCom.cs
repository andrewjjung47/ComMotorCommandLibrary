using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands
{
    public class SerialCom
    {
        private static SerialPort port = null;

        public int Baudrate
        {
            get;
            set;
        }

        public string SelectedPort
        {
            get;
            set;
        }

        public int ReadTimeout
        {
            get;
            set;
        }

        public int WriteTimeout
        {
            get;
            set;
        }

        // Used outside of the command module (eg. UI) to know whether any error has occured or not.
        private bool errorFlag = false;
        public bool ErrorFlag
        {
            get { return errorFlag; }
            protected set { errorFlag = value; }
        }

        // Used from UI to display error message. 
        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            // When errorMesssaged is added, errorFlag is always true. 
            protected set { ErrorFlag = true; errorMessage = value + Environment.NewLine; }
        }

        private void OpenPort()
        {
            // Only called from SendCommand()
            if (port == null)
            {
                port = new SerialPort();
            }

            port.PortName = SelectedPort;
            port.BaudRate = (this.Baudrate <= 0) ? 115200 : this.Baudrate;
            port.Parity = Parity.None;
            port.DataBits = 8;
            port.StopBits = StopBits.One;

            // Default timeout is 100ms when not specified.
            port.ReadTimeout = (this.ReadTimeout == null) ? 100 : this.ReadTimeout;
            port.WriteTimeout = (this.WriteTimeout == null) ? 100 : this.WriteTimeout;

            try
            {
                port.Open();
            }
            catch (UnauthorizedAccessException)
            {
                ErrorMessage += "Port access denied.";
            }
            catch (ArgumentException)
            {
                ErrorMessage += "Invalid port name.";
            }
            catch (System.IO.IOException)
            {
                ErrorMessage += "Port does not exist.";
            }
            catch (Exception)
            {
                ErrorMessage += "Unknown error while opening the serial port.";
            }
        }

        public void ClosePort()
        {
            if (port != null || port.IsOpen == true)
            {
                port.Close();
            }
        }

        public string SendCommand(string command)
        {
            if (port.IsOpen == false)
            {
                OpenPort();
            }

            try
            {
                port.Write(command);
            }
            catch (System.TimeoutException)
            {
                ErrorMessage += "Write time out.";
            }

            try
            {
                return port.ReadTo("\r");
            }
            catch (System.TimeoutException)
            {
                ErrorMessage += "Read time out. Check the connection.";

                return null;
            }
        }

        public void ResetErrorFlag()
        {
            // Recover from error by clearing errorMessage and setting errorFlag back to 'false'.
            ErrorMessage = "";
            ErrorFlag = false;
        }
    }
}
