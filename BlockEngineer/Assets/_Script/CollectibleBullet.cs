using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBullet : MonoBehaviour
{
    public static event Action<GameObject> collectBulletHappens;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collectBulletHappens?.Invoke(gameObject);

            Animator anim = gameObject.GetComponent<Animator>();
            anim.SetBool("isCollected", true);

            Destroy(gameObject,0.3f);
        }
    }
}
