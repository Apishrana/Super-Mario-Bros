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

    public async Task grow()
    {
        animator.SetBool("Unkillable", true);
        animator.SetTrigger("Grow");
        // animator.ResetTrigger("Grow");
        await Awaitable.WaitForSecondsAsync(0.7f, destroyCancellationToken);
        animator.SetBool("Unkillable", false);
    }

    public async Task shrink()
    {
        animator.SetBool("Unkillable", true);
        animator.SetTrigger("Shrink");
        // animator.ResetTrigger("Shrink");
        await Awaitable.WaitForSecondsAsync(1.333f, destroyCancellationToken);
        animator.SetBool("Unkillable", false);
    }
    public void Crouch()
    {
        animator.SetBool("Crouhed", true);
    }
    public void StopCrouch()
    {
        animator.SetBool("Crouhed", false);
    }

}
