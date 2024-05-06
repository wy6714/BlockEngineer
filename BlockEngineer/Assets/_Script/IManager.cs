using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.ParticleSystemJobs;

public class IManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int IPart;
    [SerializeField] private int totalIParts;
    [SerializeField] private GameObject helpPanel;
    [SerializeField] private GameObject NextButtonObj;
    [SerializeField] private GameObject BackButtonObj;
    [SerializeField] private GameObject playButtonObj;
    private GameObject[] IWithTag;
    void Start()
    {
        playButtonObj.SetActive(false);
        BackButtonObj.SetActive(false);
        IPart = 0;
        //store all level parts
        IWithTag = GameObject.FindGameObjectsWithTag("instruction");

        //sort object by name
        IWithTag = IWithTag.OrderBy(go => go.name).ToArray();

        //only active level part 1 
        foreach (GameObject levelPartObj in IWithTag)
        {
            levelPartObj.SetActive(false);
        }
        IWithTag[0].SetActive(true);
    }

    void Update()
    {

        if (IPart > 0)
        {
            BackButtonObj.SetActive(true);
        }
        else
        {
            BackButtonObj.SetActive(false);
        }


        if (IPart > totalIParts - 1)
        {
            NextButtonObj.SetActive(false);
            playButtonObj.SetActive(true);
        }
        else
        {
            NextButtonObj.SetActive(true);
            playButtonObj.SetActive(false);
        }
    }


    public void NextButton()
    {
        if (IPart < totalIParts)
        {
            Debug.Log("before press next, the Ipart is" + IPart);
            IWithTag[IPart].SetActive(false);
            IPart += 1;
            Debug.Log("after press next, the Ipart is" + IPart);
            IWithTag[IPart].SetActive(true);
        }
    }

    public void BackButton()
    {
        if (IPart > 0)
        {
            IWithTag[IPart].SetActive(false);
            IPart -= 1;
            IWithTag[IPart].SetActive(true);
            Debug.Log("back pressed" + IPart);
        }
    }
    public void playButton()
    {
        helpPanel.SetActive(false);
    }

}
