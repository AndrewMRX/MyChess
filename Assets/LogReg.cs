using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;
using TMPro;

public class LogReg : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputUser;
    [SerializeField] private TMP_InputField inputPassword;
    [SerializeField] private TextMeshProUGUI errorMessage;

    void Start()
    {
        
    }

    public void RegistryGet() // TEST FUNCTION
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("field1=foo&field2=bar"));
        UnityWebRequest www = UnityWebRequest.Get("https://localhost:7098/api/User?username=Corleone14&password=don2");
        www.SendWebRequest();
    }

    //public void RegistryPost()
    //{
    //    string answer;
    //    List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
    //    formData.Add(new MultipartFormDataSection("username=foo&password=bar"));
    //    UnityWebRequest www = UnityWebRequest.Post("https://localhost:7098/api/User?username=Corleone15&password=don2", formData);
    //    www.SendWebRequest();
    //    Task.Delay(5000).Wait();
    //    answer = www.downloadHandler.text;
    //    Debug.Log("Server Answer is: " + answer);
    //    www.Dispose();
    //}

    public bool ValidateInputs()
    {
        if(inputUser.text == "" || inputPassword.text == "")
        {
            errorMessage.text = "Fields cannot be empty.";
            return false;
        }
        return true;
    }

    public void RegistryPost()
    {
        if(ValidateInputs())
            StartCoroutine(RegistryPostSecond());
    }
    public IEnumerator RegistryPostSecond()
    {
        string uri = "https://localhost:7098/api/User/Register?username=" + inputUser.text + "&password=" + inputPassword.text;
        string answer;
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("username=" + inputUser.text + "&password=" +inputPassword.text));
        UnityWebRequest www = UnityWebRequest.Post(uri, formData);
        yield return www.SendWebRequest();

        answer = www.downloadHandler.text;
        if (answer == "fail")
        {
            errorMessage.color = Color.red;
            errorMessage.text = "Username is 'already in use!";
        }

        else if (answer == "")
        {
            errorMessage.color = Color.red;
            errorMessage.text = "Connection failed";
        }

        else if (answer == "success")
        {
            errorMessage.color = Color.green;
            errorMessage.text = "Registred! Press 'Login'";
        }

        Debug.Log("Server Answer is: " + answer);
        Debug.Log("Uri is: " + uri);
        //www.Dispose();
    }

    public void LoginPost()
    {
        if (ValidateInputs())
            StartCoroutine(LoginPostSecond());
    }
    public IEnumerator LoginPostSecond()
    {
        string uri = "https://localhost:7098/api/User/Login?username=" + inputUser.text + "&password=" + inputPassword.text;
        string answer;
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        //formData.Add(new MultipartFormDataSection("username=foo&password=bar"));
        UnityWebRequest www = UnityWebRequest.Post(uri, formData);
        yield return www.SendWebRequest();

        answer = www.downloadHandler.text;
        if (answer == "fail")
        {
            errorMessage.color = Color.red;
            errorMessage.text = "Incorrect Username or Password";
        }

        else if (answer == "")
        {
            errorMessage.color = Color.red;
            errorMessage.text = "Connection failed";
        }

        else if (answer == "success")
        {
            errorMessage.color = Color.green;
            errorMessage.text = "Logged";
        }

        Debug.Log("Server Answer is: " + answer);
        Debug.Log("Uri is: " + uri);
        //www.Dispose();
    }

    IEnumerator Upload()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("field1=foo&field2=bar"));
        formData.Add(new MultipartFormFileSection("my file data", "myfile.txt"));

        UnityWebRequest www = UnityWebRequest.Post("https://www.my-server.com/myform", formData);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }

    public IEnumerator POST()
    {
        
        var Data = new WWWForm();
        Data.AddField("variable1", "Текст 1");  // на сервере в PHP
        Data.AddField("variable2", "Текст 2");  // регистр заголовков выглядит
        Data.AddField("variable3", "Текст 3");  // примерно так :
        Data.AddField("variable4", "Текст 4");  // $temp = REQUEST('variable1');
        var Query = new WWW("http://www.example.com/game.php", Data);
        yield return Query;
        if (Query.error != null)
        {
            Debug.Log("Server does not respond : " + Query.error);
        }
        else
        {
            if (Query.text == "response") // в основном HTML код которым ответил сервер
            {
                Debug.Log("Server responded correctly");
            }
            else
            {
                Debug.Log("Server responded : " + Query.text);
            }
        }
        Query.Dispose();
    }
}
