using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSlimeShark : Enemy
{
    // Start is called before the first frame update
    public GameObject slimeTrail;
    [SerializeField] private float slimeTrailCD = .7f;
    protected override void Start()
    {
        base.Start();
        InvokeRepeating("SlimeTrail", 1f,slimeTrailCD);

    }

    void SlimeTrail()
    {
        Instantiate(slimeTrail, transform.position, Quaternion.identity) ;
    }
    
    
}
