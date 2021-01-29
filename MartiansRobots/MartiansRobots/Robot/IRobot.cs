using MartianRobots.robot;
using MartianRobots.Surface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Robot
{
    public interface IRobot
    {
        Position Position { get; set; }
        CardinalDirection CardinalDirection { get; set; }
        bool IsLost { get; set; }
        void Move(IEnumerable<Movement> movements, IMars _mars);
        void SetInMars(IMars _mars, Position _position, CardinalDirection _orientation);
    }
}
