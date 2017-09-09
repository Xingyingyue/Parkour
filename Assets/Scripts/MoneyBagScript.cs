using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBagScript : MonoBehaviour {
	private float valueOfMoney = 50f;
	private Transform playerTransform;
	private AudioSource soundOfJingle;
	private readonly float soundContinousTime=0.2f;

	void Start(){
		playerTransform = GameObject.Find ("Player").transform;
		soundOfJingle = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.transform.Equals (playerTransform)) {
			GameControllerScript.gameController.AddScoreGettingMoney (valueOfMoney);
			StartCoroutine (PlayBgmWhenColliding ());
		}
	}

	IEnumerator PlayBgmWhenColliding(){
		soundOfJingle.Play ();
		yield return new WaitForSeconds(soundContinousTime);
		soundOfJingle.Stop ();
		gameObject.SetActive (false);
	}
}
