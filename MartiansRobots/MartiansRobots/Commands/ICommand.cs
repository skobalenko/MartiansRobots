using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Commands
{
    public interface ICommand
    {
        CommandType GetCommandType();
        void Execute();
    }
}
