using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioAnnouncer : MonoBehaviour
{
    // At random moments, the announcer will offer dialogue that absolutely no one asked for. Will use audiomixer to have that sidechained sound
    public List<AudioClip> announcerDialogue;


    //TODO: range serializable for easier movement 
    public float timer = 0.0f;
    public float bufferTime = 10.0f; 


    //brute force approach-> while we are in current scene, play a random announcer dialouge after a certain(slight random) amount of time 
    //bad: chance that announcer/music cuts off when player presses play. Solution: make it dontdestroyonload and fade out on exit scene?
    [SerializeField]
    AudioSource audioSource; 

    private void Update()
    {
        timer += Time.deltaTime;


        if(timer > bufferTime) //buffertime is randomized within a range 
        {
            PlayRandomDialogue();
            timer = timer - bufferTime;
        }
           
    }



    private void PlayRandomDialogue()
    {
        Debug.Log("Playing random dialogue!!");
        audioSource.clip = ChooseRandomAudioClip(announcerDialogue);
        audioSource.Play();

    }

    private AudioClip ChooseRandomAudioClip(List<AudioClip> audioClips)
    {
        //TODO: remove possibility of same clip already, maybe same past 2 clips
        int num = Random.Range(0, audioClips.Count);

            return audioClips[num];
    }


}
