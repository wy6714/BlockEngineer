using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlanOperation : MonoBehaviour
{
    [SerializeField] private TMP_Text numText;
    public int blockNum;//grid need to access
    public GameManager.BlockType blockName;
    public int cost;

    [SerializeField] private TMP_Text finalNumText;//top Left Num Text UI

    private PlanSystem planSystemScript;

    public static event Action<int> changeTotalFruit;

    private void Awake()
    {
        planSystemScript = FindObjectOfType<PlanSystem>(); // Find the PlanSystem script in the scene

        finalNumText.text = "";
    }

    private void OnEnable()
    {
        PlanSystem.setFinalBlockNum += updateFinalNum;
        Grid.preplanClickGrid += UpdatePreplanBlockNum;
    }

    private void OnDisable()
    {
        PlanSystem.setFinalBlockNum -= updateFinalNum;
        Grid.preplanClickGrid -= UpdatePreplanBlockNum;
    }

    private void Update()
    {
        numText.text = blockNum.ToString();
        finalNumText.text = blockNum.ToString();
    }

    private void Start()
    {
        blockNum = 0;
    }

    public void updateFinalNum(int numOne)
    {
        finalNumText.text = blockNum.ToString();
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

    public void UpdatePreplanBlockNum(GameManager.BlockType blockType)
    {
        if(blockName == blockType)
        {
            blockNum -= 1;
        }
        
    }

}
