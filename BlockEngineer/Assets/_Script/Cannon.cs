using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cannon : MonoBehaviour
{
    private bool cannonOn = false;
    private Vector2 direction;
    public GameObject bulletObj;
    //[SerializeField] private float bulletSpeed = 5000f;
    [SerializeField] private float bulletForce = 3f;
    [SerializeField] private Transform shootTrans;
    private GameObject gridObj;
    // Start is called before the first frame update
    void Start()
    {
        gridObj = GameObject.FindWithTag("grid");
    }

    // Update is called once per frame
    void Update()
    {
        //click cannon to turn it on or off
        if (Input.GetMouseButtonDown(0))
        {
            ToggleCannon();
        }

        if (cannonOn)
        {
            gridObj.SetActive(false);
            ControlShoot();
            if (Input.GetMouseButtonDown(0))
            {
                shoot();
            }
        }
        else
        {
            gridObj.SetActive(true);
        }

    }
    private void ToggleCannon()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            cannonOn = !cannonOn; // Toggle cannon state
            Debug.Log("cannon on is: " + cannonOn);
        }
    }

    private void ControlShoot()
    {
        //rotate cannon towards to mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePos - transform.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //Debug.Log("cannon shoot");

        
    }

    private void shoot()
    {
        GameObject bullet = Instantiate(bulletObj, shootTrans.transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        //bulletRb.velocity = direction * bulletSpeed;
        bulletRb.AddForce(direction * 3f, ForceMode2D.Impulse);
        Destroy(bullet, 5f);
        Debug.Log("cannon shoot bullet");
    }
}

