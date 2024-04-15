using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    [SerializeField] private Animator candleAnim;
    [SerializeField] private GameObject lightEffect;
    // Start is called before the first frame update
    void Start()
    {
        lightEffect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bullet"))
        {
            //animation
            candleAnim.SetBool("isCandleOn", true);
            lightEffect.SetActive(true);

        }
    }
}
