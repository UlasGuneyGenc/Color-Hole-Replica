using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBehavior : MonoBehaviour
{
    
   
    private Player player;
    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.3f;
    private float dampingSpeed = 1.6f;
    private bool canShake=false;
    Vector3 initialPosition;
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>() as Player;
    }

    void OnEnable()
    {
        initialPosition = transform.localPosition;
    }
    
    void Update()
    {
        if (canShake)
        {
            if (shakeDuration > 0)
            {
                transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
                shakeDuration -= Time.deltaTime * dampingSpeed;
            }
            else
            {
                shakeDuration = 0f;
                transform.localPosition = initialPosition;
                canShake = false;
                player.RestartLevel();
            }
        }
        
    }
    
    public void TriggerShake() {
        initialPosition = transform.localPosition;
        shakeDuration = 0.3f;
        canShake = true;
    }
}
