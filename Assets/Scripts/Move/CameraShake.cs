using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 orignalPosition = transform.position;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float dx = Random.Range(-1f, 1f) * magnitude;
            float dy = Random.Range(-1f, 1f) * magnitude;
            transform.position = new Vector3(orignalPosition.x+dx, orignalPosition.y+dy, orignalPosition.z);
            elapsed += Time.deltaTime;
            yield return 0;
        }
        transform.position = orignalPosition;
    }
}
