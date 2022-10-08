using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    Rigidbody collectableRigidBoyd;
    AudioSource collectableSoundEffect;
    [SerializeField] GameObject particleBlastEffect;

    private float collectableThrust = 5f;
    
    private bool hasCollected = false;
    private bool isSpeedUpArea = false;
    private bool hasGainForce = false;
    private bool hasCollide = false;


    private void Start() 
    {
        collectableRigidBoyd = GetComponent<Rigidbody>();
        collectableSoundEffect = GetComponent<AudioSource>();
    }

    private void FixedUpdate() 
    {
        if(isSpeedUpArea && !hasGainForce)
        {
            collectableRigidBoyd.AddForce(Vector3.forward * collectableThrust, ForceMode.Impulse);
            hasGainForce = true;
        }
    }

    IEnumerator CollectedRoutine()
    {
        yield return new WaitForSeconds(1f);
        particleBlastEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Collectable Pool Trigger" && !hasCollected)
        {
            hasCollected = true;
            collectableSoundEffect.Play();
            collectableRigidBoyd.velocity = Vector3.zero;
            other.gameObject.GetComponentInParent<CollectablePool>().AddOneToCollectableValueCount();
            StartCoroutine(CollectedRoutine());
        }
        if(other.gameObject.tag == "Speed Up Area")
        {
            isSpeedUpArea = true;
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if((other.gameObject.tag == "Player" || other.gameObject.tag == "Collectable") && !hasCollide)
        {
            hasCollide = true;
            collectableSoundEffect.Play();
        }
    }


}
