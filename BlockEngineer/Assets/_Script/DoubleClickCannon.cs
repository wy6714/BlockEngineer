using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleClickCannon : MonoBehaviour
{
    private Vector2 direction;
    private GameObject gridObj;
    private Vector3 mousePos;

    [SerializeField] private GameObject bulletObj;
    [SerializeField] private Transform shootTrans;
    //[SerializeField] private bool CannonOn;

    //-----------------------------------------
    private void Start()
    {
        //once cannon has been placed, trun off grid.
        //player cannot place other block unless they delete cannon
        gridObj = GameObject.FindWithTag("grid");
        gridObj.SetActive(false);
    }

    private void Update()
    {
        //get mouse position
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        ControlCannon();

        if (Input.GetMouseButtonDown(0)) { shoot(); }
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

        if(mouseToCannonDis > 0.5f)//avoid shoot when click to delete cannon
        {
            GameObject bullet = Instantiate(bulletObj, shootTrans.transform.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.AddForce(direction * 3f, ForceMode2D.Impulse);
            Destroy(bullet, 5f);
            Debug.Log("cannon shoot bullet");
        }
    }
    

    void OnMouseDown()
    {
        gridObj.SetActive(true);
        Destroy(transform.parent.gameObject);
    }

}



