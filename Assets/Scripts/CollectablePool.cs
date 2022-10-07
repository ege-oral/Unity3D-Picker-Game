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
        yield return new WaitForSeconds(2f);

        if(collectableValueCount >= desiredCollectableValueCount)
            continueCanvas.SetActive(true);
        else
            replayCanvas.SetActive(true);
        
        allCollectables.SetActive(false);
    }


    


}
