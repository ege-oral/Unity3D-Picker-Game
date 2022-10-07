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

    [SerializeField] GameObject continueCanvas;
    [SerializeField] GameObject replayCanvas;

    [SerializeField] GameObject allCollectables;

    bool isCountDownStarted = false;



    private void Start() 
    {
        collectableCountText = GetComponentInChildren<TextMeshPro>();
        
    }

    private void Update() 
    {
        collectableCountText.text = $"{collectableValueCount} / {desiredCollectableValueCount}";
        if(collectableValueCount != 0 && !isCountDownStarted)
        {
            isCountDownStarted = true;
            print('e');
            StartCoroutine(HasDesiredNumberBeenReached());
            
        }
    }

    public void AddOneCollectableToValueCount()
    {
        collectableValueCount += 1;
    }

    IEnumerator HasDesiredNumberBeenReached()
    {
        yield return new WaitForSeconds(2f);
        if(collectableValueCount >= desiredCollectableValueCount)
        {
            continueCanvas.SetActive(true);
        }
        else
        {
            replayCanvas.SetActive(true);
        }
            allCollectables.SetActive(false);
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
