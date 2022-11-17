using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject bs;

    public GameObject[] doorTile;

    public EnemySpawner enemyManagement;

    public GameObject enemySpawner;
    private bool doorClosed = false;
    private bool cleared = false;

  
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {


            for (int i = 0; i < doorTile.Length; i++)
            {
                doorTile[i].SetActive(true);


            }
            enemySpawner.SetActive(true);
            cleared = true;

            enemyManagement.EnemiesSpawned = true;
            doorClosed = true;
        }
        

        

    }
    void Start()
    {
        bs = GameObject.Find("BPM");
    }

    // Update is called once per frame
    void Update()
    {
        
        if(enemyManagement.EnemyCount <= 0 && cleared)
        {
            for(int i = 0; i < doorTile.Length; i++)
            {
                doorTile[i].SetActive(false);
                enemySpawner.SetActive(false);
            }
            bs.GetComponent<BeatSystem>().roomsCleard++;
            Destroy(this);
        }

    }
}
