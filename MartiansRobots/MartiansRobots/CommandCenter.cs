using MartianRobots.Commands;
using MartianRobots.Helpers;
using MartianRobots.Robot;
using MartianRobots.Surface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots
{
    public class CommandCenter : ICommandCenter
    {

        private readonly IMars Mars;
        private readonly ICommandParser commandParser;
        private readonly ICommandInvoker commandInvoker;
        private readonly OutputParser reportComposer;

        private readonly IList<IRobot> robots;

        public CommandCenter(ICommandParser _commandParser, ICommandInvoker _commandInvoker)
        {
            robots = new List<IRobot>();
            Mars = new Mars();
            reportComposer = new OutputParser();


             commandParser = _commandParser;
             commandInvoker = _commandInvoker;
            commandInvoker.SetDimensionMars(Mars);
            commandInvoker.SetRobots(robots);
        }
        public void Execute(string commandString)
        {
            var commandToExecute = commandParser.Parse(commandString);
            commandInvoker.MakeMovement(commandToExecute);
            commandInvoker.ExecuteCommands();
        }

        public IMars GetMars()
        {
            return Mars;
        }
        public string GenerateOutputRobots()
        {
            return reportComposer.OutputRobots(robots);
        }
    }
}
