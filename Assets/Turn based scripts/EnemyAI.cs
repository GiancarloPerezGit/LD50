using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : CarController
{
    public bool redAvailable = true;
    public bool blueAvailable = true;
    public bool greenAvailable = true;
    

    public void redSelect()
    {
        redAvailable = true;
        blueAvailable = true;
        greenAvailable = false;
    }
    public void blueSelect()
    {
        redAvailable = true;
        blueAvailable = false;
        greenAvailable = true;
    }
    public void greenSelect()
    {
        redAvailable = false;
        blueAvailable = true;
        greenAvailable = false;
    }

    public void enemySelect(CarController cc)
    {
        if(tailwind)
        {
            if (greenAvailable)
            {
                bigSpeed();
                greenSelect();
            }
            else
            {
                smallSpeed();
                blueSelect();
            }
        }
        else if(position >= 8)
        {
            if (greenAvailable)
            {
                bigSpeed();
                greenSelect();
            }
            else
            {
                smallSpeed();
                blueSelect();
            }
        }
        else if(position <= 3)
        {
            if(greenAvailable)
            {
                shield();
                greenSelect();
            }
            else
            {
                smallSpeed();
                blueSelect();
            }

        }
        else
        {
            if(redAvailable)
            {
                bigDamage(cc);
                redSelect();
            }
            else
            {
                smallDamage(cc);
                blueSelect();
            }
        }
    }
}
