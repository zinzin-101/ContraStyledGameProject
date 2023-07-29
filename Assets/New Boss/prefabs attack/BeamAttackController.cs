using UnityEngine;

public class BeamAttackController : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        Destroy(gameObject, anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);
    }
}