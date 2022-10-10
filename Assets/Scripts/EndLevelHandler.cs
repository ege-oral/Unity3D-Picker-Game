using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndLevelHandler : MonoBehaviour
{

    PlayerMovement playerMovement;
    [SerializeField] GameObject risingPlatform;
    [SerializeField] Animator risingBarrier;
    [SerializeField] GameObject collectablePool;
    [SerializeField] GameObject preventCollectablesToPass;

    [SerializeField] GameObject continueCanvas;
    [SerializeField] GameObject replayCanvas;

    [SerializeField] float platformRiseDelay = 1f;
    [SerializeField] float barrierRiseDelay = 1f;
    [SerializeField] float playerMoveDelay = 1f;

    private void Start() 
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    public void ContinueToNextLevel()
    {
        continueCanvas.SetActive(false);
        PlayerPrefs.SetInt("Level_Number", PlayerPrefs.GetInt("Level_Number") + 1);
        StartCoroutine(ContinueToNextLevelRoutine());
    }

    public void ReplayLevel()
    {
        replayCanvas.SetActive(false);
        // Incase of future scenes.
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    IEnumerator ContinueToNextLevelRoutine()
    {
        yield return new WaitForSeconds(platformRiseDelay);
        risingPlatform.SetActive(true);
        risingPlatform.GetComponent<Animator>().SetBool("IsNumberReached", true);
        collectablePool.GetComponentInChildren<TextMeshPro>().enabled = false;

        yield return new WaitForSeconds(barrierRiseDelay);
        risingBarrier.SetBool("RaiseBarrier", true);
        preventCollectablesToPass.SetActive(false);

        yield return new WaitForSeconds(playerMoveDelay);
        playerMovement.IsReachedStopPoint = false;
        collectablePool.GetComponent<CollectablePool>().ResetCollectableValueCount();
    }
}
