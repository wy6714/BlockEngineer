using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sawH : MonoBehaviour
{
    [SerializeField] Transform point1;
    [SerializeField] Transform point2;
    [SerializeField] Transform point3;
    [SerializeField] Transform point4;
    [SerializeField] protected int speed = 3;
    public Points currentPoint;

    // Start is called before the first frame update
    void Start()
    {
        currentPoint = Points.go1;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        switch (currentPoint)
        {
            case Points.go1:
                /*
                Vector2 direction = (point1.position - transform.position).normalized;
                float distance = Vector2.Distance(transform.position, point1.position);
                if (distance < 0.1f)
                {
                    currentPoint = Points.go2;
                }
                else
                {
                    //transform.position += direction * speed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, point1.position, speed * Time.deltaTime);
                }
                */
                goTo(point1, Points.go2);
                return;
            case Points.go2:
                goTo(point2, Points.go3);
                return;
            case Points.go3:
                goTo(point3, Points.go4);
                return;
            case Points.go4:
                goTo(point4, Points.go1);
                return;
        }
    }
    public virtual void goTo(Transform targetPointTrans, Points nextPoint)
    {
        Vector2 direction = (targetPointTrans.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, targetPointTrans.position);
        if (distance < 0.1f)
        {
            Flip();
            currentPoint = nextPoint;
        }
        else
        {

            //transform.position += direction * speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPointTrans.position,
                speed * Time.deltaTime);
        }
    }


    public enum Points
    {
        go1,
        go2,
        go3,
        go4
    }
    protected void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(point1.position, 0.2f);
        Gizmos.DrawWireSphere(point2.position, 0.2f);
        Gizmos.DrawWireSphere(point3.position, 0.2f);
        Gizmos.DrawWireSphere(point4.position, 0.2f);
    }

}
