using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : MonoBehaviour
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        DoubleClickCannon.fireEffecrHappens += playFireEffectAnim;
    }

    private void OnDisable()
    {
        DoubleClickCannon.fireEffecrHappens -= playFireEffectAnim;
    }

    public void playFireEffectAnim(GameObject obj)
    {
        anim.SetTrigger("FireEffect");
    }


}
