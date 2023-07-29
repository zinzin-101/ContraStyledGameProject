using UnityEngine;

public class BossDead : StateMachineBehaviour
{

    // This function is called when the boss enters the BossDeath state.
    // We use it to trigger the "IsDead" parameter in the Animator.
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsDead", true);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

// This function is called when the boss's death animation finishes playing.
    // We use it to destroy the boss GameObject.
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator.gameObject);
    }
}