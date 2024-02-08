using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool isGetKey = false;
    private void OnEnable()
    {
        PlayerController.getKeyHappens += getKeyHappens;
    }

    private void OnDisable()
    {
        PlayerController.getKeyHappens += getKeyHappens;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isGetKey)
            {
                // go next parts
            }
        }
    }

    public void getKeyHappens(GameObject keyObj)
    {
        isGetKey = true;
        /*
        Todo:
        1. get key animation
        2. destory key in case get it multiple times

        */
    }
}
