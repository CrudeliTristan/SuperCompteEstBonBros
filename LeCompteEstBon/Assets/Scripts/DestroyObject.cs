/*This script was written by CRUDELI Tristan, open source project*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour {

    //Seconds left before gameObject is destroyed
    public float deathTimer;

	//Start the coroutine to use timer
	void Start () {
        StartCoroutine(Destroyer());
	}
	
    //This method wait for deathTimer seconds then destroy the gameObject where the script is attached
    private IEnumerator Destroyer()
    {
        yield return new WaitForSecondsRealtime(deathTimer);
        Destroy(gameObject);
    }
	
}
