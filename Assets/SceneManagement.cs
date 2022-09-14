using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
  
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Oonga Boonga");
        SceneManager.LoadScene(2);
       
    }
   
}
