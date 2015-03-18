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
            protected set { 
                errorFlag = value; 
                if (!value)
                {
                    errorMessage = "";
                }
            }
        }

        // Used from UI to display error message. 
        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            // When errorMesssaged is added, errorFlag is always true. 
            protected set 
            { 
                ErrorFlag = true;
                errorMessage = value + Environment.NewLine;
            }
        }

        /// <summary>
        /// Adds an error message to ErrorMessage and throws CommandsException.
        /// </summary>
        /// <param name="message"></param>
        public void ReportError(string message)
        {
            ErrorMessage += message;
            throw new CommandsException(message);
        }

        /// <summary>
        /// Adds an error message to ErrorMessage 
        /// and propagates inner exception using CommandsException.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="message"></param>
        public void ReportError(string message, Exception e)
        {
            ErrorMessage += message;
            throw new CommandsException(message, e);
        }

        /// <summary>
        /// Recover from error by setting errorFlag back to 'false'. This clears ErrorMessage
        /// </summary>
        public void ResetErrorFlag()
        {
            ErrorFlag = false;
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
            port.ReadTimeout = (this.ReadTimeout == 0) ? 100 : this.ReadTimeout;
            port.WriteTimeout = (this.WriteTimeout == 0) ? 100 : this.WriteTimeout;

            try
            {
                port.Open();
            }
            catch (UnauthorizedAccessException e)
            {
                ReportError("Port access denied.", e);
            }
            catch (ArgumentException e)
            {
                ReportError("Invalid port name.", e);
            }
            catch (System.IO.IOException e)
            {
                ReportError("Port does not exist.", e);
            }
            catch (Exception e)
            {
                ReportError("Unknown error while opening the serial port.", e);
            }
        }

        public static void ClosePort()
        {
            if (port != null && port.IsOpen == true)
            {
                port.Close();
            }
        }

        private readonly Object _lock = new Object();

        public string SendCommand(string command)
        {
            lock(_lock)
            {
                if (port == null || port.IsOpen == false)
                {
                    OpenPort();
                }

                try
                {
                    port.Write(command);
                }
                catch (System.TimeoutException e)
                {
                    ReportError("Write time out.", e);
                }

                try
                {
                    return port.ReadTo("\r");
                }
                catch (System.TimeoutException e)
                {
                    ReportError("Read time out. Check the connection.", e);

                    return null;
                }
            }
        }

        public class CommandsException : Exception
        {
            public CommandsException()
                : base() { }

            public CommandsException(string message)
                : base(message) { }

            public CommandsException(string format, params object[] args)
                : base(string.Format(format, args)) { }

            public CommandsException(string message, Exception innerException)
                : base(message, innerException) { }

            public CommandsException(string format, Exception innerException, params object[] args)
                : base(string.Format(format, args), innerException) { }
        }
    }
}
