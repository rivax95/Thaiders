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
using Alex.Controller;
public class PhysicsBullets : MonoBehaviour
{

    
 
    public bool calcula;
    public Vector3 currentPositioon = Vector3.zero;
    public Vector3 newPositionn = Vector3.zero;
    public Vector3 newVelocity = Vector3.zero;
    public Vector3 currentVelocityy = Vector3.zero;
    float h = 0;
    //Calculate the bullet's drag's acceleration 
    public void FixedUpdate()
    {
        if (!calcula) return;
        h = Time.fixedDeltaTime;

        Heuns(h, currentPositioon, currentVelocityy, out newPositionn, out newVelocity);
        calcula = false;

    }
    public static Vector3 CalculateDrag(Vector3 velocityVec)
    {
        //F_drag = k * v^2 = m * a 
        //k = 0.5 * C_d * rho * A  

        float m = 0.2f; // kg //peso bala 
        float C_d = 0.5f; // Coeficiente aerodinámico de resistencia. 
        float A = Mathf.PI * 0.05f * 0.05f; // m^2 Velocidad Inicial 
        float rho = 1.225f; // kg/m3 ///http://www.ambrsoft.com/calcphysics/density/table_2.htm tipo de densidad de materiales 

        float k = 0.5f * C_d * rho * A;

        float vSqr = velocityVec.sqrMagnitude;

        float aDrag = (k * vSqr) / m;

        //Has to be in a direction opposite of the bullet's velocity vector 
        Vector3 dragVec = aDrag * velocityVec * -1f;

        return dragVec;
    }
    public static void Heuns(
    float h,
    Vector3 currentPosition,
    Vector3 currentVelocity,
    out Vector3 newPosition,
    out Vector3 newVelocity)
    {
        //Init acceleration 
        //Gravity 
        Vector3 acceleartionFactorEuler = Physics.gravity;
        Vector3 acceleartionFactorHeun = Physics.gravity;


        //Init velocity 
        //Current velocity 
        Vector3 velocityFactor = currentVelocity;
        //Wind velocity 
        //velocityFactor += new Vector3(2f, 0f, 3f); 


        // 
        //Main algorithm 
        // 
        //Euler forward 
        Vector3 pos_E = currentPosition + h * velocityFactor;

        acceleartionFactorEuler += CalculateDrag(currentVelocity);

        Vector3 vel_E = currentVelocity + h * acceleartionFactorEuler;
        Debug.Log(Physics.gravity);

        //Heuns method 
        Vector3 pos_H = currentPosition + h * 0.5f * (velocityFactor + vel_E);

        acceleartionFactorHeun += CalculateDrag(vel_E);

        Vector3 vel_H = currentVelocity + h * 0.5f * (acceleartionFactorEuler + acceleartionFactorHeun);


        newPosition = pos_H;
        newVelocity = vel_H;
    }

}
