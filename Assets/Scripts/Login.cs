using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class Login : MonoBehaviour
{

    [SerializeField]
    public TbUsuario user; //Datos de usuario recogidos de la base de datos
    private const string url = "http://localhost:5000/"; //Liga a pagina web donde esta el api
    public TMP_InputField userName; //Input del usuario de su usuario
    public TMP_InputField password; //Input del usuario de su contrasena
    public GameObject panel; //Panel que contiene el form a llenar por el usuario
    public GameObject button; //Boton para acceder al juego (Comenzar a jugar)
    public TextMeshProUGUI loginMessage; //Mensaje que indica si hubo error al hacer el login o no

    //Metodo se ejecuta una vez que el usuario de click al boton en el form
    public void RetrieveUser()
    {
        //Este objeto contiene info del usuario por eso no se destruye
        DontDestroyOnLoad(transform.gameObject);
        StartCoroutine(RetrieveUserInformation());
    }


    //Conseguir la info del usuario
    IEnumerator RetrieveUserInformation()
    {
        if (userName.text.Trim() != "")
        {
            //URL a api para conseguir un usuario en especifico
            var searchUser = url + "api/TbUsuario/" + userName.text.Trim();

            using (UnityWebRequest webRequest = UnityWebRequest.Get(searchUser))
            {
                //Esperar a que la pagina responda
                yield return webRequest.SendWebRequest();
                //Si se pudo concetar o no
                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    if (webRequest.downloadHandler.text != "")
                    {
                        //Llenar informacion del usuario
                        user = JsonUtility.FromJson<TbUsuario>(webRequest.downloadHandler.text);
                        //Si la contraseña del usuario de la base de datos y la que acaba de poner el usuario concuerdan
                        if (user.contrasena == password.text.Trim())
                        {
                            //Mensaje de exito y desactivar el form
                            loginMessage.text = "Success";
                            panel.SetActive(false);
                            button.SetActive(true);
                        }
                        else
                        {
                            //Si no concuerdan entonces hacer todo null y desplegar mensaje de error
                            loginMessage.text = "Incorrect password or username";
                            user = null;
                            password.text = null;
                            userName.text = null;
                        }

                    }
                }
                else
                {
                    //Si no se encontro el username entonces desplegar error
                    loginMessage.text = "Incorrect password or username";

                }
            }
        }
    }



    IEnumerator PostDataTest()
    {
        //Lugar donde se hace post del puntuaje
        var searchUser = url + "api/TbPuntuaje";
        WWW www;
        Hashtable postHeader = new Hashtable();
        //Especificar que lo que se manda es un json
        postHeader.Add("Content-Type", "application/json");

        // Json a enviar convertido a bytes
        var formData = System.Text.Encoding.UTF8.GetBytes("{\"idUsuario\":" + user.idUsuario.ToString() + ",\"puntuaje\":" + 101.ToString() + "}");

        //Toda informacion a enviar para hacer post
        www = new WWW(searchUser, formData, postHeader);
        StartCoroutine(WaitForRequest(www));
        return www;
    }

    IEnumerator WaitForRequest(WWW data)
    {
        //Esperar a que pagina responda
        yield return data;
        //Si fue error o si se hizo el post
        if (data.error != null)
        {
            Debug.Log("There was an error sending request: " + data.error);
        }
        else
        {
            Debug.Log("WWW Request: " + data.text);
        }
    }

    //Metodo que se llama cuando el jugador choca
    public void PostData()
    {
        StartCoroutine(PostDataTest());

    }


}
