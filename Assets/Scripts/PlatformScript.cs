using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour {
	public GameObject moneyBag;

	private Transform playerTransform;
	private GameObject []moneyBags;
	private Transform ground;
	private PlayerScript playScript;
	private float rangeOfAxisX;
	private float rangeOfAxisZ;

	private readonly float bagHeight=1f;
	private readonly int moneyBagNumber=10;

	void Start(){
		playerTransform =GameObject.Find ("Player").transform;
		moneyBags = new GameObject[moneyBagNumber];
		ground = GameObject.Find(gameObject.name+"/Ground").transform;
		playScript = new PlayerScript ();
		rangeOfAxisX = (1.0f * ground.localScale.x)/2.0f;
		rangeOfAxisZ=(1.0f * ground.localScale.z)/2.0f;
	}

	void RandomDistributeMoneyBag(){
		for (int i = 0; i < moneyBags.Length; i++) {
			moneyBags[i].transform.localPosition=new Vector3(Random.Range(-rangeOfAxisX,rangeOfAxisX),bagHeight,Random.Range(-rangeOfAxisZ,rangeOfAxisZ));
			moneyBags [i].SetActive (true);
		}
	}

	void OnTriggerEnter(Collider collider){
		moneyBags = MoneyBagPool.moneyBagPool.getAllMoneyBag ();
		if (collider.gameObject.transform.Equals (playerTransform)) {
			for (int i = 0; i < moneyBags.Length; i++) {
				moneyBags [i].transform.parent = transform;
			}
			playScript.angleYOfRoad = transform.rotation;
			RandomDistributeMoneyBag ();
		}
	}
}
