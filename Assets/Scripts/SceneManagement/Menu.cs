using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator transitionAnim;
    public string sceneName;
    int currentScene;
    public void PlayGame()
    {
        currentScene =  SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(LoadScene());


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

    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(currentScene);
    }
}
