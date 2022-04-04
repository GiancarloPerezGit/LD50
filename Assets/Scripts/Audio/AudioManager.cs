using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    //singleton - Audio manager to play music throughout the game, no crossfading, very basic 
    #region Instance
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    instance = new GameObject("Spawned AudioManager", typeof(AudioManager)).GetComponent<AudioManager>();
                }
            }

            return instance;
        }
        set
        {
            instance = value;
        }
    }
    #endregion

    #region Fields
    private AudioSource musicSource;
    

    private float musicVolume = 1;
    // Multiple musics
    private bool firstMusicSourceIsActive;
    #endregion

    private void Awake()
    {
        // Keep this instance alive throughout the whole game
        DontDestroyOnLoad(this.gameObject);

        // Create audio sources, and save them as references
        musicSource = gameObject.AddComponent<AudioSource>();
       

        // Make sure to enable loop on music sources
        musicSource.loop = true;
       
    }

    #region Pete Audio Methods (with only a single audiosource)
    //problem::will need a "wait" time so there is actually time to fade in and out in one audio source 

    public bool isPlaying()
    {
        return musicSource.isPlaying;
    }
    public void PlayMusicFadeIn(AudioClip musicClip)
    {
        musicSource.clip = musicClip;
        StartCoroutine(UpdateMusicWithFadingIn(musicSource, 2f));
    }

    private IEnumerator UpdateMusicWithFadingIn(AudioSource activeSource, float transitionTime)
    {
        activeSource.Play();


        // Fade in
        for (float t = 0.0f; t <= transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = (t / transitionTime) * musicVolume;
            yield return null;
        }

        // Make sure we don't end up with a weird float value
        activeSource.volume = musicVolume;
    }

    public void StopMusicFadeOut()
    {
        //fade out active audio source 
        //musicSource.clip = musicClip;
        StartCoroutine(UpdateMusicWithFadingOut(musicSource, 2f));

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


        activeSource.Stop();

        // Make sure we don't end up with a weird float value
        activeSource.volume = musicVolume;
    }


    #endregion




    public void PlayMusic(AudioClip musicClip)
    {
        musicSource.clip = musicClip;
        musicSource.Play();
    }


   


    private IEnumerator UpdateMusicWithFadeOut(AudioSource activeSource, AudioClip music, float transitionTime)
    {
        // Make sure the source is active and playing
        if (!activeSource.isPlaying)
            activeSource.Play();

        float t = 0.0f;

        // Fade out
        for (t = 0.0f; t <= transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = (musicVolume - ((t / transitionTime) * musicVolume));
            yield return null;
        }


        activeSource.Stop();

        activeSource.volume = musicVolume;
    }



   
   
    private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip music, float transitionTime)
    {
        // Make sure the source is active and playing
        if (!activeSource.isPlaying)
            activeSource.Play();

        float t = 0.0f;

        // Fade out
        for (t = 0.0f; t <= transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = (musicVolume - ((t / transitionTime) * musicVolume));
            yield return null;
        }

        // Swap to the new music clip
        activeSource.Stop();
        activeSource.clip = music;
        // Gotta restart after changing the clip
        activeSource.Play();

        // Fade in
        for (t = 0.0f; t <= transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = (t / transitionTime) * musicVolume;
            yield return null;
        }

        // Make sure we don't end up with a weird float value
        activeSource.volume = musicVolume;
    }
   




    public void SetMusicVolume(float volume)
    {
        musicVolume = musicSource.volume = volume;
      
    }


    public void PlaySFX(AudioClip clip)
    {
        
        musicSource.PlayOneShot(clip);
    }
}
