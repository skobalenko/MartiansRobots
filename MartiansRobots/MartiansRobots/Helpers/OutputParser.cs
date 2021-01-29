using MartianRobots.robot;
using MartianRobots.Robot;
using MartianRobots.Surface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Helpers
{
    public class OutputParser
    {
        private readonly IDictionary<CardinalDirection, char> cardinalDirectionDictionary;

        public OutputParser()
        {
            cardinalDirectionDictionary = new Dictionary<CardinalDirection, char>
            {
                 {CardinalDirection.North, 'N'},
                 {CardinalDirection.South, 'S'},
                 {CardinalDirection.East, 'E'},
                 {CardinalDirection.West, 'W'}
            };
        }

        public string Compose(Position _position, CardinalDirection _cardinalDirection, bool _isLost)
        {
            var item1 = _position.X;
            var item2 = _position.Y;
            var item3 = cardinalDirectionDictionary[_cardinalDirection];

            var report = new StringBuilder();
            var isLost = _isLost ? "LOST" : string.Empty;
            report.AppendFormat("{0} {1} {2} {3}", item1, item2, item3, isLost);
            return report.ToString();
        }
        public string OutputRobots(IEnumerable<IRobot> robots)
        {
            var outputResult = outputEachRobot(robots);
            return convertToString(outputResult);
        }
        private StringBuilder outputEachRobot(IEnumerable<IRobot> robots)
        {
            var outputResult = new StringBuilder();
            foreach (var robot in robots)
            {
               
                var robotResult = Compose(robot.Position, robot.CardinalDirection,robot.IsLost);
                outputResult.AppendLine(robotResult);
            }
            return outputResult;
        }
        private static string convertToString(StringBuilder outputResults)
        {
            return outputResults.ToString()
                .TrimEnd('\n', '\r');
        }
    }
}
