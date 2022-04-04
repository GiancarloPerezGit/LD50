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
    public Material defaultColor;
    public Material tarredColor;
    public Animator anim;
    public GameObject idleRoadAnim;
    public GameObject smallNitroChargeAnim;
    public GameObject smallNitroCloudAnim;
    public GameObject bigNitroChargeAnim;
    public GameObject bigNitroCloudAnim;
    public GameObject shieldAnim;
    public GameObject sparkAnim;

    public ParticleSystem casting;
    public ParticleSystem sparkBase;
    public ParticleSystem combustionBase;
    public ParticleSystem combustionSmoke;
    public ParticleSystem tarBase;
    public ParticleSystem tarBlob;

    private ParticleSystem.EmissionModule emissionCast;
    private ParticleSystem.EmissionModule emissionBaseSpark;
    private ParticleSystem.EmissionModule emissionBaseCombustion;
    private ParticleSystem.EmissionModule emissionSmoke;
    private ParticleSystem.EmissionModule emissionTarBase;
    private ParticleSystem.EmissionModule emissionTar;

    public AudioClip bigDamageSFX;
    public AudioClip smallDamageSFX;
    public AudioClip shieldSFX;
    public AudioClip tarTrapSFX;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        emissionCast = casting.emission;
        emissionBaseSpark = sparkBase.emission;
        emissionBaseCombustion = combustionBase.emission;
        emissionSmoke = combustionSmoke.emission;
        emissionTarBase = tarBase.emission;
        emissionTar = tarBlob.emission;
    }

    // Update is called once per frame
    void Update()
    {
        if (updatePosition)
        {
            float step = Mathf.Abs(((position - 5) * 0.3f) - oldPosition) * Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, newVectorPosition, step);

            if (Vector3.Distance(transform.localPosition, newVectorPosition) < 0.001f)
            {
                updatePosition = false;
                idleRoadAnim.SetActive(true);
                smallNitroCloudAnim.SetActive(false);
                bigNitroCloudAnim.SetActive(false);

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
        audioSource.PlayOneShot(bigDamageSFX);
        StartCoroutine(bigDamageAnimation(cc));
    }

    //Coroutine that handles the animation of the move. The effects of the move are applied after the animation has completed.
    IEnumerator bigDamageAnimation(CarController cc)
    {
        anim.SetBool("isCasting", true);
        emissionCast.enabled = true;
        emissionBaseCombustion.enabled = true;
        yield return new WaitForSeconds(0.5f);
        emissionCast.enabled = false;
        emissionSmoke.enabled = true;
        anim.SetBool("isCasting", false);
        yield return new WaitForSeconds(0.5f);
        emissionBaseCombustion.enabled = false;
        emissionSmoke.enabled = false;
        bigDamageEffects(cc);
    }

    //Method that applies the effect of the move as determined by the first method. If the move has no variation then this method will always be called.
    public void bigDamageEffects(CarController cc)
    {
        //Damage taken while in position 8 or higher is increased by 1 as the driver has a harder time maintaining control at high speeds
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
    public void tarTrap(CarController cc)
    {
        audioSource.PlayOneShot(tarTrapSFX);
        StartCoroutine(tarTrapAnimation(cc));
    }

    IEnumerator tarTrapAnimation(CarController cc)
    {
        anim.SetBool("isCasting", true);
        emissionCast.enabled = true;
        emissionTarBase.enabled = true;
        yield return new WaitForSeconds(0.5f);
        emissionCast.enabled = false;
        emissionTar.enabled = true;
        anim.SetBool("isCasting", false);
        yield return new WaitForSeconds(0.5f);
        emissionTarBase.enabled = false;
        emissionTar.enabled = false;
        cc.gameObject.GetComponent<Renderer>().material = tarredColor;
        cc.idleRoadAnim.GetComponent<Renderer>().material = tarredColor;
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
        audioSource.PlayOneShot(smallDamageSFX);
        StartCoroutine(smallDamageAnimation(cc));
    }

    //Coroutine that handles the animation of the move. The effects of the move are applied after the animation has completed.
    IEnumerator smallDamageAnimation(CarController cc)
    {
        anim.SetBool("isCasting", true);
        emissionCast.enabled = true;
        emissionBaseSpark.enabled = true;
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isCasting", false);
        emissionCast.enabled = false;
        sparkAnim.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        emissionBaseSpark.enabled = false;
        sparkAnim.SetActive(false);
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
        smallNitroChargeAnim.SetActive(true);
        yield return new WaitForSeconds(1);
        idleRoadAnim.SetActive(false);
        smallNitroChargeAnim.SetActive(false);
        smallNitroCloudAnim.SetActive(true);
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
        print("Coroutine start");
        bigNitroChargeAnim.SetActive(true);
        yield return new WaitForSeconds(1);
        idleRoadAnim.SetActive(false);
        bigNitroChargeAnim.SetActive(false);
        bigNitroCloudAnim.SetActive(true);
        print("Coroutine end");
        bigSpeedEffects();
    }

    //Method that applies the effect of the move as determined by the first method. If the move has no variation then this method will always be called.
    public void bigSpeedEffects()
    {
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
    public void shield()
    {
        audioSource.PlayOneShot(shieldSFX);
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
        shieldAnim.SetActive(true);
        yield return new WaitForSeconds(0.5f);
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
