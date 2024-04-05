using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

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
    //private List<GameObject> levelList = new List<GameObject>();
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
         
       for(int i = 0; i<levelWithTag.Length; i++)
       {
            Debug.Log("Index: " + i + ", name: " + levelWithTag[i].name);
       }
        


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
        beforeReadyPanel.SetActive(false);
    }

    public void goNext()
    {
        if (levelPart < 5)
        {
            levelWithTag[levelPart - 1].SetActive(false);//inactive current level part
            levelPart += 1;
            levelWithTag[levelPart - 1].SetActive(true);//actice next level part
            Camera.main.transform.position += new Vector3(cameraMoveAmount,0f,0f);
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

}
