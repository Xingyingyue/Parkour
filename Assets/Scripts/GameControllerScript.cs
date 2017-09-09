using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour {
	public static GameControllerScript gameController;
	public Text highestScoreText;
	public Text scoreText;

	private AudioSource bgmOfGame;
	private bool playerIsAlive;
	private int highestScore;
	private float score;

	private readonly string titleOfHighestScore="High Score\n";
	private readonly string titleOfScore="Score\n";

	void  Awake(){
		if (gameController == null) {
			gameController = this;
		} else if(gameController!=this){
			Destroy (this.gameObject);
		}
	}

	void Start(){
		bgmOfGame = GetComponent<AudioSource> ();
	}

	void Update(){
		if (playerIsAlive) {
			score += 2*Time.deltaTime;
			scoreText.text =titleOfScore + (int)score;
			if (score > highestScore) {
				highestScoreText.text =titleOfHighestScore+ (int)score;
			}
			if (!bgmOfGame.isPlaying) {
				bgmOfGame.Play ();
			}
		}
	}

	void SaveHighestScore(){
		if (score > highestScore) {
			PlayerPrefs.SetInt ("HighestScore",(int)score);
		}
	}

	void ReadHighsetScore(){
		highestScore = PlayerPrefs.GetInt ("HighestScore", 0);
		highestScoreText.text =titleOfHighestScore+ highestScore;
	}

	public void StartCounting(){
		ReadHighsetScore ();
		playerIsAlive = true;
	}

	public void StopCounting(){
		SaveHighestScore ();
		playerIsAlive = false;
	}

	public void AddScoreGettingMoney(float value){
		if (playerIsAlive) {
			score += value;
		}
	}
}
