using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectablePool : MonoBehaviour
{

    TextMeshPro collectableCountText;

    private int collectableValueCount = 0;
    private int desiredCollectableValueCount = 10;
    private bool isCountDownStarted = false;
    private float waitTime = 3f;

    [SerializeField] GameObject continueCanvas;
    [SerializeField] GameObject replayCanvas;
    [SerializeField] GameObject allCollectables;


    private void Start() 
    {
        collectableCountText = GetComponentInChildren<TextMeshPro>();
    }

    private void Update() 
    {
        collectableCountText.text = $"{collectableValueCount} / {desiredCollectableValueCount}";

        if(collectableValueCount > 0 && !isCountDownStarted)
        {
            isCountDownStarted = true;
            StartCoroutine(HasDesiredNumberBeenReached());
        }
    }

    public void AddOneToCollectableValueCount()
    {
        collectableValueCount += 1;
    }

    public void ResetCollectableValueCount()
    {
        collectableValueCount = 0;
    }

    IEnumerator HasDesiredNumberBeenReached()
    {
        // Waiting for other collectables to be counted.
        yield return new WaitForSeconds(waitTime);

        if(collectableValueCount >= desiredCollectableValueCount)
            continueCanvas.SetActive(true);
        else
            replayCanvas.SetActive(true);
        
        // At the end of the count, we destroy all remaining collectables.
        allCollectables.SetActive(false);
    }


    


}
