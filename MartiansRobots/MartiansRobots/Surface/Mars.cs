using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Surface
{
    public class Mars: IMars
    {
        
        private Dimension dimension { get; set; }
        public List<Position> positionLocked { get; set; }
        public void SetDimension(Dimension _dimension)
        {
            dimension = _dimension;
        }
        
        public Dimension GetDimension()
        {
            return dimension;
        }
        private void setPositionLocked(Position _position)
        {
            positionLocked.Add(_position);
        }

        public Mars()
        {
            positionLocked = new List<Position>();
            dimension = new Dimension();
        }
        public bool IsValid(Position _position)
        {
            var isValidX = _position.X >= 0 && _position.X <= dimension.Width && !positionLocked.Any(x=>x.Equals(_position));
            var isValidY = _position.Y >= 0 && _position.Y <= dimension.Height && !positionLocked.Any(x => x.Equals(_position));
            if (!isValidX && !isValidY)
                setPositionLocked(_position);
            return isValidX && isValidY;
        }
    }
}
