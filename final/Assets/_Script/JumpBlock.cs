using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBlock:MonoBehaviour
{
    private float bounce = 20f;
    public static event Action<GameObject> JumpHappened;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("jump block collide with player");
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up*bounce, ForceMode2D.Impulse);
            Destroy(transform.parent.gameObject, 0.5f);
            JumpHappened?.Invoke(other.gameObject);
        }
    }
}
