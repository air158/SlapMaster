using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitControllor : MonoBehaviour
{
    public CameraShake cameraShake;
    public CircleScaler HitDet;
    public Slider HP,durability;  //实例化一个Slider
    public Text Speed,Crit;
    public float hurt=2F;
    public int speedv=0;
    public float px,py;
    public float dx,dy;
    public ReadText txt;
    public GameObject hand; //当前物体
    public GameObject targetGo; //目标物体
    public Animator animator;
    public float value,speed;
    public Vector2 screenXY;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    public bool Hit = false,Back=false,txtFlag=false;
    public double HitDis,LeaveDis;
    float mxX=-10000000.0F,mxY=-10000000.00F;
    
    int idx=0,time=0;
    void Awake(){
        initialPosition = hand.transform.position;
        initialRotation = hand.transform.rotation;
        txt=gameObject.GetComponent<ReadText>();
        HitDet=gameObject.GetComponent<CircleScaler>();
        HP.value = 1;  //Value的值介于0-1之间，且为浮点数
        Speed.resizeTextMinSize =300;
        Crit.resizeTextMinSize =300;
    }
    private void Update() {
        updataXY();
        updateRot();
        MoveHand(hand,targetGo.transform.position);
        if(txtFlag){
            changeTxt();
        }
        
    }
    void updataXY(){
        screenXY.x=(txt.y[idx]-txt.y[0])/dx+px;
        screenXY.y=(txt.x[idx]-txt.x[0])/dy+py;
        time++;
        if(time%2==0){
            idx++;
            idx%=txt.x.Count;
        }
        
    }
    void updateRot(){
         float dis = Vector2.Distance(new Vector2(hand.transform.position.x,hand.transform.position.y), new Vector2(targetGo.transform.position.x,targetGo.transform.position.y));
         hand.transform.localEulerAngles=new Vector3(hand.transform.localEulerAngles.x,180+90*dis,hand.transform.localEulerAngles.z);
    }
    void MoveHand(GameObject handGo,Vector3 targetPos){
        // autoMove(handGo,targetPos);
        handMove(handGo,targetPos);
        
        //判定是否到达目标点
        // float dis = Vector3.Distance (handGo.transform.position, targetPos);
        float dis = Vector2.Distance(new Vector2(handGo.transform.position.x,handGo.transform.position.y), new Vector2(targetPos.x,targetPos.y));
        if (dis <= HitDis) {
            Debug.Log ( "到达目标点" +Hit+initialPosition);
            mxX=screenXY.x;
            mxY=screenXY.y;
            if(!Back){
                Hit=true;
                Back=true;
                txtFlag=true;
                changeAni();
                changeHP();
            }
        }
        else if(dis >= LeaveDis){
            Hit=false;
            Back=false;
        }
    }
    Vector3 CalculatePosition(Vector2 screenPos, Vector3 target)
    {
        if(mxX>screenXY.x)screenXY.x=mxX;
        if(mxY>screenXY.y)screenXY.y=mxY;
        Vector3 worldPos = new Vector3(screenPos.x, screenPos.y,0)+initialPosition;
        // Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Camera.main.nearClipPlane));
        float distance = Vector2.Distance(new Vector2(worldPos.x,worldPos.y), new Vector2(target.x,target.y));
        Vector3 direction = (worldPos - target).normalized;
        return target + direction * distance;
    }
    void handMove(GameObject handGo,Vector3 targetPos){
        float step = speed * Time.deltaTime;
        // 使用worldPoint作为目标物体的位置
        handGo.transform.position = Vector3.MoveTowards(handGo.transform.position, CalculatePosition(screenXY,targetPos), step);
    }

    void autoMove(GameObject handGo,Vector3 targetPos){
        
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenXY.x, screenXY.y, Camera.main.nearClipPlane));
        //计算中心点
        Vector3 center = (handGo.transform.position + targetPos) / 2;
        center -= new Vector3 ( 0, value, 0 );
        Vector3 start = handGo.transform.position - center;
        Vector3 end = targetPos - center;
        //弧形插值
        handGo.transform.position = Vector3.Slerp (start, end, Time.deltaTime * speed );
        handGo.transform.position += center;
        // handGo.transform.position=new Vector3(worldPos.x,worldPos.y,handGo.transform.position.z);
    }
    void changeAni(){
        if(Hit){
            animator.SetTrigger("Hit");
        }
    }
    void changeHP(){
        float value=HitDet.scaleFactor;
        if(value>=0.4&&value<=0.8){
            hurt=10;
            StartCoroutine(cameraShake.Shake(0.15f, 0.1f));
        }
        else{
            hurt=2;
            StartCoroutine(cameraShake.Shake(0.1f, 0.05f));
        } 
        hurt+=Random.Range(0f, 1f);
        speedv=Random.Range(1, 12);
        durability.value = durability.value - hurt*0.1f; 
        if(durability.value<=0.1f){
            if(hurt==10)
                HP.value = HP.value - 1.5f*0.1f; 
            else
                HP.value = HP.value - 0.1f;

            durability.value=1f;
        }
    }
    float t = 0;//每帧增加的插值
    void changeTxt(){
        t += Time.deltaTime*0.9f;
        //修改正方体在x轴上面的位移
        int scale = (int)Mathf.Lerp(300, 10, t);
        Speed.resizeTextMinSize = scale;
        Crit.resizeTextMinSize = scale;
        Crit.text = "Crit! "+(int)hurt*99;
        Speed.text = "SPEED: "+speedv+"km/h";
        if(scale<=10){
            txtFlag=false;
            t=0;
            Speed.resizeTextMinSize = 300;
            Crit.resizeTextMinSize = 300;
        }
    }
}
