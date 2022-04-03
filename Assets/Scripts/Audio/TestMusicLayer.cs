using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMusicLayer : MonoBehaviour
{

    public MusicLayerSystem musicLayerSystem;

    [Range(75, 200)]
    public int maxPoints;

    public int currentPoints = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
            RandomAdder();

        if (Input.GetKeyDown(KeyCode.D))
            RandomSubtractor();


        LayerChecker();
    }

    void RandomAdder() //assign to a keycode 
    {
        currentPoints += Random.Range(10, 41);
    }

    void RandomSubtractor() //assign to a keycode 
    {
        currentPoints -= Random.Range(10, 41);
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
