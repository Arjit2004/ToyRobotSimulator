using System;
using System.Collections.Generic;

namespace ToyRobotSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            var simulator = new CommandProcessor();
            simulator.ProcessCommand();
        }        
    }
}
