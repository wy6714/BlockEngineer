using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    [SerializeField] private bool cellOn = false;
    [SerializeField] private float chasingSpeed = 1f;
    [SerializeField] private Transform playerTrans;
    [SerializeField] private int attackRange = 3;
    [SerializeField] private bool Freze;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        Freze = false;

    }
    private void OnEnable()
    {
        playerTrans = GameObject.FindWithTag("Player").transform;
        Bullet.shootBatHappen += AttackBat;
    }

    private void OnDisable()
    {
        Bullet.shootBatHappen -= AttackBat;
    }
    private void Update()
    {
        playerTrans = GameObject.FindWithTag("Player").transform;
        float disToPlayer = Vector2.Distance(transform.position, playerTrans.position);
        if (disToPlayer <= attackRange)
        {
            cellOn = true;
            ChasingPlayer();
            anim.SetBool("isFly", true);
        }
        else
        {
            cellOn = false;
            anim.SetBool("isFly", false);
        }

        Flip();

    }

    private void ChasingPlayer()
    {
        if (!Freze) // if canno hits bat, bat freze and play hit animation
        {
            Vector2 direction = (playerTrans.position - transform.position).normalized;
            transform.Translate(direction * chasingSpeed * Time.deltaTime);
        }
        
    }
   

    public void AttackBat(GameObject batObj)
    {
        if (cellOn)
        {
            Debug.Log("current cell is: " + cellOn + ", bat is killed.");
            Freze = true;
            anim.SetTrigger("batDie");
            Destroy(batObj,0.3f);//gameobject is detect player collider, so cannot destory this.gameobjetc
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

    private void Flip()
    {
        //player | bat
        if (playerTrans.position.x < transform.position.x)
        {
            Vector3 localScale = transform.localScale;
            localScale.x = Mathf.Abs(localScale.x); 
            transform.localScale = localScale;
        }
        //Bat | player -> flip
        else if (playerTrans.position.x > transform.position.x)
        {
            Vector3 localScale = transform.localScale;
            localScale.x = -Mathf.Abs(localScale.x); 
            transform.localScale = localScale;
        }

    }
}
