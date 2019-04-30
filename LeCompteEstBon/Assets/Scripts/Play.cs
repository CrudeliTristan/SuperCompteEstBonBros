/*This script was written by CRUDELI Tristan, open source project*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour {

    //The Update loop check if Space or Return is pressed to start the game and quit if press escape
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) StartGame();
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }

    //If the button is clicked we start the game with the first world
    public void StartGame()
    {
        SceneManager.LoadScene("OverWorld");
    }
}
