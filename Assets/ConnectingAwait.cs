using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

public class ConnectingAwait : MonoBehaviour
{
    public static float delayInt = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        delayInt++;
        if(delayInt > 100)
        {
            SendName_Get(PlayerData.NickName);
            if (ServerAnswers.EnemyName != "")
            {
                GetColor_Get(PlayerData.NickName);
                Thread.Sleep(2000);
                Scenes.LoadScene(2);
            }
            delayInt = 0;
        }
        
    }

    public void SendName_Get(string myName)
    {
        StartCoroutine(SendName_GetSecond(myName));
    }

    public IEnumerator SendName_GetSecond(string myName)
    {

        Debug.Log("METHOD SENDNAME ACTIVE");

        string url = "https://localhost:7098/api/Battle/EnemyName?myName=" + myName;
        string answer = "null";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        answer = www.downloadHandler.text;

        Debug.Log("ANSWER IS: " + answer);
        ServerAnswers.EnemyName = answer;

        www.Dispose();
    }

    public void GetColor_Get(string myName)
    {
        StartCoroutine(GetColor_GetSecond(myName));
    }

    public IEnumerator GetColor_GetSecond(string myName)
    {

        Debug.Log("METHOD SENDNAME ACTIVE");

        string url = "https://localhost:7098/api/Battle/MyColor?myName=" + myName;
        string answer = "null";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        answer = www.downloadHandler.text;

        Debug.Log("ANSWER IS: " + answer);
        ServerAnswers.MyColor = answer;

        www.Dispose();
    }

}


