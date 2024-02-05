using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Whale : MonoBehaviour
{
    public GameObject Left;
    public GameObject Right;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform targetPos;
    public float speed=5;

    public static event Action<GameObject> WhaleDieHappen;

    public bool Freze;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        targetPos = Left.transform;
        Freze = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Freze)
        {
            speed = 0;
        }
        else
        {
            speed = 5f;
        }
        Movement();
        
    }

    public void Movement()
    {
        Vector2 point = targetPos.position - transform.position;
        //set move direction
        if(targetPos == Right.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        //check to change target postion
        if(Vector2.Distance(transform.position, targetPos.position) < 0.5f && targetPos == Left.transform)
        {
            Flip();
            targetPos = Right.transform;
        }
        if (Vector2.Distance(transform.position, targetPos.position) < 0.5f && targetPos == Right.transform)
        {
            Flip();
            targetPos = Left.transform;
        }
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Left.transform.position, 0.5f);
        Gizmos.DrawWireSphere(Right.transform.position, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Spikes"))
        {
            Debug.Log("whale collide with spikes");
            Freze = true;
            WhaleDieHappen?.Invoke(gameObject);
            anim.SetTrigger("WhaleDie");
            Destroy(gameObject, 0.3f);
            Destroy(other.transform.parent.gameObject);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Freze = true;
            anim.SetTrigger("WhaleEat");
            Freze = false;
            Invoke("ResetScene", 1f);
        }
    }

    public void ResetScene()
    {
        SceneManager.LoadScene("level1");
    }

}
