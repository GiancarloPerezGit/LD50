using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TurnController : MonoBehaviour
{
    public ThreeRules tr;
    public CarController player;
    public EnemyAI enemy;
    //Health represents both the speed and a measure to compare how far the cars are from each other. Cars earn points equal to their health
    public int healthP = 4;
    public int healthE = 4;
    //Turn is the variable to track if it is the player's or the enemy's turn. Even turns are the player, odd turns are the enemy
    public int turn = 1;
    //A variable to track how close to the end of the race the cars are at. Once points is equal to race length or greater, the race is over. This variable is used instead of turn in case we want a way to speed up a race.
    public int pointsP;
    //Race length is the amount of points needed to finish the race. 
    public int raceLength;
    //A variable used to halt the turn order until the cars have had their position updated.
    public int positionUpdateDone = 0;
    //The test UI, used only for debugging. Delete when implementing final.
    public GameObject canvas;
    public GameObject winSprite;
    public GameObject loseSprite;
    public GameObject enemyIndicator;
    public GameObject playerIndicator;
    public Color defaultColor;
    public Color tailwindColor;
    public Color damageColor;


    public TextMeshProUGUI turnCounter;

    public AudioSource audioSource; 
    public AudioClip revForward;
    public AudioClip revBackward;

    /* Method called after the CarController has calculated all the effects of an action.
     * This method will update the TurnController's copy of position of the cars to use for checking End Game conditions.
     * After updating the variables, it will then tell each car to move to their new positions and then wait until the movement is done.
    */
    public void valueSync()
    {
        if(healthP > player.position)
        {
            //Player has moved backwards
            audioSource.PlayOneShot(revBackward);
        }
        else if(healthP < player.position)
        {
            //Player has moved forwards
            audioSource.PlayOneShot(revForward);
        }
        else if (healthE > enemy.position)
        {
            //Enemy has moved backwards
            audioSource.PlayOneShot(revBackward);
        }
        else if (healthE < enemy.position)
        {
            //Enemy has moved forwards
            audioSource.PlayOneShot(revForward);
        }

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
            player.idleRoadAnim.GetComponent<SpriteRenderer>().color = defaultColor;
            enemy.idleRoadAnim.GetComponent<SpriteRenderer>().color = defaultColor;
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
            
            playerIndicator.GetComponent<RectTransform>().anchoredPosition = new Vector3((healthP - 5f) * 20f, -6f, 0);
            enemyIndicator.GetComponent<RectTransform>().anchoredPosition = new Vector3((healthE - 5f) * 20f, 6f, 0);
            addPoints();
            positionUpdateDone = 0;
        }
    }

    /* Method to increment the points of each car.
     * After updating the points, the turn controller must check if the game is over.
     */
    public void addPoints()
    {
        pointsP += 1;
        gameOverCheck();
    }

    /* Method to check if the game is over.
     * There are 3 ways to win and 3 ways to lose.
     * If a car reaches above 10 health, their competitor reaches below 0 health, or the race is over and the car is furthest ahead.
     * If a car reaches below 0 health, their competitor reaches above 10 health, or the race is over and the car is furthest behind.
     * If none of the conditions are met, the turn changes.
     */
    public void gameOverCheck()
    {
        if (healthP > 10 || healthE < 0)
        {
            winSprite.SetActive(true);
            tr.off();
            return;
        }
        else if (healthE > 10 || healthP < 0)
        {
            loseSprite.SetActive(true);
            tr.off();
            return;
        }
        else if(pointsP >= raceLength)
        {
            if(healthP > healthE)
            {
                winSprite.SetActive(true);
                tr.off();
                return;
            }
            else if (healthP < healthE)
            {
                loseSprite.SetActive(true);
                tr.off();
                return;
            }
            if (healthP == healthE)
            {
                winSprite.SetActive(true);
                tr.off();
                return;
            }
        }

        else
        {
            endTurn();
        }
    }

    /* Method that handles changing between the player and enemy's turn.
     * Car variables are reset when a turn starts.
     */
    public void endTurn()
    {
        turn += 1;
        if (turn % 2 == 1)
        {
            print("Player turn");
            turnCounter.text = "Turn " + ((turn + 1)/2).ToString() + "/" + (raceLength/2).ToString();
            //A player shield/tar only lasts until their next turn
            if(player.shielded)
            {
                player.shieldAnim.SetActive(false);
            }
            enemy.GetComponent<Renderer>().material = enemy.defaultColor;
            enemy.idleRoadAnim.GetComponent<Renderer>().material = enemy.defaultColor;
            player.shielded = false;
            enemy.tarred = false;
            //An enemy's action is based on the positions before the players move. Therefore we need to save the positions of player and enemy before the player makes an action.
            player.lastPos = healthP;
            enemy.lastPos = healthE;
            //The UI turns back on when its the players turn.
            //canvas.SetActive(true);
            tr.on();
        }
        else
        {
            
            if (enemy.shielded)
            {
                enemy.shieldAnim.SetActive(false);
            }
            player.GetComponent<Renderer>().material = player.defaultColor;
            player.idleRoadAnim.GetComponent<Renderer>().material = player.defaultColor;
            //The UI turns back off when its the enemys turn.
            //canvas.SetActive(false);
            print("Enemy turn");
            //An enemy shield/tar only lasts until their next turn
            enemy.shielded = false;
            player.tarred = false;
            //Query the enemy for its next move
            enemy.enemySelect(player);
        }
    }

    

}
