using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool isGetKey = false;
    [SerializeField] private Transform targetTrans;
    [SerializeField] private Transform cameraTargetTrans;
    [SerializeField] private GameObject currentLevel;
    [SerializeField] private GameObject nextLevel;
    [SerializeField] private int cureentLevelNum = 2;

    public static event Action<int> updateCurrenlevelNum;
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
                other.gameObject.transform.position = targetTrans.position;
                Camera.main.transform.position = cameraTargetTrans.position;

                updateCurrenlevelNum?.Invoke(cureentLevelNum);

                nextLevel.SetActive(true);
                currentLevel.SetActive(false);
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
        Animator keyAnim = keyObj.GetComponent<Animator>();
        keyAnim.SetTrigger("getKey");
        Destroy(keyObj, 0.3f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetTrans.transform.position, 0.2f);

    }
}
