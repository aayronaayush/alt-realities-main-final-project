using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAreaController : MonoBehaviour
{
    int isJumpingHash;

    private void Awake()
    {
        isJumpingHash = Animator.StringToHash("isJumping");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("vanguard"))
        {
            Animator animator = other.gameObject.GetComponentInParent<Animator>();
            animator.SetBool(isJumpingHash, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("vanguard"))
        {
            Animator animator = other.gameObject.GetComponentInParent<Animator>();
            animator.SetBool(isJumpingHash, false);
        }
    }
}
