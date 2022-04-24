using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Stat Boost class" , menuName = "Item/StatBoosters")]
public class StatBoostItems : ItemClass
{
    public float attackSpeed;
    public float coolDownReduction;
    public float permHealth;


    public override ItemClass GetItem() { return this; }
    public override StatBoostItems GetStatBoost(){ return this; }
}
