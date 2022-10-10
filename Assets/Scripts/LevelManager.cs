using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{

    [Header("Level Settings")]
    [Tooltip("If the current level greater than starting level. This variable can't do anything. Clear All PlayerPrefs.")]
    [Range(0, 10)][SerializeField] int startingLevel = 0;
    [SerializeField] GameObject[] numberOfLevels;

    [SerializeField] TextMeshProUGUI levelIndicator;
    [SerializeField] GameObject player;
    [SerializeField] GameObject levels;
    private List<GameObject> levelsStartPositions = new List<GameObject>();

    int currentLevelNumber;
    private int previousLevelCount = 0;

    private void Awake() 
    {
        PlayerPrefsHandler();
        DisablePreviousLevelsInLoad();
        LoadPlayerPosition();
    }

    private void Update() 
    {
        currentLevelNumber = PlayerPrefs.GetInt("Level_Number");
        DisplayCurrentLevel();
        DisablePreviousLevelsInRuntime();
        AddLevelInRuntime();
    }

    private void PlayerPrefsHandler()
    {
        // If player reaches 10+ level we reset the level number.
        if(PlayerPrefs.GetInt("Level_Number") > 10 && startingLevel == 0)
            PlayerPrefs.SetInt("Level_Number", 1);
        else if(PlayerPrefs.GetInt("Level_Number") > 10 && startingLevel != 0)
            PlayerPrefs.SetInt("Level_Number", startingLevel);
        else
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
        currentLevelNumber = PlayerPrefs.GetInt("Level_Number");
    }

    private void DisablePreviousLevelsInRuntime()
    {
        if(currentLevelNumber - 2 > 0)
        {
            // This level is in different array position.
            // Thats why we don't disable currentLevel by -2.
            levelsStartPositions[currentLevelNumber - 3].SetActive(false);
        }
    }

    private void AddLevelInRuntime()
    {
        if(levels.transform.childCount - currentLevelNumber < 5)
        {
            GameObject newLevel = Instantiate(numberOfLevels[Random.Range(0, 10)], 
                                              new Vector3(0, -0.1f, levels.transform.childCount * 78f),
                                              Quaternion.identity);
            newLevel.transform.parent = levels.transform;
            levelsStartPositions.Add(newLevel); 
        }
    }

    private void DisablePreviousLevelsInLoad()
    {
        foreach(Transform level in levels.transform)
        {
            if(previousLevelCount < currentLevelNumber - 1)
            {
                level.gameObject.SetActive(false);
            }
            levelsStartPositions.Add(level.gameObject);
            previousLevelCount += 1;
        }
    }

    private void LoadPlayerPosition()
    {
        player.transform.position = levelsStartPositions[currentLevelNumber - 1].transform.position;
    }

    private void DisplayCurrentLevel()
    {
        if(currentLevelNumber <= 10)
        {
            levelIndicator.text = $"Level: {PlayerPrefs.GetInt("Level_Number").ToString()}";
            levelIndicator.fontStyle = FontStyles.Bold;
        }
        else
        {
            levelIndicator.text = "INFINITE";
            levelIndicator.fontStyle = FontStyles.Bold;
        }
    }
}
