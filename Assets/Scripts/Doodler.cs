using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum DoodlerState
{
    Normal,
    FlyingWithPropeller,
    FlyingWithJetpack,
    JumpingOnSpring
}

public class Doodler : MonoBehaviour
{
    public DoodlerState CurrentState = DoodlerState.Normal;
    public event Action OnDoodlerDestroyed;
    public ScoreCounter ScoreCounter;
    public float JumpForce;
    public float MoveSpeed;
    public Rigidbody2D BulletPrefab;
    public float BulletSpeed;

    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private Vector3 _startScale; 
    private Animator _animator;
    private Collider _bulletCollider;
    private Camera _camera;
    private string _jumpAnim = "Jump"; // записываем имя параметра!!!

    public void ActivePowerUp(DoodlerState newState)
    {
        if(CurrentState == DoodlerState.Normal)
        {
            CurrentState = newState;
            Debug.Log($"POWER UP ACTIVATED, DOODLER IS {CurrentState}");
        }
    }

    public void EndPowerup()
    {
        CurrentState = DoodlerState.Normal;
        Debug.Log("POWER UP ENDED, DOODLER IS NORMAL");
    }

    private void Start()
    {
        CurrentState = DoodlerState.Normal;
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody2D>();
        _startScale = _transform.localScale;
        _animator = GetComponent<Animator>();
        _camera = Camera.main;
        _rigidbody.velocity = Vector3.up * JumpForce * 2.5f;

        int doodlerLayer = LayerMask.NameToLayer("Platform");
        int bulletLayer = LayerMask.NameToLayer("Bullet");
        Physics2D.IgnoreLayerCollision(doodlerLayer, bulletLayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DoodlerRBLogic();

        if(collision.gameObject.CompareTag("EnemyTag"))
        {
            float offsetY = 0.26f;
            Vector3 hitPoint = collision.contacts[0].point;
            Transform hitTransform = collision.collider.transform;
             
            if(_transform.position.y > hitTransform.position.y + offsetY)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                ScoreCounter.AddScore(500);
                Destroy(gameObject);                
            }
        }        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _animator.SetBool(_jumpAnim, false);
    }

    private void Update()
    {
        Movement();
        MirrorScalingX(); 
        JumpTroughBorders();       
        Shoot();
    }

    private void OnDestroy()
    {
        OnDoodlerDestroyed?.Invoke();
    }

    private void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        _transform.Translate(Vector2.right * MoveSpeed * horizontal * Time.deltaTime);
    }

    private void MirrorScalingX()
    {       
        float horizontalAxis = Input.GetAxis("Horizontal");

        if (horizontalAxis < 0)
        {
            _transform.localScale = _startScale;
        }

        if (horizontalAxis > 0)
        {
            _transform.localScale = new Vector3(-_startScale.x, _startScale.y, _startScale.z);
        }
    }

    private void JumpTroughBorders()
    {
        float doodlerBorderX = 2.88f;

        if(transform.position.x > doodlerBorderX)
        {
            transform.position = new Vector2(-doodlerBorderX, transform.position.y);
        }
        else if(transform.position.x < -doodlerBorderX)
        {
            transform.position = new Vector2(doodlerBorderX, transform.position.y);
        }
    }

    private void DoodlerRBLogic()
    {
        _rigidbody.freezeRotation = true;
        _rigidbody.velocity = Vector2.zero;
        _animator.SetBool(_jumpAnim, true);
        _rigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }

    private void Shoot()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 mousePosition = Input.mousePosition;
            Vector2 mousePositionOnScreen = _camera.ScreenToWorldPoint(mousePosition);
            //mousePositionOnScreen.z = 0f; использовать в случае если обрабатываешь Vector3 
            Vector2 shootDirection = (mousePositionOnScreen - (Vector2)transform.position).normalized;
            Rigidbody2D bulletRB = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
            bulletRB.velocity = shootDirection * BulletSpeed;
        }
    }
}
