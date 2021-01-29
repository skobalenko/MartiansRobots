using MartianRobots.Surface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Commands
{
    public class MarsDimensionCommand : IMarsDimensionCommand
    {
        public Dimension Dimension {get;set;}
        private IMars Mars;
        public MarsDimensionCommand(Dimension _dimension, IMars _mars)
        {
            Mars = _mars;
            Dimension = _dimension;
        }
        public CommandType GetCommandType()
        {
            return CommandType.MarsDimensionCommand;
        }
        public void Execute()
        {
            Mars.SetDimension(Dimension);
        }

        public void SetReceiver(IMars _mars)
        {
            Mars = _mars;
        }
    }
}
