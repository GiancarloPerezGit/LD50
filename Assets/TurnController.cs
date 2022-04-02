using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    //Health represents both the speed and a measure to compare how far the cars are from each other. Cars earn points equal to their health
    public int healthP = 4;
    public int healthE = 4;
    //Turn is the variable to track if it is the player's or the enemy's turn. Even turns are the player, odd turns are the enemy
    public int turn = 0;
    //Points represent the amount of the race that a car has gone through. 50 points in a race length of 100 would mean the cars are halfway through the race.
    public int pointsP;
    public int pointsE;
    //Race length is the amount of points needed to finish the race. 
    public int raceLength;
    //A variable used to halt the turn order until the cars have had their position updated.
    public bool positionUpdateDone = false;

    private void Start()
    {
        //Turn on action UI
    }

    void Update()
    {
       if(positionUpdateDone)
       {
            positionUpdateDone = false;
            endTurn();
       }
    }

    public void endTurn()
    {
        turn += 1;
        if (turn % 2 == 0)
        {
            addPoints();
            //Turn on action UI
        }
        else
        {
            //Turn off action UI
            //Run enemy action method
        }
    }


    public void positionUpdate(int newPlayerPosition, int newEnemyPosition)
    {
        //Start coroutine for player position update
        //Start coroutine for enemy position update
    }
    

    public void addPoints()
    {
        pointsP += healthP;
        pointsE += healthE;
        if(pointsP >= pointsE && pointsP >= 100)
        {
            //Player wins
        }
        else if(pointsE > pointsP && pointsE >= 100)
        {
            //Enemy wins
        }
        
    }

}
