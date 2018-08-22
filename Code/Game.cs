using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public float fall = 0;//Help calculate next move
	public static int gridWeight = 10;
    public static int gridHeight = 20;
	[SerializeField]
	float TimeInSecForNextMove;
	public int GameScore;
    public static Transform[,] grid = new Transform[gridWeight, gridHeight];
	//Take "MoveSpeeed" from Check1 on game start
	void OnEnable()
	{
		GameObject MaintCamera = GameObject.Find("Main Camera");
		Check1 speed = MaintCamera.GetComponent<Check1>();
		TimeInSecForNextMove = speed.NextMoveInSec;
	}

	//Go to menu, delete all blocks.
	void Start()
    {
		if (!isValidPosition())
        {
			SceneManager.LoadScene(0);
            Destroy(gameObject);
        }
    }

    void Update()
	{//Take "MoveSpeeed" from Check1 when game run
		GameObject MaintCamera = GameObject.Find("Main Camera");
		Check1 speed = MaintCamera.GetComponent<Check1>();
		TimeInSecForNextMove = speed.NextMoveInSec;
		//Move right and lock if on border
		if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0);

            if (isValidPosition())//Check position
                GridUpdate();
            else
                transform.position += new Vector3(-1, 0, 0);//Stop move
        }
		//Move left and lock if on border
		else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (isValidPosition())
                GridUpdate();
            else
                transform.position += new Vector3(1, 0, 0);
        }
		//Rotate
		else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            transform.Rotate(0, 0, -90);
            if (isValidPosition())
                GridUpdate();
            else
                transform.Rotate(0, 0, 90);
        }
		//Move down 1 block for 1 press
		else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            transform.position += new Vector3(0, -1, 0);
            if (isValidPosition())
            {
                GridUpdate();
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);
               
            }
 
        }
		//Move down fast 
		else if (Input.GetKey(KeyCode.Space))
		{
			transform.position += new Vector3(0, -1f, 0);
			if (isValidPosition())
			{
				GridUpdate();
			}
			else
			{
				transform.position += new Vector3(0, 1, 0);

			}

		}
		//Move down, delete if horizontal full and spawn new object
		else if (Time.time - fall >= TimeInSecForNextMove)
		{
			transform.position += new Vector3(0, -1, 0);//Move down
			if (isValidPosition())
			{
				GridUpdate();
			}
			else
			{
				transform.position += new Vector3(0, 1, 0);
				DeleteRow();//Check if can delete horizontal
				FindObjectOfType<SpawnBox>().SpawnNewBox();//spawn new object
				enabled = false;
			}
		fall = Time.time;//Calc time
		}
	}
	//Check position
    bool isValidPosition()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = round(child.position);
            if (!isInsideGrid(v))
                return false;
            if (grid[(int)v.x, (int)v.y] != null &&
                grid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }
        return true;
    }
	//Update grid
    void GridUpdate()
    {
        for (int y = 0; y < gridHeight; ++y)
            for (int x = 0; x < gridWeight; ++x)
                if (grid[x, y] != null)
                    if (grid[x, y].parent == transform)
                        grid[x, y] = null;
        foreach (Transform child in transform)
        {
            Vector2 v = round(child.position);
            grid[(int)v.x, (int)v.y] = child;
        }
    }

    public Vector2 round(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }
	//Check if in game area
    public bool isInsideGrid(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < gridWeight && (int)pos.y >= 0);
    }
	//Delete horizontal objects
    public void Delete(int y)
    {
        for (int x = 0; x < gridWeight; ++x)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
		
	}
	//Check if horizontal is full
    public bool isFull(int y)
    {
        for (int x = 0; x < gridWeight; ++x)
            if (grid[x, y] == null)
                return false;
        return true;
    }
	//Delete horizontal objects
	public void DeleteRow()
    {
        for (int y = 0; y < gridHeight; ++y)
        {
            if (isFull(y))
            {
                Delete(y);
                RowDownAll(y + 1);
                --y;
            }
        }
    }
	//Move all objects 1 block down
    public void RowDown(int y)
    {
        for (int x = 0; x < gridWeight; ++x)
        {
            if (grid[x, y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }
	//Move all objects 1 block down
	public void RowDownAll(int y)
    {
        for (int i = y; i < gridHeight; ++i)
            RowDown(i);
			Score();
	}
	//Send score to Check1
	public void Score()
	{
		GameObject.Find("Main Camera").GetComponent<Check1>().GameScore += 1;
	}
}
