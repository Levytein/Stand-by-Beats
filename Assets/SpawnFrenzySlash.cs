using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFrenzySlash : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int slashAmount = 4;

    [SerializeField]
    private float arc = 180;
    [SerializeField]
    private float aimAngle = 0;
    private int counter = 0;
    private bool increment = false;

    private Vector2 bulletmoveDirection;
    void Start()
    {
        
    }

    // Update is called once per frame
    
    private void Fire()
    {
        //float angleStep = (arc) / slashAmount;
        //float angle = aimAngle - (arc / 2 );
        increment = !increment;
        for(int i = 0; i < slashAmount + (increment? 1:0) ;  i++)
        {
            GameObject slas = FrenzyPool.FrenzyPoolIntense.GetSlash();
            slas.transform.position = transform.position;
            slas.transform.rotation = Quaternion.identity * Quaternion.AngleAxis(((2f * i / (slashAmount + (increment ? 2 : 1) - 1f))- 1f)* arc , Vector3.forward);
            slas.transform.rotation *= Quaternion.AngleAxis(aimAngle, Vector3.forward);



            //slas.transform.rotation = Quaternion.identity * Quaternion.AngleAxis(-(arc/2) + (i * (arc / 2)/(slashAmount-1)) + 180 , Vector3.forward);
            //slas.transform.rotation *= Quaternion.AngleAxis(aimAngle, Vector3.forward);
            slas.SetActive(true);
            //Debug.Log(-(arc / 2) + (i * arc / (slashAmount - 1)) + 180);
            //Old way of handling slash directions
            /*float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;
            */

            /*GameObject slas = FrenzyPool.FrenzyPoolIntense.GetSlash();
            slas.transform.position = transform.position;
            slas.transform.rotation = transform.rotation;
            
            */
            //slas.GetComponent<FrenzySlash>().SetMoveDirection(bulDir);


            //angle += angleStep ;


        }
        if(!GetComponent<Animator>().GetBool("EnragedSlash")) 
        {
            GetComponent<Animator>().SetBool("EnragedSlash", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("EnragedSlash", false);

        }
        counter++;
        Debug.Log(counter);
        if(counter >= 10)
        {
            GetComponent<Animator>().SetBool("Exhausted", true);
        }
        
    }

    private void CountDown()
    {
        counter -= 4;
        if (counter <= 0)
        {
            GetComponent<Animator>().SetBool("Exhausted", false);
            GetComponent<Animator>().SetBool("EnragedSlash", false);

        }
        Debug.Log(counter);
    }
}
