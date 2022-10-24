using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class Menu : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator transitionAnim;
    public string sceneName;
    int currentScene;
    public AudioMixer audioMixer;

    public GameObject PauseMenu;
   
    const string MIXER_SFX = "SFXVolume";
    const string MIXER_SONG = "BGVolume";

    public GameObject OptionsMenu;

    public bool menuOpen = false;


    private void Start()
    {
        audioMixer.SetFloat("volume", 0);
    }

    public void OptionMenu()
    {
      
        OptionsMenu.SetActive(true);
        PauseMenu.SetActive(false);
        
    }
    public void OptionsMenuClose()
    {
        OptionsMenu.SetActive(false);
        PauseMenu.SetActive(true);
    }
    
    public void SetVolume(float volume)
    {
        var dbVolume = Mathf.Log10(volume) * 20;
        if (volume == 0.0f)
        {
            dbVolume = -80.0f;
        }
        audioMixer.SetFloat("volume", dbVolume);
    }
    public void SetVolumeSFX(float volume)
    {
        var dbVolume = Mathf.Log10(volume) * 20;
        if (volume == 0.0f)
        {
            dbVolume = -80.0f;
        }
        audioMixer.SetFloat(MIXER_SFX, dbVolume);
    }
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
