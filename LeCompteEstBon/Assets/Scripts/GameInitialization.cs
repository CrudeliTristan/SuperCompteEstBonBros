/*This script was written by CRUDELI Tristan, open source project*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameInitialization : MonoBehaviour {

    //UI Default Buttons
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    public GameObject button6;

    public GameObject goNext;

    //UI Operators Buttons
    public Button operatorPlus;
    public Button operatorMinus;
    public Button operatorDivided;
    public Button operatorMultiplied;

    //UI Stop and Restart Buttons
    public Button stopButton;
    public Button restartButton;

    //UI Console Text
    public Text console;

    //Number to find
    public int finalNumber;

    //GameManager
    private GameManager gm;

    //List of all numbers can be attributed
    public int[] numbers;

    //List of buttons
    public GameObject[] buttons;

    //Sound of brick destroy and Tube
    private AudioSource sound;
    public AudioClip bricks;
    public AudioClip tube;

    //Next scene
    public string nextScene;

    //The sound to play is Tube or Bricks ?
    private bool isTubeSound;

    //All buttons are organized and game numbers are generated and initialized
    void Start()
    {
        buttons = new GameObject[] { button1, button2, button3, button4, button5, button6 };
        gm = FindObjectOfType<GameManager>();
        NumbersGenerator();
        sound = gameObject.GetComponentInChildren<AudioSource>();
        isTubeSound = false;
    }

    //This method generate game's numbers
    private void NumbersGenerator()
    {

        numbers = new int[] { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 25, 25, 50, 50, 75, 75, 100, 100 };

        //Shuffle the list
        for (int i = 0; i < numbers.Length; i++)
        {
            int temp = numbers[i];
            int randomIndex = Random.Range(i, numbers.Length);
            numbers[i] = numbers[randomIndex];
            numbers[randomIndex] = temp;
        }

        //Change the UI buttons text with the firsts list parameters
        for (int i=0;i < 6; i++)
        {
            gm.ButtonAspect(buttons[i], true);
            buttons[i].GetComponentInChildren<Text>().text = numbers[i].ToString();
            
        }

        //Generate the number to find
        finalNumber = Random.Range(100, 1000);
        console.text += finalNumber.ToString();

        gm.OperatorsInteraction(false);
        
        stopButton.interactable = true;
        goNext.SetActive(false);
    }

    //This method reset all states to retry the level
    public void ResetGame()
    {
        if (gm.soluceVisible) gm.ShowSoluce();

        goNext.SetActive(false);
        gm.Resetvalues();

        for (int i = 0; i < 6; i++)
        {
            gm.ButtonAspect(buttons[i], true);
            buttons[i].GetComponentInChildren<Text>().text = numbers[i].ToString();
        }

        gm.OperatorsInteraction(false);
        gm.cancelButton.SetActive(false);

        stopButton.interactable = true;

        console.text = "Le nombre à trouver est : " + finalNumber.ToString();
        if (isTubeSound) sound.clip = tube;
        else sound.clip = bricks;
        isTubeSound = false;
        sound.Play();

        if(!gm.ambientMusic.isPlaying) gm.ambientMusic.Play();
    }

    //This method end the current game, find the difference between the result and the number to find
    //Play another song if difference = 0
    public void Stop()
    {
        int finalDifference = Mathf.Abs(int.Parse(buttons[0].GetComponentInChildren<Text>().text) - finalNumber);
        for (int i = 0; i < 6; i++)
        {
            if (buttons[i].GetComponentInChildren<Text>().IsActive())
            {
                int buttonValue = int.Parse(buttons[i].GetComponentInChildren<Text>().text);
                int valueDifference = Mathf.Abs(buttonValue - finalNumber);
                if (valueDifference < finalDifference) finalDifference = valueDifference;
            }
            gm.ButtonAspect(buttons[i], false);
        }

        gm.OperatorsInteraction(false);

        stopButton.interactable = false;

        goNext.SetActive(true);

        console.text += "\n" + "Difference : " + finalDifference.ToString();
        sound.clip = bricks;
        sound.Play();

        StartCoroutine(VictorySong(finalDifference));
    }

    //This method play tube sound then start next level coroutine to use WaitWhile
    public void Restart()
    {
        sound.clip = tube;
        sound.Play();
        StartCoroutine(NextLevel());
    }

    //This coroutine called from Stop method play the good music after bricks sound end
    private IEnumerator VictorySong(int dif)
    {
        yield return new WaitWhile (() => sound.isPlaying);
        if (dif != 0) gm.GetComponent<AudioSource>().clip = gm.end1;
        else gm.GetComponent<AudioSource>().clip = gm.end2;
        gm.GetComponent<AudioSource>().Play();
        gm.ambientMusic.Stop();
        yield return new WaitWhile(() => gm.GetComponent<AudioSource>().isPlaying);
    }

    //This coroutine load the next level after the tube sound end
    private IEnumerator NextLevel()
    {
        yield return new WaitWhile(() => sound.isPlaying);
        SceneManager.LoadScene(nextScene);
    }

}
