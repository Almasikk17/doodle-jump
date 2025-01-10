using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// привет Василий
public class BlackHole : MonoBehaviour
{
    private CircleCollider2D _collider;
    private Rigidbody2D _doodlerRB;
    private float _targetScale = 0.2f;

    private void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
        _doodlerRB = FindObjectOfType<Doodler>().GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        Doodler doodler = collision.GetComponent<Doodler>();
        if(doodler != null && doodler.CurrentState == DoodlerState.Normal)
        {
            StartCoroutine(PullAndDownscalingDoodler(collision.transform));
        }
    }

    private IEnumerator PullAndDownscalingDoodler(Transform doodlerTransform)
    {
        GameObject.FindObjectOfType<Doodler>().MoveSpeed = 0;
        _doodlerRB.bodyType = RigidbodyType2D.Kinematic;
        _doodlerRB.velocity = Vector3.zero;
        Vector3 centerHole = transform.position;
        while(doodlerTransform.position != centerHole)
        {
            doodlerTransform.localScale = Vector3.Lerp(doodlerTransform.localScale, new Vector2(_targetScale,_targetScale), 0.5f * Time.deltaTime);
            doodlerTransform.position = Vector3.MoveTowards(doodlerTransform.position, centerHole, 0.5f * Time.deltaTime);
            if (doodlerTransform == null)
            {
                yield return null;
            }
            if (doodlerTransform.position == centerHole)
            {
                doodlerTransform.position = centerHole;
                doodlerTransform.localScale = new Vector2(_targetScale, _targetScale);
                break;
            }
            yield return null;
        }
        Destroy(doodlerTransform.gameObject);
    }
}
