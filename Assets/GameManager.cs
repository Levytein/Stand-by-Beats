using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    // Start is called before the first frame update
    private void Awake()
    {

        instance = this;


        //SceneManager.LoadSceneAsync((int)SceneIndexes.)
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
