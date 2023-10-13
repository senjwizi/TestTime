using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class TimeController : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ShowMessage(string str);

    private string url = "https://worldtimeapi.org/api/timezone/Europe/Moscow";

    public Button getTimeButton;

    public class TimeData
    {
        public string datetime;
    }

    private void Start()
    {
        getTimeButton.onClick.AddListener(delegate{StartCoroutine(GetMoscowTime());});
    }

    private IEnumerator GetMoscowTime()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();
            switch (webRequest.result)
            {   
                case UnityWebRequest.Result.ConnectionError:
                    ShowMessage("Проблемы с подключением!");
                    break;
                case UnityWebRequest.Result.Success:
                    string respond = webRequest.downloadHandler.text;
                    TimeData dateTime = JsonUtility.FromJson<TimeData>(respond);
                    DateTime timeMoscow = Convert.ToDateTime(dateTime.datetime);
                    ShowMessage(timeMoscow.ToString("HH:mm"));
                    break;
            }
        }
    }
}
