using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MartiniTransition : StateMachineBehaviour
{
    public float speed = 5f;
    Rigidbody2D rb;
    Transform EnragedPosition;
    Martini boss;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EnragedPosition = GameObject.FindGameObjectWithTag("MartiniEnraged").transform;

    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //boss.Transition();

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    
}
