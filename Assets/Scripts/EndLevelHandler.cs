﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndLevelHandler : MonoBehaviour
{

    [SerializeField] Animator risingPlatform;
    [SerializeField] Animator risingBarrier;
    [SerializeField] GameObject collectablePool;
    [SerializeField] GameObject preventPassForCollectables;
    [SerializeField] PlayerMovement playerMovement;


    [SerializeField] GameObject continueCanvas;
    [SerializeField] GameObject replayCanvas;

    [SerializeField] float platformRiseDelay = 1f;
    [SerializeField] float barrierRiseDelay = 1f;
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
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    IEnumerator ContinueToNextLevelRoutine()
    {
        yield return new WaitForSeconds(platformRiseDelay);
        risingPlatform.SetBool("IsNumberReached", true);
        collectablePool.GetComponentInChildren<TextMeshPro>().enabled = false;

        yield return new WaitForSeconds(barrierRiseDelay);
        risingBarrier.SetBool("RaiseBarrier", true);
        preventPassForCollectables.SetActive(false);

        yield return new WaitForSeconds(playerMoveDelay);
        playerMovement.IsReachedStopPoint = false;
        collectablePool.GetComponent<CollectablePool>().ResetCollectableValueCount();
    }
}
