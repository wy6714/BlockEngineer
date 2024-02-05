using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public GameObject Top;
    public GameObject Left;
    public GameObject Down;
    private Transform targetPos;
    private Rigidbody2D rb;
    public float speed = 5;
    public Actions currentAction;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPos = Left.transform;
        currentAction = Actions.goUp;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    public void Movement()
    {
        
        switch (currentAction)
        {
            case Actions.goUp:
                rb.velocity = new Vector2(0, speed);
                if(Vector2.Distance(transform.position, Top.transform.position) < 0.5)
                {
                    currentAction = Actions.goLeft;
                }
                return;
            case Actions.goLeft:
                rb.velocity = new Vector2(-speed, 0);
                if(Vector2.Distance(transform.position, Left.transform.position) < 0.5)
                {
                    currentAction = Actions.goRight;
                }
                return;
            case Actions.goRight:
                rb.velocity = new Vector2(speed, 0);
                if(Vector2.Distance(transform.position, Top.transform.position) < 0.5)
                {
                    currentAction = Actions.goDown;
                }
                return;
            case Actions.goDown:
                rb.velocity = new Vector2(0, -speed);
                if (Vector2.Distance(transform.position, Down.transform.position) < 0.5)
                {
                    currentAction = Actions.goUp;
                }
                return;

        }
    }
   

    public enum Actions
    {
        goUp,
        goLeft,
        goRight,
        goDown
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Left.transform.position, 0.5f);
        Gizmos.DrawWireSphere(Top.transform.position, 0.5f);
        Gizmos.DrawWireSphere(Down.transform.position, 0.5f);
    }
}

