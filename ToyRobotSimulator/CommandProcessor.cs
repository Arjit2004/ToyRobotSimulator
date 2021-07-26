using System;
using System.Collections.Generic;
using System.Linq;

namespace ToyRobotSimulator
{
    public class CommandProcessor
    {
        private int _x, _y;
        private Directions _direction;
        private bool _isReported = false;
        private bool _isRobotPlaced = false;

        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public Directions Direction { get => _direction; set => _direction = value; }

        /// <summary>
        /// To accept, process and action the commands given by the user.
        /// </summary>
        public void ProcessCommand()
        {
            Console.WriteLine("Please enter your commands for the toy robot:");            

            while (!_isReported)
            {
                var command = Console.ReadLine().ToUpper().TrimStart();

                if (!_isRobotPlaced && command.Contains("PLACE"))
                {
                    if (IsValidCommand(command))
                    {
                        _isRobotPlaced = true;
                    }
                    //else
                    //{
                    //    Console.WriteLine("The PLACE command should follow the correct pattern, e.g: PLACE 0,0,NORTH");
                    //}
                }
                else if (_isRobotPlaced)
                {
                    if (IsValidCommand(command))
                    {
                        switch (command)
                        {
                            case "REPORT":
                                Console.WriteLine(@"Output: {0},{1},{2}", X, Y, Direction);
                                _isReported = true;
                                break;
                            case "MOVE":
                                MoveRobot();
                                break;
                            case "LEFT":
                            case "RIGHT":
                                TurnRobot(command);
                                break;
                            case "PLACE":
                                PlaceRobot(command);
                                break;
                        }
                    }
                    //else
                    //{
                    //    Console.WriteLine("Entered command is not valid.The valid commands are: MOVE, LEFT, RIGHT, REPORT and PLACE X,Y");
                    //}
                }
                //else
                //{
                //    Console.WriteLine("Robot needs to be placed first. Use PLACE command to place the robot, e.g: PLACE 0,0,NORTH");
                //}
            }
        }

        /// <summary>
        /// To validate the commands given by the user.
        /// </summary>
        /// <param name="command">Command to validate.</param>
        /// <returns>True, if the command is valid, false otherwise.</returns>
        public bool IsValidCommand(string command)
        {
            var validCommands = new List<string> { "MOVE", "LEFT", "RIGHT", "REPORT" };

            if (validCommands.Contains(command))
            {
                return true;
            }
            else if (command.Contains("PLACE"))
            {
                return PlaceRobot(command);
            }

            return false;
        }

        /// <summary>
        /// To move the robot one square ahead, in the direction it is facing towards.
        /// </summary>
        public void MoveRobot()
        {
            switch (Direction)
            {
                case Directions.NORTH: Y = Y < 5 ? Y+1 : Y;
                    break;
                case Directions.SOUTH: Y = Y > 1 ? Y-1 : Y;
                    break;
                case Directions.EAST: X = X < 5 ? X+1 : X;
                    break;
                case Directions.WEST: X = X > 1 ? X-1 : X;
                    break;
            }
        }

        /// <summary>
        /// To turn the robot to either its left or right side.
        /// </summary>
        /// <param name="side">Side, towards which the robot should turn. Left or Right.</param>
        public void TurnRobot(string side)
        {
            switch (Direction)
            {
                case Directions.NORTH:
                    Direction = side.Equals("LEFT") ? Directions.WEST : Directions.EAST;
                    break;
                case Directions.SOUTH:
                    Direction = side.Equals("LEFT") ? Directions.EAST : Directions.WEST;
                    break;
                case Directions.EAST:
                    Direction = side.Equals("LEFT") ? Directions.NORTH : Directions.SOUTH;
                    break;
                case Directions.WEST:
                    Direction = side.Equals("LEFT") ? Directions.SOUTH : Directions.NORTH;
                    break;
            }
        }

        /// <summary>
        /// To verify and place the robot on the board, based on the data provided.
        /// </summary>
        /// <param name="command">Place command, containing (x,y) co-ordinated and direction where robot is facing. e.g. PLACE 0,0,NORTH</param>
        /// <returns>True, if the command is valid and robot is successfully placed, false otherwise.</returns>
        private bool PlaceRobot(string command)
        {
            try
            {
                var placeCommand = command[6..].Split(',');
            
                var a = int.Parse(placeCommand[0]);
                var b = int.Parse(placeCommand[1]);

                if (Enumerable.Range(0, 6).Contains(a) && Enumerable.Range(0, 6).Contains(b))
                {
                    X = a;
                    Y = b;

                    if (!_isRobotPlaced || placeCommand.Length > 2)
                    { 
                        Direction = (Directions)Enum.Parse(typeof(Directions), placeCommand[2]);
                    }

                    return true;
                }                
            }
            catch
            {
                return false;
            }

            return false;
        }
    }

    /// <summary>
    /// Possible directions, where robot could face.
    /// </summary>
    public enum Directions
    {
        NORTH,
        SOUTH,
        EAST,
        WEST
    }
}
