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
            debugtext.text="iosxy: "+hitControllor.dxdy.x+","+hitControllor.dxdy.y+"\n"+"senXY: "+hitControllor.screenXY.x+","+hitControllor.screenXY.y+"\n"+"handxy: "+hand.transform.position.x+","+hand.transform.position.y;
            msgtxt.text=eventScript.msgFromiOS;
        }
        else{
            debugtext.text="";
            msgtxt.text="";
        }
        console.SetActive(debugFlag);
    }
}
