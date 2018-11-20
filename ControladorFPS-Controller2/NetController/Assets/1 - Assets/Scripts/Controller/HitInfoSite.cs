﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitInfoSite : MonoBehaviour,IDamageable<float,string,string,int> {
    public enum Part
    {
        Cabeza,Hombro,Brazo,Mano,TroncoInferior,Pecho,Pierna,Pie
}
   
    Health SettingsHealth;
    public float value;
    public Part Site;
   
	// Use this for initialization
	void Start () {
        //GUI = Resources.Load("KillLog") as GameObject;
        SettingsHealth = transform.root.GetComponent<Health>();
        switch (Site)
        {
            case Part.Cabeza:
                value = 2;
                break;
            case Part.Hombro:
                value = 0.8f;
                break;
            case Part.Brazo:
                value = 0.5f;
                break;
            case Part.Mano:
                value = 0.3f;
                break;
            case Part.TroncoInferior:
                value = 0.9f;
                break;
            case Part.Pecho:
                value = 1;
                break;
            case Part.Pierna:
                value = 0.8f;
                break;
            case Part.Pie:
                value = 0.5f;
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Damage(float damageTaken,string Name,string gunName,int Wall)
    {
        SettingsHealth.value-= damageTaken * value;
        Debug.Log("Daño recibido= " + damageTaken * value +"TOTAAAAAAAAAAAL");

        if(SettingsHealth.value < 0){
            Debug.Log(Name + " Con " + gunName + " " + Wall + "ah " + "PlayerNames" + " en " + Site.ToString());
            CanvasManager.instance.Kill(Name, transform.root.GetComponent<PlayerInfo>().NombreJugador, gunName, Site.ToString(), Wall);
        }
    }
}

