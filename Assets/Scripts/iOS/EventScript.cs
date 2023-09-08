
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using System.Runtime.InteropServices;

using UnityEngine.SceneManagement;


public class EventScript : MonoBehaviour {
    public string msgFromiOS="-1";
    // 定义一个数组，用于存储最后两个浮点数
    public float x = 0,y = 0;
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
        if(!line.Contains('[')){
            return;
        }

        // 按照逗号分割字符串，得到一个字符串数组
        string[] numbers = line.Split(',');

        string numx=numbers[numbers.Length - 2].Trim(' ','(', ')','[', ']',',');
        string numy=numbers[numbers.Length - 1].Trim(' ','(', ')','[', ']',',');

        float numberx=0,numbery=0;

        // Debug.Log(numx+" "+numy);
        if(float.TryParse(numx, out numberx)&&float.TryParse(numy, out numbery))
        {
            //转换成功, 输出数字
            Debug.Log ("数字是:" + numberx+" "+numbery);
            // 取出最后两个元素，转换为浮点数，并存储到数组中
            x=numberx;
            y=numbery;
        }else{
            //转换失败, 字符串不是只是数字
            Debug.Log("这个不是数字");
        }
    }

    public Vector2 ReaddXdY(){
        Vector2 res=new Vector2(0,0);
            res.x=x;
            res.y=y;
        return res;
    }

}