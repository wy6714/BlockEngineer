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
    [SerializeField] private GameObject gridObj;

    public static event Action<int> updateLife;
    public static event Action<bool> selectPickaxeHappen;

    public int life;

    private string selected;

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
        
            
    }

    private void OnDisable()
    {
        PlayerController.playerDie -= RespawnPoint;
        CollectibleBullet.collectBulletHappens -= addBulletsNum;
        DoubleClickCannon.bulletCostHappens -= minusBulletNum;
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
        if(selected != "cannon")
        {
            gridObj.SetActive(true);
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
    }

    public void NormalButton()
    {
        //pickaxeMode = false;
        currentBlock = normal;
        selected = "normal";
        Debug.Log(selected);
    }

    public void JumpButton()
    {
        //pickaxeMode = false;
        currentBlock = jumpBlock;
        selected = "jump";
        Debug.Log(selected);
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
}
