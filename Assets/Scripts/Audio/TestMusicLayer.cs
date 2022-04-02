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
        
    }

    void RandomAdder() //assign to a keycode 
    {
        currentPoints += Random.Range(10, 41);
    }


    void LayerChecker()
    {
        if(currentPoints > (maxPoints / 3))
        {
            //TODO: make a method that actually mutes/unmutes the music layers 
            //This way might not work actually, what happens if it dips under the number again
        }
    }
}
