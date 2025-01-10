using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlatform : MonoBehaviour
{
    public BoxCollider2D Collider;
    public Animator Animator;

    private const string _fallAnim = "Fall"; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider.enabled = false;
        Animator.SetBool(_fallAnim, true);
        Destroy(gameObject, Animator.GetCurrentAnimatorStateInfo(0).length);
    }
}
