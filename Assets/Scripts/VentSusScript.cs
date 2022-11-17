using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class VentSusScript : MonoBehaviour
{
    public GameObject bs;

    void Start()
    {
        bs = GameObject.Find("BPM");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bs.GetComponent<BeatSystem>().level++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
