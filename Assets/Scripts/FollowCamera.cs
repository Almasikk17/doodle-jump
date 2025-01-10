using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform DoodlerTransform;

    private void Update()
    {
        if(DoodlerTransform == null)
        {
            return;
        }

        if(DoodlerTransform.position.y > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, DoodlerTransform.position.y, -10);
        }
    }
}
