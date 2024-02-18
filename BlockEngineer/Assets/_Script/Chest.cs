using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chest : MonoBehaviour
{
    [SerializeField] private string nextLevel;
    [SerializeField] private bool isGetKey = false;
    private Animator anim;
    public static event Action<GameObject> getChest;
    // Start is called before the first frame update
    private void OnEnable()
    {
        PlayerController.getKeyHappens += getKeyHappens;
    }

    private void OnDisable()
    {
        PlayerController.getKeyHappens += getKeyHappens;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {

            if (isGetKey)
            {
                anim.SetTrigger("isChestUnlock");
                //go to the next level in 1f seconds
                //Debug.Log("Chest is unlocked");
                getChest?.Invoke(gameObject);
                Invoke("LoadScene", 1f);
            }
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(nextLevel);
    }

    public void getKeyHappens(GameObject keyObj)
    {
        isGetKey = true;
        Animator keyAnim = keyObj.GetComponent<Animator>();
        keyAnim.SetTrigger("getKey");
        Destroy(keyObj, 0.3f);
    }
}
