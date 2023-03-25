using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDataController : MonoBehaviour
{
    public static string Nickname {get; set;}
    TextMeshProUGUI TextNickName;

    // Start is called before the first frame update
    void Start()
    {
        var messages = FindObjectsOfType<TextMeshProUGUI>();
        foreach (var m in messages)
        {
            if (m.text == "NickName;")
            {
                TextNickName = m;
                TextNickName.text = PlayerData.NickName;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
