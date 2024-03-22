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
    public GameObject cannonBlock;
    public Sprite pickaxeSprite;
    public string currentLevel;
    public GameObject gridObj;

    public static event Action<int> updateLife;
    public static event Action<bool> selectPickaxeHappen;

    public int life;

    public string selected;

    public bool pickaxeMode = false;

    //cannon
    public int cannonBulletNum;


    void Awake()
    {
        gm = this;
        gridObj = GameObject.FindWithTag("grid");
    }
    private void OnEnable()
    {
        PlayerController.playerDie += RespawnPoint;
        CollectibleBullet.collectBulletHappens += addBulletsNum;
        DoubleClickCannon.bulletCostHappens += minusBulletNum;

        Door.updateCurrenlevelNum += updateGridObj;
    }

    private void OnDisable()
    {
        PlayerController.playerDie -= RespawnPoint;
        CollectibleBullet.collectBulletHappens -= addBulletsNum;
        DoubleClickCannon.bulletCostHappens -= minusBulletNum;

        Door.updateCurrenlevelNum -= updateGridObj;
    }
    // Start is called before the first frame update
    void Start()
    {
        selected = "null";
        currentBlock = null;
        life = 3;
        pickaxeMode = false;
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

        if (selected == "pixkaxe")
        {
            pickaxeMode = true;
        }
        else
        {
            pickaxeMode = false;
        }

        //avoid cannon disappear, but grid is off
        //if(selected == "normal" || selected == "jump" || selected == "spikes")
        if(selected != "cannon")
        {
            
            gridObj.SetActive(true);
            //Debug.Log("grid is not null, and set actived");
            
            
            //destory cannon
            GameObject cannonObj = GameObject.FindWithTag("cannon");

            if (cannonObj != null)
            {
                Destroy(cannonObj.transform.parent.gameObject);
            }
        }

    }
    public void RespawnPoint(GameObject player)
    {
       
        life -=1;
        updateLife?.Invoke(life);
        GameObject respawnPoint = GameObject.FindWithTag("Respawn");
        Instantiate(player, respawnPoint.transform.position, respawnPoint.transform.rotation);
        
        //player.transform.position = respawnPoint.transform.position;
    }

    public void SpikesButton()
    {
        //pickaxeMode = false;
        currentBlock = spikes;
        selected = "spikes";
        Debug.Log(selected);

        //gridObj.SetActive(true);
    }

    public void NormalButton()
    {
        //pickaxeMode = false;
        currentBlock = normal;
        selected = "normal";
        Debug.Log(selected);

        gridObj.SetActive(true);
    }

    public void JumpButton()
    {
        //pickaxeMode = false;
        currentBlock = jumpBlock;
        selected = "jump";
        Debug.Log(selected);

        //gridObj.SetActive(true);
    }

    public void PickaxeBlcokButton()
    {
        currentBlock = null;
        selected = "pickaxe";
        currentSelectUI.sprite = pickaxeSprite;
        selectPickaxeHappen?.Invoke(true);
    }

    public void CannonBlockButton()
    {
        selected = "cannon";
        currentBlock = cannonBlock;
        Debug.Log(selected);
    }

    private void addBulletsNum (GameObject obj)
    {
        cannonBulletNum += 1;
        
    }

    private void minusBulletNum(GameObject obj)
    {
        cannonBulletNum -= 1;
    }

    public void updateGridObj(int levelNum)
    {
        gridObj = GameObject.FindWithTag("grid"); 
    }
}
