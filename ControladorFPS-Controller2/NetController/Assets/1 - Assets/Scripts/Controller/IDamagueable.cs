using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a basic interface with a single required
//method.
public interface IKillable
{
    void Kill();
}

//This is a generic interface where T is a placeholder
//for a data type that will be provided by the 
//implementing class.
public interface IDamageable<T,Y,N,B>
{
    void Damage(T damageTake,Y Name,N Gun,B ShootInfo);
}