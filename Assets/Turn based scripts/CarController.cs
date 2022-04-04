using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CarController : MonoBehaviour
{
    public int position = 5;
    public bool tailwind = false;
    public bool tarred = false;
    public bool shielded = false;
    public bool animationWait = false;
    public TurnController tc;
    public Vector3 newVectorPosition;
    public bool updatePosition = false;
    public float oldPosition;
    public int lastPos = 5;
    public Animator anim;
    public ParticleSystem casting;
    private ParticleSystem.EmissionModule emissionCast;
    // Start is called before the first frame update
    void Start()
    {
        emissionCast = casting.emission;
    }

    // Update is called once per frame
    void Update()
    {
        if(updatePosition)
        {
            float step = Mathf.Abs(position - oldPosition) * 0.3f * Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, newVectorPosition, step);
            if(Vector3.Distance(transform.localPosition, newVectorPosition) < 0.001f)
            {
                updatePosition = false;
                tc.positionUpdated();
            }
        }
    }

    public void takeDamage(int damageAmount)
    {
        if (shielded)
        {
            position += damageAmount;
        }
        else
        {
            position -= damageAmount;
        }
        tc.valueSync();
    }
    public void speedUp(int speedAmount)
    {
        if (tarred)
        {
            position -= speedAmount;
        }
        else
        {
            position += speedAmount;
        }
        tc.valueSync();
    }
    
    public void changePosition()
    {
        newVectorPosition = new Vector3((position - 5) * 0.3f, transform.localPosition.y, transform.localPosition.z);
        oldPosition = transform.localPosition.x;
        updatePosition = true;
    }


    #region RED ABILTIES
    #region BIG DAMAGE
    //Method called by the UI after the action is selected. This method will start the animation and handle any logic needed before a moves effect can be calculated.
    public void bigDamage(CarController cc)
    {
        StartCoroutine(bigDamageAnimation(cc));
    }

    //Coroutine that handles the animation of the move. The effects of the move are applied after the animation has completed.
    IEnumerator bigDamageAnimation(CarController cc)
    {
        anim.SetBool("isCasting", true);
        emissionCast.enabled = true;
        yield return new WaitForSeconds(1);
        anim.SetBool("isCasting", false);
        emissionCast.enabled = false;
        bigDamageEffects(cc);
    }

    //Method that applies the effect of the move as determined by the first method. If the move has no variation then this method will always be called.
    public void bigDamageEffects(CarController cc)
    {
        //Damage taken while in position 8 or higher is increased by 1 as the driver has a harder time maintaining control at high speeds
        if(cc.position >= 8)
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
    public void tarTrap(CarController cc)
    {
        StartCoroutine(tarTrapAnimation(cc));
    }

    IEnumerator tarTrapAnimation(CarController cc)
    {
        anim.SetBool("isCasting", true);
        emissionCast.enabled = true;
        yield return new WaitForSeconds(1);
        anim.SetBool("isCasting", false);
        emissionCast.enabled = false;
        tarTrapEffects(cc);
    }

    public void tarTrapEffects(CarController cc)
    {
        cc.tarred = true;
        tc.valueSync();
    }
    #endregion
    #endregion
    #region BLUE ABILTIES
    #region SMALL DAMAGE
    //Method called by the UI after the action is selected. This method will start the animation and handle any logic needed before a moves effect can be calculated.
    public void smallDamage(CarController cc)
    {
        StartCoroutine(smallDamageAnimation(cc));
    }

    //Coroutine that handles the animation of the move. The effects of the move are applied after the animation has completed.
    IEnumerator smallDamageAnimation(CarController cc)
    {
        anim.SetBool("isCasting", true);
        emissionCast.enabled = true;
        yield return new WaitForSeconds(1);
        anim.SetBool("isCasting", false);
        emissionCast.enabled = false;
        smallDamageEffects(cc);
    }

    //Method that applies the effect of the move as determined by the first method. If the move has no variation then this method will always be called.
    public void smallDamageEffects(CarController cc)
    {
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
        StartCoroutine(smallSpeedAnimation());
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
        StartCoroutine(bigSpeedAnimation());
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
        if(tailwind)
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
    public void shield()
    {
        StartCoroutine(shieldAnimation());
    }

    //Coroutine that handles the animation of the move. The effects of the move are applied after the animation has completed.
    IEnumerator shieldAnimation()
    {
        anim.SetBool("isCasting", true);
        emissionCast.enabled = true;
        yield return new WaitForSeconds(1);
        anim.SetBool("isCasting", false);
        emissionCast.enabled = false;
        shieldEffects();
    }

    //Method that applies the effect of the move as determined by the first method. If the move has no variation then this method will always be called.
    public void shieldEffects()
    {
        shielded = true;
        tc.valueSync();
    }
    #endregion
    #endregion
}
