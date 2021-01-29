using Autofac;
using MartianRobots.Commands;
using MartianRobots.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Text;

namespace MartianRobots
{
    public class Program
    {
        static void Main(string[] args)
        {

            var containerBuilder = createContainerBuilder();
            var commandList = buildCommandString();
            using (var container = containerBuilder.Build())
            {
                var _commandCenter = container.Resolve<ICommandCenter>();
                _commandCenter.Execute(commandList);
                GenerateOutput(commandList,_commandCenter.GenerateOutputRobots());
            }

          
        }

        private static ContainerBuilder createContainerBuilder()
        {
            var programAssembly = Assembly.GetExecutingAssembly();

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(programAssembly)
                .AsImplementedInterfaces();

            return builder;
        }

  
        private static void Configuration(IServiceCollection services)
        {
            services.AddSingleton<IMarsDimensionCommand, MarsDimensionCommand>();
            services.AddSingleton<IRobotPositionCommand, RobotPositionCommand>();
            services.AddSingleton<IRobotMovementCommand, RobotMovementCommand>();
            services.AddSingleton<ICommandTranslate, CommandTranslate>();
            services.AddSingleton<ICommandParser, CommandParser>();
            services.AddSingleton<ICommandInvoker, CommandInvoker>();
            services.AddSingleton<ICommandCenter, CommandCenter>();
        }

        private static void GenerateOutput(string commandString, string outputResult)
        {
            Console.WriteLine("Sample Input:");
            Console.WriteLine(commandString);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Sample Output:");
            Console.WriteLine(outputResult);
            Console.Write(Environment.NewLine);
            Console.Write("Press any to exit...");
            Console.ReadLine();
        }

        private static string buildCommandString()
        {
            var commandStringBuilder = new StringBuilder();
            commandStringBuilder.AppendLine("44 8");
            commandStringBuilder.AppendLine("1 2 N");
            commandStringBuilder.AppendLine("LFLFLFLFF");
            commandStringBuilder.AppendLine("3 3 E");
            commandStringBuilder.Append("FFRFFFRRF");
            return commandStringBuilder.ToString();
        }
    }
}
