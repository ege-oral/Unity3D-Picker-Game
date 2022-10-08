using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    Rigidbody collectableRigidBoyd;
    [SerializeField] GameObject particleBlastEffect;

    private float collectableThrust = 5f;
    
    private bool hasCollected = false;
    private bool isSpeedUpArea = false;
    private bool hasGainForce = false;


    private void Start() 
    {
        collectableRigidBoyd = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() 
    {
        if(isSpeedUpArea && !hasGainForce)
        {
            collectableRigidBoyd.AddForce(Vector3.forward * collectableThrust, ForceMode.Impulse);
            hasGainForce = true;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Collectable Pool Trigger" && !hasCollected)
        {
            hasCollected = true;
            collectableRigidBoyd.velocity = Vector3.zero;
            other.gameObject.GetComponentInParent<CollectablePool>().AddOneToCollectableValueCount();
            StartCoroutine(CollectedRoutine());
        }
        if(other.gameObject.tag == "Speed Up Area")
        {
            isSpeedUpArea = true;
        }
    }

    IEnumerator CollectedRoutine()
    {
        yield return new WaitForSeconds(1f);
        particleBlastEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

}
