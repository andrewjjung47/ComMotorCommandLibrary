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
        public int GetCurrentReduction()
        {
            return MotorIntResponse("Zr");
        }

        public int GetPhaseCurrent()
        {
            return MotorIntResponse("Zi");
        }

        public int GetStepMode()
        {
            return MotorIntResponse("Zg");
        }

        public bool SetCurrentReduction(int currentReduction)
        {
            return MotorNoResponse("r" + currentReduction);
        }

        public bool SetPhaseCurrent(int phaseCurrent)
        {
            return MotorNoResponse("i" + phaseCurrent);
        }

        public bool SetRampType(int rampType)
        {
            return MotorNoResponse(":ramp_mode=" + rampType);
        }

        public bool SetStepMode(int stepMode)
        {
            return MotorNoResponse("g" + stepMode);
        }

        // Manipulating record settings
        public bool ChooseRecord(int recordNumber)
        {
            return MotorNoResponse("y" + recordNumber);
        }

        public bool SetRecord(int recordNumber)
        {
            return MotorNoResponse(">" + recordNumber);
        }

        public int GetBrakeRamp(int operationNumber = 0)
        {
            return MotorIntResponse("Z" + operationNumber + "B");
        }

        public int GetDirection(int operationNumber = 0)
        {
            return MotorIntResponse("Z" + operationNumber + "d");
        }

        public int GetMaxFrequency(int operationNumber = 0)
        {
            return MotorIntResponse("Z" + operationNumber + "o");
        }

        public int GetPositionType(int operationNumber = 0)
        {
            return MotorIntResponse("Z" + operationNumber + "p");
        }

        public int GetRamp(int operationNumber = 0)
        {
            return MotorIntResponse("Z" + operationNumber + "b");
        }

        public int GetRampType(int operationNumber = 0)
        {
            return MotorIntResponse("Z" + operationNumber + ":ramp_mode");
        }

        public int GetRepeat(int operationNumber = 0)
        {
            return MotorIntResponse("Z" + operationNumber + "W");
        }

        public int GetStartFrequency(int operationNumber = 0)
        {
            return MotorIntResponse("Z" + operationNumber + "u");
        }

        public int GetSteps(int operationNumber = 0)
        {
            return MotorIntResponse("Z" + operationNumber + "s");
        }

        public bool SetBrakeRamp(int rampBrake)
        {
            return MotorNoResponse("B" + rampBrake);
        }

        public bool SetDirection(int direction)
        {
            return MotorNoResponse("d" + direction);
        }

        public bool SetMaxFrequency(int maxFrequency)
        {
            return MotorNoResponse("o" + maxFrequency);
        }

        public bool SetPositionType(int positionType)
        {
            return MotorNoResponse("p" + positionType);
        }

        public bool SetRamp(int ramp)
        {
            return MotorNoResponse("b" + ramp);
        }

        public bool SetRepeat(int repeats)
        {
            return MotorNoResponse("W" + repeats);
        }

        public bool SetStartFrequency(int startFrequency)
        {
            return MotorNoResponse("u" + startFrequency);
        }

        public bool SetSteps(int steps)
        {
            return MotorNoResponse("s" + steps);
        }

        // Movement commands
        public bool StartTravelProfile()
        {
            return MotorNoResponse("A");
        }

        public bool StopTravelProfile()
        {
            // Stop with brake ramp.
            return MotorNoResponse("S1");
        }

        public bool QuickStopTravelProfile()
        {
            // Stop abruptly without brake ramp.
            return MotorNoResponse("S");
        }

        // Miscellaneous commands
        public int GetStatusByte()
        {
            return MotorIntResponse("$");
        }

        public bool SetInputMaskEdge(int ioMask)
        {
            return MotorNoResponse("h" + ioMask);
        }

        public bool SetLimitSwitchBehavior(int refInternal, int norInternal, 
            int refExternal, int norExternal)
        {
            return MotorNoResponse("l" + (refInternal + norInternal + refExternal + norExternal));
        }

        public int GetPosition()
        {
            return MotorIntResponse("C");
        }
    }
}
