using System;
using Xunit;

namespace ToyRobotSimulator.Tests
{
    public class CommandProcessorTests
    {
        private readonly CommandProcessor _processor;

        public CommandProcessorTests()
        {
            _processor = new CommandProcessor();
        }

        [Theory]
        [InlineData("Move", true)]
        [InlineData("left", true)]
        [InlineData("RIGHT", true)]
        [InlineData("report", true)]
        [InlineData("PLACE 0,1", false)]
        [InlineData("random", false)]
        [InlineData("place", false)]
        [InlineData("place 0123", false)]
        [InlineData("place 5,8,up", false)]
        [InlineData("place 5,8,north", false)]
        [InlineData("place 5,5,north", true)]
        public void EnsureCommandValidationWorks(string command, bool isValid)
        {
            var outcome = _processor.IsValidCommand(command.ToUpper());
            Assert.Equal(isValid, outcome);
        }

        [Theory]
        [InlineData(Directions.NORTH, 2, 3)]
        [InlineData(Directions.SOUTH, 2, 1)]
        [InlineData(Directions.EAST, 3, 2)]
        [InlineData(Directions.WEST, 1, 2)]
        public void EnsureRobotIsMoving(Directions direction, int x, int y)
        {
            _processor.X = _processor.Y = 2;
            _processor.Direction = direction;

            _processor.MoveRobot();

            Assert.Equal(x, _processor.X);
            Assert.Equal(y, _processor.Y);
        }

        [Theory]
        [InlineData(0, 5, Directions.NORTH)]
        [InlineData(0, 0, Directions.SOUTH)]
        [InlineData(5, 0, Directions.EAST)]
        [InlineData(0, 5, Directions.WEST)]
        public void EnsureRobotIsMovingWithInBoard(int x, int y, Directions direction)
        {
            _processor.X = x;
            _processor.Y = y;
            _processor.Direction = direction;

            _processor.MoveRobot();

            Assert.Equal(x, _processor.X);
            Assert.Equal(y, _processor.Y);
            Assert.Equal(direction, _processor.Direction);
        }

        [Theory]
        [InlineData(Directions.NORTH, "LEFT", Directions.WEST)]
        [InlineData(Directions.NORTH, "RIGHT", Directions.EAST)]
        [InlineData(Directions.SOUTH, "LEFT", Directions.EAST)]
        [InlineData(Directions.SOUTH, "RIGHT", Directions.WEST)]
        [InlineData(Directions.EAST, "LEFT", Directions.NORTH)]
        [InlineData(Directions.EAST, "RIGHT", Directions.SOUTH)]
        [InlineData(Directions.WEST, "LEFT", Directions.SOUTH)]
        [InlineData(Directions.WEST, "RIGHT", Directions.NORTH)]
        public void EnsureRobotIsTurning(Directions direction, string command, Directions expectedDirection)
        {
            _processor.X = 0;
            _processor.Y = 0;
            _processor.Direction = direction;

            _processor.TurnRobot(command);

            Assert.Equal(expectedDirection, _processor.Direction);
        }
    }
}
