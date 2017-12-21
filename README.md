# solveMaze
This program is a C# .Net Core API Service that accepts a Json body through a POST and returns a Json back with the answer of how many steps it took as well as the edited maze showing the path it took.

# Getting Started
These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

# Prerequisites
Microsoft Visual Studio 2017.
A way to send a Json to a Post Method(I used Postman)
A maze to send to the API using
  . that represents an open road
  # that represents a blocked road
  A that represents the starting point
  B that represents the destination point

# Installing
After installing Microsoft Visual Studio, Double Click on the .cproj.
Press C+F5 to run the program.

# (option A - has the visual information returned)
Open Postman.
Select New in the top left corner.
Select Request option.
Add a name to the request called "DemoMaze".
Click Create Collection near the bottom.
Type "Maze" in the Name your collection box.
Select the checkmark.
Select "Save to Maze" button.
Change the Drop Down from "GET" to "POST".
Enter URL "http://localhost:8080/solveMaze" in the URL Box.
Below that, Select Body as your option.
Below that, select the raw radio button.
Change the DropDown from "Text" to "Json (application/json)"
Copy your Maze that you would like solved into the body below or you can take this maze that i have added for you.
##########
#A...#...#
#.#.##.#.#
#.#.##.#.#
#.#....#B#
#.#.##.#.#
#....#...#
##########

Select The Blue "Send" Button.
View the results in the footer.

# (option B - runs a unit test to validate the maze is solved)
In Visual Studion, Select Solution Explorer on the right.
Double click on UnitTest1.
Right Click on the TestMethod1 and select Run Test.
If the DIalog opens and changed to green, the test passed.

  This test tested that the maze results took 14 steps and that the maze has the path filled in.
