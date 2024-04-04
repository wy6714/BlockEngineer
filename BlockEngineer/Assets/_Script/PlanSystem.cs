using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlanSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text totalFruitText;
    public int totalFruitNum;

    //view level
    private int levelPart;
    private float cameraMoveAmount = 20f;
    //[SerializeField] private GameObject part1;
    //[SerializeField] private GameObject part2;
    //[SerializeField] private GameObject part3;
    //[SerializeField] private GameObject part4;
    //[SerializeField] private GameObject part5;
    private List<GameObject> levelList = new List<GameObject>();

    public static event Action<int> setFinalBlockNum;

    private void Start()
    {
        levelPart = 1;

        //store all level parts
        GameObject[] levelWithTag = GameObject.FindGameObjectsWithTag("level");
        foreach (GameObject levelPartObj in levelWithTag)
        {
            levelList.Insert(0, levelPartObj);//ensure level3.1 is the first item
        }
        //only active level part 1 
        foreach (GameObject levelPartObj in levelList)
        {
            levelPartObj.SetActive(false);
        }
        levelList[0].SetActive(true);

        /* print items in the LevelList
         
        foreach (GameObject levelObject in levelList)
        {
            Debug.Log("Index: " + levelList.IndexOf(levelObject) + ", Name: " + levelObject.name);
        }
        */


    }

    private void Update()
    {
        totalFruitText.text = totalFruitNum.ToString();
    }

    private void OnEnable()
    {
        PlanOperation.changeTotalFruit += changeFruit;
    }

    private void OnDisable()
    {
        PlanOperation.changeTotalFruit -= changeFruit;
    }

    public void changeFruit(int cost)
    {
        totalFruitNum += cost;
    }

    public void ReadyButton()
    {
        setFinalBlockNum?.Invoke(1);//int does not mean anything
    }

    public void goNext()
    {
        if (levelPart < 5)
        {
            levelList[levelPart - 1].SetActive(false);//inactive current level part
            levelPart += 1;
            levelList[levelPart - 1].SetActive(true);//actice next level part
            Camera.main.transform.position += new Vector3(cameraMoveAmount,0f,0f);
        }
    }
    public void goPrevious()
    {
        if (levelPart > 1)
        {
            levelList[levelPart - 1].SetActive(false);
            levelPart -= 1;
            levelList[levelPart - 1].SetActive(true);
            Camera.main.transform.position += new Vector3(-cameraMoveAmount, 0f, 0f);
        }
    }

}
