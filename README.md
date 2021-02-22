# Calculator App

This console application prompts the user for an equasion, solves it, and gives the user the solution. To do this, it uses three main components:
1. Calculator.cs - handles user IO and error handling. Includes the Main function which runs the program.
2. ParseHelper.cs - Splits the input string into MathTree objects by operators (+,-,*,/,^).
3. MathTree.cs - Abstract class where implementations are the operators that we want to parse and implement a .Solve() function that returns the solution.

## Calculator.cs

This class contains the UI and is the launchpoint of the application with the Main method.

## ParseHelper.cs

This method has one primary public method (Parse) and helper method. User to convert a string input to a MathTree object that can be solved.

## MathTree.cs

Abstract class that contains a list of operators the program knows about as well as the Solve method signature which all MathTree objects must implement. 

The idea is for the object to hold a list of values that can be combined with an operator (ie. Add could hold the values 2, 3, & 4 which the solve method would then resolve to 9). I called this a tree as the values could be a root value (an individual number like 9) or another operator that would then hold sub-values (ie. Add could hold (2, 3, (Minus 4, 3)) which would then resolve to 2 + 3 + (4 - 3) = 6). Thus when solve is called on the top-level operator, all sub-operators would resolve to a value and the 'tree' would be solved.

## Calculator.Tests

This project also includes Unit tests to ensure changes in the program do not cause unforseen consequences.