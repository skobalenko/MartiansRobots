using MartianRobots.Surface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Commands
{
    public interface IMarsDimensionCommand:ICommand
    {
        Dimension Dimension { get; }
        void SetReceiver(IMars _mars);
    }
}
