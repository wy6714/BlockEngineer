using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class TestLog : MonoBehaviour
{
    private DateTime startTime;
    private DateTime endTime;
    public string ID;
    [SerializeField] private double duration;
    [SerializeField] private int fruitCost = 0;
    [SerializeField] private int fruitLeft = 0;
    [SerializeField] private int level1_1 = 0;
    [SerializeField] private int level1_2 = 0;
    [SerializeField] private int level1_3 = 0;
    [SerializeField] private int level1_4 = 0;
    [SerializeField] private int level1_5 = 0;
    [SerializeField] private int currentLevel = 1;


    private void OnEnable()
    {
        Chest.getChest += RecordPlayTime;
        Grid.updateFruit += addCost;
        PlayerController.playerDie += countFail;
        Door.updateCurrenlevelNum += updateCurrentLevel;
    }

    private void OnDisable()
    {
        Chest.getChest -= RecordPlayTime;
        Grid.updateFruit -= addCost;
        PlayerController.playerDie -= countFail;
        Door.updateCurrenlevelNum -= updateCurrentLevel;
    }
    // Start is called before the first frame update
    void Start()
    {
        startTime = DateTime.Now;
        endTime = DateTime.Now;//avoid duration empty

        string filePath = Application.dataPath + "/TestLog.csv";
        bool fileExists = File.Exists(filePath);
        using (StreamWriter sw = new StreamWriter(filePath, true))
            //true -> extend, false -> overwirte
        {
            if (!fileExists)
            {
                sw.WriteLine("ID, Total Time (seconds), Cost, Left, 1, 2, 3, 4, 5");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecordPlayTime(GameObject obj)
    {
        endTime = DateTime.Now;
        duration = (endTime - startTime).TotalSeconds;
        fruitLeft = UIManagment.fruitNum;
        string filePath = Application.dataPath + "/TestLog.csv";
        using (StreamWriter sw = new StreamWriter(filePath, true))
        {
            string data = string.Format("{0}, {1}, {2}, {3}, {4}, {5},{6},{7},{8}",
                ID, duration, fruitCost, fruitLeft, level1_1, level1_2, level1_3,
                level1_4, level1_5) ;
            sw.WriteLine(data);
        }

    }

    public void addCost(int cost)
    {
        fruitCost = fruitCost + cost;
    }

    public void countFail(GameObject obj)
    {
        if(currentLevel == 1)
        {
            level1_1 = level1_1 + 1;
        }
        if (currentLevel == 2)
        {
            level1_2 = level1_2 + 1;
            Debug.Log("you die at level" + currentLevel + "this is your" +
                level1_2 + "times die");
        }
        if (currentLevel == 3)
        {
            level1_3 = level1_3 + 1;
        }
        if (currentLevel == 4)
        {
            level1_4 = level1_4 + 1;
        }
        if (currentLevel == 5)
        {
            level1_5 = level1_5 + 1;
        }

    }

    public void updateCurrentLevel(int previouslevel)
    {
        currentLevel = currentLevel+1;
        Debug.Log("current level is" + currentLevel);
    }
}
