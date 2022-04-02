using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MusicState : MonoBehaviour
{ //music state system, each state holds a reference to the motor(driver) and can hold one or more audio clips 

    public MusicMotor motor;

    public AudioClip clip;

    public virtual void StateChangeCheck()
    {

    }




    public virtual void StartPlaying()
    {

    }


    public virtual void StopPlaying()
    {
        AudioManager.Instance.StopMusicFadeOut();
    }
}
