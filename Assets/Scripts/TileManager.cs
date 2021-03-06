using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour 
{

    public GameObject[] tilePrefabs;

    public GameObject currentTile;

    private static TileManager instance;

    private Stack<GameObject> leftTiles = new Stack<GameObject>();

    private Stack<GameObject> topTiles = new Stack<GameObject>();

    public Stack<GameObject> LeftTiles
    {
        get { return leftTiles; }
        set { leftTiles = value; }
    }

    public Stack<GameObject> TopTiles
    {
        get { return topTiles; }
        set { topTiles = value; }
    }

    public static TileManager Instance
    {
        get 
        {
            if (instance == null) 
            {
                instance = GameObject.FindObjectOfType<TileManager>();
            }

            return instance; 
        
        }

    }

    

	void Start () 
    {
        CreateTiles(1);

        for (int i = 0; i < 10; i++)
        {
            SpawnTile();
        }
        
	}
	
    public void CreateTiles(int amount)
    {   

        for (int i = 0; i < amount; i++)
        {   
            leftTiles.Push(Instantiate(tilePrefabs[0]));

            topTiles.Push(Instantiate(tilePrefabs[1]));

            topTiles.Peek().name = "TopTile";
            topTiles.Peek().SetActive(false);
            leftTiles.Peek().name = "LeftTile";
            leftTiles.Peek().SetActive(false);
            
        }

    }

    public void SpawnTile()
    {
        if (leftTiles.Count == 0 || topTiles.Count == 0)
        {
            CreateTiles(10);
        }

        int randomIndex = Random.Range(0, 2);


        if (randomIndex == 0)
        {
            GameObject tmp = leftTiles.Pop();
            tmp.SetActive(true);
            tmp.transform.position = currentTile.transform.GetChild(0).transform.GetChild(randomIndex).position;
            currentTile = tmp;
        }
        else if(randomIndex == 1) 
        {
            GameObject tmp = topTiles.Pop();
            tmp.SetActive(true);
            tmp.transform.position = currentTile.transform.GetChild(0).transform.GetChild(randomIndex).position;
            currentTile = tmp;
        }

        int spawnPickup = Random.Range(0, 10); 

        if (spawnPickup == 0) 
        {
            currentTile.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void ResetGame()
    {   
        Application.LoadLevel(Application.loadedLevel);
    }
}
