using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject[] levelStartPositions;
    [SerializeField] TextMeshProUGUI levelIndicator;
    [SerializeField] GameObject player;

    private void Awake() 
    {
        if(!PlayerPrefs.HasKey("Level_Number"))
            PlayerPrefs.SetInt("Level_Number" , 1);
        
        int Level_Number = PlayerPrefs.GetInt("Level_Number");

        for(int i = 0; i < Level_Number - 1; i++)
        {
            levelStartPositions[i].SetActive(false);
        }

        player.transform.position = levelStartPositions[Level_Number - 1].transform.position;
    }

    private void Update() 
    {
        levelIndicator.text = $"Level: {PlayerPrefs.GetInt("Level_Number").ToString()}";
    }
}
