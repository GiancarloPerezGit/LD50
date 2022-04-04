using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaCarController : MonoBehaviour
{
    public int position = 4;
    public bool tailwind = false;
    public bool tarred = false;
    public bool shielded = false;
    public bool animationWait = false;
    public ManaTurnController tc;
    public Vector3 newVectorPosition;
    public bool updatePosition = false;
    public float oldPosition;
    public int nitro = 4;
    public bool sped = false;
    public bool damaged = false;
    public ManaCarController pcc;
    public bool countered = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (updatePosition)
        {
            float step = Mathf.Abs(position - oldPosition) * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, newVectorPosition, step);
            if (Vector3.Distance(transform.position, newVectorPosition) < 0.001f)
            {
                updatePosition = false;
                tc.positionUpdated();
            }
        }
    }

    public void takeDamage(int damageAmount)
    {
        if(pcc.shielded)
        {
            if (pcc.nitro < 2)
            {
                nitro += pcc.nitro;
                pcc.nitro = 0;
            }
            else
            {
                pcc.nitro -= 2;
                nitro += 2;
            }
            if (nitro > 4)
            {
                nitro = 4;
            }
        }
        position -= damageAmount;
        tc.valueSync();
    }
    public void speedUp(int speedAmount)
    {
        position += speedAmount;
        tc.valueSync();
    }

    public void changePosition()
    {
        newVectorPosition = new Vector3(position - 5, transform.position.y, transform.position.z);
        oldPosition = transform.position.x;
        updatePosition = true;
    }


    #region RED ABILTIES
    #region BIG DAMAGE
    //Method called by the UI after the action is selected. This method will start the animation and handle any logic needed before a moves effect can be calculated.
    public void bigDamage(ManaCarController cc)
    {
        if (nitro < 2)
        {
            tc.valueSync();
        }
        else
        {
            nitro -= 2;
            StartCoroutine(bigDamageAnimation(cc));
            
        }
    }

    //Coroutine that handles the animation of the move. The effects of the move are applied after the animation has completed.
    IEnumerator bigDamageAnimation(ManaCarController cc)
    {
        yield return new WaitForSeconds(1);
        bigDamageEffects(cc);
    }

    //Method that applies the effect of the move as determined by the first method. If the move has no variation then this method will always be called.
    public void bigDamageEffects(ManaCarController cc)
    {
        //Damage taken while in position 8 or higher is increased by 1 as the driver has a harder time maintaining control at high speeds
        damaged = true;
        if (cc.position >= 8)
        {
            cc.takeDamage(3);
        }
        else
        {
            cc.takeDamage(2);
        }
    }
    #endregion
    #region TAR TRAP
    public void tarTrap(ManaCarController cc)
    {
        StartCoroutine(tarTrapAnimation(cc));
    }

    IEnumerator tarTrapAnimation(ManaCarController cc)
    {
        yield return new WaitForSeconds(1);
        tarTrapEffects(cc);
    }

    public void tarTrapEffects(ManaCarController cc)
    {
        cc.tarred = true;
        countered = true;
        tc.valueSync();
    }
    #endregion
    #endregion
    #region BLUE ABILTIES
    #region SMALL DAMAGE
    //Method called by the UI after the action is selected. This method will start the animation and handle any logic needed before a moves effect can be calculated.
    public void smallDamage(ManaCarController cc)
    {
        if(nitro < 1)
        {
            tc.valueSync();
        }
        else
        {
            nitro -= 1;
            StartCoroutine(smallDamageAnimation(cc));
        }
    }

    //Coroutine that handles the animation of the move. The effects of the move are applied after the animation has completed.
    IEnumerator smallDamageAnimation(ManaCarController cc)
    {
        yield return new WaitForSeconds(1);
        smallDamageEffects(cc);
    }

    //Method that applies the effect of the move as determined by the first method. If the move has no variation then this method will always be called.
    public void smallDamageEffects(ManaCarController cc)
    {
        damaged = true;
        //Damage taken while in position 8 or higher is increased by 1 as the driver has a harder time maintaining control at high speeds
        if (cc.position >= 8)
        {
            cc.takeDamage(2);
        }
        else
        {
            cc.takeDamage(1);
        }
    }
    #endregion
    #region SMALL SPEED
    //Method called by the UI after the action is selected. This method will start the animation and handle any logic needed before a moves effect can be calculated.
    public void smallSpeed()
    {
        print("Speed pressed");
        if (nitro < 1)
        {
            tc.valueSync();
        }
        else
        {
            nitro -= 1;
            StartCoroutine(smallSpeedAnimation());
        }
    }

    //Coroutine that handles the animation of the move. The effects of the move are applied after the animation has completed.
    IEnumerator smallSpeedAnimation()
    {
        print("Coroutine start");
        yield return new WaitForSeconds(1);
        print("Coroutine end");
        smallSpeedEffects();
    }

    //Method that applies the effect of the move as determined by the first method. If the move has no variation then this method will always be called.
    public void smallSpeedEffects()
    {
        sped = true;
        print("Speed effected");
        //If within 3 positions behind the enemy, speed boosts are increased by 1
        if (tailwind)
        {
            speedUp(2);
        }
        else
        {
            speedUp(1);
        }
    }
    #endregion
    #endregion
    #region GREEN ABILTIES
    #region BIG SPEED
    //Method called by the UI after the action is selected. This method will start the animation and handle any logic needed before a moves effect can be calculated.
    public void bigSpeed()
    {
        if(nitro < 2)
        {
            tc.valueSync();
        }
        else
        {
            nitro -= 2;
            StartCoroutine(bigSpeedAnimation());
        }
        
    }

    //Coroutine that handles the animation of the move. The effects of the move are applied after the animation has completed.
    IEnumerator bigSpeedAnimation()
    {
        yield return new WaitForSeconds(1);
        bigSpeedEffects();
    }

    //Method that applies the effect of the move as determined by the first method. If the move has no variation then this method will always be called.
    public void bigSpeedEffects()
    {
        sped = true;
        if (tailwind)
        {
            speedUp(3);
        }
        else
        {
            speedUp(2);
        }
    }
    #endregion
    #region SHIELD
    //Method called by the UI after the action is selected. This method will start the animation and handle any logic needed before a moves effect can be calculated.
    public void shield(ManaCarController cc)
    {
        StartCoroutine(shieldAnimation(cc));
    }

    //Coroutine that handles the animation of the move. The effects of the move are applied after the animation has completed.
    IEnumerator shieldAnimation(ManaCarController cc)
    {
        yield return new WaitForSeconds(1);
        shieldEffects(cc);
    }

    //Method that applies the effect of the move as determined by the first method. If the move has no variation then this method will always be called.
    public void shieldEffects(ManaCarController cc)
    {
        cc.shielded = true;
        countered = true;
        tc.valueSync();
    }
    #endregion
    #endregion
    public void recharge()
    {
        StartCoroutine(rechargeAnimation());
    }
    IEnumerator rechargeAnimation()
    {
        yield return new WaitForSeconds(1);
        rechargeEffects();
    }

    //Method that applies the effect of the move as determined by the first method. If the move has no variation then this method will always be called.
    public void rechargeEffects()
    {
        nitro += 1;
        if (nitro > 4)
        {
            nitro = 4;
        }
        tc.valueSync();
    }
}
