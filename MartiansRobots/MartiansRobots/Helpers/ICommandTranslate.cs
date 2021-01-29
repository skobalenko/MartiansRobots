using MartianRobots.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Helpers
{
    public interface ICommandTranslate
    {
        CommandType GetCommandType(string command);
    }
}
