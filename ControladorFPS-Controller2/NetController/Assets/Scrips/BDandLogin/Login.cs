//                                          ▂ ▃ ▅ ▆ █ ZEN █ ▆ ▅ ▃ ▂ 
//                                        ..........<(+_+)>...........
// Loginn.cs (8/09/2018)
//Autor: Alejandro Rivas                 alejandrotejemundos@hotmail.es
//Desc: Operaciones del Display y operaciones del registro (en futuras revisiones ira una cosa por cada lado)
//Mod : 0.2
//Rev : Implementado registro
//..............................................................................................\\
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
public class Login : MonoBehaviour
{
   
    //Variables publicas
    #region PublicVar
    public string urlRegistro;
    public string urlLogin;
    public InputField nombre;   //LOGIN
    public InputField contraseña; //LOGIN
    public int MaxNoticesDisplay;
    [HideInInspector]
    public GameObject prefabNotice;
    public GameObject[] NoticiasContenedor;
    public Animator canvasAnim;
    public Button notButton;
    public InputField[] InputRegistro;
     

    #region RegistroVar 
    //UI
    public TextMeshProUGUI MyTEXT;
    public InputField Nick;
    public InputField NombreRE;
    public InputField Apellido;
    public InputField Dia;
    public InputField año;
    public InputField correo;
    public InputField PasswordRE;
    public Toggle terminos;
    public Toggle publicidad;
    public Dropdown pais;
    public Dropdown mes;
    public InputField Authentification;
    //Buttons
    public Button terminosreader;
    public Button cancelar;
    public Button aceptar;
    public Button authAceptar;
    //Alamcenamiento de variabler
    [HideInInspector]
    public string sNombreRE;
    [HideInInspector]
    public string sApellido;
    [HideInInspector]
    public string sDia;
    [HideInInspector]
    public string saño;
    [HideInInspector]
    public string sCorreo;
    [HideInInspector]
    public string sPasswordRE;
    [HideInInspector]
    public bool bTerminos;
    [HideInInspector]
    public bool bPublicidad;
    [HideInInspector]
    public string sPais;
    [HideInInspector]
    public string sMes;
    [HideInInspector]
    public string sNick;
    #endregion

    #endregion
    //Variables Privadas
    #region PrivateVar
    private bool ButtonNoticeState=true;
#endregion
    //Metodos publicos
    #region PublicMethods
         //Botones
         #region Buttons
    public void RegistroBoton()
    {
        if (!GameObject.Find("MainCanvas").transform.Find("PanelRegistro").gameObject.activeSelf)
        {
            GameObject.Find("MainCanvas").transform.Find("PanelRegistro").gameObject.SetActive(true);
        }
        else
        {
            GameObject.Find("MainCanvas").transform.Find("PanelRegistro").gameObject.SetActive(false);
        }
    }
    public void AceptarRegistro()
    {
        StartCoroutine(registroPHP());
     //   Debug.Log("clico aceptar");
        
    }
    public void CancelarRegistro()
    {
        limpiarDisplay();
        GameObject.Find("MainCanvas").transform.Find("PanelRegistro").gameObject.SetActive(false);
    }
    public void botonAuthe(int authe)
    {
        int numImput = Int32.Parse(Authentification.text);
        Debug.Log("Numero es" + numImput);
        if (authe == numImput)
        {
            // authetificacion aceptada
            GameObject.Find("MainCanvas").transform.Find("PanelAuthetification").gameObject.SetActive(false);
            string _log = "`usuarios` WHERE `Nombre` LIKE '" + nombre.text +  "'";
            int id=-1;
        
          
            //TODOO CARGAR LOBBY
            //TODOO aceptar cuenta en bd cambiando el valor de aceptar a true
        }
        else
        {
            // sumar un intento
            // si supera x intentos la cuenta queda bloqueada
        }
    }
    public void Logear()
    {
        StartCoroutine(LoginPHP());
    }
    public void VisualEffect()
    {

    }
    #endregion
          //PanelDeNoticias
         #region PanelDeNoticias
    public void LoadNotices(List<NoticiaBase>ListNotices){
         if (MaxNoticesDisplay <= 0)
         {
             //OcultarPanel();
             return;
         }
         else
         {
             //MostrarPanel();
             //Pullear Objetos
             for (int i = 0; i < MaxNoticesDisplay; i++)
             {
                 if (i == ListNotices.Count)
                 {
                     return;
                 }
                 NoticiasContenedor[i].SetActive(true);

                 NoticiasContenedor[i].transform.Find("TitleText").GetComponent<Text>().text = ListNotices[i].titulo.ToString();
                 NoticiasContenedor[i].transform.Find("NoticiaText").GetComponent<Text>().text = ListNotices[i].noticia.ToString();
                 NoticiasContenedor[i].transform.Find("Autor").GetComponent<Text>().text = ListNotices[i].autor.ToString();
                 NoticiasContenedor[i].transform.Find("DateTime").GetComponent<Text>().text = ListNotices[i].fecha.ToString();
             }
           
         }
     }
  
    #endregion


    #endregion
    public void Update()
    {
        aceptar.interactable = (NombreRE.text.Length > 3 && Apellido.text.Length > 3 && Dia.text.Length >= 1 && año.text.Length > 1 && correo.text.Length > 5&& PasswordRE.text.Length > 5 && Nick.text.Length > 3 && terminos.isOn);
    }
    //Metodos Privados
    #region PrivateMethods
    public void OcultarPanel()
    {
        ButtonNoticeState = false;
        canvasAnim.Play("MinimiceNotices"); //todoo comprobar strings
        StartCoroutine(ButtonChangeState());
    }
    private void MostrarPanel()
    {
        ButtonNoticeState = true;
        canvasAnim.Play("MaximizarNoticia");
        StartCoroutine(ButtonChangeState());
    }
#endregion

    #region MethodIni
    void Awake()
     {
         prefabNotice = Resources.Load("PrefabNotice") as GameObject;
     }
    void Start () {
        //LoadNotices(MYSQLconnection.script.listaNoticias);
     
       // Debug.Log(SystemaDeEnvio.mensaje_error);
	}
    #endregion

    #region corrutines
    IEnumerator ButtonChangeState()
    {
        //TODOO AUDPLAY
        //quitar todas las sobcripciones
        notButton.onClick.RemoveAllListeners();
        yield return new WaitForSeconds(0.7f);
        //Si esta cerrado abrir y si esta abierto cerrar
        if (ButtonNoticeState)
        {
            Debug.Log("Cierra");
           notButton.onClick.AddListener(OcultarPanel);
         
        }
        else
        {
            Debug.Log("Abre");
            notButton.onClick.AddListener(MostrarPanel);
           
        }
    }
    IEnumerator registroPHP()
    {
        recojerInformacion();
       
        WWWForm formulario = new WWWForm();
        formulario.AddField("Nombre",sNombreRE);
        formulario.AddField("Apellidos",sApellido);
        formulario.AddField("Nacimiento",sDia+"/"+sMes+"/"+saño);
        formulario.AddField("Nacionalidad", sPais);
        formulario.AddField("Nick",sNick);
        formulario.AddField("Email",sCorreo);
        formulario.AddField("Password",sPasswordRE);
        formulario.AddField("Ban",0);
        WWW www = new WWW(urlRegistro,formulario);
        yield return www;
        if (www.text == "0")
        {
            MyTEXT.text = "Cuenta creada con exito";
            
        }
        else
        {
            MyTEXT.text = www.text;
        }
    }
    IEnumerator LoginPHP()
    {
     
        WWWForm formulario = new WWWForm();

        formulario.AddField("Email", nombre.text);
        formulario.AddField("Password", contraseña.text);

        WWW www = new WWW(urlLogin, formulario);
        yield return www;
        if (www.text[2] != null)
        {
            if (www.text[2]=='k')
            {
                DBManager.Email = nombre.text;
                DBManager.Friends = new List<string>(www.text.Split(',')); ;
                DBManager.Friends.RemoveAt(0);
                Debug.Log(DBManager.Friends.Count + DBManager.Friends[0]);
                if (DBManager.LoggedIn)
                {
                    Debug.Log("sucesfull");
                }
            }
            else
            {
                Debug.Log("User login failed. Error#" + www.text);
            }
        }
        else
        {
            Debug.Log("Problema desconocido");
        }
    }
    #endregion



    //Registro
    #region UserRegistrer
    //Simple metodo para registrar usuarios Utilizando el metodo de escritura.
    public void recojerInformacion()
    {
        sNombreRE = NombreRE.text;
        sApellido = Apellido.text;
        sDia = Dia.text;
        saño = año.text;
        sCorreo = correo.text;
        sPasswordRE = PasswordRE.text;
        bTerminos = terminos.isOn;
        bPublicidad = publicidad.isOn;
        sPais = pais.options[pais.value].text;
        sMes = mes.options[mes.value].text;
        sNick = Nick.text;
    }
    public void limpiarDisplay()
    {
        NombreRE.text = "";
        Apellido.text = "";
        Dia.text = "";
        año.text = "";
        correo.text = "";
        PasswordRE.text = "";
        terminos.isOn = false;
        publicidad.isOn = true;
        pais.value = 0;
        mes.value = 0;
    }
    
    #endregion
    //Comprobaciones
#region ComprobationsMethods
    // Metodos necesarios para hacer comprobaciones
  public void Comprobation() //TODOO Falta meterle caña a esto y encriptar password en registro
  {
     
      SendMail mail = new SendMail();
    
      int random = UnityEngine.Random.Range(0, 99999);
      mail.Envia(sCorreo, "Athetification ZEN", "Authenthification Numeric:"+random+" .");
     Debug.Log("correo es: "+ sCorreo);
      
      if (!correoComp(mail))
      {
          // Activar mensajeDeError
        //  return;
      }
      Debug.Log("llego");
      try
      {
        
      }
      catch(Exception ex)
      {
          Debug.Log(ex.Message);
            
      }

  }
  
  public bool correoComp(SendMail mailsender)
  {
      bool estado=true;
    
    
      if (mailsender.Estado)
      {
          return estado;
      }
      else
      {
          estado = mailsender.Estado;
          return estado;
      }
  }
  
#endregion
}
