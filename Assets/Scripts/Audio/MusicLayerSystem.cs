using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLayerSystem : MonoBehaviour
{
    [SerializeField]
    private AudioSource musicSource1;
    [SerializeField]
    private AudioSource musicSource2;
    [SerializeField]
    private AudioSource musicSource3;

    [SerializeField]
    private AudioClip layer1;
    [SerializeField]
    private AudioClip layer2;
    [SerializeField]
    private AudioClip layer3;

    // Start is called before the first frame update
    void Start()
    {
        if (musicSource1 == null)
            Debug.LogWarning("Make sure to assign musicSource1! ");
        if (musicSource2 == null)
            Debug.LogWarning("Make sure to assign musicSource2! ");
        if (musicSource3 == null)
            Debug.LogWarning("Make sure to assign musicSource3! ");

        if (layer1 == null)
            Debug.LogWarning("Make sure to assign layer1! ");
        if (layer2 == null)
            Debug.LogWarning("Make sure to assign layer2! ");
        if (layer3 == null)
            Debug.LogWarning("Make sure to assign layer3! ");

        musicSource1.clip = layer1;
        musicSource2.clip = layer2;
        musicSource3.clip = layer3;


      
        musicSource1.Play();
        musicSource2.Play();
        musicSource3.Play();




    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.T))
            musicSource2.mute = !musicSource2.mute;

        if (Input.GetKeyDown(KeyCode.G))
            musicSource3.mute = !musicSource3.mute;


    }

    
   
   



}
