using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Surface
{
    public interface IMars
    {
        void SetDimension(Dimension _dimension);
        List<Position> positionLocked { get; set; }
        Dimension GetDimension();

        bool IsValid(Position _position);
    }
}
