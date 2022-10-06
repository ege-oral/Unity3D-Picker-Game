using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectablePool : MonoBehaviour
{
    private int collectableValue = 0;
    private int desiredCollectableValue = 10;
    TextMeshPro collectableCountText;

    private void Start() 
    {
        collectableCountText = GetComponentInChildren<TextMeshPro>();
    }

    private void Update() 
    {
        collectableCountText.text = $"{collectableValue} / {desiredCollectableValue}";
    }

    private void IsRea()
    {

    }

    private void OnTriggerEnter(Collider other) 
    {
        collectableValue += 1;
    }

}
