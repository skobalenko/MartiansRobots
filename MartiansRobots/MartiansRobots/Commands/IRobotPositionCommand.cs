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
    public interface IRobotPositionCommand : ICommand
    {
        Position RobotPosition { get; set; }
        CardinalDirection RobotOrientation { get; set; }
        void SetReceiver(IRobot _robot, IMars _mars);
    }
}
