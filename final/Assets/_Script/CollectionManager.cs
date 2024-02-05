using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{

    private void OnEnable()
    {
        PlayerController.collectFruit += collectFruit;
    }

    private void OnDisable()
    {
        PlayerController.collectFruit -= collectFruit;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void collectFruit(GameObject fruitObj)
    {
        //Debug.Log("invoke collect manager");
        Animator anim = fruitObj.GetComponent<Animator>();
        anim.SetBool("isCollected", true);
        Destroy(fruitObj, 1f);
    }

    
}
