using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public GameObject currentBlock;
    public GameObject normal;
    public GameObject spikes;
    public GameObject jumpBlock;
    // public Transform RespwanPoint1;
    // public Transform RespwanPoint2;
    // public Transform RespwanPoint3;
    // public Transform RespwanPoint4;
    // public Transform RespwanPoint5;

    private string selected;


    void Awake()
    {
        gm = this;
    }
    private void OnEnable()
    {
        PlayerController.playerDie += RespawnPoint;
    }

    private void OnDisable()
    {
        PlayerController.playerDie -= RespawnPoint;
    }
    // Start is called before the first frame update
    void Start()
    {
        selected = "null";
        currentBlock = null;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void RespawnPoint(GameObject player)
    {
        GameObject respawnPoint = GameObject.FindWithTag("Respawn");
        Instantiate(player, respawnPoint.transform.position, respawnPoint.transform.rotation);

        //player.transform.position = respawnPoint.transform.position;
    }

    public void SpikesButton()
    {
        currentBlock = spikes;
        selected = "spikes";
        Debug.Log(selected);
    }

    public void NormalButton()
    {
        currentBlock = normal;
        selected = "normal";
        Debug.Log(selected);
    }

    public void JumpButton()
    {
        currentBlock = jumpBlock;
        selected = "jump";
        Debug.Log(selected);
    }



}
