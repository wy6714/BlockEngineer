using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHelper : MonoBehaviour
{
    public GameObject testHelperPanel;
    private List<GameObject> levelObjects = new List<GameObject>();
    public GameObject part1;
    public GameObject part2;
    public GameObject part3;
    public GameObject part4;
    public GameObject part5;
    public static event Action<int> addFruitHappens;

    private void Start()
    {
        /*
        GameObject[] foundObjects = GameObject.FindGameObjectsWithTag("level");
        foreach (GameObject obj in foundObjects)
        {
            levelObjects.Add(obj);
            Debug.Log("add level");
        }

        // Check if any objects were found
        if (levelObjects.Count > 0)
        {
            // Print the name of the first GameObject in the list
            Debug.Log("First object tagged as 'level': " + levelObjects[0].name);
            Debug.Log(levelObjects.Count);
        }
        */

        levelObjects.Add(part1);
        levelObjects.Add(part2);
        levelObjects.Add(part3);
        levelObjects.Add(part4);
        levelObjects.Add(part5);

    }
    public void OpenTestHelper()
    {
        testHelperPanel.SetActive(true);
    }
    public void ClosetestHelper()
    {
        testHelperPanel.SetActive(false);
    }
    public void goPart1()
    {
        turnOffParts();
        part1.SetActive(true);
        setCamera(new Vector3(0, 0, 0));
        setPlayerPos(new Vector3(-8.72f, -3.3f, 10));
        testHelperPanel.SetActive(false);
    }
    public void goPart2()
    {
        turnOffParts();
        part2.SetActive(true);
        setCamera(new Vector3(20, 0, 0));
        setPlayerPos(new Vector3(10.7f, -3.3f, 10));
        testHelperPanel.SetActive(false);
    }
    public void goPart3()
    {
        turnOffParts();
        part3.SetActive(true);
        setCamera(new Vector3(40, 0, 0));
        setPlayerPos(new Vector3(30.5f, -3.3f, 10));
        testHelperPanel.SetActive(false);
    }
    public void goPart4()
    {
        turnOffParts();
        part4.SetActive(true);
        setCamera(new Vector3(60, 0, 0));
        setPlayerPos(new Vector3(50.5f, -3.3f, 10));
        testHelperPanel.SetActive(false);
    }
    public void goPart5()
    {
        turnOffParts();
        part5.SetActive(true);
        setCamera(new Vector3(80, 0, 0));
        setPlayerPos(new Vector3(70.5f, -3.3f, 10));
        testHelperPanel.SetActive(false);
    }


    public void turnOffParts()
    {
        foreach(GameObject obj in levelObjects)
        {
            obj.SetActive(false);
        }
    }

    public void setCamera(Vector3 cameraPos)
    {
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mainCamera.transform.position = cameraPos;
    }

    public void setPlayerPos(Vector3 playerPos)
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        playerObj.transform.position = playerPos;
    }

    public void addfruit()
    {
        addFruitHappens?.Invoke(20);
        testHelperPanel.SetActive(false);
    }

}
