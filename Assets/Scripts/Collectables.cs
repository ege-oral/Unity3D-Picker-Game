using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    private bool hasCollected = false;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Collectable Pool Trigger" && !hasCollected)
        {
            hasCollected = true;
            other.gameObject.GetComponentInParent<CollectablePool>().AddOneCollectableToValueCount();
        }
    }
}
