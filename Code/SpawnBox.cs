using UnityEngine;
using System.Collections;

public class SpawnBox : MonoBehaviour {

	public GameObject[] boxList;
	
	int i;
	GameObject clone;

	//Spawn on gameStart 2 objects with random
	void Start () {
		i = Random.Range(0, boxList.Length);//Random
		clone = Instantiate(boxList[i], transform.position, Quaternion.identity);//Spawn object on position inGame
		clone.AddComponent<Game>();//Add script Game to object
		i = Random.Range(0, boxList.Length);//Random
		clone = Instantiate(boxList[i], new Vector3(16, 14, 0), Quaternion.identity);//Spawn next object that not inGame
		
		//enabled = false;
		//SpawnNewBox();
	}

	public void SpawnNewBox() {

		clone.transform.localPosition = new Vector3(5, 18, 0);//Move object inGame
		clone.AddComponent<Game>();//Add script Game to object
		i = Random.Range(0, boxList.Length);//Random
		clone = Instantiate(boxList[i], new Vector3(16, 14, 0), Quaternion.identity);//Spawn next object that not inGame
	}
}