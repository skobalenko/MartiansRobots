using MartianRobots.Surface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots
{
    public interface ICommandCenter
    {
        void Execute(string commandString);
        IMars GetMars();
        string GenerateOutputRobots();
    }
}
