using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject Shrimp;

    private int NumWave = 0;

    [SerializeField]
    private float ShrimpInterval = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(ShrimpInterval, Shrimp));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        while (NumWave < 4)
        {
            yield return new WaitForSeconds(interval);
            GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5), Random.Range(-6f, 6f), 0), Quaternion.identity);
            NumWave++;
        }
        
        
    }
}
