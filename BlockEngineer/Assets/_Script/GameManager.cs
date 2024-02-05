using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public int flagNum = 0;

    public GameObject champion;
    public GameObject champion2;

    public GameObject currentBlock;
    public GameObject normal;
    public GameObject spikes;
    public GameObject jumpBlock;
    public GameObject level2Obj;
    public GameObject level1Obj;
    public GameObject level2ChampionObj;

    private string selected;

    public Camera mainCamera;
    public float targetCameraXPosition = 20f;

    public static event Action<GameObject> WinHappened;

    void Awake()
    {
        gm = this;
        champion.SetActive(false);
        level2Obj.SetActive(false);
    }
    private void OnEnable()
    {
        PlayerController.GetFlag += UpdateFlagNum;
        PlayerController.GetFlag += FlagAnim;

        PlayerController.showLevel2 += DisplayLevel2;

        PlayerController.EnterLevel2 += moveCamera;
        PlayerController.EnterLevel2 += SceneUpdates;

        WinHappened += showChampion;
    }

    private void OnDisable()
    {

        PlayerController.GetFlag -= UpdateFlagNum;
        PlayerController.GetFlag -= FlagAnim;

        PlayerController.showLevel2 -= DisplayLevel2;

        PlayerController.EnterLevel2 -= moveCamera;
        PlayerController.EnterLevel2 -= SceneUpdates;

        WinHappened -= showChampion;
    }
    // Start is called before the first frame update
    void Start()
    {
        selected = "null";
        currentBlock = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (flagNum == 2)
        {
            WinHappened?.Invoke(champion);
            WinHappened?.Invoke(champion2);
        }
    }

    public void UpdateFlagNum(GameObject obj)
    {
        Flag objScript = obj.GetComponent<Flag>();
        flagNum += objScript.num;
        objScript.num = 0;
    }

    public void showChampion(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void FlagAnim(GameObject obj)
    {
        Animator objAnim = obj.GetComponent<Animator>();
        objAnim.SetBool("NoFlag", true);
    }

    public void DisplayLevel2(GameObject obj)
    {
        level2Obj.SetActive(true);
    }

    //levelChecker.enterlevel2
    public void moveCamera(GameObject obj)
    {
        Vector3 targetPosition = new Vector3(targetCameraXPosition, mainCamera.transform.position.y, mainCamera.transform.position.z);
        mainCamera.transform.position = targetPosition;
    }

    public void SceneUpdates(GameObject obj)
    {
        //insctive level1
        level1Obj.SetActive(false);
        flagNum = 0;
        level2ChampionObj.SetActive(false);
    }

    public void SpikesButton()
    {
        currentBlock = spikes;
        selected = "spikes";
        Debug.Log(selected);
    }

    public void NormalButton()
    {
        currentBlock = normal;
        selected = "normal";
        Debug.Log(selected);
    }

    public void JumpButton()
    {
        currentBlock = jumpBlock;
        selected = "jump";
        Debug.Log(selected);
    }

    
    
}
