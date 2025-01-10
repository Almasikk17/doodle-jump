using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerHat : MonoBehaviour
{
    public GameObject PropellerHatPrefab;
    public float FlyDuration;
    public float FlySpeed;

    private Transform _propellerHatSpawnPoint;
    private Rigidbody2D _playerRigidbody;
    private float _flyTimeRemaining;
    private bool _isFlying = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Doodler doodler = other.GetComponent<Doodler>();
        if(doodler != null && doodler.CurrentState == DoodlerState.Normal)
        {
            Rigidbody2D doodlerRb = doodler.GetComponent<Rigidbody2D>();
            _playerRigidbody = doodlerRb;
            AttachHatToDoodler(_playerRigidbody.transform);
            doodler.ActivePowerUp(DoodlerState.FlyingWithPropeller);
            StartFly();
        }
    }

    private void AttachHatToDoodler(Transform doodler)
    {
        Transform flyPoint = doodler.Find("PropellerHatSpawnPoint");
        if(flyPoint != null)
        {
            transform.SetParent(flyPoint);
            transform.localPosition = Vector3.zero;
        }
    }

    private void Update()
    {
        if (_isFlying)
        {       
            _flyTimeRemaining -= Time.deltaTime;
            if (_flyTimeRemaining <= 0)
            {
                EndFly();
            }
        }
    }

    private void FixedUpdate()
    {
        if(_isFlying && _playerRigidbody != null)
        {
            _playerRigidbody.velocity = new Vector2(0, FlySpeed);
        }
    }

    private void StartFly()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetBool("Fly", true);
        _isFlying = true;
        _flyTimeRemaining = FlyDuration;
        if(_playerRigidbody != null)
        {
            _playerRigidbody.velocity = Vector3.zero;
            _playerRigidbody.bodyType = RigidbodyType2D.Kinematic;
        }
        Invoke(nameof(EndFly), FlyDuration);
    }

    private void EndFly()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetBool("Fly", false);
        _isFlying = false;
        _playerRigidbody.bodyType = RigidbodyType2D.Dynamic;
        FindObjectOfType<Doodler>().EndPowerup();
        Destroy(gameObject);
    }
}
