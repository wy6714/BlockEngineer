using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int cost;
    private bool isPickaxeMode;
    private void OnEnable()
    {
        GameManager.selectPickaxeHappen += usePickaxe;
    }

    private void OnDisable()
    {
        GameManager.selectPickaxeHappen += usePickaxe;
    }

    public void usePickaxe(bool pickaxeMode)
    {
        isPickaxeMode = pickaxeMode;
    }

    private void Update()
    {
        if (isPickaxeMode && Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the mouse cursor position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Perform the raycast and check if it hits this object
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

}
