using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoubleClickCannon : MonoBehaviour
{
    private Vector2 direction;
    private GameObject gridObj;
    private Vector3 mousePos;

    [SerializeField] private GameObject bulletObj;
    [SerializeField] private Transform shootTrans;
    [SerializeField] private int bulletsNum;
    //[SerializeField] private bool CannonOn;

    private Animator anim;
    public static event Action<GameObject> fireEffecrHappens;
    public static event Action<GameObject> cannonShootHappens;
    public static event Action<bool> cannonOnHappens;
    public static event Action<GameObject> bulletNotEnoughHappens;
    public static event Action<GameObject> bulletCostHappens;
    //-----------------------------------------
    private void Start()
    {
        //once cannon has been placed, trun off grid.
        //player cannot place other block unless they delete cannon
        gridObj = GameObject.FindWithTag("grid");
        gridObj.SetActive(false);

        anim = GetComponent<Animator>();

        cannonOnHappens?.Invoke(true);
    }

    private void Update()
    {
        //get mouse position
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (GameManager.gm.cannonBulletNum > 0)
        {
            ControlCannon();
            if (Input.GetMouseButtonDown(0))
            {

                if (EventSystem.current.IsPointerOverGameObject())

                {

                    return; // Do nothing, the click was on UI

                }
                else
                {
                    shoot();
                }

            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {


                if (EventSystem.current.IsPointerOverGameObject())

                {

                    return; // Do nothing, the click was on UI

                }
                else
                {
                    bulletNotEnoughHappens?.Invoke(gameObject);//audio play
                }

            }

        }

    }

    private void ControlCannon()
    {
        direction = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void shoot()
    {
        float mouseToCannonDis = Vector2.Distance(mousePos, gameObject.transform.position);

        if (mouseToCannonDis > 0.5f)//avoid shoot when click to delete cannon
        {
            anim.SetTrigger("CannonFire");
            fireEffecrHappens?.Invoke(gameObject);//fire effect anim
            cannonShootHappens?.Invoke(gameObject);

            //shoot bullet
            GameObject bullet = Instantiate(bulletObj, shootTrans.transform.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.AddForce(direction * 3f, ForceMode2D.Impulse);
            Destroy(bullet, 5f);
            Debug.Log("cannon shoot bullet");

            //if used the canon, clear gamestate data, so it won't undo
            Grid.previousStates.Clear();

            //cost bullet num
            bulletCostHappens?.Invoke(gameObject);
            Debug.Log("shoot bullet, bullet-1, current bullet is:" +
                GameManager.gm.cannonBulletNum);
        }
    }


    void OnMouseDown()
    {
        cannonOnHappens?.Invoke(false);
        gridObj.SetActive(true);
        Destroy(transform.parent.gameObject);
    }

    private void OnDestroy()
    {
        cannonOnHappens?.Invoke(false);
        gridObj.SetActive(true);
    }
}



