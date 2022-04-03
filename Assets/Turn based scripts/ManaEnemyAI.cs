using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaEnemyAI : ManaCarController
{
    public int nitroStolen = 0;
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

    public void enemySelect(ManaCarController cc)
    {
        if (tailwind)
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
        else if (position >= 8)
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
        else if (position <= 3)
        {
            if (greenAvailable)
            {
                shield(cc);
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
    }

    //Method called by the UI after the action is selected. This method will start the animation and handle any logic needed before a moves effect can be calculated.
    public new void shield(ManaCarController cc)
    {
        StartCoroutine(shieldAnimation(cc));
    }

    //Coroutine that handles the animation of the move. The effects of the move are applied after the animation has completed.
    IEnumerator shieldAnimation(ManaCarController cc)
    {
        yield return new WaitForSeconds(1);
        shieldEffects(cc);
    }
    public new void speedUp(int speedAmount)
    {
        if(tarred)
        {
            if (nitro < 2)
            {
                pcc.nitro += nitro;
                nitro = 0;
            }
            else
            {
                nitro -= 2;
                pcc.nitro += 2;
            }
            if (pcc.nitro > 4)
            {
                pcc.nitro = 4;
            }
        }
        position += speedAmount;
        tc.valueSync();
    }
    
    public new void tarTrapEffects(ManaCarController cc)
    {
        if(cc.sped)
        {
            if(cc.nitro < 2)
            {
                nitro += cc.nitro;
                cc.nitro = 0;   
            }
            else
            {
                cc.nitro -= 2;
                nitro += 2;
            }
            if (nitro > 4)
            {
                nitro = 4;
            }
        }
        countered = true;
        tc.valueSync();
    }
    public new void shieldEffects(ManaCarController cc)
    {
        if (cc.damaged)
        {
            if (cc.nitro < 2)
            {
                nitro += cc.nitro;
                cc.nitro = 0;
            }
            else
            {
                cc.nitro -= 2;
                nitro += 2;
            }
            if (nitro > 4)
            {
                nitro = 4;
            }
        }
        countered = true;
        tc.valueSync();
    }
}
