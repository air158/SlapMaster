
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using System.Runtime.InteropServices;

using UnityEngine.SceneManagement;


public class EventScript : MonoBehaviour {
    public string msgFromiOS="-1";
    // 定义一个数组，用于存储最后两个浮点数
    public List<float> x = new List<float>(),y = new List<float>();
    public int listSize=0,idx=0;


    // The iOS side will implement this method and receive the value
    // [DllImport("__Internal")]
    // private static extern void ReceiveUnityObjectName(string name);

    // [DllImport("__Internal")]
    // private static extern void ReceiveUnityMessage(string message);


    void Start()
    {
        Debug.Log("### begin");
        // ReceiveUnityObjectName("Text");
        
        Debug.Log(SwiftForUnity.HiFromSwift());
    }

    public void InputEvent() 
    {
    }


    // The iOS side will call this method and pass the value
    public void ReceiveiOSMessage(string msg)
    {       
        Debug.Log(msg);
        msgFromiOS=msg;
        ProcessMsg(msgFromiOS);
    }

    void ProcessMsg(string line){
        line = line.Trim('[', ']');

        // 按照逗号分割字符串，得到一个字符串数组
        string[] numbers = line.Split(',');

        string numx=numbers[numbers.Length - 2].Trim(' ','(', ')');
        string numy=numbers[numbers.Length - 1].Trim(' ','(', ')');

        // 取出最后两个元素，转换为浮点数，并存储到数组中
        x.Add(float.Parse(numx));
        y.Add(float.Parse(numy));

        listSize=x.Count;
    }

    public Vector2 ReaddXdY(){
        Vector2 res=new Vector2(0,0);
        if(listSize>0){
            res.x=x[idx]-x[0];
            res.y=y[idx]-y[0];
            idx++;
            if(idx>listSize){
                idx=listSize-1;
            }
        }
        return res;
    }

}