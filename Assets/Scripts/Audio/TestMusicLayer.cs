using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMusicLayer : MonoBehaviour
{

    public MusicLayerSystem musicLayerSystem;

    private int maxPoints = 100;

    public int currentPoints = 0;
    [SerializeField]
    TurnController turnController;
    // Start is called before the first frame update
    void Start()
    {
       if (turnController == null)
            turnController = FindObjectOfType<TurnController>();


        maxPoints = turnController.GetRaceLength();
    }

    // Update is called once per frame
    void Update()
    {


        CalculatePlayerProgression();


        LayerChecker();
    }

    private void CalculatePlayerProgression()
    {
       currentPoints = turnController.pointsP;
    }

    void LayerChecker() //really hacky way at checking states, but still useable
    {
        //problem: must have checks if they are already in play/stop mode, else this will run every frame. 
        if(currentPoints < (maxPoints / 3))
        {

            //mute layer 2 and 3
            if(musicLayerSystem.getMusicSource2().mute == false)
                musicLayerSystem.StopMusicSource2();

            if (musicLayerSystem.getMusicSource3().mute == false)
                musicLayerSystem.StopMusicSource3();

        } 
        else if(currentPoints < (maxPoints * 2 / 3))
        {
            //play layer 2 
            if (musicLayerSystem.getMusicSource2().mute == true)
                musicLayerSystem.PlayMusicSource2();
            //mute layer 3 
            if (musicLayerSystem.getMusicSource3().mute == false)
                musicLayerSystem.StopMusicSource3();
        }
        else if (currentPoints < maxPoints)
        {
            //play  layer 2 
            if (musicLayerSystem.getMusicSource2().mute == true)
                musicLayerSystem.PlayMusicSource2();
            //play layer  3
            if (musicLayerSystem.getMusicSource3().mute == true)
                musicLayerSystem.PlayMusicSource3();
            
        }


    }
}
