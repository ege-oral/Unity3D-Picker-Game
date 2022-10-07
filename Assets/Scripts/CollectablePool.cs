using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectablePool : MonoBehaviour
{
    

    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Animator risingPlatform;
    TextMeshPro collectableCountText;

    private int collectableValueCount = 0;
    private int desiredCollectableValueCount = 10;

    [SerializeField] float platformRiseDelay = 2f;
    [SerializeField] float playerMoveDelay = 1f;



    private void Start() 
    {
        collectableCountText = GetComponentInChildren<TextMeshPro>();
    }

    private void Update() 
    {
        collectableCountText.text = $"{collectableValueCount} / {desiredCollectableValueCount}";
        HasDesiredNumberBeenReached();
    }

    public void AddOneCollectableToValueCount()
    {
        collectableValueCount += 1;
    }

    private void HasDesiredNumberBeenReached()
    {
        if(collectableValueCount >= desiredCollectableValueCount)
        {
            StartCoroutine(RisePlatform());
        }
    }


    IEnumerator RisePlatform()
    {
        yield return new WaitForSeconds(platformRiseDelay);
        risingPlatform.SetBool("IsNumberReached", true);
        collectableCountText.enabled = false;

        yield return new WaitForSeconds(playerMoveDelay);
        playerMovement.IsReachedStopPoint = false;
        collectableValueCount = 0;
    }
}
