using System;
using System.Text;
using UnityEngine;
public class SerialIdManager
{
    public static SerialIdManager Instance = new SerialIdManager();
    private int serialNumber = 0;
    private string _lastTime = "";
    public String GetSid()
    {
        string curId = System.DateTime.Now.ToString();
        if(curId == _lastTime)//相同时间
        {
            _lastTime = curId;
            serialNumber++;
            return curId + serialNumber;
        }
        else//不同时间
        {
            serialNumber = 0;
            _lastTime = curId;
            return curId + serialNumber;
        }
        Debug.LogError("错误id");
        return "";
    }
}
