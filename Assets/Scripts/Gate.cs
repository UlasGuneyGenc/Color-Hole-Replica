using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public float duration; //set the duration in the inspector
    private float elapsedTime;
    private bool active = false;
 
    void Update() {
        if (elapsedTime < duration && active) {
            transform.Translate(Vector3.down*Time.deltaTime);
            elapsedTime += Time.deltaTime;
        }
    }

    public void setActive()
    {
        active = true;
    }
}
