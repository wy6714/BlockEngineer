using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public GameObject instructionPanel;
    public GameObject creditPanel;
    // Start is called before the first frame update
    void Start()
    {
        instructionPanel.SetActive(false);
        creditPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Z))
        {
            creditPanel.SetActive(false);
            instructionPanel.SetActive(false);
        }
    }

    public void showInstruction()
    {
        creditPanel.SetActive(false);
        instructionPanel.SetActive(true); 
    }

    public void showCredit()
    {
        instructionPanel.SetActive(false);
        creditPanel.SetActive(true);
    }

    public void play()
    {
        SceneManager.LoadScene("level1");
    }
}
