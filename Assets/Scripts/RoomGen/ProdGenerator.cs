using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProdGenerator : MonoBehaviour
{

    public Rooms[,] rooms;

    public Vector2Int maxDimensions = Vector2Int.one * 6;

    public int maxCap = 25;

    public GameObject roomPrefab;

    public Vector2 roomSize;

    void Start()
    {
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateMap()
    {
        rooms = new Rooms[maxDimensions.x, maxDimensions.y]; 
        int counter = 0;

        Vector2Int currentCord = new Vector2Int(Random.Range(0, maxDimensions.x), Random.Range(0, maxDimensions.y));
        rooms[currentCord.x, currentCord.y] = GameObject.Instantiate(roomPrefab).GetComponent<Rooms>();

        rooms[currentCord.x, currentCord.y].startingRoom = true;

        rooms[currentCord.x, currentCord.y].transform.position = new Vector3(currentCord.x * roomSize.x, currentCord.y * roomSize.y, 0);

        for (int i = 0; i < 50; i++)
        {
            Debug.Log("Testing");
            if (counter >= maxCap)
            {
                break;
            }


            Vector2Int direction = Vector2Int.zero;


            switch (Random.Range(0, 4))
            {
                case 0:
                    direction = Vector2Int.right;
                    break;
                case 1:
                    direction = Vector2Int.up;
                    break;
                case 2:
                    direction = Vector2Int.left;
                    break;
                case 3:
                    direction = Vector2Int.down;
                    break;
            }


            if (currentCord.x + direction.x < 0 || currentCord.y + direction.y < 0 || currentCord.x + direction.x > maxDimensions.x || currentCord.y + direction.y > maxDimensions.y)
            {
               
            }
            else
            {
                if (rooms[currentCord.x + direction.x, currentCord.y + direction.y] == null)
                {
                    counter++;
                    currentCord = currentCord + direction;

                    rooms[currentCord.x, currentCord.y] = GameObject.Instantiate(roomPrefab).GetComponent<Rooms>();

                    rooms[currentCord.x, currentCord.y].transform.position = new Vector3(currentCord.x * roomSize.x, currentCord.y * roomSize.y, 0);
                }
                else
                {
                    Debug.Log("Inside else");
                }
              
            }


        }
    }
}
