using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrenzyPool : MonoBehaviour
{
    // Start is called before the first frame update
    public static FrenzyPool FrenzyPoolIntense;

    [SerializeField]
    private GameObject pooledSlash;
    private bool notEnoughSlashes = true;

    private List<GameObject> slashes;


    private void Awake()
    {
        FrenzyPoolIntense = this;
    }
    void Start()
    {
        slashes = new List<GameObject>();
    }

    public GameObject GetSlash()
    {
        if(slashes.Count > 0)
        {
            for(int i = 0; i < slashes.Count;i++)
            {
                if(!slashes[i].activeInHierarchy)
                {
                    return slashes[i];
                }
            }
        }

        if(notEnoughSlashes)
        {
            GameObject slas = Instantiate(pooledSlash);
            slas.SetActive(false);
            slashes.Add(slas);
            return slas;
        }
        return null;
    }
   
}
