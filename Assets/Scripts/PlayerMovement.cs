using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Touch touch;
    Camera mainCamera;
    Rigidbody playerRigidBoyd;
    [SerializeField] GameObject dragToStartImage;

    [SerializeField] float playerForwardSpeed = 10f;
    [SerializeField] float playerLeftRightSpeed = 10f;

    private float playerLeftRightOffset = 1.7f;

    private bool startPlaying = false;
    public bool StartPlaying{ get { return startPlaying; } set{ startPlaying = value; } }

    private bool isReachedStopPoint = false;
    public bool IsReachedStopPoint{ get { return isReachedStopPoint; } set{ isReachedStopPoint = value; } }


    private void Start()
    {
        mainCamera = Camera.main;
        playerRigidBoyd = GetComponent<Rigidbody>();
        dragToStartImage.SetActive(true);
    }

    private void FixedUpdate() 
    {
        if(startPlaying)
        {
            MovePlayerForward();    
            MovePlayerLeftRight();
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
                dragToStartImage.SetActive(false);
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
    }

    private void MovePlayerLeftRight()
    {
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));
        playerRigidBoyd.velocity = new Vector3(((worldPosition.x * playerLeftRightOffset) - transform.position.x ) * playerLeftRightSpeed, 
                                                0f, 
                                                playerRigidBoyd.velocity.z);
    }

    // Check If player reaches stop point.
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Stop Point")
        {
            isReachedStopPoint = true;
            other.gameObject.SetActive(false);
        }
    }
}
