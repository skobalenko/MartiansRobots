using MartianRobots.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MartianRobots.Helpers
{
    public class CommandTranslate: ICommandTranslate
    {
        private IDictionary<string, CommandType> commandTypeDictionary;

        public CommandTranslate()
        {
            InitializeCommandTypeDictionary();
        }

        public CommandType GetCommandType(string command)
        {
            try
            {
                var commandType = commandTypeDictionary.First(
                    regexToCommandType => new Regex(regexToCommandType.Key).IsMatch(command));

                return commandType.Value;
            }
            catch (InvalidOperationException e)
            {
                var exceptionMessage = String.Format("String '{0}' is not a valid command", command);
                throw new Exception(exceptionMessage, e);
            }
        }

        private void InitializeCommandTypeDictionary()
        {
            commandTypeDictionary = new Dictionary<string, CommandType>
            {
                  { @"^([0-4]?[0-9]|5[0]) ([0-4]?[0-9]|5[0])$", CommandType.MarsDimensionCommand },
                { @"^\d+ \d+ [NSEW]$", CommandType.RobotPositionCommand },
                { @"^[LRF]{0,100}$", CommandType.RobotMovementCommand }
            };
        }

     
    }
}
