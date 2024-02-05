using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource errorAudio;
    [SerializeField] private AudioSource flagAudio;
    [SerializeField] private AudioSource getFruitAudio;
    [SerializeField] private AudioSource hitAudio;
    [SerializeField] private AudioSource JumpAudio;
    [SerializeField] private AudioSource noMoneyAudio;
    [SerializeField] private AudioSource placeBlockAudio;
    [SerializeField] private AudioSource winAudio;

    private void OnEnable()
    {
        JumpBlock.JumpHappened += jumpAudio;

        PlayerController.collectFruit += playGetFruitAudio;//get Fruit Audio
        
        PlayerController.playerDie += playerDieAudio;

        PlayerController.GetFlag += playGetFlagAudio;

        //GameManager.WinHappened += playWinAudio;

        Grid.updateFruit += playPlaceBlockAudio;//place block Audio

        Grid.ErrorHappened += playerErrorAudio;

        Whale.WhaleDieHappen += playWhaleDieAudio;

        
    }

    private void OnDisable()
    {
        JumpBlock.JumpHappened -= jumpAudio;

        PlayerController.collectFruit -= playGetFruitAudio;

        PlayerController.playerDie -= playerDieAudio;

        PlayerController.GetFlag -= playGetFlagAudio;

        //GameManager.WinHappened -= playWinAudio;

        Grid.updateFruit -= playPlaceBlockAudio;

        Grid.ErrorHappened -= playerErrorAudio;

        Whale.WhaleDieHappen -= playWhaleDieAudio;

        

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void jumpAudio(GameObject obj)=>JumpAudio.Play();

    public void playGetFruitAudio(GameObject obj) => getFruitAudio.Play();

    public void playPlaceBlockAudio(int cost) => placeBlockAudio.Play();

    public void playerDieAudio(GameObject obj) => hitAudio.Play();

    public void playWhaleDieAudio(GameObject obj) => hitAudio.Play();

    public void playerErrorAudio(GameObject obj) => errorAudio.Play();

    //public void playWinAudio(GameObject obj) => winAudio.Play();

    public void playGetFlagAudio(GameObject obj)
    {
        Flag FlagScript = obj.GetComponent<Flag>();
        if(FlagScript.num != 0)
        {
            flagAudio.Play();
        }
    }


}
