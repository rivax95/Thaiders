using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : WeaponBase
{
    bool Puedoapuntar = true;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            CheckApuntar();
        }
    }
    public void CheckApuntar()
    {
        if (isReloading) { return; }
        if (!Puedoapuntar) { return; }
        apuntar();
    }
    public void apuntar()
    {

    }
    public void OnBolt()
    {
        Puedoapuntar = true;
    }
    public override void FIRE()
    {
        base.FIRE();
        Puedoapuntar = false;
    }
}
