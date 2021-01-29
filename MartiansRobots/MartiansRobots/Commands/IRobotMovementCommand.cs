using MartianRobots.robot;
using MartianRobots.Robot;
using MartianRobots.Surface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Commands
{
    public interface IRobotMovementCommand : ICommand
    {
        IList<Movement> Movements { get; }
       void SetReceiver(IRobot _robot, IMars _mars);
    }
}
