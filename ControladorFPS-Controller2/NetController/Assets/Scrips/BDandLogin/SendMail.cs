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
using System.Net.Mail;
using System;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
public class SendMail : MonoBehaviour  {

    bool estado = true;
    string merror;
    void Start()
    {
    //   Envia("alejandroterereejemundos@hotmail.es", "furulaaaaaaaa", "aaaaaaaaaaaaa");
    }
        public  void Envia(string destinatario,string asunto, string mensaje)//object sender, EventArgs e)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient();

            mail.From = new MailAddress("proyectodragga@gmail.com", "RunicProject", System.Text.ASCIIEncoding.UTF8);
            mail.To.Add(destinatario);
            mail.Subject = asunto;
            mail.SubjectEncoding = System.Text.ASCIIEncoding.UTF8;
            mail.Body = mensaje;
            mail.BodyEncoding = System.Text.ASCIIEncoding.UTF8;

            mail.IsBodyHtml = false; // no admite HTML

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("proyectodragga@gmail.com", "Mercadat1965") as ICredentialsByHost;
     //       SmtpServer.EnableSsl = true;
            SmtpServer.Host = "smtp.gmail.com";
            SmtpServer.EnableSsl = true;

           ServicePointManager.ServerCertificateValidationCallback = delegate(object a, X509Certificate certificado, X509Chain china, SslPolicyErrors sspolicyerrors) { return true; };

            try
            {
               
                SmtpServer.Send(mail);
                Debug.Log("Enviado un email");
              
            }
            catch (Exception ex)
            {
                
                Debug.Log(ex.Message.ToString());
                Debug.Log(ex.ToString());
                merror = ex.ToString();
                estado = false;
            }
        }
        public bool Estado
        {
            get { return estado; }
        }
        public string mensaje_error
        {
            get { return merror; }
        }
    
}
