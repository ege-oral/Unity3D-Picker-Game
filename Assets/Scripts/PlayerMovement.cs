using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Camera mainCamera;
    Rigidbody playerRigidBoyd;
    Touch touch;

    [SerializeField] float playerForwardSpeed = 10f;
    [SerializeField] float playerLeftRightSpeed = 10f;

    private bool startPlaying = false;
    public bool StartPlaying{ get { return startPlaying; } set{ startPlaying = value; } }

    private bool isReachedStopPoint = false;
    public bool IsReachedStopPoint{ get { return isReachedStopPoint; } set{ isReachedStopPoint = value; } }

    // float minXPosition = -2.5f;
    // float maxXPosition = 2.5f;


    private void Start()
    {
        mainCamera = Camera.main;
        playerRigidBoyd = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() 
    {
        if(startPlaying)
        {
            MovePlayerForward();    
            MovePlayerLeftRight();
            KeepPlayerOnTrack();
        }
    }

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Moved)
            {
                startPlaying = true;
                
                

                // transform.position = Vector3.Lerp(transform.position, 
                //                                   new Vector3(worldPosition.x * 2f, transform.position.y, transform.position.z), 
                //                                   10f * Time.deltaTime);

                
            }
            
        }  
    }

    

    private void MovePlayerForward()
    {   
        if(isReachedStopPoint)
        {
            playerRigidBoyd.velocity = Vector3.zero;
            return;
        }
        playerRigidBoyd.velocity = Vector3.forward * playerForwardSpeed;
        
        //transform.position += transform.forward * Time.deltaTime * playerSpeed;
    }

    private void MovePlayerLeftRight()
    {
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));
        playerRigidBoyd.velocity = new Vector3((worldPosition.x - transform.position.x) * playerLeftRightSpeed, 
                                                0f, 
                                                playerRigidBoyd.velocity.z);
    }

    private void KeepPlayerOnTrack()
    {
        // Vector3 clampXPosition = transform.position;
        // clampXPosition.x = Mathf.Clamp(transform.position.x, minXPosition, maxXPosition);
        // transform.position = clampXPosition;
        // if(transform.position.x < -2.4f || transform.position.x > 2.4f)
        //     playerRigidBoyd.velocity = new Vector3(0f, 0f, playerRigidBoyd.velocity.z);
    }

    // private void FixedUpdate() 
    // {
    //     playerRigidBoyd.velocity = Vector3.forward * playerSpeed;

    // }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Stop Point")
        {
            isReachedStopPoint = true;
            other.gameObject.SetActive(false);
        }
    }
}
