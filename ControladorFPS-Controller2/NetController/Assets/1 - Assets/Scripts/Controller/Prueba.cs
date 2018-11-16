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

public class Prueba : MonoBehaviour {

    public Transform _turret;
    public Transform _muzzle;
    public float bounce_damping;
    float _fireVelocity = 0f;
    int physics_steps = 0;
    Vector3 _gravity;
    LineRenderer _line;

    // Use this for initialization
    void Start()
    {
        _line = GetComponent<LineRenderer>();
        _gravity = Physics.gravity;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Vector3 last_pos = _muzzle.position;
        Vector3 velocity = _muzzle.forward * _fireVelocity;
        _line.SetVertexCount(1);
        _line.SetPosition(0, last_pos);
        int i = 1;
        while (i < physics_steps) ///<<<<< Here, how to get physics_steps?

        {
            velocity += _gravity * Time.fixedDeltaTime;
            RaycastHit hit;
            if (Physics.Linecast(last_pos, (last_pos + (velocity * Time.fixedDeltaTime)), out hit))
            {
                velocity = Vector3.Reflect(velocity * bounce_damping, hit.normal); //<<<<<< here, how to get bounce_damping?
                last_pos = hit.point;
            }
            _line.SetVertexCount(i + 1);
            _line.SetPosition(i, last_pos);
            last_pos += velocity * Time.fixedDeltaTime;
            i++;
        }
    }

}
