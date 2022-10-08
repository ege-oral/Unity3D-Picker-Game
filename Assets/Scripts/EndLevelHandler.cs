using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndLevelHandler : MonoBehaviour
{

    [SerializeField] Animator risingPlatform;
    [SerializeField] GameObject collectablePool;
    [SerializeField] PlayerMovement playerMovement;


    [SerializeField] GameObject continueCanvas;
    [SerializeField] GameObject replayCanvas;

    [SerializeField] float platformRiseDelay = 1f;
    [SerializeField] float playerMoveDelay = 1f;


    public void ContinueToNextLevel()
    {
        print("Continue");
        continueCanvas.SetActive(false);
        PlayerPrefs.SetInt("Level_Number", PlayerPrefs.GetInt("Level_Number") + 1);
        StartCoroutine(ContinueToNextLevelRoutine());
    }

    public void ReplayLevel()
    {
        print("Replay");
        replayCanvas.SetActive(false);
    }

    IEnumerator ContinueToNextLevelRoutine()
    {
        yield return new WaitForSeconds(platformRiseDelay);
        risingPlatform.SetBool("IsNumberReached", true);
        collectablePool.GetComponentInChildren<TextMeshPro>().enabled = false;

        yield return new WaitForSeconds(playerMoveDelay);
        playerMovement.IsReachedStopPoint = false;
        collectablePool.GetComponent<CollectablePool>().ResetCollectableValueCount();
    }
}
