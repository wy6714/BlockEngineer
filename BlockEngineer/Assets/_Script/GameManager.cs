using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public GameObject currentBlock;
    public Image currentSelectUI;
    public GameObject normal;
    public GameObject spikes;
    public GameObject jumpBlock;
    public string currentLevel;
    public bool gameMode;//avoid UI touch gameplay

    public static event Action<int> updateLife;

    public int life;

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
        life = 3;
        gameMode = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (life < 0)
        {
            SceneManager.LoadScene(currentLevel);
        }

        if(currentBlock != null)
        {
            currentSelectUI.sprite = currentBlock.GetComponent<SpriteRenderer>().sprite;
        }
     
    }
    public void RespawnPoint(GameObject player)
    {
       
        life = life - 1;
        updateLife?.Invoke(life);
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
