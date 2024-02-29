using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UIManagment : MonoBehaviour
{
    public TMP_Text fruitNumText;
    public TMP_Text lifeText;
    public static int fruitNum;
    public GameObject askExchangePanel;
    public GameObject creditPanel;
    public GameObject instructionPanel;

    private void OnEnable()
    {
        fruitNum = 20;
        fruitNumText.text = ":" + fruitNum.ToString();
        Grid.updateFruit += UpdateFruitText;
        PlayerController.collectFruit += CollectFruitUI;
        GameManager.updateLife += lifeUI;
        Grid.UndoHappen += undoFruit;
        Grid.UndoHappen += undoBlock;
    }

    private void OnDisable()
    {
        Grid.updateFruit -= UpdateFruitText;
        PlayerController.collectFruit -= CollectFruitUI;
        GameManager.updateLife -= lifeUI;
        Grid.UndoHappen -= undoFruit;
        Grid.UndoHappen -= undoBlock;
    }
    // Start is called before the first frame update
    void Start()
    {
        askExchangePanel.SetActive(false);
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
            fruitNumText.text = fruitNum.ToString() + " Not enough! Click [Buy Fruits] Button on the right";
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

    public void exchangeLifeFruit()
    {
        
        GameManager.gm.life = GameManager.gm.life - 1;
        fruitNum = fruitNum + 10;
        lifeText.text = ":" + GameManager.gm.life.ToString();
        fruitNumText.text = ":" + fruitNum.ToString();
        askExchangePanel.SetActive(false);
       
       
    }

    public void closeaskExchangePanel()
    {
        askExchangePanel.SetActive(false);
    }

    public void exchangeButton()
    {
      
        askExchangePanel.SetActive(true);
       
    }

    public void undoFruit(Grid.GameState currentState)
    {
        fruitNum = fruitNum + currentState.fruitCost;
        fruitNumText.text = ":" + fruitNum.ToString();
    }

    public void undoBlock(Grid.GameState currenState) => Destroy(currenState.placedBlock);

    public void closeCreditPanel() => creditPanel.SetActive(false);
    public void closeInstructionPaenl() => instructionPanel.SetActive(false);
    

}
