/*This script was written by CRUDELI Tristan, open source project*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputScript : MonoBehaviour
{
    //Input where the script is attached
    private InputField customInput;

    private void Start()
    {
        customInput = gameObject.GetComponent<InputField>();
    }

    //This method prevent for negatives and null number
    public void UnableChar()
    {
        if (customInput.text.Length > 0 && (customInput.text[0] == '-' || customInput.text[0] == '0')) customInput.text = "";
    }

    //This method prevent for good number range - (1-100) for plates & (100-999) for number to find
    public void DigitsRequiered() {
        if (customInput.text != "")
        {
            if (gameObject.name == "Input7" && int.Parse(customInput.text) < 100) customInput.text = "";
            else if (gameObject.name != "Input7" && int.Parse(customInput.text) > 100) customInput.text = "";
        }
    }

}