using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackV2 : MonoBehaviour
{
    
    public float BoostDuration;
    public float BoostForce;
    
    private Rigidbody2D _playerRB;
    private Animator _animator;
    private bool _isBoosting = false;
    private float _boostTimeRemaining;
    private Doodler _doodler;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(_isBoosting)
        {
            _boostTimeRemaining -= Time.deltaTime;

            if(_boostTimeRemaining <= 0)
            {
                EndBoost();
            }
        }
    }

    private void FixedUpdate()
    {
        if(_isBoosting && _playerRB != null)
        {
           _playerRB.velocity = new Vector2(0, BoostForce);
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        Doodler doodler = trigger.GetComponent<Doodler>();

        if(doodler != null && doodler.CurrentState == DoodlerState.Normal)
        {
            Rigidbody2D doodlerRB = doodler.GetComponent<Rigidbody2D>();
            _playerRB = doodlerRB;
            AttachJetPackToDoodler(doodler.transform);
            doodler.ActivePowerUp(DoodlerState.FlyingWithJetpack);            
            StartBoost();
        }
    }

    private void AttachJetPackToDoodler(Transform doodler)
    {
        Transform mountPoint = doodler.Find("JetPackSpawPoint");
        if(mountPoint != null)
        {
            transform.SetParent(mountPoint);
            transform.localPosition = Vector3.zero;
        }
    }

    private void StartBoost()
    {
        _isBoosting = true;
        _animator.SetTrigger("Boost");
        _boostTimeRemaining = BoostDuration;
        if(_playerRB != null)
        {
            _playerRB.velocity = Vector3.zero;
            _playerRB.bodyType = RigidbodyType2D.Kinematic;
        }       
        Invoke(nameof(EndBoost), BoostDuration);
    }

    private void EndBoost()
    {
        _isBoosting = false;
        _playerRB.bodyType = RigidbodyType2D.Dynamic;
        FindObjectOfType<Doodler>().EndPowerup();
        Destroy(gameObject);
    }
}
