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
    [SerializeField]
    private float timer = 2.0f;

  
    private float ShrimpInterval = 1f;

    public bool EnemiesSpawned = false;
    // Start is called before the first frame update
    void Start()
    {
        EnemyCount = TotalAmountWaves;
    }

    // Update is called once per frame
    void Update()
    {
      if(EnemiesSpawned == true)
        {
            
            StartCoroutine(spawnEnemy(ShrimpInterval, Shrimp));
            Debug.Log("Spawning Enemies");
        }

     
       
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        if(timer<=0)
        {
            while (NumWave < TotalAmountWaves)
            {
                yield return new WaitForSeconds(interval);
                GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5), Random.Range(-6f, 6f), 0), Quaternion.identity);
                NumWave++;
            }
        }
    
        
        
    }
}
