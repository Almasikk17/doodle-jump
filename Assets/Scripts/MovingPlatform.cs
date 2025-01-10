using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float MovingDistance;
    public float MovingSpeed;

    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _startPosition = _transform.position;
        _endPosition = _startPosition + Vector3.right * MovingDistance;
    }

    private void Start()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while(true)
        {
            while(Vector3.Distance(_transform.position, _endPosition) >= 0.1f)
            {
                _transform.position = Vector3.MoveTowards(_transform.position, _endPosition, MovingSpeed * Time.deltaTime);
                yield return null;
            }

            while(Vector3.Distance(_transform.position, _startPosition) >= 0.1f)
            {
                _transform.position = Vector3.MoveTowards(_transform.position, _startPosition, MovingSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
