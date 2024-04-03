using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlanSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text totalFruitText;
    public int totalFruitNum;

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

}
