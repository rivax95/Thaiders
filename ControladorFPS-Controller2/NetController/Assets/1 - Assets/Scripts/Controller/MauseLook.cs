//                                          ▂ ▃ ▅ ▆ █ ARC █ ▆ ▅ ▃ ▂ 
//                                        ..........<(+_+)>...........
// MauseLook.cs (01/10/18)
//Autor: Alejandro Rivas                 alejandrotejemundos@hotmail.es
//Desc: Mause Look Controller
//Mod : 0.1
//Rev :ini
//..............................................................................................\\
#region Librerias
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
namespace Alex.MouseLook
{
    public class MauseLook : MonoBehaviour
    {
        #region variablesPublicas
        public enum RotationAxes
        {
            MouseX, MoueseY
        }
        public RotationAxes axes = RotationAxes.MoueseY;
        #endregion
        #region VariablesPublicas
        private float currentSensivity_X = 1.5f;
        private float currentSensivity_Y = 1.5f;
        private float sensivity_X = 1.5f;
        private float sensivity_Y = 1.5f;
        public float rotation_X, rotation_Y;
       
        private float minimum_X = -360f;
        private float maximun_X = 360F;

        private float minimum_Y = -80f;
        private float maximun_Y = 80F;

        private float recoil=0f;

        public Quaternion originalRotation;
        public Quaternion xquuat;
        private float mouseSensivity = 1.7f;
        #endregion
        #region inicializadores
        void Start()
        {
            originalRotation = transform.rotation;
        }
        #endregion
        #region Actualizadores
        void FixedUpdate()
        {

        }
        void Update()
        {
            HandleRotation();
        }

        #endregion
        #region MetodosPrivados
        /// <summary>
        /// Sirve para clampear en los 360grados
        /// </summary>
        float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360)
            {
                angle += 360f;
            }
            if (angle > 350f)
            {
                angle -= 360f;
            }
            return Mathf.Clamp(angle, min, max);
        }
        /// <summary>
        /// Controla la rotacion de la camra
        /// </summary>
        void HandleRotation()
        {
            if (currentSensivity_X != mouseSensivity || currentSensivity_Y != mouseSensivity)
            {
                currentSensivity_X = currentSensivity_Y = mouseSensivity;
            }
            sensivity_X = currentSensivity_X;
            sensivity_Y = currentSensivity_Y;
            if (axes == RotationAxes.MouseX)
            {
                rotation_X += Input.GetAxis("Mouse X") * sensivity_X;
                rotation_X = ClampAngle(rotation_X, minimum_X, maximun_X);
         
               
                 xquuat = Quaternion.AngleAxis(rotation_X, Vector3.up);
                transform.localRotation = (originalRotation * xquuat);
                //Quaternion reco = Quaternion.Euler(-recoil, 0f, 0f);
                //transform.localRotation *= reco;
                //if (recoil > 0f)
                //{
                //    recoil -= Time.deltaTime*4;
                //}
                //else
                //{
                //    recoil = 0f;
                //}
            }
            if (axes == RotationAxes.MoueseY)
            {
                rotation_Y += Input.GetAxis("Mouse Y") * -sensivity_Y;
                rotation_Y = ClampAngle(rotation_Y, minimum_Y, maximun_Y);
                Quaternion xquuat = Quaternion.AngleAxis(rotation_Y, Vector3.right);

                transform.localRotation = originalRotation * xquuat;
            }
         
        }
        #endregion
        public void Recoil(float amount)
        {
            recoil += amount;
        }
    }
}