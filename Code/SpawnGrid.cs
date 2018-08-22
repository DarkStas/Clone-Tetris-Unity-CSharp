using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGrid : MonoBehaviour {

	public GameObject[] grid;

	// Spawn Prefabs from array on level start
	void Start () {

		for (int i = 0; i < 9; i++)
		{
			Instantiate(grid[0], new Vector3(0.5f + i, 9.5f, -0.5f), Quaternion.Euler(-90, 0, 0));//spawn object index0
		}
		for (int i = 0; i < 19; i++)
		{
			Instantiate(grid[1], new Vector3(4.5f, 0.5f + i, -0.5f), Quaternion.Euler(-90, 0, 0));//spawn object index1
		}
		//Quaternion.identity
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
