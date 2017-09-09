using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaserScript : MonoBehaviour {
	public GameObject effectsOfDeath;

	private Transform playerTransform;
	private NavMeshAgent chaserOfNavMeshAgent;
	private GameObject effectsOfExamples;

	void Start(){
		playerTransform = GameObject.Find ("Player").transform;
		chaserOfNavMeshAgent = GetComponent<NavMeshAgent> ();
	}

	void Update(){
		if (Input.anyKeyDown) {
			chaserOfNavMeshAgent.destination = playerTransform.position;
		}
	}
	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.transform.Equals (playerTransform)) {
			effectsOfExamples=Instantiate (effectsOfDeath);
			effectsOfExamples.transform.position = transform.position;
			transform.gameObject.SetActive (false);
		}
	}
}
