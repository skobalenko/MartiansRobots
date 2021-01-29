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
    public class RobotPositionCommand : IRobotPositionCommand
    {
        public Position RobotPosition { get; set; }
        public CardinalDirection RobotOrientation { get; set; }
        private IRobot Robot;
        private IMars Mars;
        public RobotPositionCommand(Position _position, CardinalDirection _orientation)
        {
            RobotPosition = _position;
            RobotOrientation = _orientation;
        }
        public CommandType GetCommandType()
        {
            return CommandType.RobotPositionCommand;
        }
        public void Execute()
        {
            Robot.SetInMars(Mars, RobotPosition, RobotOrientation);
        }
        public void SetReceiver(IRobot _robot, IMars _mars)
        {
            Robot = _robot;
            Mars = _mars;
        }
    }
}
