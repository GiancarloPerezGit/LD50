using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioAnnouncer : MonoBehaviour
{
    // At random moments, the announcer will offer dialogue that absolutely no one asked for. Will use audiomixer to have that sidechained sound
    public List<AudioClip> announcerDialogue;

    private AudioClip lastClipPlayed; //utility variable for the audioclip randomizer
   
    
    private float bufferTime = 12.0f;


    private float timer = 0.0f;
    [Range(10.0f, 15.0f)]
    public float minBufferTime;
    [Range(20.0f, 25.0f)]
    public float maxBufferTime;


    // TODO: make it dontdestroyonload and fade out on exit scene?
    [SerializeField]
    AudioSource audioSource;

    private void Start()
    {
        lastClipPlayed = null;
    }


    private void Update()
    {
        timer += Time.deltaTime;


        if(timer > bufferTime) //buffertime is randomized within a range 
        {
            timer = timer - bufferTime;
            PlayRandomDialogue();
            bufferTime = getRandomFloat(minBufferTime, maxBufferTime);
            
           
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
        
        int num = Random.Range(0, audioClips.Count);
       

        if(lastClipPlayed != audioClips[num])
        {
            lastClipPlayed=audioClips[num];

            return lastClipPlayed;
        }
        
        return audioClips[0]; 
    }

    private float getRandomFloat(float minFloat, float maxFloat)
    {
        return Random.Range(minFloat, maxFloat);
    }

}
