using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWood : MonoBehaviour
{
    public float time = 0.5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //destroty the game object's parent after 0.5 seconds
            Destroy(transform.parent.gameObject, time);
        }
    }
}
