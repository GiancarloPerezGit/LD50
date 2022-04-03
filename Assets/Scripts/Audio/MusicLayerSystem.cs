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

    private float musicVolume = 1;

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

        musicSource2.mute = !musicSource2.mute;
        musicSource3.mute = !musicSource3.mute;

    }

    // Update is called once per frame
    void Update()
    {



        if (Input.GetKeyDown(KeyCode.T))
            PlayMusicSource2();

        if (Input.GetKeyDown(KeyCode.G))
            StopMusicSource2();


        if (Input.GetKeyDown(KeyCode.Y))
            PlayMusicSource3();

        if (Input.GetKeyDown(KeyCode.H))
            StopMusicSource3();
    }

    
   
   public void PlayMusicSource2()
    {
       
        StartCoroutine(UpdateMusicWithFadingIn(musicSource2, 5f));
    }
    public void StopMusicSource2()
    {
        StartCoroutine(UpdateMusicWithFadingOut(musicSource2, 5f));
    }

    public void PlayMusicSource3()
    {

        StartCoroutine(UpdateMusicWithFadingIn(musicSource3, 5f));
    }
    public void StopMusicSource3()
    {
        StartCoroutine(UpdateMusicWithFadingOut(musicSource3, 5f));
    }

    private IEnumerator UpdateMusicWithFadingIn(AudioSource activeSource, float transitionTime)
    {
        //activeSource.Play();
        activeSource.volume = 0.0f;
        activeSource.mute = false;

        // Fade in
        for (float t = 0.0f; t <= transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = (t / transitionTime) * musicVolume;
            yield return null;
        }

        // Make sure we don't end up with a weird float value
        activeSource.volume = musicVolume;
    }

    private IEnumerator UpdateMusicWithFadingOut(AudioSource activeSource, float transitionTime)
    {


        float t = 0.0f;

        // Fade out
        for (t = 0.0f; t <= transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = (musicVolume - ((t / transitionTime) * musicVolume));
            yield return null;
        }

        activeSource.mute = true;
       

        // Make sure we don't end up with a weird float value
        activeSource.volume = musicVolume;
    }
   


    public AudioSource getMusicSource2()
    {
        return musicSource2;
    }

    public AudioSource getMusicSource3()
    {
        return musicSource3;
    }

}
