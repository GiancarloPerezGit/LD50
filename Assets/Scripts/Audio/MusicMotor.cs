using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMotor : MonoBehaviour
{
    //class responsible for moving between music states. method is IEnumerator in order to have a slight buffer time between music states 

    public MusicState activeState;
    public MusicState intialState;

    // Start is called before the first frame update
    void Start()
    {
        activeState = intialState;
        activeState.StartPlaying();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public IEnumerator changeState(MusicState state)
    {
       
        activeState.StopPlaying();
        //2-3 sec buffer wait time here, allows actual silence between music  
        yield return new WaitForSeconds(2);
        activeState = state;
        activeState.StartPlaying();


    }

    public MusicState getActiveState()
    {
        return activeState;
    }
}
