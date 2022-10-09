using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelIndicator;
    [SerializeField] GameObject player;

    // Important !
    // If the current level greater than starting level.
    // This variable can't do anything. Clear All PlayerPrefs.
    [Tooltip("If the current level greater than starting level. This variable can't do anything. Clear All PlayerPrefs.")]
    [SerializeField][Range(0, 10)] int startingLevel = 0;

    [SerializeField] GameObject levels;
    private List<GameObject> levelsStartPositions = new List<GameObject>();
    private int previousLevelCount = 0;

    private void Awake() 
    {
        PlayerPrefsHandler();

        int currentLevelNumber = PlayerPrefs.GetInt("Level_Number");

        foreach(Transform level in levels.transform)
        {
            if(previousLevelCount < currentLevelNumber - 1)
            {
                level.gameObject.SetActive(false);
            }
            levelsStartPositions.Add(level.gameObject);
            previousLevelCount += 1;
        }
        player.transform.position = levelsStartPositions[currentLevelNumber - 1].transform.position;
    }

    private void Update() 
    {
        levelIndicator.text = $"Level: {PlayerPrefs.GetInt("Level_Number").ToString()}";
        DisablePreviousLevelsInRuntime();
    }

    private void PlayerPrefsHandler()
    {
        if(startingLevel != 0)
        {
            // If current level lower than starting level. Set current level to starting level.
            if(PlayerPrefs.GetInt("Level_Number", 0) <= startingLevel)
                PlayerPrefs.SetInt("Level_Number", startingLevel);
        }
        else
        {
            // If there is no Level_Number key.
            if(!PlayerPrefs.HasKey("Level_Number"))
                PlayerPrefs.SetInt("Level_Number", 1);
        }
    }

    private void DisablePreviousLevelsInRuntime()
    {
        int currentLevel = PlayerPrefs.GetInt("Level_Number");
        if(currentLevel - 2 > 0)
        {
            // This level is in different array position.
            // Thats why we don't disable currentLevel by -2.
            levelsStartPositions[currentLevel - 3].SetActive(false);
        }
    }
}
