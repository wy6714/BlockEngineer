using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    [SerializeField] private bool cellOn = false;
    [SerializeField] private float chasingSpeed = 1f;
    [SerializeField] private Transform playerTrans;
    [SerializeField] private int attackRange = 3;
    

    private void OnEnable()
    {
        Bullet.shootBatHappen += AttackBat;
    }

    private void OnDisable()
    {
        Bullet.shootBatHappen -= AttackBat;
    }
    private void Update()
    {
        float disToPlayer = Vector2.Distance(transform.position, playerTrans.position);
        if (disToPlayer <= attackRange)
        {
            cellOn = true;
            ChasingPlayer();
        }
        else
        {
            cellOn = false;
        }

    }

    private void ChasingPlayer()
    {
        Vector2 direction = (playerTrans.position - transform.position).normalized;
        transform.Translate(direction * chasingSpeed * Time.deltaTime);
    }
    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        cellOn = true;
    //        isChasing = true;
    //        Debug.Log("cell on is: " + cellOn);

    //    }
    //}

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        cellOn = false;
    //        Debug.Log("cell on is: " + cellOn);
    //    }
    //}

    public void AttackBat(GameObject batObj)
    {
        if (cellOn)
        {
            Debug.Log("current cell is: " + cellOn + ", bat is killed.");
            Destroy(batObj);//gameobject is detect player collider, so cannot destory this.gameobjetc
        }
        else
        {
            Debug.Log("bullet shoot bat, but bat is cell off.");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue ;
        Gizmos.DrawWireSphere(this.transform.position, attackRange);
    }
}
