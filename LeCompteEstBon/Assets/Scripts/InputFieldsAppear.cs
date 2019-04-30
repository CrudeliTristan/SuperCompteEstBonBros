/*This script was written by CRUDELI Tristan, open source project*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldsAppear : MonoBehaviour {

    //All inputs to play with custom values
    public GameObject input1;
    public GameObject input2;
    public GameObject input3;
    public GameObject input4;
    public GameObject input5;
    public GameObject input6;
    public GameObject input7;
    public GameObject applyButton;

    //Inputs array to organize them
    private GameObject[] inputs;

    //Display or not custom values pannel
    private bool isVisible;

    //GameInitialization to apply custom inserts
    private GameInitialization gi;

    private void Start()
    {
        gi = FindObjectOfType<GameInitialization>();
        inputs = new GameObject[] { input1, input2, input3, input4, input5, input6, input7 };
        isVisible = false;
    }

    //This method display or hide input fields and apply button
    public void InputVisibility()
    {
        isVisible = !isVisible;
        for (int i = 0; i < 7; i++)
        {
            inputs[i].SetActive(isVisible);
            applyButton.SetActive(isVisible);
        }
        
    }

    //This method Apply custom values to game values array
    public void ApplyCustomInserts()
    {
        for (int i = 0; i < 6; i++)
        {
            //Change only filled values
            if(inputs[i].GetComponentInChildren<Text>().text != "")
            {
                gi.numbers[i] = int.Parse(inputs[i].GetComponentInChildren<Text>().text);
                
            }
            
        }
        //Change number to find
        if (input7.GetComponentInChildren<Text>().text != "")
        {
            gi.finalNumber = int.Parse(input7.GetComponentInChildren<Text>().text);
        }
        //Do a reset to restart with custom values
        gi.ResetGame();
    }

}
