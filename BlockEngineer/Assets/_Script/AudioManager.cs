using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource errorAudio;
    [SerializeField] private AudioSource getFruitAudio;
    [SerializeField] private AudioSource hitAudio;
    [SerializeField] private AudioSource JumpAudio;
    [SerializeField] private AudioSource placeBlockAudio;
    [SerializeField] private AudioSource getKeyAudio;
    [SerializeField] private AudioSource getChestAudio;

    //bat
    [SerializeField] private AudioSource failedAttackBatAudio;
    [SerializeField] private AudioSource birdMovementAudio;

    //cannon
    [SerializeField] private AudioSource cannonShootAudio;
    [SerializeField] private AudioSource cannonOnFireAudio;
    [SerializeField] private AudioSource breakableBlockCrashAudio;

    //bullet
    [SerializeField] private AudioSource collectBulletAudio;

    //UI Button
    [SerializeField] private AudioSource addButtonAudio;
    [SerializeField] private AudioSource minusButtonAudio;
    [SerializeField] private AudioSource playButtonAudio;
    [SerializeField] private AudioSource selectButtonAudio;
    [SerializeField] private AudioSource viewPreviousButtonAudio;
    [SerializeField] private AudioSource viewNextButtonAudio;

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

        //cannon
        DoubleClickCannon.cannonShootHappens += playCannonShootAudio;
        DoubleClickCannon.cannonOnHappens += playCannonOnAudio;
        DoubleClickCannon.bulletNotEnoughHappens += playNoBulletAudio;

        //bullet
        //block crashed by cannon bullet
        Bullet.shootBreakableBlock += playBlockCrashedByCannonAudio;

        //collectible Bullet
        CollectibleBullet.collectBulletHappens += playCollectBulletAudio;

        //UI
        UIManagment.NoMoneyUIAudioHappens += playerErrorAudio;

        //preplan place block audio
        Grid.preplanPlaceBlockAudio += preplanPlaceBlockAudioPlay;
        



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

        //cannon
        DoubleClickCannon.cannonShootHappens -= playCannonShootAudio;
        DoubleClickCannon.cannonOnHappens -= playCannonOnAudio;

        //bullet
        //block crashed by cannon
        Bullet.shootBreakableBlock -= playBlockCrashedByCannonAudio;

        //collectible bullet
        CollectibleBullet.collectBulletHappens -= playCollectBulletAudio;
        DoubleClickCannon.bulletNotEnoughHappens -= playNoBulletAudio;

        //UI
        UIManagment.NoMoneyUIAudioHappens -= playerErrorAudio;

        //preplan place block audio
        Grid.preplanPlaceBlockAudio -= preplanPlaceBlockAudioPlay;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.gm.selected != GameManager.BlockType.cannon)
        {
            cannonOnFireAudio.Pause();
        }
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

    //cannon
    public void playCannonShootAudio(GameObject obj) => cannonShootAudio.Play();
    public void playCannonOnAudio(bool isOn)
    {
        if (isOn) { cannonOnFireAudio.Play(); }
        else { cannonOnFireAudio.Pause(); }

    }

    //bullet
    public void playBlockCrashedByCannonAudio(GameObject obj) => breakableBlockCrashAudio.Play();
    public void playNoBulletAudio(GameObject obj) => errorAudio.Play();

    //collectible bullet
    public void playCollectBulletAudio(GameObject obj) => collectBulletAudio.Play();

    //preplan place block
    public void preplanPlaceBlockAudioPlay(bool isEnoughLeft)
    {
        if (isEnoughLeft)
        {
            placeBlockAudio.Play();
        }
        else
        {
            errorAudio.Play();
        }
    }

    public void playAddButtonAudio()=> addButtonAudio.Play();
   
    public void playMinusButtonAduio()=> minusButtonAudio.Play();

    public void playPlayButtonAduio() => playButtonAudio.Play();

    public void PlaySelectButtonAudio() => selectButtonAudio.Play();

    public void PlayViewPreviousAudio() => viewPreviousButtonAudio.Play();

    public void PlayViewNextAudio() => viewNextButtonAudio.Play();



}
