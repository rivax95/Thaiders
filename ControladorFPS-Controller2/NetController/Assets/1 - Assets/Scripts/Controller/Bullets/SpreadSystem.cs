//                                          ▂ ▃ ▅ ▆ █ ZEN █ ▆ ▅ ▃ ▂ 
//                                        ..........<(+_+)>...........
// SpreadSystem.cs (26/10/18)
//Autor: Alejandro Rivas                 alejandrotejemundos@hotmail.es
//Desc: Este systema controla las penalizaciones de disparo
//Mod : 0.1
//Rev :Ini
//..............................................................................................\\
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alex.Controller;



    public class SpreadSystem : MonoBehaviour
    {

        #region VariablesPublicas
        
         SpreadSystem instancia;
        public Controller controlador;
         RecoilAffect reco;
        public float spraybase;
        #endregion

        #region VariablesPrivadas
      
    private SpreadConfiguration Config;
    [HideInInspector]
    public float penalizationCrounch;
    [HideInInspector]
    public float penalizationMoving;
    [HideInInspector]
    public float penalizationGrounded;
        #endregion

        #region Inicializadores
        private void Awake()
        {
            if (instancia == null)
            {
                instancia = this;
            }
            else
            {
                Debug.LogError("Ya existe un SpreadSystem en la escena");
            }

            reco = this.gameObject.GetComponent<RecoilAffect>();
        }
        void Start()
        {
        spraybase = WeaponManager.instance.WeaponbaseCurrent.spreat;
    }
        #endregion

        #region Actualizadores
        // Update is called once per frame
        void Update()
        {
        if (WeaponManager.instance.Switch)
        {
            spraybase = WeaponManager.instance.WeaponbaseCurrent.spreatBase;
        }
        float total =GetSpray() ;
        //Debug.Log(total + " total");
        float spray = total > 0.95f ? 0.95f : total;
        WeaponManager.instance.WeaponbaseCurrent.spreat = spray;
        }
        #endregion

        #region MetodosPrivados
        public bool IsMoving()
        {
        return controlador.is_Moving;
        }
        public bool IsGrounded()
        {
        return controlador.is_Grounded;
        }
        public bool IsCrouching()
    {
        return controlador.is_Crouching;
    }
 
    private float GetSpray()
    {
        float total = 0f;
        float move= IsMoving() ? penalizationMoving : 0f;
        float crunch = IsCrouching() ? penalizationCrounch : 0f;
        float ground= IsGrounded() ? 0f : penalizationGrounded;
        //float Recoil = Mathf.Abs(reco.spreadRecoil);
        //Debug.Log("Recoil "+Recoil);
        total = ((move + ground + spraybase) - crunch) + Mathf.Abs(reco.spreadRecoil);
        total = total > 0.98f ? 0.98f : total;
        //Debug.LogWarningFormat("Info: {0} = {1} + {2} + {3} - {4} + {5}", total, move, ground, spraybase, crunch, reco.spreadRecoil);
        //total += Recoil;
        //Debug.Log("Spray ="+total);
        return total;
    }
        #endregion

        #region MetodosPublicos
        #endregion

        #region MetodosVirtuales
#endregion

        #region Corrutinas

        #endregion
    }
