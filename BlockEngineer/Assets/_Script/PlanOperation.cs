using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlanOperation : MonoBehaviour
{
    [SerializeField] private TMP_Text numText;
    private int blockNum;
    [SerializeField] private int cost;

    private PlanSystem planSystemScript;

    public static event Action<int> changeTotalFruit;

    private void Awake()
    {
        planSystemScript = FindObjectOfType<PlanSystem>(); // Find the PlanSystem script in the scene
    }

    private void Update()
    {
        numText.text = blockNum.ToString();
    }

    private void Start()
    {
        blockNum = 0;
    }

    public void addButton()
    {
        if(planSystemScript.totalFruitNum >= cost)//if left fruit is enough to get block
        {
            blockNum += 1;
            changeTotalFruit?.Invoke(-cost);// more block, cost furit, so minus cost
        }
    }

    public void minusButton()
    {
        if (blockNum > 0)
        {
            blockNum -= 1;
            changeTotalFruit?.Invoke(cost); //less block,more fruit left, so add cost
        }
        
    }

}
