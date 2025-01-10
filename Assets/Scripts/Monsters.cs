using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Monsters : MonoBehaviour
{
    public event Action<Monsters> OnEnemyDestroyed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<BulletScript>(out var bullet))
        {
            OnEnemyDestroyed?.Invoke(this);
        }
    }
}
