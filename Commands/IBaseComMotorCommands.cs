using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands
{
    public interface IBaseComMotorCommands
    {
        // Accessors
        int Baudrate();

        int MotorAddresse();

        string SelectedPort();

        // Motor settings related
        int GetCurrentReduction();

        bool SetCurrentReduction(int currentReduction);

        int GetPhaseCurrent();

        bool SetPhaseCurrent(int phaseCurrent);

        int GetStepMode();

        bool SetStepMode(int stepMode);

        bool SetInputMaskEdge(int ioMask);

        bool SetLimitSwitchBehavior(int refInternal, int norInternal,
            int refExternal, int norExternal);

        // Record settings related
        bool ChooseRecord(int recordNumber);

        bool SetRecord(int recordNumber);

        int GetBrakeRamp(int operationNumber = 0);

        bool SetBrakeRamp(int rampBrake);

        int GetDirection(int operationNumber = 0);

        bool SetDirection(int direction);

        int GetMaxFrequency(int operationNumber = 0);

        bool SetMaxFrequency(int maxFrequency);

        int GetPositionType(int operationNumber = 0);

        bool SetPositionType(int positionType);

        int GetRamp(int operationNumber = 0);

        bool SetRamp(int ramp);

        int GetRampType(int operationNumber = 0);

        bool SetRampType(int rampType);

        int GetRepeat(int operationNumber = 0);

        bool SetRepeat(int repeats);

        int GetStartFrequency(int operationNumber = 0);

        bool SetStartFrequency(int startFrequency);

        int GetSteps(int operationNumber = 0);

        bool SetSteps(int steps);
        
        // Communication
        int GetPosition();

        int GetStatusByte();

        bool StartTravelProfile();

        bool StopTravelProfile();

        bool QuickStopTravelProfile();
    }
}
