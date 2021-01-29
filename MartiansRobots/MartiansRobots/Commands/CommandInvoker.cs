using MartianRobots.Robot;
using MartianRobots.Surface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Commands
{
    public class CommandInvoker:ICommandInvoker
    {
        private readonly Func<IRobot> roverFactory;
        private readonly IDictionary<CommandType, Action<ICommand>> setReceiversMethodDictionary;

        private IMars Mars;
        private IList<IRobot> Robots;
        private IEnumerable<ICommand> commandList;

        public CommandInvoker(Func<IRobot> aRoverFactory)
        {
            roverFactory = aRoverFactory;

            setReceiversMethodDictionary = new Dictionary<CommandType, Action<ICommand>>
            {
                {CommandType.MarsDimensionCommand, SetReceiverMarsDimensionCommand},
                {CommandType.RobotMovementCommand, SetReceiverORobotMovementCommand},
                {CommandType.RobotPositionCommand, SetReceiversOnRobotPositionCommand}
                
            };
        }

        public void SetDimensionMars(IMars _mars)
        {
            Mars = _mars;
        }

        public void SetRobots(IList<IRobot> _robots)
        {
            Robots = _robots;
        }

        public void MakeMovement(IEnumerable<ICommand> aCommandList)
        {
            commandList = aCommandList;
        }

        public void ExecuteCommands()
        {
            foreach (var command in commandList)
            {
                setReceivers(command);
                command.Execute();
            }
        }

        private void setReceivers(ICommand command)
        {
            setReceiversMethodDictionary[command.GetCommandType()]
                .Invoke(command);
        }

        private void SetReceiverMarsDimensionCommand(ICommand command)
        {
            var marsDimensionCommand = (IMarsDimensionCommand)command;
            marsDimensionCommand.SetReceiver(Mars);
        }

        private void SetReceiverORobotMovementCommand(ICommand command)
        {
            var robotMovementCommand = (IRobotMovementCommand)command;
            var latestrobot = Robots[Robots.Count - 1];
          
            robotMovementCommand.SetReceiver(latestrobot, Mars);
        }

        private void SetReceiversOnRobotPositionCommand(ICommand command)
        {
            var robotPositionCommand = (IRobotPositionCommand)command;
            var newRobot = roverFactory();
            Robots.Add(newRobot);
            robotPositionCommand.SetReceiver(newRobot, Mars);
        }


    }
}
