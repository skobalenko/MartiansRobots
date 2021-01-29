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
    public class RobotMovementCommand : IRobotMovementCommand
    {
        public IList<Movement> Movements { get; set; }
        public IRobot robot { get; set; }
        public IMars mars { get; set; }

        public RobotMovementCommand(IList<Movement> _movements)
        {
               Movements = _movements;
        }

        public CommandType GetCommandType()
        {
            return CommandType.RobotMovementCommand;
        }
        public void Execute()
        {
            robot.Move(Movements, mars);
        }

        public void SetReceiver(IRobot _robot, IMars _mars)
        {
            robot = _robot;
            mars = _mars;
        }
    }
}
