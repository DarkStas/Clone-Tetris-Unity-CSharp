using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Check1 : MonoBehaviour {

	public float NextMoveInSec = 1;
	public int SpeedUpEveryScore = 2;
	public int GameScore;
	[SerializeField]
	int ScoreForCalc;//Make Score count 1 time and dont spam speed++
	int SpeedForText = 1;//More frendly display of move speed (for GUI)
	//Add text
	public Text ScoreText;
	public Text TimeText;
	public Text Speed;
	bool IsEscape = false;//Dont spam Esc and open many Menu
	public bool Reset = false;//Reset score/time/speed

	void Start()
	{
		ScoreForCalc = SpeedUpEveryScore;
	}

	// Update is called once per frame
	void Update()
	{
		//Reset score/time/speed
		if (Reset == true)
		{
			GameScore = 0;
			SpeedForText = 1;
			Reset = false;
		}

		//GUI
		ScoreText.text = "Score: " + GameScore;
		TimeText.text = "Time: " + (int)Time.timeSinceLevelLoad;
		Speed.text = "Speed: " + SpeedForText;
		//Add "Speed" for every SpeedUpEveryScore and make game re-calculate "speed" if forgot
		if (GameScore % SpeedUpEveryScore == 0 && GameScore != 0 && ScoreForCalc == GameScore || ScoreForCalc < GameScore)
		{
			NextMoveInSec /= 1.1f;
			ScoreForCalc += SpeedUpEveryScore;
			SpeedForText += 1;
		}
		//Go to Menu
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (Time.timeScale == 1)//Check if game unpause and make menu work
			{
				IsEscape = false;
			}
			if (IsEscape == false)//Check can LoadScene or Close
			{
				SceneManager.LoadScene("MenuInGame", LoadSceneMode.Additive);// LoadScene MenuInGame
				Time.timeScale = 0;//Stop time
				IsEscape = true;//Make next Esc close MenuInGame 
			}
			else
			{
				SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("MenuInGame"));// Close scene MenuInGame
				Time.timeScale = 1;// Unfreeze game
				IsEscape = false;//Make next Esc Open MenuInGame
			}
			
				


		}
	}
}
