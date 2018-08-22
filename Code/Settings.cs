using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour {

	bool isFullScreen;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	//Option FullScreen
	public void FullScreenToggle()
	{
		isFullScreen = !isFullScreen;
		Screen.fullScreen = !isFullScreen;
	}
}
