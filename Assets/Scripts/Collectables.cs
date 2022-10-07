using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    private bool hasCollected = false;
    Rigidbody collectableRigidBoyd;
    float collectableThrust = 5f;

    bool isSpeedUpArea = false;
    bool hasGainForce = false;

    [SerializeField] GameObject particleBlastEffect;


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
            other.gameObject.GetComponentInParent<CollectablePool>().AddOneCollectableToValueCount();
            StartCoroutine(CollectedRoutine());
        }
        if(other.gameObject.tag == "Speed Up Area")
        {
            isSpeedUpArea = true;
        }
    }

    IEnumerator CollectedRoutine()
    {
        particleBlastEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

}
