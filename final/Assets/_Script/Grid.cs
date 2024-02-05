using System;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject normalBlock;
    public GameObject spikesBlock;

    private GameObject placeBlock;

    public Sprite hoverSprite;
    public Sprite normalSprite;

    private SpriteRenderer spriteRenderer;

    public static event Action<int> updateFruit;

    public static event Action<GameObject> ErrorHappened;

    private Block blockScript;
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


        if (Input.GetMouseButtonDown(0) && placeBlock != null)
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



}
