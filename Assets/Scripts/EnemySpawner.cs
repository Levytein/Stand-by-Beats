using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject Shrimp;

    private int NumWave = 0;

    
    public int TotalAmountWaves;

    public int EnemyCount;
    
    private float timer = 2.0f;

  
    private float ShrimpInterval = 1f;

    public bool EnemiesSpawned = false;

  

    Coroutine enemyManager;
    // Start is called before the first frame update
    void Start()
    {
        EnemyCount = TotalAmountWaves * 2;
    }

    // Update is called once per frame
    void Update()
    {
      if(EnemiesSpawned == true)
        {
            if(enemyManager == null)
            {
                enemyManager = StartCoroutine(spawnEnemy(ShrimpInterval, Shrimp));
            }
         
            
        }

        Debug.Log(EnemyCount);
        timer--;
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        if(timer<=0)
        {
            while (NumWave < TotalAmountWaves)
            {
                yield return new WaitForSeconds(interval);
                GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-10f, 10), Random.Range(-2f, 18f), 0), Quaternion.identity);
                NumWave++;
            }
        }


        enemyManager = null;
    }
}
