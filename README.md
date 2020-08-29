# RoverRobot

## Description
The application is a simulation of a rover moving on a square tabletop, of dimensions 5 units x 5
units. There are no other obstructions on the table surface. The rover is free to roam around the
surface of the table, but must be prevented from falling to destruction. Any movement that would
result in the rover falling from the table must be prevented, however further valid movement
commands must still be allowed.

1. Create a console application (.NET Framework or .NET Core) that can read in commands of
the following form:
PLACE X,Y,F
MOVE
LEFT
RIGHT
REPORT
2. PLACE will put the rover on the table in position X,Y and facing NORTH, SOUTH, EAST or
WEST.
3. The origin (0,0) can be considered to be the SOUTH WEST most corner.
4. The first valid command to the rover is a PLACE command, after that, any sequence of
    commands may be issued, in any order, including another PLACE command. The application
    should discard all commands in the sequence until a valid PLACE command has been
    executed.
5. MOVE will move the rover one unit forward in the direction it is currently facing.
6. LEFT and RIGHT will rotate the rover 90 degrees in the specified direction without changing
the position of the rover.
7. REPORT will announce the X,Y and F of the rover.
8. A rover that is not on the table can choose to ignore the MOVE, LEFT, RIGHT and REPORT
commands.
9. Provide tests with good coverage to exercise the application.

## Constraints
1. The rover must not fall off the table during movement. This also includes the initial
    placement of the rover.
2.  Any move that would cause the rover to fall must be ignored.
3.  Input can be from a file or from standard input. It must be possible to accept input from
either a file or from standard input.

# Sample Input
PLACE 0,0,NORTH
MOVE
REPORT

PLACE 0,0,NORTH
LEFT
REPORT

PLACE 1,2,EAST
MOVE
MOVE
LEFT
MOVE
REPORT

# Sample Output
0,1,NORTH
0,0,WEST
3,3,NORTH