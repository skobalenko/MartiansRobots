using Autofac;
using MartianRobots;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MartiansRobots.Test
{
    [TestFixture]
    public class CommandCenterTest
    {
        private IContainer container;
        [SetUp]
        public void Init()
        {
            var programAssembly = Assembly.GetAssembly(typeof(Program));

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterAssemblyTypes(programAssembly)
                .AsImplementedInterfaces();
            container = containerBuilder.Build();


        }

        [TestCase("8 8", 8, 8)]
        public void Set_Dimension_in_Mart(string commandList, int expectedHeight, int expectedWidht)
        {
            var commandCenter = container.Resolve<ICommandCenter>();
            commandCenter.Execute(commandList);
            var dimensionMars = commandCenter.GetMars().GetDimension();
            Assert.AreEqual(expectedHeight, dimensionMars.Height);
            Assert.AreEqual(expectedWidht, dimensionMars.Width);

        }
        [TestCase("1 4 N", "1 4 N ")]
        public void Set_PositionRobot_in_Mart(string commandList, string expectedOutput)
        {
            var commandStringBuilder = new StringBuilder();
            var _dimensionFilledMars = getDimensionFilledMars();

            commandStringBuilder.AppendLine(_dimensionFilledMars);
            commandStringBuilder.Append(commandList);

            var commandCenter = container.Resolve<ICommandCenter>();
            commandCenter.Execute(commandStringBuilder.ToString());
            var result = commandCenter.GenerateOutputRobots();

            Assert.AreEqual(result, expectedOutput);

        }

        [TestCase("LFLFLFFLF", "3 4 N ")]
        public void Movement_in_Surface_in_Mart(string commandList, string expectedOutput)
        {
            var commandStringBuilder = new StringBuilder();
            var _dimensionFilledMars = getDimensionFilledMars();
            var _positionRobotsInMars = getPositionRobotsInMars();
            commandStringBuilder.AppendLine(_dimensionFilledMars);
            commandStringBuilder.AppendLine(_positionRobotsInMars);
            commandStringBuilder.Append(commandList);

            var commandCenter = container.Resolve<ICommandCenter>();
            commandCenter.Execute(commandStringBuilder.ToString());
            var result = commandCenter.GenerateOutputRobots();
            Assert.AreEqual(result, expectedOutput);


        }

        [TestCase("LFFFFFFFFFFFLF")]
        public void lost_robots_in_Surface_in_Mart(string commandList)
        {
            var commandStringBuilder = new StringBuilder();
            var _dimensionFilledMars = getDimensionFilledMars();
            var _positionRobotsInMars = getPositionRobotsInMars();
            commandStringBuilder.AppendLine(_dimensionFilledMars);
            commandStringBuilder.AppendLine(_positionRobotsInMars);
            commandStringBuilder.Append(commandList);


            var commandCenter = container.Resolve<ICommandCenter>();
            commandCenter.Execute(commandStringBuilder.ToString());
            var result = commandCenter.GenerateOutputRobots();
            Assert.IsTrue(result.Contains("LOST"));


        }
        private string getDimensionFilledMars()
        {

            var commandStringBuilder = new StringBuilder();
            commandStringBuilder.Append("10 10");
            return commandStringBuilder.ToString();
        }
        private string getPositionRobotsInMars()
        {
            var commandStringBuilder = new StringBuilder();
            commandStringBuilder.Append("2 4 N");
            return commandStringBuilder.ToString();
        }
    }
}
