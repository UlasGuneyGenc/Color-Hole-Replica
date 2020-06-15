using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Player player;
    private Rigidbody rb;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
         player = GameObject.Find("Player").GetComponent<Player>() as Player;
         rb = GetComponent<Rigidbody>();

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timer += Time.deltaTime;
            rb.AddForce((other.gameObject.transform.position-transform.position)*23);
            if (timer > 0.3f)
            {
                Destroy(this.gameObject,0.1f);
            }
                
        }
       
    }
    
    private void OnTriggerExit(Collider other)
    {
        timer = 0;
    }

    private void OnDestroy()
    {
        player.increaseTargetCount();
    }
}
