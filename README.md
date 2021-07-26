# ToyRobotSimulator

This program is written to simulate the movement of a Toy Robot on a 6 X 6 board.
Once started, it will ask user to enter commands to place/move/turn the robot. Once done, user can report the whereabouts of the robot, on the board.

Before anything else, user first have to place the robot on the board, by using PLACE command, in the format:

PLACE 0,0,NORTH

where, PLACE is the command to place the robot on board.
       first '0' is the X co-ordinate, and the second one is the Y co-ordinate of the robot.
       NORTH is the direction, in which robot is facing.
       
Once robot is placed on the board, user can use:
- MOVE command, to move the robot one place forward, in the direction it is facing towards.
- LEFT/RIGHT commands, to turn the robot left/right and change its direction by 90 degrees, without changing its position on the board.
- REPORT command, to get the wherabouts of the robot on the board. The outcome of the REPORT command would be in format:
        Ouptput: 0,0,NORTH
        where 0,0 are the robot's co-ordinates, and NORTH is the direction it is facing towards.
        
 There are few rules to follow, while using this simulator:
 * The first valid command to the robot is a PLACE command. After that, any sequence of commands may be issued, in any order, including another PLACE command. The library 
 would ignore all commands in the sequence until a valid PLACE command has been executed.
 * Once the robot is on the table, subsequent PLACE commands could leave out the direction and only provide the coordinates. When this happens, the robot moves to the new 
 coordinates without changing the direction.
 * The PLACE command would be ignored if it places the robot outside of the table. e.g.: PLACE 0,6,NORTH
 * All other commands, not mentioned above, would be ignored. However, and valid subsequent command would be processed, as-usual.
 
 Enjoy the simulator!!
