using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelIndicator;
    [SerializeField] GameObject player;

    [SerializeField] GameObject levels;
    private List<GameObject> levelsStartPositions = new List<GameObject>();
    private int previousLevelCount = 0;

    private void Awake() 
    {
        if(!PlayerPrefs.HasKey("Level_Number"))
            PlayerPrefs.SetInt("Level_Number" , 1);
        
        int Level_Number = PlayerPrefs.GetInt("Level_Number");

        foreach(Transform level in levels.transform)
        {
            if(previousLevelCount < Level_Number - 1)
            {
                level.gameObject.SetActive(false);
            }
            levelsStartPositions.Add(level.gameObject);
            previousLevelCount += 1;
        }

        player.transform.position = levelsStartPositions[Level_Number - 1].transform.position;
    }


    private void Update() 
    {
        levelIndicator.text = $"Level: {PlayerPrefs.GetInt("Level_Number").ToString()}";
    }
}
