using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public int openingDirection;

    private RoomTemplates templates;
    private int rand;

    private bool spawned = false;
    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.5f);
    }
     void Spawn()
    {
        if (spawned == false)
        {
            if (openingDirection == 1)
            {
                rand = Random.Range(0, templates.bottomRooms.Length-1);
                Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
            }
            else if (openingDirection == 2)
            {
                rand = Random.Range(0, templates.topRooms.Length-1);
                Instantiate(templates.topRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
            }
            else if (openingDirection == 3)
            {
                rand = Random.Range(0, templates.leftRooms.Length-1);
                Instantiate(templates.leftRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
            }
            else if (openingDirection == 4)
            {
                rand = Random.Range(0, templates.rightRooms.Length-1);
                Instantiate(templates.rightRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
            }
            else if (openingDirection == 5)
            {
                rand = Random.Range(0, templates.rightRooms.Length-1);
                Instantiate(templates.startingRoom, transform.position, templates.bottomRooms[rand].transform.rotation);
            }
            spawned = true;
            }
        }

     void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("SpawnPoint"))
        {
            if(other.GetComponent<SpawnPoint>().spawned == false && spawned == false)
            {
               
                Debug.Log("I spawned a closed room");
            }
            spawned = true;
        }

    }
}

