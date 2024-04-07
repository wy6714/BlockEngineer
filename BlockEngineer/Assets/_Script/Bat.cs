using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    [SerializeField] private bool cellOn = false;
    [SerializeField] private float chasingSpeed = 1f;
    [SerializeField] private Transform playerTrans = null;
    [SerializeField] private int attackRange = 3;
    [SerializeField] private bool Freze;
    //public bool Freze;
    private Animator anim;
    private Vector3 batOriginPos;

    public static event Action<GameObject> failedAttackBatHappens;
    public static event Action<GameObject> batFlyHappens;
    public static event Action<GameObject> batDeadHappens;

    private void Start()
    {
        anim = GetComponent<Animator>();
        Freze = false;
        batOriginPos = gameObject.transform.position;

        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            playerTrans = playerObj.transform;
        }
        //playerTrans = GameObject.FindWithTag("Player").transform;

    }
    private void OnEnable()
    {
        PlayerController.playerDie += resetBatPos;
        Bullet.shootBatHappen += AttackBat;
    }

    private void OnDisable()
    {
        PlayerController.playerDie -= resetBatPos;
        Bullet.shootBatHappen -= AttackBat;
    }
    private void Update()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            playerTrans = playerObj.transform;

            //chasing player
            float disToPlayer = Vector2.Distance(transform.position, playerTrans.position);
            if (disToPlayer <= attackRange)
            {
                cellOn = true;
                ChasingPlayer();
                anim.SetBool("isFly", true);

            }
            else
            {
                batFlyHappens?.Invoke(gameObject);
                cellOn = false;
                anim.SetBool("isFly", false);
            }

            Flip();
        }

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
            batObj.GetComponent<Bat>().Freze = true;
            Animator batObjAnim = batObj.GetComponent<Animator>();
            batObjAnim.SetTrigger("batDie");
            batDeadHappens?.Invoke(batObj);
            Destroy(batObj,0.3f);//gameobject is detect player collider, so cannot destory this.gameobjetc
        }
        else
        {
            failedAttackBatHappens?.Invoke(gameObject);
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

    private void resetBatPos(GameObject Playerobj)
    {
        Debug.Log("invoke bat back to its original position");
        transform.position = batOriginPos;
        
    }
}
