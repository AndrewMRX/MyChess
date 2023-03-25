using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Requests : MonoBehaviour
{
  
        public void SendName_Get(string myName)
        {
            StartCoroutine(SendName_GetSecond(myName));
        }

        public IEnumerator SendName_GetSecond(string myName)
        {

            Debug.Log("METHOD SENDNAME ACTIVE");

            myName = PlayerData.NickName;

            string url = "https://localhost:7098/api/Battle/AddRedyToPlay?name=" + myName;
            string answer = "null";
            UnityWebRequest www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();

            answer = www.downloadHandler.text;

            Debug.Log("ANSWER IS: " + answer);
            ServerAnswers.serverAnswerMove = answer;

            www.Dispose();
        }
}
