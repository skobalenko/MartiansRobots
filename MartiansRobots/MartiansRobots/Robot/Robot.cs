using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartianRobots.Robot;
using MartianRobots.Surface;

namespace MartianRobots.robot
{
    public class Robot: IRobot
    {
        public CardinalDirection CardinalDirection { get; set; }
        public Position Position { get; set; }
        public bool IsLost { get; set; }

        private readonly IDictionary<Movement, Action<IMars>> movementMethodDictionary;
        private readonly IDictionary<CardinalDirection, Action<IMars>> leftMoveDictionary;
        private readonly IDictionary<CardinalDirection, Action<IMars>> rightMoveDictionary;
        private readonly IDictionary<CardinalDirection, Action<IMars>> forwardMoveDictionary;


        public Robot()
        {
            IsLost = false;
            movementMethodDictionary = new Dictionary<Movement, Action<IMars>>
            {
                {Movement.Left, (_mars) => leftMoveDictionary[CardinalDirection].Invoke(_mars)},
                {Movement.Right, (_mars) => rightMoveDictionary[CardinalDirection].Invoke(_mars)},
                {Movement.Forward, (_mars) => forwardMoveDictionary[CardinalDirection].Invoke(_mars)}
            };

            leftMoveDictionary = new Dictionary<CardinalDirection, Action<IMars>>
            {
                {CardinalDirection.North, (_mars) =>MoveCardinateOrientation(CardinalDirection.West)},
                {CardinalDirection.East, (_mars) => MoveCardinateOrientation(CardinalDirection.North)},
                {CardinalDirection.South, (_mars) => MoveCardinateOrientation(CardinalDirection.East)},
                {CardinalDirection.West, (_mars) => MoveCardinateOrientation(CardinalDirection.South)}
            };

            rightMoveDictionary = new Dictionary<CardinalDirection, Action<IMars>>
            {
                {CardinalDirection.North, (_mars) => MoveCardinateOrientation(CardinalDirection.East)},
                {CardinalDirection.East, (_mars) => MoveCardinateOrientation(CardinalDirection.South)},
                {CardinalDirection.South, (_mars) => MoveCardinateOrientation(CardinalDirection.West)},
                {CardinalDirection.West, (_mars) => MoveCardinateOrientation(CardinalDirection.North)}
            };

            forwardMoveDictionary = new Dictionary<CardinalDirection, Action<IMars>>
            {
                {CardinalDirection.North, (_mars) => {MovePosition( new Position(Position.X, Position.Y + 1),_mars);}},
                {CardinalDirection.East, (_mars) => {MovePosition( new Position(Position.X + 1, Position.Y),_mars);}},
                {CardinalDirection.South, (_mars) => {MovePosition( new Position(Position.X, Position.Y - 1),_mars);}},
                {CardinalDirection.West, (_mars) => {MovePosition( new Position(Position.X - 1, Position.Y),_mars);}}
            };


        }
        private void MoveCardinateOrientation(CardinalDirection _cardinalDirection)
        {
            if (!IsLost)
            {
                CardinalDirection = _cardinalDirection;
            }
        }
        private void MovePosition(Position _position, IMars mars)
        {
            if (!IsLost)
            {
                if (mars.IsValid(_position))
                {
                    Position = _position;
                }
                else
                {
                    IsLost = true;
                }
            }
        }
        public void SetInMars(IMars _mars, Position _position, CardinalDirection _orientation)
        {
            if (_mars.IsValid(_position))
            {
                Position = _position;
                CardinalDirection = _orientation;
                return;
            }
        }
        public void Move(IEnumerable<Movement> movements, IMars _mars)
        {
            foreach (var movement in movements)
            {
                movementMethodDictionary[movement].Invoke(_mars);
            }
        }

      
    }
}
