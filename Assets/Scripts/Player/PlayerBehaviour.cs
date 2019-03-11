﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Life Attributes", order = 0)]
    [Space(10, order = 1)]
    public float currentLife;
    public float maxLife;
    public float minLife;
    public bool isInvicible;
    public float invincibleDuration;

    [Header("Life Skills Attributes", order = 0)]
    [Space(10, order = 1)]
    public float minLifeToShoot;
    public float lifeUsageOnShoot;
    public float lightUsageOnShoot;
    [Space(5, order = 2)]
    public float minLifeToBeam;
    public float lifeUsageToLoadBeam;
    public float lifeUsageEachInterval;
    public float lightUsageOnBeam;
    [Space(5, order = 3)]
    public float minLifeToDash;
    public float lifeUsageOnDash;
    public float lightUsageOnDash;
    [Space(5, order = 4)]
    public float lifeRegen;
    public float lightRegen;
    public bool canShoot;
    public bool canBeam;
    public bool canDash;
    private Light lantern;

    private void Start()
    {
        lantern = GetComponentInChildren<Light>();
        currentLife = maxLife;
        CheckIfPlayerCanUseSkills();
    }

    public void RegenLifeOnCac()
    {
        if (currentLife < maxLife && currentLife > minLife)
        {
            currentLife += lifeRegen / LightMagnetism.nbParticles;
            DOTween.To(() => lantern.intensity, x => lantern.intensity = x, lantern.intensity + lightRegen, .2f);
            lantern.intensity += lightRegen;
            if (currentLife > maxLife)
            {
                currentLife = maxLife;
            }
            if (currentLife < minLife)
            {
                currentLife = minLife;
            }

        }
        CheckIfPlayerCanUseSkills();

    }
    public void UseLifeOnShoot()
    {
        CheckIfPlayerCanUseSkills();
        //Use X% of player maxLife
        if (canShoot)
        {
            currentLife -= lifeUsageOnShoot;
            DOTween.To(() => lantern.intensity, x => lantern.intensity = x, lantern.intensity + 300f, 0.2f);
            DOTween.To(() => lantern.intensity, x => lantern.intensity = x, lantern.intensity - lightUsageOnShoot, .5f);
            CheckIfPlayerCanUseSkills();

        }
    }

    public void UseLifeEachInterval()
    {
        CheckIfPlayerCanUseSkills();

        if(canBeam)
        {
            currentLife -= lifeUsageEachInterval;
            DOTween.To(() => lantern.intensity, x => lantern.intensity = x, lantern.intensity - lifeUsageEachInterval, 0.5f);
            CheckIfPlayerCanUseSkills();
        }
    }

    public void UseLifeToLoadBeam()
    {
        CheckIfPlayerCanUseSkills();
        
        if(canBeam)
        {
            currentLife -= lifeUsageToLoadBeam;
            DOTween.To(() => lantern.intensity, x => lantern.intensity = x, lantern.intensity - lifeUsageToLoadBeam, 0.5f);
            CheckIfPlayerCanUseSkills();
        }
    }
    

    public void UseLifeOnDash()
    {
        CheckIfPlayerCanUseSkills();
        //Use X% of player maxLife
        if (canDash)
        {
            currentLife -= lifeUsageOnDash;
            DOTween.To(() => lantern.intensity, x => lantern.intensity = x, lantern.intensity - lightUsageOnDash, .5f);
            CheckIfPlayerCanUseSkills();

        }

    }
    private void CheckIfPlayerCanUseSkills()
    {
        if (currentLife < minLifeToDash && currentLife > minLife)
        {
            canDash = false;
        }
        else
        {
            canDash = true;
        }

        if (currentLife < minLifeToShoot && currentLife > minLife)
        {
            canShoot = false;
        }
        else
        {
            canShoot = true;
        }

        if(currentLife < minLifeToBeam && currentLife > minLife)
        {
            canBeam = false;
        }
        else
        {
            canBeam = true;
        }
        
    }
    public void TakeHit(int damage)
    {
        if (isInvicible == false)
        {
            currentLife -= damage;
            isInvicible = true;
            StartCoroutine("InvicibleTime");
        }
    }
    IEnumerator InvicibleTime()
    {
        yield return new WaitForSeconds(invincibleDuration);
        isInvicible = false;
        StopCoroutine("InvincibleTime");
    }
}