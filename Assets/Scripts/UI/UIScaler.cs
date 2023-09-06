using UnityEngine;
using UnityEngine.UI;

public class UIScaler : MonoBehaviour
{
    // The value to scale the circle by
    public float value=0,speed=0.5f,scaleFactor;

    // The minimum and maximum scale of the circle
    public float minScale = 0f;
    public float maxScale = 1f;

    bool startFlag=false;

    void Start(){
        startFlag=false;
        init();
    }

    // Update is called once per frame
    void Update()
    {
        if(startFlag){
            value+=speed;
            
            // Calculate the scale factor based on the value
            scaleFactor = Mathf.Lerp(minScale, maxScale, value);

            // Set the circle's scale to the scale factor
            this.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);

            if(scaleFactor>=maxScale){
                startFlag=false;
                init();
            }
        }
    }

    void init(){
        this.transform.localScale = new Vector3(0, 0, 1f);
        value=0;
    }

    public void display(){
        if(startFlag==false){
            startFlag=true;
            init();
        }
        
    }
}
