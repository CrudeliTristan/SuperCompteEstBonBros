/*This script was written by CRUDELI Tristan, open source project*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    //3 states are used to do a calculation
    //1- Get 1st number
    //2- Get the operator
    //3- Get the 2nd number
    private int state;
    public int number1;
    public int number2;
    public char Operator;

    //GameInitialization and SolverScript values are used in this script
    private GameInitialization gi;
    private SolverScript solv;

    //Buttons clicked to hide them
    private GameObject firstButton;
    private GameObject secondButton;

    //Texts to show/hide the solution and resize the console
    public GameObject console;
    public GameObject soluce;

    //Cancel operation button
    public GameObject cancelButton;

    //Result of the operation
    public int currentResult;
    
    //Compte est bon end music and end music
    public AudioClip end1;
    public AudioClip end2;

    //Ambiant Music for the current level
    public AudioSource ambientMusic;

    //Coins points for 1up
    public int points = 0;

    //Show or Hide Solution
    public bool soluceVisible = false;

	//Reset all values
	void Start () {
        gi = FindObjectOfType<GameInitialization>();
        solv = FindObjectOfType<SolverScript>();
        currentResult = 0;
        number1 = 0;
        number2 = 0;
        Operator = '0';
        state = 0;
	}
	
	//Check if state = 2 to try the operation and display it
	void Update () {

        //Quit the game if press escape
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();


        if(state == 2)
        {
            int result = 0;
            float fresult = 0;

            //Which operator is used
            switch (Operator)
            {
                case 'x':
                    result = number1 * number2;
                    fresult = result;
                    break;
                case '/':
                    result = number1 / number2;
                    fresult = (float)number1 / (float)number2;
                    break;
                case '+':
                    result = number1 + number2;
                    fresult = result;
                    break;
                case '-':
                    result = number1 - number2;
                    fresult = result;
                    break;
            }

            //Try the operation, if success, display it then create the new button value
            if (result > 0 && fresult == (float)result)
            {
                gi.console.text += "\n" + number1.ToString() + " " + Operator + " " + number2.ToString() + " = " + result.ToString();
                currentResult = result;
                ButtonAspect(firstButton, true);
                cancelButton.SetActive(false);
                firstButton.GetComponentInChildren<Text>().text = currentResult.ToString();
                currentResult = 0;
            } else //else display initials buttons
            {
                ButtonAspect(firstButton, true);
                ButtonAspect(secondButton, true);
            }
            //Then reset values
            state = 0;
            number1 = 0;
            number2 = 0;
            Operator = '0';
        }
	}

    //This method get buttons text and follow steps to do a calculation
    public void Operation(string Btext, GameObject ButtonNumber)
    {
        //Check if value is int
        bool isInt;
        try
        {
            int.Parse(Btext);
            isInt = true;
        }
        catch
        {
            isInt = false;
        }

        //1st state if it is int, it's the 1st number so make interactable operators buttons
        if (state == 0 && isInt)
        {
            try
            {
                ButtonAspect(firstButton, true);
            }
            catch { }
            number1 = int.Parse(Btext);
            firstButton = ButtonNumber;
            OperatorsInteraction(true);

        }
        //If it's not int, is the operator, display cancel button to cancel current operation
        else if (state == 0 && !isInt)
        {
            Operator = char.Parse(Btext);
            cancelButton.SetActive(true);
            state = 1;
        }
        //2nd State if it's int, it's the 2nd number, make non interactable operators buttons then do calculation
        else if (state == 1 && isInt)
        {
            OperatorsInteraction(false);
            number2 = int.Parse(Btext);
            secondButton = ButtonNumber;
            state = 2;
        }
        //if it's not int, just change the current operator
        else if (state == 1 && !isInt)
        {
            OperatorsInteraction(true);
            ButtonAspect(ButtonNumber, true);
            Operator = char.Parse(Btext);
        }
    }

    //This method reset all operation values
    public void Resetvalues()
    {
        currentResult = 0;
        number1 = 0;
        number2 = 0;
        Operator = '0';
        state = 0;
        firstButton = null;
        secondButton = null;
        cancelButton.SetActive(false);
    }

    //This method cancel the current operation
    public void CancelOperation()
    {
        ButtonAspect(firstButton, true);
        OperatorsInteraction(false);
        number1 = 0;
        number2 = 0;
        Operator = '0';
        state = 0;
        firstButton = null;
        secondButton = null;
        cancelButton.SetActive(false);
    }

    //Make interactable or not operators, show or hide cancel button
    public void OperatorsInteraction(bool visible)
    {
        gi.operatorDivided.interactable = visible;
        gi.operatorMinus.interactable = visible;
        gi.operatorMultiplied.interactable = visible;
        gi.operatorPlus.interactable = visible;
        cancelButton.SetActive(visible);
    }

    //this method Show or Hide the button in parameter if it is int, else make it interactable or not
    public void ButtonAspect(GameObject button, bool state)
    {
        bool isInt;
        try
        {
            int.Parse(button.GetComponentInChildren<Text>().text);
            isInt = true;
        }
        catch
        {
            isInt = false;
        }

    if (isInt) {
        button.GetComponent<Button>().enabled = state;
        button.GetComponent<Image>().enabled = state;
        button.GetComponentInChildren<Text>().enabled = state;
    } else {
        button.GetComponent<Button>().interactable = state;
    }

    }

    //This method Show or Hide one of the current solutions (if there is) then resize the console
    public void ShowSoluce()
    {
        soluceVisible = !soluceVisible;
        if (soluceVisible) {
            solv.Solution();
            console.GetComponent<RectTransform>().anchorMin = new Vector2(0.05f, 0.5f);
        }
        else console.GetComponent<RectTransform>().anchorMin = new Vector2(0.05f, 0.05f);
        console.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        soluce.SetActive(soluceVisible);
    }
}
