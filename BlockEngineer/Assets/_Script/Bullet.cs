using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static event Action<GameObject> shootBatHappen;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bat"))
        {
            shootBatHappen?.Invoke(other.gameObject);
        }

        if (other.CompareTag("canShootTerrain"))
        {
            Destroy(other.transform.parent.gameObject);
            Destroy(gameObject);
        }
    }

}
