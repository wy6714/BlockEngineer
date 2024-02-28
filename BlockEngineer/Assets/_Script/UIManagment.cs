using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UIManagment : MonoBehaviour
{

    public TMP_Text fruitNumText;
    public TMP_Text lifeText;
    public static int fruitNum;

    private void OnEnable()
    {
        fruitNum = 20;
        fruitNumText.text = ":" + fruitNum.ToString();
        Grid.updateFruit += UpdateFruitText;
        PlayerController.collectFruit += CollectFruitUI;
        GameManager.updateLife += lifeUI;
    }

    private void OnDisable()
    {
        Grid.updateFruit -= UpdateFruitText;
        PlayerController.collectFruit -= CollectFruitUI;
        GameManager.updateLife -= lifeUI;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gm.currentBlock != null)
        {
            Block blockScript = GameManager.gm.currentBlock.GetComponent<Block>();
        }

    }
    public void UpdateFruitText(int cost)
    {
        if (cost > 0)
        {
            fruitNum -= cost;
            fruitNumText.text = ":" + fruitNum.ToString();
        }
        else
        {
            fruitNumText.text = fruitNum.ToString() + " Not enough!";
        }

    }

    public void CollectFruitUI(GameObject fruitObj)
    {
        fruitNum += 1;
        fruitNumText.text = ":" + fruitNum.ToString();
    }

    public void lifeUI(int life)
    {
        lifeText.text = ":" + life.ToString(); 
    }
}
