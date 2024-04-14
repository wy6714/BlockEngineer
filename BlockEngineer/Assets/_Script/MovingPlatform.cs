using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private List<Transform> wayPoints;
    [SerializeField] private float movespeed;
    [SerializeField] private int target;

    //---------------------------------------------
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            wayPoints[target].position,
            movespeed * Time.deltaTime);

    }
    private void FixedUpdate()
    {
        if (transform.position == wayPoints[target].position)
        {
            if (target == wayPoints.Count - 1)
            {
                target = 0;
            }
            else
            {
                target += 1;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
        if (collision == null)
        {
            return;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.gameObject.activeSelf)
                collision.transform.SetParent(null);
        }
    }

}
