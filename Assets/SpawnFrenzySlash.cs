using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFrenzySlash : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int slashAmount = 4;

    [SerializeField]
    private float startAngle = 90f, endAngle = 270f;
    private Vector2 bulletmoveDirection;
    void Start()
    {
        InvokeRepeating("Fire", 0f, 1.5f);
    }

    // Update is called once per frame
    
    private void Fire()
    {
        float angleStep = (endAngle - startAngle) / slashAmount;
        float angle = startAngle;
        slashAmount = Random.Range(4, 8);
        for(int i = 0; i < slashAmount +1;  i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject slas = FrenzyPool.FrenzyPoolIntense.GetSlash();
            slas.transform.position = transform.position;
            slas.transform.rotation = transform.rotation;
            slas.SetActive(true);
            slas.GetComponent<FrenzySlash>().SetMoveDirection(bulDir);

            
            angle += angleStep ;
            

        }
    }
}
