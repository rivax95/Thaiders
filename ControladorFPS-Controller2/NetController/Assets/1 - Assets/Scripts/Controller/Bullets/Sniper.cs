using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alex.MouseLook;
public class Sniper : WeaponBase
{
   public bool Puedoapuntar = true;
    int mirageState = 0;
    public GameObject PointOfView;
    public Camera CamaraSnip;
    public Camera Guncamera;
    public MauseLook[] Sensi;

    protected override void CheckApuntar()
{
        if (isReloading) { return; }
        if (!Puedoapuntar) { return; }
        if (mirageState == 2) mirageState = 0;
        else { mirageState++; }
        apuntar();
        
    }
    void ChangeSensi(float valuex,float valuey)
    {
        foreach (var item in Sensi)
        {
            item.sensivity_X = valuex;
            item.sensivity_Y= valuey;
        }
    }
    public void apuntar()
    {
        switch (mirageState)
        {
            case 0:
                PointOfView.SetActive(false);
                Guncamera.enabled = true;
                CamaraSnip.fieldOfView = 60;
                ChangeSensi(Sensi[0].StandarSensi_x, Sensi[0].StandarSensi_y);
                Pointing = false;
                
                break;
            case 1:
                PointOfView.SetActive(true);
                Guncamera.enabled = false;
                CamaraSnip.fieldOfView = 30;
                ChangeSensi(1, 1);
                Pointing = true;
                break;
            case 2:
                PointOfView.SetActive(true);
                Guncamera.enabled = false;
                CamaraSnip.fieldOfView = 10;
                ChangeSensi(0.2f, 0.2f);
                Pointing = true;
                break;
 
        }
        AudioClickMira();
    }
    public void OnBolt()
    {
        audiosource.PlayOneShot(boltSound);
     
      
    }
   
    public void OnPump()
    {
       
        Puedoapuntar = true;
      
        isReloading = false;
    }
    //public override void FIRE()
    //{
    //    base.FIRE();
    //    Puedoapuntar = false;

    //}
    public override void OnSwich()
    {
        base.OnSwich();
        PointOfView.SetActive(false);
        Guncamera.enabled = true;
        Puedoapuntar = true;
        mirageState = 0;
        CamaraSnip.fieldOfView = 60;
        ChangeSensi(Sensi[0].StandarSensi_x, Sensi[0].StandarSensi_y);
        Pointing = false;
    }
    //NetworkMethod
    public void AudioClickMira()
    {

    }
    public void OnFire()
    {
        Puedoapuntar = false;
        Pointing = false;
        PointOfView.SetActive(false);
        Guncamera.enabled = true;
        CamaraSnip.fieldOfView = 60;
        ChangeSensi(Sensi[0].StandarSensi_x, Sensi[0].StandarSensi_y);
        mirageState = 0;
    }
    public override void PlayFireAnimation()
    {
        //base.PlayFireAnimation();
        if (bulletsInClip > 1)
        {
            animator.CrossFadeInFixedTime("Fire", 0.1f);
        }
        else
        {
            animator.CrossFadeInFixedTime("FireLast", 0.1f);
        }
    }
   
}
