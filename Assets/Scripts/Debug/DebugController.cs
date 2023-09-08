using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugController : MonoBehaviour
{
    public bool debugFlag;
    public Text debugtext,msgtxt;
    public EventScript eventScript;
    public HitControllor hitControllor;
    public GameObject hand,console;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(debugFlag){
            debugtext.text="senXY: "+hitControllor.screenXY.x+","+hitControllor.screenXY.y+"\n"+"handxy: "+hand.transform.position.x+","+hand.transform.position.y;
            msgtxt.text="iosxy: "+hitControllor.IOSdxdy.x+","+hitControllor.IOSdxdy.y+"\n"+"txtxy: "+hitControllor.TXTdxdy.x+","+hitControllor.TXTdxdy.y+"\n"+eventScript.msgFromiOS;
        }
        else{
            debugtext.text="";
            msgtxt.text="";
        }
        console.SetActive(debugFlag);
    }
}
