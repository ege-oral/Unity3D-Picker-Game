using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectablePool : MonoBehaviour
{
    private int collectableValueCount = 0;

    private int desiredCollectableValueCount = 10;
    TextMeshPro collectableCountText;

    private void Start() 
    {
        collectableCountText = GetComponentInChildren<TextMeshPro>();
    }

    private void Update() 
    {
        collectableCountText.text = $"{collectableValueCount} / {desiredCollectableValueCount}";
        HasDesiredNumberBeenReached();
    }

    private void HasDesiredNumberBeenReached()
    {
        if(collectableValueCount >= desiredCollectableValueCount)
        {
            print("Yey");
        }
    }

    public void AddOneCollectableToValueCount()
    {
        collectableValueCount += 1;
    }

    // private void OnTriggerEnter(Collider other) 
    // {
    //     collectableValueCount += 1;

    // }

}
