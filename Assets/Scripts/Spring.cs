using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public float ThrowForce;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        Doodler doodler = trigger.GetComponent<Doodler>();
        if(doodler != null && doodler.CurrentState == DoodlerState.Normal)
        {
            if (doodler.transform.position.y >= transform.position.y)
            {
                Rigidbody2D doodlerRb = doodler.GetComponent<Rigidbody2D>();
                ThrowUp(doodlerRb);
                doodler.ActivePowerUp(DoodlerState.JumpingOnSpring);
                _animator.SetBool("Streched", true); 
                doodler.EndPowerup();
            }
        }
    }

    private void ThrowUp(Rigidbody2D rigidbody)
    {
        rigidbody.AddForce(Vector3.up * ThrowForce, ForceMode2D.Impulse);
        //FindObjectOfType<Doodler>().EndPowerup();
    }
}
