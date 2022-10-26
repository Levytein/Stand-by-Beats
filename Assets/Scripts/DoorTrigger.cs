using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] doorTile;

    public EnemySpawner enemyManagement;

    public GameObject enemySpawner;
    private bool doorClosed = false;

  
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {


            for (int i = 0; i < doorTile.Length; i++)
            {
                doorTile[i].SetActive(true);


            }
            enemySpawner.SetActive(true);

            enemyManagement.EnemiesSpawned = true;
            doorClosed = true;
        }
        

        

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(enemyManagement.EnemyCount <= 0 )
        {
            for(int i = 0; i < doorTile.Length; i++)
            {
                doorTile[i].SetActive(false);
                enemySpawner.SetActive(false);
            }
        }

    }
}
