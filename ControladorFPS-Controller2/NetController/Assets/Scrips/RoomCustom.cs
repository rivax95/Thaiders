using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[SerializeField]
[CreateAssetMenu(fileName = " Configuraciones ", menuName = " Room / Configuraciones ")]
public class RoomCustom : ScriptableObject
{
    /// <summary>
    /// Nombre de la sala
    /// </summary>
    
    public string Nombre;
    /// <summary>
    /// Maxima Cantidad de jugadores
    /// </summary>
    public byte MaxJugadores;

    /// <summary>
    /// IDENTIFICADOR UNICO
    /// </summary>
//    public short ID;

    /// <summary>
    /// Identifica si el Room esta activo;
    /// </summary>
    
    public bool roomOnly;
    /// <summary>
    /// Nombre de la escena que carga el nivel.
    /// </summary>
    public string SceneName;
    /// <summary>
    /// Cantidad de jugadores conectados a la sala
    /// </summary>
    public int actOnlyPlayers = 0;
   
}
