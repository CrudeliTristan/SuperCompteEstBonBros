/*This script was written by CRUDELI Tristan, open source project*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SolverScript : MonoBehaviour {


    //4 operations
    long Plus(long a, long b) { return a + b; }
    long Minus(long a, long b) { return a - b; }
    long Times(long a, long b) { return (a == 1 || b == 1) ? -10000 : a * b; }
    long Obelus(long a, long b) { return (b != 0 && a % b == 0) ? a / b : -10000; }

    private const long solFound = 1;
    private const long solNotFound = 0;

    private string result = "";
    private string[] operatorSymbol = { "/", "x", "+", "-" };

    //Declaration of GameInitialization to get used numbers for the game
    private GameInitialization gi;

    //UI Text to display solver result
    public Text solverText;

    void Start()
    {
        gi = FindObjectOfType<GameInitialization>();
    }

    public void Solution()
    {

        long[] platesNumbers = new long[6];
        for (int i = 0; i < 6; i++)
        {
            platesNumbers[i] = (long)gi.numbers[i];
        }
        if (resolve((long)gi.finalNumber, 6, platesNumbers) == 0) solverText.text = "Aucune solution exacte !";
    }


    //This method return 0 if there is no solution, 1 if there is one
    private long resolve(long numberToFind, long remainingElementsInArray, long[] platesArray)
    {
        long[] v = new long[6];
        long lVal = 0;
        int i, j, k, l, op;

        solverText.text = "";

        //If not elements return 0
        if (remainingElementsInArray <= 0)
        {
            return solNotFound;
        }

        if (remainingElementsInArray == 1)
        {
            if (numberToFind == platesArray[0])
            {
                //If the last plate is the value to find
                result = "Solution : " + platesArray[0];
                solverText.text += "\n" + result;

                //Return 1
                return solFound;
            }
            //Return 0
            else return solNotFound;
        }

        //If not found, try all calculation
        for (i = 0; i < remainingElementsInArray; ++i)
        {
            //Solution found if a plate equal to the number to find
            if (platesArray[i] == numberToFind)
            {
                solverText.text += "\n" + "Solution : " + platesArray[i];

                return solFound;
            }

            for (j = 1; j < remainingElementsInArray; ++j)
            {
                if (j == i) continue;
                for (op = 0; op < 4; ++op)
                {
                    switch (op)
                    {
                        case 0:
                            v[0] = Obelus(platesArray[i], platesArray[j]);
                            break;
                        case 1:
                            v[0] = Times(platesArray[i], platesArray[j]);
                            break;
                        case 2:
                            v[0] = Plus(platesArray[i], platesArray[j]);
                            break;
                        case 3:
                            v[0] = Minus(platesArray[i], platesArray[j]);
                            break;
                    }
                    if (v[0] < 0)
                    {
                        continue;
                    }
                    for (k = 1, l = 0; l < remainingElementsInArray; ++l)
                    {
                        if (l == i || l == j)
                            continue;
                        v[k++] = platesArray[l];
                    }

                    //If the solution is founded we display all operations
                    if (resolve(numberToFind, remainingElementsInArray - 1, v) == 1)
                    {
                        switch (op)
                        {
                            case 0:
                                lVal = Obelus(platesArray[i], platesArray[j]);
                                break;
                            case 1:
                                lVal = Times(platesArray[i], platesArray[j]);
                                break;
                            case 2:
                                lVal = Plus(platesArray[i], platesArray[j]);
                                break;
                            case 3:
                                lVal = Minus(platesArray[i], platesArray[j]);
                                break;
                        }
                        result = lVal + " = " + platesArray[i] + " " + operatorSymbol[op] + " " + platesArray[j];
                        solverText.text += "\n" + result;

                        return solFound;
                    }
                }
            }
        }
        return solNotFound;
    }
}
