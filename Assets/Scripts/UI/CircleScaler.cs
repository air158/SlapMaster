using UnityEngine;
using UnityEngine.UI;

public class CircleScaler : MonoBehaviour
{
    // The circle image component
    public Image circle;

    // The value to scale the circle by
    public float value=0,speed=0.5f,scaleFactor;

    // The minimum and maximum scale of the circle
    public float minScale = 0.1f;
    public float maxScale = 1f;

    // Update is called once per frame
    void Update()
    {
        value+=speed;
        if(value>1f||value<0f){
            speed=-speed;
            value+=speed;
        }
        // Clamp the value between 0 and 1
        value = Mathf.Clamp01(value);

        // Calculate the scale factor based on the value
        scaleFactor = Mathf.Lerp(minScale, maxScale, value);

        // Set the circle's scale to the scale factor
        circle.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
    }
}
