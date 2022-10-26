using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthController : MonoBehaviour
{
    // Start is called before the first frame update
    private static HealthController activeHealthController;
    public static HealthController ActiveHealthController
    {
        get
        {
            return activeHealthController;
        }


    }


    private List<Image>  hearts = new List<Image>();
    public GameObject heartObject;

    [SerializeField] Transform healthParent;
    public int healthCounter;

    bool gameHasEnded = false;

    public GameObject player;

    public GameObject gameOver;

     private void Start()
    {
        gameOver.gameObject.SetActive(false);
        RefreshHealth();
    }

    // Update is called once per frame
    public void UpdateHealth()
    {
        for(int i = 0; i < hearts.Count; i++)
        {
            if(i < Player.ActivePlayer.currentHealth)
            {
                hearts[i].color = Color.white;
            }
            else
            {
                hearts[i].color = Color.black;
            }
        }

        if(Player.ActivePlayer.currentHealth == 0)
        {
            EndGame();
            gameOver.gameObject.SetActive(true);
        }

    }


    public void RefreshHealth()
    {
        foreach(Image img in hearts)
        {
            Destroy(img.gameObject);
        }

        for(int i = 0; i < Player.maxHealth; i ++)
        {
            hearts.Add(Instantiate(heartObject,healthParent).GetComponent<Image>());

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
