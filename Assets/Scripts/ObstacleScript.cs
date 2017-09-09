using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour {
	public GameObject effectsOfDeath;

	private GameObject chaser;
	private GameObject effectsOfExamples;
	private Transform playerTransform;

	void Start(){
		playerTransform = GameObject.Find ("Player").transform;
		chaser=GameObject.Find ("Chaser");
	}

	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.transform.Equals (playerTransform)) {
			effectsOfExamples=Instantiate (effectsOfDeath);
			effectsOfExamples.transform.position = playerTransform.position;

			//让追击者角色消失，从而触发游戏结束的条件
			chaser.SetActive (false);
		}
	}
}
