using MartianRobots.Commands;
using MartianRobots.robot;
using MartianRobots.Surface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MartianRobots.Helpers
{
    public class CommandParser: ICommandParser
    {

        private readonly Func<Dimension, IMarsDimensionCommand> marsCommandFactory;
        private readonly Func<Position, CardinalDirection, IRobotPositionCommand> robotPositionCommandFactory;
        private readonly Func<IList<Movement>, IRobotMovementCommand> robotMovementCommandFactory;

        private readonly ICommandTranslate commandMatcher;
        private readonly IDictionary<CommandType, Func<string, ICommand>> commandParserDictionary;
        private readonly IDictionary<char, CardinalDirection> cardinalDirectionDictionary;
        private readonly IDictionary<char, Movement> movementDictionary;

        public CommandParser(ICommandTranslate _commandTraslate,
           Func<Dimension, IMarsDimensionCommand> _marsCommandFactory,
           Func<Position, CardinalDirection, IRobotPositionCommand> _robotPositionCommandFactory,
           Func<IList<Movement>, IRobotMovementCommand> _robotMovementCommandFactory)
        {
            commandMatcher = _commandTraslate;
            marsCommandFactory = _marsCommandFactory;
            robotPositionCommandFactory = _robotPositionCommandFactory;
            robotMovementCommandFactory = _robotMovementCommandFactory;

            commandParserDictionary = new Dictionary<CommandType, Func<string, ICommand>>
            {
                 {CommandType.MarsDimensionCommand, ParseMarsDimensionCommand},
                 {CommandType.RobotPositionCommand, ParseRobotPositionCommand},
                 {CommandType.RobotMovementCommand, ParseRobotMovementCommand}
            };

            cardinalDirectionDictionary = new Dictionary<char, CardinalDirection>
            {
                 {'N', CardinalDirection.North},
                 {'S', CardinalDirection.South},
                 {'E', CardinalDirection.East},
                 {'W', CardinalDirection.West}
            };

            movementDictionary = new Dictionary<char, Movement>
            {
                 {'L', Movement.Left},
                 {'R', Movement.Right},
                 {'F', Movement.Forward}
            };
        }

        public IEnumerable<ICommand> Parse(string commandString)
        {
            var commands = commandString.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            return commands.Select(
                command => commandParserDictionary[commandMatcher.GetCommandType(command)]
                    .Invoke(command)).ToList();
        }

        private ICommand ParseMarsDimensionCommand(string toParse)
        {
            var arguments = toParse.Split(' ');
            var width = int.Parse(arguments[0]);
            var height = int.Parse(arguments[1]);
            var dimension = new Dimension(width, height);

            var populatedCommand = marsCommandFactory(dimension);
            return populatedCommand;
        }

        private ICommand ParseRobotPositionCommand(string toParse)
        {
            var arguments = toParse.Split(' ');

            var posX = int.Parse(arguments[0]);
            var posY = int.Parse(arguments[1]);

            var directionSignifier = arguments[2][0];
            var robotDirection = cardinalDirectionDictionary[directionSignifier];

            var robotPosition = new Position(posX, posY);

            var populatedCommand = robotPositionCommandFactory(robotPosition, robotDirection);
            return populatedCommand;
        }

        private ICommand ParseRobotMovementCommand(string toParse)
        {
            var arguments = toParse.ToCharArray();
            var movements = arguments.Select(argument => movementDictionary[argument]).ToList();
            var populatedCommand = robotMovementCommandFactory(movements);
            return populatedCommand;
        }
    }
}
