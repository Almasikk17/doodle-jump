using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPack : MonoBehaviour
{
    public GameObject JetPackPrefab;
    public float FlyTime;
    public float FlySpeed;

    private Transform _JetPackSpawnPoint;
    private Rigidbody2D _playerRB;
    private float _flyTimeRemaining = 0f;
    private bool _isFlying = false;
    private int _blackHoleLayer;
    private int _doodlerLayer;

    private void Start()
    {
        _blackHoleLayer = LayerMask.NameToLayer("BlackHole");
        _doodlerLayer = LayerMask.NameToLayer("Doodler");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Rigidbody2D>(out var rigidbody))
        {
            _playerRB = rigidbody;
            AttachJetPackToDoodler(_playerRB.transform);
            StartFly();
        }
    }

    private void AttachJetPackToDoodler(Transform doodler)
    {
        Transform flyPoint = doodler.Find("JetPackSpawPoint");
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
        if(_isFlying && _playerRB != null)
        {
            _playerRB.velocity = new Vector2(0, FlySpeed);
        }
    }

    private void StartFly()
    {
        _isFlying = true;
        _flyTimeRemaining = FlyTime;
        if(_playerRB != null)
        {
            _playerRB.velocity = Vector3.zero;
            _playerRB.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    private void EndFly()
    {
        _isFlying = false;
        _playerRB.bodyType = RigidbodyType2D.Dynamic;
        Physics2D.IgnoreLayerCollision(_blackHoleLayer, _doodlerLayer, false);
        Destroy(gameObject);
    }
}
