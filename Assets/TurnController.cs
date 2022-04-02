using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public int healthP = 4;
    public int healthE = 4;
    public int turn = 0;
    public int pointsP;
    public int pointsE;
    private bool waitForAnimation = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(turn % 2 == 0)
        {
            if(!waitForAnimation)
            {
                endTurn();
                waitForAnimation = true;
            }    
        }
        else if(turn % 2 == 1)
        {
            if(!waitForAnimation)
            {
                healthE += 1;
                endTurn();
            }
            
        }

    }

    IEnumerator animationPlay()
    {
       yield return new WaitForSeconds(1);
        waitForAnimation = false;
    }

    public void actionSelected()
    {
        StartCoroutine(animationPlay());
    }


    public void endTurn()
    {
        turn += 1;
        if(turn % 2 == 0)
        {
            addPoints();
        }
    }

    public void addPoints()
    {
        pointsP += healthP;
        pointsE += healthE;
    }

}
