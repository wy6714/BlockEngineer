using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

public class PlanSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text totalFruitText;
    [SerializeField] private TMP_Text BulletNumText;
    public int totalFruitNum;

    //view level
    private int levelPart;
    private float cameraMoveAmount = 20f;

    private GameObject[] levelWithTag;

    [SerializeField] private GameObject beforeReadyPanel;

    public static event Action<int> setFinalBlockNum;

    private void Start()
    {
        levelPart = 1;

        //store all level parts
        levelWithTag = GameObject.FindGameObjectsWithTag("level");

        //sort object by name
        levelWithTag = levelWithTag.OrderBy(go => go.name).ToArray();

        //only active level part 1 
        foreach (GameObject levelPartObj in levelWithTag)
        {
            levelPartObj.SetActive(false);
        }
        levelWithTag[0].SetActive(true);


        //print items in the LevelList

        //for(int i = 0; i<levelWithTag.Length; i++)
        //{
        //     Debug.Log("Index: " + i + ", name: " + levelWithTag[i].name);
        //}



    }

    private void Update()
    {
        totalFruitText.text = totalFruitNum.ToString();
    }

    private void OnEnable()
    {
        PlanOperation.changeTotalFruit += changeFruit;
        CollectibleBullet.collectBulletHappens += changeBulletNumUI;
    }

    private void OnDisable()
    {
        PlanOperation.changeTotalFruit -= changeFruit;
        CollectibleBullet.collectBulletHappens -= changeBulletNumUI;
    }

    public void changeFruit(int cost)
    {
        totalFruitNum += cost;
    }

    public void ReadyButton()
    {
        setFinalBlockNum?.Invoke(1);//int does not mean anything
        beforeReadyPanel.SetActive(false);
    }

    public void goNext()
    {
        if (levelPart < 5)
        {
            levelWithTag[levelPart - 1].SetActive(false);//inactive current level part
            levelPart += 1;
            levelWithTag[levelPart - 1].SetActive(true);//actice next level part
            Camera.main.transform.position += new Vector3(cameraMoveAmount, 0f, 0f);
        }
    }
    public void goPrevious()
    {
        if (levelPart > 1)
        {
            levelWithTag[levelPart - 1].SetActive(false);
            levelPart -= 1;
            levelWithTag[levelPart - 1].SetActive(true);
            Camera.main.transform.position += new Vector3(-cameraMoveAmount, 0f, 0f);
        }
    }

    public void changeBulletNumUI(GameObject bulletObj)
    {
        BulletNumText.text = GameManager.gm.cannonBulletNum.ToString();
    }

}
