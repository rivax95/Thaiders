//                                          ▂ ▃ ▅ ▆ █ ZEN █ ▆ ▅ ▃ ▂ 
//                                        ..........<(+_+)>...........
// MYSQLconnection.cs (8/09/2018)
//Autor: Alejandro Rivas                 alejandrotejemundos@hotmail.es
//Desc: Operaciones logicas con la base de datos
//Mod : 0.2
//Rev :ini
//..............................................................................................\\
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using MySql.Data;
using MySql.Data.MySqlClient;



public class MYSQLconnection : MonoBehaviour {
    public static MYSQLconnection script;
    public string host, database, user, password;
    public bool pooling = true;
    public List<NoticiaBase> listaNoticias = new List<NoticiaBase>();
    private string DataConection;
    public MySqlConnection con = null;
    private MySqlCommand cmd = null;
    private MySqlDataReader rdr = null;
	void Awake () {
        script = this;
        DontDestroyOnLoad(this.gameObject);
        DataConection = "Server=" + host + ";Database=" + database + ";User=" + user + ";Password=" + password + ";Pooling=";
        if (pooling)
        {
            DataConection += "true";
        }
        else
        {
            DataConection += "false";
        }
        try {
            con = new MySqlConnection(DataConection);
            Debug.Log("Conectado");
            con.Open();
                   }
        catch (System.Exception e)
        {
            Debug.Log(e);

        }
        finally
        {
            con.Close();
        }
       
        listaNoticias = mostrar();
	}
    void Start()
    {
       
    }
    public MySqlDataReader Select(string _select)
    {
        
        con.Open();
        MySqlCommand cmd = con.CreateCommand();
        cmd.CommandText = "SELECT * FROM" + _select;
        cmd.Connection = con;
        
        MySqlDataReader resul = cmd.ExecuteReader();
       
        return resul;
    }

    public void OnApplicationQuit()
    {
        if (con != null)
        {
            if (con.State.ToString() != "Closed")
            {
                con.Close();
            }
            
        }
    }
     public  List<NoticiaBase> mostrar()
    {
        
        List<NoticiaBase> lista = new List<NoticiaBase>();


        MySqlDataReader reader = MYSQLconnection.script.Select("`noticia`");
        
        NoticiaBase i = new NoticiaBase();
     
        while (reader.Read())
        {

            i.noticia = reader.GetString(0);
            i.autor = reader.GetString(1);
            i.id = reader.GetInt32(2);
            i.fecha = reader.GetInt32(3);
            i.titulo = reader.GetString(4); // NO ESTA AÑADIDA EN LA BASE DE DATOS TODOOOOOOOOOOOOO
            lista.Add(i);
        }
        reader.Close();
        foreach (NoticiaBase item in lista)
        {
            Debug.Log(item.autor);
        }
        con.Close();
           return lista;
    }

        //Escritras en BD
    #region MysqlWriting
    //Diseñar un metodo para escribir con facilidad en la BD usaremos POLIMORFISMO

     public void AceptarCuenta(int id)
     {
         con.Open();
         string log = "UPDATE `usuarios` SET `Aceptada` = '1' WHERE `usuarios`.`id` = " + id;
         MySqlCommand command = new MySqlCommand(log, con);
         if (command.ExecuteNonQuery() == 1)
         {
             Debug.Log("Cuenta Aceptada");
         }
         else
         {
             Debug.Log("Cuenda Denegada");
         }
         con.Close();

     }
     public void writingDB(string nombre,string contraseña,string nac,string apellido,int dianac,string mesnac,int añonac,string correo,bool aceptapubli,bool aceptaTerminos,int Authentification)
     {
        cmd = con.CreateCommand();

        cmd.CommandText = "INSERT INTO usuarios(nombre,contraseña,nacionalidad,apellido,dianacimiento,mesnacimiento,añonacimiento,correoelectronico,aceptapublicidad,aceptaterminos,Authentification) VALUES(?nombre,?contraseña,?nacionalidad,?apellido,?dianacimiento,?mesnacimiento,?añonacimiento,?correoelectronico,?aceptapublicidad,?aceptaterminos,?Authentification)";
         cmd.Parameters.Add("?nombre", MySqlDbType.VarChar).Value = nombre;
         cmd.Parameters.Add("?contraseña", MySqlDbType.VarChar).Value = contraseña;
         cmd.Parameters.Add("?nacionalidad", MySqlDbType.VarChar).Value = nac;
         cmd.Parameters.Add("?apellido", MySqlDbType.VarChar).Value = apellido;
         cmd.Parameters.Add("?mesnacimiento", MySqlDbType.VarChar).Value = mesnac;
         cmd.Parameters.Add("?añonacimiento", MySqlDbType.Int32).Value = añonac;
         cmd.Parameters.Add("?dianacimiento", MySqlDbType.VarChar).Value = dianac;
         cmd.Parameters.Add("?correoelectronico", MySqlDbType.VarChar).Value = correo;
         cmd.Parameters.Add("?aceptapublicidad", MySqlDbType.Bit).Value = aceptapubli;
         cmd.Parameters.Add("?aceptaterminos", MySqlDbType.Bit).Value = aceptaTerminos;
         cmd.Parameters.Add("?Authentification", MySqlDbType.Int32).Value = Authentification;
       int res =  cmd.ExecuteNonQuery();

        Debug.Log("Filas afectadas " + res);
        con.Close();
     }
    #endregion
    // Consultas
    #region MysqlReading
     public bool consultaAuth(int numeric)
     {
      


         MySqlDataReader reader = MYSQLconnection.script.Select("`usuarios`");
         int intento;
         int numero;
      
         numero = reader.GetInt32(14);
         intento = reader.GetInt32(16);
         
         reader.Close();
         con.Close();
       
         if (intento > 5)
         {
             return false;
         }
         if(numero==numeric)
         {
             cmd = con.CreateCommand();
             intento++;
             cmd.CommandText = "INSERT INTO usuarios(Aceptada) VALUES(?aceptada)";
             cmd.Parameters.Add("?Aceptada", MySqlDbType.Bit).Value = 1;
             con.Close();
             return true;
         }
         else
         {
             cmd = con.CreateCommand();
             intento++;
             cmd.CommandText = "INSERT INTO usuarios(intentos) VALUES(?intentos)";
             cmd.Parameters.Add("?nombre", MySqlDbType.Int32).Value = intento;
             con.Close();
             return false;
         }
      
        // con.Close();
         
        
     }
    #endregion
}
