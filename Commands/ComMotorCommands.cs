using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands
{
    public class ComMotorCommands : BaseComMotorCommands
    {
        // Manipulating motor settings
        public virtual int GetCurrentReduction()
        {
            return MotorIntResponse("Zr");
        }

        public virtual int GetPhaseCurrent()
        {
            return MotorIntResponse("Zi");
        }

        public virtual int GetStepMode()
        {
            return MotorIntResponse("Zg");
        }

        public virtual bool SetCurrentReduction(int currentReduction)
        {
            return MotorNoResponse("r" + currentReduction);
        }

        public virtual bool SetPhaseCurrent(int phaseCurrent)
        {
            return MotorNoResponse("i" + phaseCurrent);
        }

        public virtual bool SetRampType(int rampType)
        {
            return MotorNoResponse(":ramp_mode=" + rampType);
        }

        public virtual bool SetStepMode(int stepMode)
        {
            return MotorNoResponse("g" + stepMode);
        }

        // Manipulating record settings
        public virtual bool ChooseRecord(int recordNumber)
        {
            return MotorNoResponse("y" + recordNumber);
        }

        public virtual bool SetRecord(int recordNumber)
        {
            return MotorNoResponse(">" + recordNumber);
        }

        public virtual int GetBrakeRamp(int operationNumber = 0)
        {
            return MotorIntResponse("Z" + operationNumber + "B");
        }

        public virtual int GetDirection(int operationNumber = 0)
        {
            return MotorIntResponse("Z" + operationNumber + "d");
        }

        public virtual int GetMaxFrequency(int operationNumber = 0)
        {
            return MotorIntResponse("Z" + operationNumber + "o");
        }

        public virtual int GetPositionType(int operationNumber = 0)
        {
            return MotorIntResponse("Z" + operationNumber + "p");
        }

        public virtual int GetRamp(int operationNumber = 0)
        {
            return MotorIntResponse("Z" + operationNumber + "b");
        }

        public virtual int GetRampType(int operationNumber = 0)
        {
            return MotorIntResponse("Z" + operationNumber + ":ramp_mode");
        }

        public virtual int GetRepeat(int operationNumber = 0)
        {
            return MotorIntResponse("Z" + operationNumber + "W");
        }

        public virtual int GetStartFrequency(int operationNumber = 0)
        {
            return MotorIntResponse("Z" + operationNumber + "u");
        }

        public virtual int GetSteps(int operationNumber = 0)
        {
            return MotorIntResponse("Z" + operationNumber + "s");
        }

        public virtual bool SetBrakeRamp(int rampBrake)
        {
            return MotorNoResponse("B" + rampBrake);
        }

        public virtual bool SetDirection(int direction)
        {
            return MotorNoResponse("d" + direction);
        }

        public virtual bool SetMaxFrequency(int maxFrequency)
        {
            return MotorNoResponse("o" + maxFrequency);
        }

        public virtual bool SetPositionType(int positionType)
        {
            return MotorNoResponse("p" + positionType);
        }

        public virtual bool SetRamp(int ramp)
        {
            return MotorNoResponse("b" + ramp);
        }

        public virtual bool SetRepeat(int repeats)
        {
            return MotorNoResponse("W" + repeats);
        }

        public virtual bool SetStartFrequency(int startFrequency)
        {
            return MotorNoResponse("u" + startFrequency);
        }

        public virtual bool SetSteps(int steps)
        {
            return MotorNoResponse("s" + steps);
        }

        // Movement commands
        public virtual bool StartTravelProfile()
        {
            return MotorNoResponse("A");
        }

        public virtual bool StopTravelProfile()
        {
            // Stop with brake ramp.
            return MotorNoResponse("S1");
        }

        public virtual bool QuickStopTravelProfile()
        {
            // Stop abruptly without brake ramp.
            return MotorNoResponse("S");
        }

        // Miscellaneous commands
        public virtual int GetStatusByte()
        {
            return MotorIntResponse("$");
        }

        /// <summary>
        /// Returns a boolean whether the motor is referenced or not.
        /// </summary>
        /// <returns>'true' if the motor is referenced, 
        /// 'false' if the motor is yet referenced.</returns>
        public virtual bool IsReferenced()
        {
            return Convert.ToBoolean(2 & GetStatusByte());
        }

        public virtual bool SetInputMaskEdge(int ioMask)
        {
            return MotorNoResponse("h" + ioMask);
        }

        public virtual bool SetLimitSwitchBehavior(int refInternal, int norInternal, 
            int refExternal, int norExternal)
        {
            return MotorNoResponse("l" + (refInternal + norInternal + refExternal + norExternal));
        }

        public virtual int GetPosition()
        {
            return MotorIntResponse("C");
        }
    }
}
