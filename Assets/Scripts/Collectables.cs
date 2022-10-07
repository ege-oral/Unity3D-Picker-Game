using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    private bool hasCollected = false;
    Rigidbody collectableRigidBoyd;
    float collectableSpeed = 8f;

    bool isTouchingPlayer = false;

    private void Start() 
    {
        collectableRigidBoyd = GetComponent<Rigidbody>();    
    }

    private void FixedUpdate() 
    {
        if(isTouchingPlayer)
            collectableRigidBoyd.velocity = Vector3.forward * collectableSpeed;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Collectable Pool Trigger" && !hasCollected)
        {
            hasCollected = true;
            other.gameObject.GetComponentInParent<CollectablePool>().AddOneCollectableToValueCount();

            // TODO 
            // Add particle effect.

            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player")
        {
            isTouchingPlayer = true;
            
        }
    }

    private void OnCollisionExit(Collision other) 
    {
        isTouchingPlayer = false;
    }

}
