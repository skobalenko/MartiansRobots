using MartianRobots.Robot;
using MartianRobots.Surface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Commands
{
    public interface ICommandInvoker
    {
        void SetDimensionMars(IMars _mars);
        void SetRobots(IList<IRobot> robots);
        void MakeMovement(IEnumerable<ICommand> _movementList);
        void ExecuteCommands();
    }
}
