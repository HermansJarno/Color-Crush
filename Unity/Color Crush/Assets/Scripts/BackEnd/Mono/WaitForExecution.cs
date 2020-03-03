using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForExecution
{
    private MonoBehaviour monoBehaviour;

    public WaitForExecution(){

    }

    public WaitForExecution(float time){
        monoBehaviour = GameObject.FindObjectOfType<MonoBehaviour>();
        monoBehaviour.StartCoroutine(Wait(time));
    }

	private IEnumerator Wait(float time){
		yield return new WaitForSeconds(time);
	}
}
