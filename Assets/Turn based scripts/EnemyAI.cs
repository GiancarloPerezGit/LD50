using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : CarController
{
    public bool redAvailable = true;
    public bool blueAvailable = true;
    public bool greenAvailable = true;
    public bool tailwindCalc = false;

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
        if(cc.lastPos - lastPos <= 3)
        {
            tailwindCalc = true;
        }
        if(lastPos > 8)
        {
            if(tailwindCalc && blueAvailable)
            {
                smallSpeed();
                blueSelect();
            }
            else if(greenAvailable)
            {
                bigSpeed();
                greenSelect();
            }
        }
        else if(lastPos < 2)
        {
            if(greenAvailable)
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
        else if(cc.lastPos > 8)
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
        else if(cc.lastPos < 2)
        {
            if (redAvailable)
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
        else if(tailwind)
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
