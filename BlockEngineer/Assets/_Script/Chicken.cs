using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Chicken : sawH
{
    // Start is called before the first frame update
    public bool isChickenTouchLight = false;
    void Start()
    {
        base.currentPoint = sawH.Points.go1;
    }

    // Update is called once per frame
    void Update()
    {
        base.Movement();
    }

    public void ToggleChickenSpeed()
    {
        if (isChickenTouchLight)
        {
            speed = 0;
        }
        else
        {
            speed = 3;

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "candleLight")
        {
            isChickenTouchLight = true;
            ToggleChickenSpeed();
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "candleLight")
        {
            isChickenTouchLight = true;
            ToggleChickenSpeed();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "candleLight")
        {
            isChickenTouchLight = false;
            ToggleChickenSpeed();
        }
    }
}
