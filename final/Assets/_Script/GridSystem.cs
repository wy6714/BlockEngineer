using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public GameObject selectedShadow;
    public GameObject unselectedShadow;
    public int rows=22;
    public int columns=13;
    // Start is called before the first frame update
    void Start()
    {
        for(int x =0; x < rows; x++)
        {
            for(int y=0; y<columns; y++)
            {
                Vector3 pos = transform.position;
                pos.x = pos.x + x * 1;
                pos.y = pos.y + y * 1;
                GameObject unselectedObj = Instantiate(unselectedShadow, pos, transform.rotation);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
