using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WebApplication1.Models;

namespace WebApplication1.BusinessService
{
    public class MazeService
    {

        public MazeValues solveMaze(string maze)
        {
            return mazeSolver(maze);
        }

        #region Global Variables
        private List<MazePlot> goodSteps;
        private List<MazePlot> optionalSteps;
        private MazePlot currentStep;
        private string[] maze;
        private int endingRow;
        private int endingColumn;
        private int currentRow;
        private int currentColumn;
        private int blackListCounter = 2;
        #endregion Global Variables

        #region Private BusinesService Methods
        private MazeValues mazeSolver(string m)
        {
            maze = m.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            bool isComplete = false;
            var startingRow = Array.FindIndex(maze, row => row.Contains("A"));
            var startingColumn = maze[startingRow].IndexOf("A");
            endingRow = Array.FindIndex(maze, row => row.Contains("B"));
            endingColumn = maze[endingRow].IndexOf("B");
            currentRow = startingRow;
            currentColumn = startingColumn;
            currentStep = new MazePlot { row = currentRow, column = currentColumn };

            while (!isComplete)
            {
                if(currentStep.row != 0)//top
                {
                    optionalStepChecker(new MazePlot { row = currentStep.row - 1, column = currentStep.column });
                }
                if (currentStep.row != 0)//left
                {
                    optionalStepChecker(new MazePlot { row = currentStep.row, column = currentStep.column - 1 });
                }
                if (currentStep.row < maze.Length - 1)//bottom
                {
                    optionalStepChecker(new MazePlot { row = currentStep.row + 1, column = currentStep.column });
                }
                if (currentStep.column != maze[0].Length)//right
                {
                    optionalStepChecker(new MazePlot { row = currentStep.row, column = currentStep.column + 1 });
                }
                //Makes the next move and checks if complete
                isComplete = addMoveChoice();
            }

            //complete the maze and return values.
            var finalStepsList = goodSteps.Where(x => !x.blacklisted);
            var mazeOutput = buildMazeSolution(finalStepsList);
            return new MazeValues { steps = finalStepsList.Count(), solution = mazeOutput };
        }

        private bool addMoveChoice()
        {
            var currentOption = optionalSteps.Where(x => x != currentStep).LastOrDefault(x => !x.blacklisted);
            foreach (var option in optionalSteps.Where(x => !x.blacklisted && x != currentStep))
            {
                //Finds the closes move choice from optional moves to finish
                if((Math.Abs(option.row - endingRow)) + (Math.Abs(option.column - endingColumn)) 
                    < (Math.Abs(currentOption.row - endingRow)) + (Math.Abs(currentOption.column - endingColumn)) 
                    && !goodSteps.Where(x => !x.blacklisted).Contains(option))
                {
                    currentOption = option;
                }
            }

            //Adds the next move to a list
            if(goodSteps == null)
            {
                goodSteps = new List<MazePlot>();
                goodSteps.Add(new MazePlot { row = currentOption.row, column = currentOption.column });
            }
            else
            {
                // If No moves available, blacklist the current location and back up.
                if(currentOption == null)
                {
                    if (goodSteps.Count > 2)
                    {
                        var step = goodSteps.FirstOrDefault(x => x.row == currentStep.row && x.column == currentStep.column);
                        step.blacklisted = true;
                        currentStep = goodSteps[goodSteps.Count - blackListCounter];
                        blackListCounter += 1;
                        return false;
                    }
                }

                //Adds the temporary good step to the steps made and resets blacklisted counter.
                var temporaryGoodStep = new MazePlot { row = currentOption.row, column = currentOption.column };
                goodSteps.Add(temporaryGoodStep);
                blackListCounter = 2;
                
            }

            //Clears the optional steps for any next moves.
            //Updates the current cycle with move made.
            //Returns the checker to see if the maze is complete and will break the loop.
            optionalSteps.Clear();
            currentStep = currentOption;
            return currentStep.row == endingRow && currentStep.column == endingColumn;
        }

        //This is the optional step method a node can move to.
        private void optionalStepChecker(MazePlot location)
        {
            //Gets the optional node string identifier.
            var character = maze[location.row];
            var str = character.Substring(location.column, 1);

            //If optional node is a #(wall), skip it and return.
            if(str != "#" || str == string.Empty)
            {
                //If optional step list is null, create and add optional node. Otherwise, check to see if optional node has already been added and add.
                if (optionalSteps == null)
                {
                    optionalSteps = new List<MazePlot>();
                    optionalSteps.Add(location);
                }
                else if (!optionalSteps.Any(x => x == location))
                {
                    if (goodSteps == null)
                    {
                        optionalSteps.Add(location);
                    }
                    else if (goodSteps.Count(x => x.row == location.row && x.column == location.column) == 0)
                    {
                        optionalSteps.Add(location);
                    }
                }
            }
        }

        //Builds final maze output
        private string buildMazeSolution(IEnumerable<MazePlot> finalStepsList)
        {
            foreach (var plot in finalStepsList)
            {
                StringBuilder sb = new StringBuilder(maze[plot.row]);
                sb[plot.column] = '@';
                maze[plot.row] = sb.ToString();
            }
            string mazeOutput = string.Empty;
            foreach (var row in maze)
                mazeOutput += row + Environment.NewLine;
            return mazeOutput;
        }
        #endregion Private BusinesService Methods
    }

    public class MazePlot
    {
        public int row;
        public int column;
        public bool blacklisted;
    }
}
