using System.Threading.Tasks;
using UnityEngine;

public class PlayerAnimate : MonoBehaviour
{
    private Animator animator;


    void Start()
    {
        animator = transform.GetComponent<Animator>();
    }

    public void Walk()
    {
        animator.SetBool("Walking", true);
    }
    public void StopWalk()
    {
        animator.SetBool("Walking", false);
    }
    public void Jump()
    {
        animator.SetBool("Jumping", true);
        animator.SetBool("Walking", false);
    }
    public void StopJump()
    {
        animator.SetBool("Jumping", false);
    }
    public void Sprint()
    {
        animator.SetFloat("WalkSpeed", 1.25f);
    }
    public void StopSprint()
    {
        animator.SetFloat("WalkSpeed", 1f);
    }

    public void grow()
    {
        animator.SetTrigger("Grow");
    }

    public void shrink()
    {
        animator.SetTrigger("Shrink");
    }
    public void Unkillable(bool val)
    {
        animator.SetBool("Unkillable", val);
    }
    public void Crouch()
    {
        animator.SetBool("Crouhed", true);
    }
    public void StopCrouch()
    {
        animator.SetBool("Crouhed", false);
    }
    public void dead()
    {

        animator.SetTrigger("Dead");
    }

}
