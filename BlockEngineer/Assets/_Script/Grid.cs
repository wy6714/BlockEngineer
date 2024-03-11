using System;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject normalBlock;
    //public GameObject spikesBlock;
    //public GameObject playerObj;

    private GameObject placeBlock;

    public Sprite hoverSprite;
    public Sprite normalSprite;

    private SpriteRenderer spriteRenderer;

    public static event Action<int> updateFruit;
    public static event Action<GameObject> ErrorHappened;
    public static event Action<GameState> UndoHappen;

    private Block blockScript;

    public struct GameState
    {
        public Vector3 playerPos;
        public int fruitCost;
        public GameObject placedBlock;
    }

    public static Stack<GameState> previousStates = new Stack<GameState>();

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        placeBlock = normalBlock;
    }

    private void Update()
    {
        if (GameManager.gm.currentBlock != null)
        {
            blockScript = GameManager.gm.currentBlock.GetComponent<Block>();
        }

        //undo keycode
        if (Input.GetKeyUp(KeyCode.Z) && previousStates.Count>0)
        {
            if(previousStates.Peek().placedBlock != null)//if block has been used, it cannot undo
            {
                UndoHappen?.Invoke(previousStates.Pop());
            }
            
        }


        if (Input.GetMouseButtonDown(0) && GameManager.gm.currentBlock != null)
        {
            //mouse click place block
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, 10f);

                if(UIManagment.fruitNum - blockScript.cost >= 0)
                {
                    GameObject blockObj = Instantiate(GameManager.gm.currentBlock, spawnPosition, Quaternion.identity);

                    // use observer pattern to update fruit UI, pass its cost
                    //Block blockScript = blockObj.GetComponent<Block>();
                    updateFruit?.Invoke(blockScript.cost);
                    Debug.Log("Placed a block");

                    //when placed block, store gamestate:playerPos, placed block obj, blockcost
                    Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
                    StoreGameState(playerPos, blockObj, blockScript.cost);
                }
                else
                {
                    updateFruit?.Invoke(-1);//do not have enough fruit
                    ErrorHappened?.Invoke(gameObject);
                }
                
            }
        }

    }

    private void OnMouseOver()//mouse hover -> hoverSprite
    {
        spriteRenderer.sprite = hoverSprite;
    }


    private void OnMouseExit()//mouse exit -> back normal
    {
        spriteRenderer.sprite = normalSprite;
    }

    public void StoreGameState(Vector3 playerPos, GameObject placedBlock, int blockCost)
    {
        GameState currentState = new GameState
        {
            playerPos = playerPos,
            fruitCost = blockCost,
            placedBlock = placedBlock
        };

        previousStates.Clear();
        previousStates.Push(currentState);
    }

    public void UndoButton()
    {
        if (previousStates.Count > 0)
        {
            if (previousStates.Peek().placedBlock != null)//if block has been used, it cannot undo
            {
                UndoHappen?.Invoke(previousStates.Pop());
            }

        }
    }



}
