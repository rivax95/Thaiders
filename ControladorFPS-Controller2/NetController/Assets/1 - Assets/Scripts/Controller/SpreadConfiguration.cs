using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SpreadData", menuName = "Weapon/Spread", order = 1)]
public class SpreadConfiguration : ScriptableObject
{
   
    [Range (0f,0.1f)]
    public float PenalizationCrounch;
    [Range(0f, 0.1f)]
    public float PenalizationMoving;
    [Range(0f, 0.1f)]
    public float PenalizationGrounded;
    
    [Range(0f,1f)]
    public float maxRecoilSpray;
    [Range(0.01f, 0.99f)]
    public float Enfriamiento;
    [Range(0.0f, 0.09f)]
    public float SprayShoot;
    [Range(0.3f, 0.89f)]
    public float SprayBase;

}
 