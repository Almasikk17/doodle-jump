using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    private void Start()
    {
        int doodlerLayer = LayerMask.NameToLayer("Platform");
        int bulletLayer = LayerMask.NameToLayer("Bullet");
        Physics2D.IgnoreLayerCollision(doodlerLayer, bulletLayer);
        Destroy(gameObject, 1f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Monsters monsters))
        {            
            Destroy(monsters.gameObject);
            Destroy(gameObject);
        }
    }
}
