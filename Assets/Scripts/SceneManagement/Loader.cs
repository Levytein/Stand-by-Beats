using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Loader : MonoBehaviour
{

    public GameObject loadingScreen;
    public Slider slider;

    // Start is called before the first frame update
    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynch(sceneIndex));


    }

    // Update is called once per frame
    
    IEnumerator LoadAsynch(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while(!operation.isDone)
        {

            float progress = Mathf.Clamp01(operation.progress / .9f);
            Debug.Log(operation.progress);
            slider.value = progress;

            yield return null;
        }
    }
}
