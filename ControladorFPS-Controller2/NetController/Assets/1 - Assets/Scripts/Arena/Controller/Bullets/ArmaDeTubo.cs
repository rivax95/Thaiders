//                                          ▂ ▃ ▅ ▆ █ ZEN █ ▆ ▅ ▃ ▂ 
//                                        ..........<(+_+)>...........
// .cs (//)
//Autor: Alejandro Rivas                 alejandrotejemundos@hotmail.es
//Desc:
//Mod : 
//Rev :
//..............................................................................................\\
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaDeTubo : WeaponBase {

    [Header("Sounds")]
    public AudioClip ammoInsert;
  
    protected override void Reload()
    {
     

        if (isReloading) return;
        isReloading = true;
        if (bulletsInClip <= 0)
        {
            animator.CrossFadeInFixedTime("ReloadStartEmpty", 0.1f);
        }
        else
        {
            animator.CrossFadeInFixedTime("ReloadStart", 0.1f);
        }
    }
    protected override void ReloadAmmo()
    {
        bulletsLeft--;
        bulletsInClip++;
    }
    public void CheckNextReload()
    {
        bool stopInserting = false;
        if (bulletsInClip >= clipSize)
        {
            stopInserting = true;
        }
        else if(bulletsLeft<=0){

            stopInserting = false;
        }
        if (stopInserting)
        {
            animator.CrossFadeInFixedTime("ReloadEnd", 0.1f);
        }
        else
        {
            animator.CrossFadeInFixedTime("ReloadInsert", 0.1f);
        }
    }
    public void OnAmmoInserted()
    {
        audiosource.PlayOneShot(ammoInsert);
        ReloadAmmo();
    }
}
