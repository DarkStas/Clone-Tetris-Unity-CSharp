using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	// Buttom Play\Reset
	public void PlayPressed()
	{
		
		SceneManager.LoadScene("Tetris");//ReloadScene Tetris
		Time.timeScale = 1;//StopPause

		GameObject MaintCamera = GameObject.Find("Main Camera");//Change bool Reset in Check1
		Check1 speed = MaintCamera.GetComponent<Check1>();
		speed.Reset = true;
	}
	// Buttom Continue
	public void ContinuePressed()
	{
		
		SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("MenuInGame"));//Quit scene MenuInGame
		Time.timeScale = 1;//StopPause


	}
	// Buttom Exit
	public void ExitPressed()
	{
		Application.Quit();//Exit game
	}


}
