using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource errorAudio;
    [SerializeField] private AudioSource getFruitAudio;
    [SerializeField] private AudioSource hitAudio;
    [SerializeField] private AudioSource JumpAudio;
    [SerializeField] private AudioSource noMoneyAudio;
    [SerializeField] private AudioSource placeBlockAudio;
    [SerializeField] private AudioSource getKeyAudio;
    [SerializeField] private AudioSource getChestAudio;

    //bat
    [SerializeField] private AudioSource failedAttackBatAudio;
    [SerializeField] private AudioSource birdMovementAudio;

    private void OnEnable()
    {
        JumpBlock.JumpHappened += jumpAudio;

        PlayerController.collectFruit += playGetFruitAudio;//get Fruit Audio

        PlayerController.playerDie += playerDieAudio;

        Grid.updateFruit += playPlaceBlockAudio;//place block Audio

        Grid.ErrorHappened += playerErrorAudio;

        Whale.WhaleDieHappen += playWhaleDieAudio;
        PlayerController.getKeyHappens += playeGetKeyAudio;
        Chest.getChest += playGetChestAudio;

        //bat
        Bat.failedAttackBatHappens += playFailedAttackBatAudio;
        Bat.batFlyHappens += playBatFlyAudio;
        Bat.batDeadHappens += playBatDeadAudio;


    }

    private void OnDisable()
    {
        JumpBlock.JumpHappened -= jumpAudio;

        PlayerController.collectFruit -= playGetFruitAudio;

        PlayerController.playerDie -= playerDieAudio;

        Grid.updateFruit -= playPlaceBlockAudio;

        Grid.ErrorHappened -= playerErrorAudio;

        Whale.WhaleDieHappen -= playWhaleDieAudio;
        PlayerController.getKeyHappens -= playeGetKeyAudio;
        Chest.getChest -= playGetChestAudio;

        //bat
        Bat.failedAttackBatHappens -= playFailedAttackBatAudio;
        Bat.batFlyHappens -= playBatFlyAudio;
        Bat.batDeadHappens -= playBatDeadAudio;


    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void jumpAudio(GameObject obj) => JumpAudio.Play();

    public void playGetFruitAudio(GameObject obj) => getFruitAudio.Play();

    public void playPlaceBlockAudio(int cost) => placeBlockAudio.Play();

    public void playerDieAudio(GameObject obj) => hitAudio.Play();

    public void playWhaleDieAudio(GameObject obj) => hitAudio.Play();

    public void playerErrorAudio(GameObject obj) => errorAudio.Play();
    public void playeGetKeyAudio(GameObject obj) => getKeyAudio.Play();
    public void playGetChestAudio(GameObject obj) => getChestAudio.Play();

    //bat
    public void playFailedAttackBatAudio(GameObject obj) => failedAttackBatAudio.Play();
    public void playBatFlyAudio(GameObject obj) => birdMovementAudio.Play();
    public void playBatDeadAudio(GameObject obj) => hitAudio.Play();

}
