/*This script was written by CRUDELI Tristan, open source project*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTextGetter : MonoBehaviour {
    
    //Load GameManager script
    private GameManager gm;

    //References for the prefabs
    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject pts;
    [SerializeField] private GameObject life;

    void Start () {
        gm = FindObjectOfType<GameManager>();
	}
	
    //This method attached to game buttons, call gm operation with his text content and itself
    //For UI animations, when a LuckyBloc is pressed a coin is instantiate with 200 points like the real Super Mario Bros game
    //If the player get 1000 points it instantiate a coin with 1up !
    public void getNumber()
    {

        gm.points += 200;
        gm.Operation(gameObject.GetComponentInChildren<Text>().text, gameObject);
        gm.ButtonAspect(gameObject, false);

        if(gameObject.tag == "LuckyBloc")
        {
            Vector2 coinJump = new Vector2(0f, 1f);
            (Instantiate(coin, gameObject.transform)).GetComponent<Rigidbody2D>().AddForce(gameObject.transform.position * coinJump * 100);
            if (gm.points >= 1000)
            {
                (Instantiate(life, gameObject.transform)).GetComponent<Rigidbody2D>().AddForce(gameObject.transform.position * coinJump * 10);
                gm.points -= 1000;
            } else
            {
                (Instantiate(pts, gameObject.transform)).GetComponent<Rigidbody2D>().AddForce(gameObject.transform.position * coinJump * 10);
            }
            
        }

    }

}