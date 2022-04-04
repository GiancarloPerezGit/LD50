using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public CarController player;
    public EnemyAI enemy;
    //Health represents both the speed and a measure to compare how far the cars are from each other. Cars earn points equal to their health
    public int healthP = 4;
    public int healthE = 4;
    //Turn is the variable to track if it is the player's or the enemy's turn. Even turns are the player, odd turns are the enemy
    public int turn = 1;
    //Points represent the amount of the race that a car has gone through. 50 points in a race length of 100 would mean the cars are halfway through the race.
    public int pointsP;
    public int pointsE;
    //Race length is the amount of points needed to finish the race. 
    public int raceLength;
    //A variable used to halt the turn order until the cars have had their position updated.
    public int positionUpdateDone = 0;



    public GameObject canvas;
    private void Start()
    {
       
    }

    void Update()
    {
       
    }
    
    /* Method called after the CarController has calculated all the effects of an action.
     * This method will update the TurnController's copy of position of the cars to use for checking End Game conditions.
     * After updating the variables, it will then tell each car to move to their new positions and then wait until the movement is done.
    */
    public void valueSync()
    {
        healthP = player.position;
        healthE = enemy.position;
        player.changePosition();
        enemy.changePosition();
    }
    /* Method called by both cars once they have reached their new position.
     * Since it is called twice (once by each car) but we only want the effects of the method to occur once, a variable is used to track how many times the method has been called.
     * Once the method is called for the second time, the new points value is calculated based on the cars' new positions.
     * The method also is in charge of adding and removing the tailwind effect depending on how close the 2 cars are.
     */
    public void positionUpdated()
    {
        positionUpdateDone += 1;
        if (positionUpdateDone == 2)
        {
            if (healthE - healthP <= 3 && healthE != healthP)
            {
                player.tailwind = true;
            }
            else
            {
                player.tailwind = false;
            }
            if (healthP - healthE <= 3 && healthE != healthP)
            {
                enemy.tailwind = true;
            }
            else
            {
                enemy.tailwind = false;
            }
            addPoints();
            positionUpdateDone = 0;
        }
    }

    /* Method to increment the points of each car. A reminder that the points a car owns is a representation of how far through the race it has gone.
     * After updating the points, the turn controller must check if the game is over.
     */
    public void addPoints()
    {
        pointsP += 1;
        gameOverCheck();
    }

    /* Method to check if the game is over.
     * There are 3 ways to win and 3 ways to lose.
     * If a car reaches above 10 health, their competitor reaches below 0 health, or a car has more points than the length of the race, that car wins.
     * If a car reaches below 0 health, their competitor reaches above 10 health, or a competitor has more points than the length of the race, that car loses.
     * If none of the conditions are met, the turn changes.
     */
    public void gameOverCheck()
    {
        if (healthP > 10 || healthE < 0)
        {
            //Player wins
            return;
        }
        else if (healthE > 10 || healthP < 0)
        {
            //Player loses
            return;
        }
        else if(pointsP >= raceLength)
        {
            return;
        }

        else
        {
            endTurn();
        }
    }

    /* Method that handles changing between the player and enemy's turn.
     */
    public void endTurn()
    {
        turn += 1;
        if (turn % 2 == 1)
        {
            canvas.SetActive(true);
            print("Player turn");
            player.shielded = false;
            enemy.tarred = false;
            player.lastPos = healthP;
            enemy.lastPos = healthE;
            //Turn on action UI
        }
        else
        {
            canvas.SetActive(false);
            print("Enemy turn");
            enemy.shielded = false;
            player.tarred = false;
            enemy.enemySelect(player);
            //Turn off action UI
            //Run enemy action method
        }
    }

    

}
