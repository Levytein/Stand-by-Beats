using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrenzySlash : MonoBehaviour
{

    public float moveSpeed = 10f;
    private Vector2 moveDirection;

    
    // Start is called before the first frame update
  
    private void OnEnable()
    {
        Invoke("Destroy", 2f);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
}
