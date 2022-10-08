using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectablePool : MonoBehaviour
{

    TextMeshPro collectableCountText;

    private int collectableValueCount = 0;
    [SerializeField] int desiredCollectableValueCount = 10;
    
    private bool startCountdown = false;
    private float waitTime = 4f;

    [SerializeField] GameObject continueCanvas;
    [SerializeField] GameObject replayCanvas;
    [SerializeField] GameObject allCollectables;
    [SerializeField] GameObject startCountDownTrigger;


    private void Start() 
    {
        collectableCountText = GetComponentInChildren<TextMeshPro>();
    }

    private void Update() 
    {
        collectableCountText.text = $"{collectableValueCount} / {desiredCollectableValueCount}";

        if(startCountdown)
        {
            startCountdown = false;
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


    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            startCountdown = true;
            startCountDownTrigger.SetActive(false);
        }
    }


}
