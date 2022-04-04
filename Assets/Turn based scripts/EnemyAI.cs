using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyAI : CarController
{
    public bool redAvailable = true;
    public bool blueAvailable = true;
    public bool greenAvailable = true;
    public bool tailwindCalc = false;
    public bool pTailwind = false;
    public void redSelect()
    {
        redAvailable = false;
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
        if(cc.lastPos - lastPos <= 3 && cc.lastPos != lastPos)
        {
            tailwindCalc = true;
        }
        else
        {
            tailwindCalc = false;
        }
        if (lastPos - cc.lastPos <= 3 && cc.lastPos != lastPos)
        {
            pTailwind = true;
        }
        else
        {
            pTailwind = false;
        }
        
        if (lastPos > 8)
        {
            print("Option 1");
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
            print("Option 2");
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
        else if(cc.lastPos > 7)
        {
            print("Option 3");
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
        else if(cc.lastPos < 2)
        {
            print("Option 4");
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
        else if(pTailwind && lastPos > 5)
        {
            if(redAvailable)
            {
                print("Tarred");
                tarTrap(cc);
                
                redSelect();
            }
            else
            {
                smallDamage(cc);
                blueSelect();
            }
        }
        else if (pTailwind && lastPos < 5)
        {
            if (greenAvailable)
            {
                print("Shielded");
                shield();
                
                greenSelect();
            }
            else
            {
                smallDamage(cc);
                blueSelect();
            }
        }
        else if(tailwindCalc)
        {
            print("Option 5");
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
            print("Option 6");
            if (redAvailable)
            {
                bigDamage(cc);
                redSelect();
            }
            else
            {
                smallSpeed();
                blueSelect();
            }
        }
    }
}
