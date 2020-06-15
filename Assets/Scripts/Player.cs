using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool shouldMove = true;
    private bool firstStage = true;
    private bool secondStage = false;
    public bool autoMoveStage = false;
    private bool finished = false;
    private int deadTarget1,deadTarget2;
    [SerializeField] private Camera _camera;
    private int numberOfTarget1;
    private int numberOfTarget2;
    private ShakeBehavior shake;
    public Text text;
    public bool enemyEaten=false;
    
    [SerializeField] private Transform pullCenter;
    [SerializeField] private Slider slider1;
    [SerializeField] private Slider slider2;
    [SerializeField] private Gate gate;
    [SerializeField] private ParticleSystem particle;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] targets1 = GameObject.FindGameObjectsWithTag("Target1");
        GameObject[] targets2 = GameObject.FindGameObjectsWithTag("Target2");
        numberOfTarget1 = targets1.Length;
        numberOfTarget2 = targets2.Length;
        shake = GameObject.Find("Main Camera").GetComponent<ShakeBehavior>() as ShakeBehavior;
       
    }

    // Update is called once per frame
    void Update()
    {
        MoveWithMouse();

        if (deadTarget1==numberOfTarget1)
        {
            gate.setActive();
            autoMoveStage = true;
            autoMove();
        }


        if (deadTarget2==numberOfTarget2 && secondStage)
        {
            text.gameObject.SetActive(true);
            particle.Play();
            secondStage = false;
            finished = true;
            
        }

        if (enemyEaten)
        {
            shake.TriggerShake();
            enemyEaten = false;
            
        }
        
    }
    
    private void MoveWithMouse()
    {
        if (Input.GetMouseButton(0) && shouldMove && autoMoveStage==false && finished==false)
        {
            float xPos = Input.GetAxis("Mouse X");
 
            float yPos = Input.GetAxis("Mouse Y");
  
            
           
            Vector3 dir = new Vector3(xPos, 0, yPos);
            if (xPos<6 && yPos<6 && xPos>-6 && yPos>-6)
            {
                transform.Translate(dir*speed*Time.deltaTime);
            }
            
            
            Vector3 clampedPosition = transform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, -3.56f, 3.9f);

            
            if (firstStage)
            {
                clampedPosition.z = Mathf.Clamp(clampedPosition.z, -3.05f, 10.9f);
            }
            else if(secondStage)
            {
                clampedPosition.z = Mathf.Clamp(clampedPosition.z, 44.5f, 58.3f);
            }
            transform.position = clampedPosition;

        }
        
        
    }

    private void autoMove()
    {
        transform.position = Vector3.MoveTowards(transform.position,new Vector3(0, transform.position.y, transform.position.z), Time.deltaTime * 3);
        if (transform.position==new Vector3(0, transform.position.y, transform.position.z))
        {
            transform.position = Vector3.MoveTowards(transform.position,new Vector3(transform.position.x, transform.position.y, 44.34f), Time.deltaTime * 7);
            _camera.transform.position = Vector3.MoveTowards(_camera.transform.position,new Vector3(_camera.transform.position.x, _camera.transform.position.y, 40), Time.deltaTime * 7);
        }

        if (transform.position == new Vector3(transform.position.x, transform.position.y, 44.34f))
        {
            _camera.transform.position = Vector3.MoveTowards(_camera.transform.position,new Vector3(_camera.transform.position.x, _camera.transform.position.y, 40), Time.deltaTime * 7);
            firstStage = false;
            autoMoveStage = false;
            secondStage = true;
            if (_camera.transform.position==new Vector3(_camera.transform.position.x, _camera.transform.position.y, 40))
            {
                deadTarget1 = 0;
            }
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.CompareTag("midDummy"))
        {
          Destroy(other.gameObject);
           
        }
    }

    public void increaseTargetCount()
    {
        
        if (firstStage)
        {
            slider1.value = (float)deadTarget1 / (float)numberOfTarget1;
            deadTarget1++;
        }
        else
        {
            slider2.value = (float)deadTarget2 / (float)numberOfTarget2;
            deadTarget2++;
        }
    }

    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    
}
