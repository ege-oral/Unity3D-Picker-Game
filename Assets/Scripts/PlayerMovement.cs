using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Camera mainCamera;
    Rigidbody playerRigidBoyd;

    [SerializeField] float playerSpeed = 10f;

    private bool startPlaying = false;
    private bool isReachedStopPoint = false;


    void Start()
    {
        mainCamera = Camera.main;
        playerRigidBoyd = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Input.touchCount > 0)
        {
            print("touched");
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Moved)
            {
                startPlaying = true;
                Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));
                transform.position = Vector3.Lerp(transform.position, 
                                                  new Vector3(worldPosition.x, transform.position.y, transform.position.z), 
                                                  20f * Time.deltaTime);
            }
        }

        if(startPlaying && !isReachedStopPoint)
            transform.position += transform.forward * Time.deltaTime * playerSpeed;
    }

    private void OnTriggerEnter(Collider other) 
    {
        isReachedStopPoint = true;
    }
}
