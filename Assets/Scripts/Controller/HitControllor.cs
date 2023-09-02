using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitControllor : MonoBehaviour
{
    public bool useiOS=true;
    public EventScript ios;
    public CameraShake cameraShake;
    public MusicController musicController;
    // public CircleScaler HitDet;
    public Slider HP,durability;  //实例化一个Slider
    public Slider Power;
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
    public bool Hit = false,Back=false,txtFlag=false;
    public double HitDis,LeaveDis;
    

    GameObject HitGo;

    float mxX=-10000000.0F,mxY=-10000000.00F;
    float HPValue=1f,durabilityValue=1f;
    string SpeedS,CritS;
    Vector3 initialPosition;
    Quaternion initialRotation;
    int idx=0,time=0;
    float Thp=0,Tdu=0,Tpow=0,Ttxt = 0;//每帧增加的插值

    void Awake(){
        initialPosition = hand.transform.position;
        initialRotation = hand.transform.rotation;

        txt=gameObject.GetComponent<ReadText>();
        // HitDet=gameObject.GetComponent<CircleScaler>();
        ios=gameObject.GetComponent<EventScript>();
        musicController=gameObject.GetComponent<MusicController>();
        
        HP.value = 1;  //Value的值介于0-1之间，且为浮点数
        Speed.resizeTextMinSize =300;
        Crit.resizeTextMinSize =300;
    }
    private void Update() {
        updataXY();
        updateRot();

        updataPower();

        MoveHand(hand,targetGo.transform.position);

        updateSpeed();

        updateHP();

        if(txtFlag){
            changeTxt();
        }
        updateTxt();
    }
    
    void updateHP(){
        Thp += Time.deltaTime*0.5f;
        float hp = Mathf.Lerp(HP.value, HPValue, Thp);
        HP.value = hp;
        if(hp<=HPValue)Thp=0;

        Tdu += Time.deltaTime*0.5f;
        float du = Mathf.Lerp(durability.value, durabilityValue, Tdu);
        durability.value = du;
        if(du<=durabilityValue)Tdu=0;
    }
    int dir=1;
    void updataPower(){
        Tpow += Time.deltaTime*0.5f;
        if(dir==1){
            float pow = Mathf.Lerp(0, 1, Tpow);
            Power.value = pow;
            if(pow>=1){
                Tpow=0;
                dir=0;
            }
        }
        else{
            float pow = Mathf.Lerp(1, 0, Tpow);
            Power.value = pow;
            if(pow<=0){
                Tpow=0;
                dir=1;
            }
        }
        
    }
    Vector3 curpos,lastpos;
    public float _speed;
    void updateSpeed(){
        curpos = hand.transform.position;//当前点
		_speed = (Vector3.Magnitude(curpos - lastpos) / Time.deltaTime);//与上一个点做计算除去当前帧花的时间。
		lastpos = curpos;//把当前点保存下一次用
    }
    void updateTxt(){
        if(txtFlag==true){
            Ttxt += Time.deltaTime*0.9f;
        }
        //修改正方体在x轴上面的位移
        int scale = (int)Mathf.Lerp(300, 10, Ttxt);
        Speed.resizeTextMinSize = scale;
        Crit.resizeTextMinSize = scale;
        Crit.text = CritS;
        Speed.text = SpeedS;
        if(scale<=10){
            txtFlag=false;
            Ttxt=0;
            Speed.resizeTextMinSize = 300;
            Crit.resizeTextMinSize = 300;
        }
    }
    void updateRot(){
         float dis = Vector2.Distance(new Vector2(hand.transform.position.x,hand.transform.position.y), new Vector2(targetGo.transform.position.x,targetGo.transform.position.y));
         hand.transform.localEulerAngles=new Vector3(hand.transform.localEulerAngles.x,180+90*dis,hand.transform.localEulerAngles.z);
    }
    void updataXY(){
        
        if(useiOS){
            //ReadFromiOS
            dx=0f;
            Vector2 dxdy=ios.ReaddXdY();
            screenXY.x=(dxdy.y)/dx+px;
            screenXY.y=(dxdy.x)/dy+py;
        }
        else{
            //ReadFromTxt
            px=-0.7f;
            screenXY.x=(txt.y[idx]-txt.y[0])/dx+px;
            screenXY.y=(txt.x[idx]-txt.x[0])/dy+py;
        }

        time++;
        if(time%2==0){
            idx++;
            idx%=txt.x.Count;
        }
    }

    void MoveHand(GameObject handGo,Vector3 targetPos){
        // autoMove(handGo,targetPos);
        handMove(handGo,targetPos);
        
        //判定是否到达目标点
        float dis = Vector2.Distance(new Vector2(handGo.transform.position.x,handGo.transform.position.y), new Vector2(targetPos.x,targetPos.y));
        if (dis <= HitDis) {
            // mxX=screenXY.x;
            // mxY=screenXY.y;
            if(!Back){
                Hit=true;
                Back=true;
                txtFlag=true;

                changeHurt();

                musicController.changeHitMusic((int)hurt);
                changeCameraShake();
                changeHitGo();
                changeAni();
                changeSpeed();
                changeHP();
            }
        }
        else if(dis >= LeaveDis){
            Hit=false;
            Back=false;
        }
    }
    void changeHurt(){
        float value=Power.value;
        if(value>=0.6&&value<=0.85){
            if(value>=0.68&&value<=0.78){
                hurt=10;
            }
            else{
                hurt=6;
            }
        }
        else{
            hurt=2;
        } 
    }
    
    void changeCameraShake(){
        if(hurt==10){
            StartCoroutine(cameraShake.Shake(0.15f, 0.15f));
        }
        else if(hurt==6){
            StartCoroutine(cameraShake.Shake(0.1f, 0.1f));
        }
        else{
            StartCoroutine(cameraShake.Shake(0.1f, 0.05f));
        }
    }
    void changeHitGo(){
        GameObject.Destroy(HitGo);
        if(hurt==10){
            HitGo = Resources.Load("Perfabs/Hit10") as GameObject;
        }
        else if(hurt==6){
            HitGo = Resources.Load("Perfabs/Hit6") as GameObject;
        }
        else{
            HitGo = Resources.Load("Perfabs/Hit2") as GameObject;
        }
        HitGo=GameObject.Instantiate(HitGo);
    }
    
    void changeSpeed(){
        speedv=(int)_speed;
        // speedv=Random.Range(1, 12);
    }
    void changeTxt(){
        CritS = "Crit! "+(int)hurt*99;
        SpeedS = "SPEED: "+speedv+"km/h";
    }
    void changeAni(){
        if(Hit){
            animator.SetTrigger("Hit");
        }
    }
    void changeHP(){
        float hurtvalue=(hurt+speed)*Random.Range(0.9f, 1.1f);
        durabilityValue = durabilityValue - hurtvalue*0.1f; 
        if(durability.value<=0.1f||hurt==10){
            if(hurt==10)
                HPValue = HPValue - 1.5f*0.1f; 
            else
                HPValue = HPValue - 0.1f;
            durabilityValue=1f;
        }
    }


    

    
    Vector3 CalculatePosition(Vector2 screenPos, Vector3 target)
    {
        if(mxX>screenXY.x)screenXY.x=mxX;
        if(mxY>screenXY.y)screenXY.y=mxY;
        Vector3 worldPos = new Vector3(screenPos.x, screenPos.y,0)+initialPosition;
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
    }
}
