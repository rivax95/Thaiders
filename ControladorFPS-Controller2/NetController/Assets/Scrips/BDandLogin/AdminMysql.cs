using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
public class AdminMysql : MonoBehaviour {
    [Header("Datos BD")]
    public string SVBD;
    public string NombreBD;
    public string UserBD;
    public string PassBD;

    private string DatosConexion;
    private MySqlConnection conexion;

    public static AdminMysql script;
	// Use this for initialization
	void Start () {
        script = this;
		DatosConexion = "Server="+SVBD + ";Database="+NombreBD+";Uid="+UserBD+";Pwd="+PassBD+";";
        EstablecerConecxion();
	}
	
	private void EstablecerConecxion()
    {
        conexion = new MySqlConnection(DatosConexion);
        try
        {
            conexion.Open();
            Debug.Log("Conectado a la Base De Datos");
            
        }
        catch (System.Exception error)
        {
            Debug.LogError("No se puede establecer la conecxion: " + error);
        }
    }

  
    public MySqlDataReader Select(string _select)
    {
        MySqlCommand cmd = conexion.CreateCommand();
        cmd.CommandText = "SELECT * FROM" + _select;
        MySqlDataReader resul = cmd.ExecuteReader();
        return resul;
    }
}
