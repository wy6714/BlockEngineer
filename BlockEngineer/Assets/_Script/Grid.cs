using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

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
    public static event Action<GameManager.BlockType> preplanClickGrid;

    private Block blockScript;

    public PlayMode.mode currentMode;
    private int preplanBlockLeft;
    private bool enoughPreplanLeft;

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
        if (Input.GetKeyUp(KeyCode.Z) && previousStates.Count > 0)
        {
            if (previousStates.Peek().placedBlock != null)//if block has been used, it cannot undo
            {
                UndoHappen?.Invoke(previousStates.Pop());
            }

        }


        if (Input.GetMouseButtonDown(0) && GameManager.gm.currentBlock != null)
        {
            //mouse click place block
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //if there is UI element, won't trigger game interativities
            if (EventSystem.current.IsPointerOverGameObject())

            {

                return; // Do nothing, the click was on UI

            }

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, 10f);

                // normal play mode:
                switch (currentMode)
                {
                    case PlayMode.mode.mode1:
                        if (UIManagment.fruitNum - blockScript.cost >= 0)
                        {
                            GameObject blockObj = Instantiate(GameManager.gm.currentBlock, spawnPosition, Quaternion.identity);

                            // use observer pattern to update fruit UI, pass its cost
                            //Block blockScript = blockObj.GetComponent<Block>();
                            updateFruit?.Invoke(blockScript.cost);
                            Debug.Log("Placed a block");

                            //when placed block, store gamestate:playerPos, placed block obj, blockcost
                            Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
                            StoreGameState(playerPos, blockObj, blockScript.cost);

                            //store placed game object for latter clear
                            GameObject allPlaced = GameObject.FindWithTag("placed");
                            blockObj.transform.parent = allPlaced.transform;
                        }
                        else
                        {
                            updateFruit?.Invoke(-1);//do not have enough fruit
                            ErrorHappened?.Invoke(gameObject);
                        }
                        return;

                    case PlayMode.mode.mode2:
                        //pre plan mode:
                        if(GameManager.gm.selected == GameManager.BlockType.normal)
                        {
                            accessPreplanBlock(GameManager.BlockType.normal);
                            enoughPreplanLeft = preplanBlockLeft >= 1 ? true : false;
                        }

                        if (enoughPreplanLeft)
                        {
                            GameObject blockObj = Instantiate(GameManager.gm.currentBlock, spawnPosition, Quaternion.identity);
                            Debug.Log("placed block");
                            preplanClickGrid?.Invoke(GameManager.BlockType.normal);
                        }
                        
                        return;
                }

                
                
            }
        }

    }

    public void accessPreplanBlock(GameManager.BlockType blockName)
    {
        PlanOperation[] planOperations = FindObjectsOfType<PlanOperation>(); // Find all PlanOperation scripts in the scene

        foreach (PlanOperation planOperation in planOperations)
        {
            if (planOperation.blockName == GameManager.BlockType.normal)
            {
                preplanBlockLeft = planOperation.blockNum;
            }
        }
    }

    private void OnMouseOver()//mouse hover -> hoverSprite
    {
        if (EventSystem.current.IsPointerOverGameObject())

        {

            return; // Do nothing, the click was on UI

        }
        spriteRenderer.sprite = hoverSprite;
    }


    private void OnMouseExit()//mouse exit -> back normal
    {
        if (EventSystem.current.IsPointerOverGameObject())

        {

            return; // Do nothing, the click was on UI

        }
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
