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
        instructionPanel.SetActive(true);
        creditPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

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

    public void closeCreditPanel() => creditPanel.SetActive(false);
    public void closeInstructionPaenl() => instructionPanel.SetActive(false);
}
