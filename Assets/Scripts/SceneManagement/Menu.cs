using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayGame()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(currentScene);


    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void BackHub()
    {
        SceneManager.LoadScene(2);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);

    }
    
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
