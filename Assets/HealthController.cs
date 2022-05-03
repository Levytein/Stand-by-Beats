using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthController : MonoBehaviour
{
    // Start is called before the first frame update
    public int playerHealth;

    [SerializeField] private Image[] hearts;
    public int healthCounter;

    bool gameHasEnded = false;

    public GameObject player;

    public GameObject gameOver;

     private void Start()
    {
        gameOver.gameObject.SetActive(false);

    }

    // Update is called once per frame
    public void UpdateHealth()
    {
        for(int i = 0; i < hearts.Length; i++)
        {
            if(i<playerHealth)
            {
                hearts[i].color = Color.white;
            }
            else
            {
                hearts[i].color = Color.black;
            }
        }

        if(playerHealth == 0)
        {
            EndGame();
            gameOver.gameObject.SetActive(true);
        }

    }

    public void EndGame()
    {
        if(gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("GAME OVER");
            Destroy(player);
        }
    }
}
