using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player player;
    private Rigidbody rb;
    private float timer;
    private float length;
    private Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>() as Player;
        rb = GetComponent<Rigidbody>();
        length = GetComponent<Renderer>().bounds.size.y;
       
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!player.autoMoveStage)
            {
                timer += Time.deltaTime;
                rb.AddForce((other.gameObject.transform.position-transform.position)*23);
                if (timer > length / 2f){
                    player.enemyEaten = true;
                    Destroy(this.gameObject, 0.1f);
                } 
            }
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        timer = 0;
    }
}
