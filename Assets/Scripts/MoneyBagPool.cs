using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBagPool : MonoBehaviour {
	public static MoneyBagPool moneyBagPool;
	public GameObject moneyBag;

	private List<GameObject> moneyBagList;
	private readonly int moneyBagNumber=10;

	void Awake(){
		if (moneyBagPool == null) {
			moneyBagPool = this;
		} else if(moneyBagPool!=this){
			Destroy (this.gameObject);
		}
	}

	void Start(){
		moneyBagList = new List<GameObject> ();
		for (int i = 0; i < moneyBagNumber; i++) {
			GameObject exampleOfMoneyBag = (GameObject)Instantiate (moneyBag);
			exampleOfMoneyBag.SetActive (true);
			moneyBagList.Add (exampleOfMoneyBag);
		}
	}

	public GameObject[] getAllMoneyBag(){
		GameObject[] moneyBags = new GameObject[moneyBagNumber];
		for (int i = 0; i < moneyBagList.Count; i++) {
			moneyBags [i] = moneyBagList [i];
		}
		return moneyBags;
	}
}
