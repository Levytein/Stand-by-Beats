using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;


public class Menu : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator transitionAnim;
    public string sceneName;
    int currentScene;
    public AudioMixer audioMixer;

    public GameObject PauseMenu;
    public GameObject GraphicMenu;
    public GameObject SoundMenu;
    public GameObject itemPanel;

   
    const string MIXER_SFX = "SFXVolume";
    const string MIXER_SONG = "BGVolume";

    public GameObject OptionsMenu;

    //Resolution UI
    public bool menuOpen = false;

    public bool isSettingsopen = false;
    public bool isItemPanelOpen = false;
    public TMP_Dropdown resolutionDropDown;

    Resolution[] resolutions;

    private void Start()
    {
        audioMixer.SetFloat("volume", 0);
        resolutions = Screen.resolutions;

        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width+ "x" +  resolutions[i].height;
            options.Add(option);


            if(resolutions[i].width  == Screen.currentResolution .width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }


        }


        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
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
    public void OpenGraphicSettings()
    {
        GraphicMenu.SetActive(true);
        isSettingsopen = true;
        if (isSettingsopen == true)
        {
            SoundMenu.SetActive(false);
            
        }
        else
        {

        }
    }
    public void CloseItemPanel()
    {

       
            itemPanel.SetActive(false);
        
        
    }
    public void OpenSoundSettings()
    {
        SoundMenu.SetActive(true);
        if(isSettingsopen == true)
        {
            GraphicMenu.SetActive(false);
        }
        else
        {
            
        }
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
        Time.timeScale = 1;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;

    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;

    }

    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(currentScene);
    }
}
