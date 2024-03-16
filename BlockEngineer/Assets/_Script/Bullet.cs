using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static event Action<GameObject> shootBatHappen;
    public static event Action<GameObject> shootBreakableBlock;
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D bulletRb;

    private void Start()
    {
        bulletRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bat"))
        {
            BulletExplode();
            shootBatHappen?.Invoke(other.gameObject);
            
        }

        if (other.CompareTag("canShootTerrain"))
        {
            BulletExplode();
            shootBreakableBlock?.Invoke(gameObject);
            Destroy(other.transform.parent.gameObject);
            
        }
    }

    private void BulletExplode()
    {
        //freze bullet
        bulletRb.isKinematic = true;
        bulletRb.velocity = Vector2.zero;
        bulletRb.angularVelocity = 0f;

        //bullet anim
        
        anim.SetTrigger("bulletExplode");

        //destory bullet
        Destroy(gameObject, 0.3f);
    }

}
