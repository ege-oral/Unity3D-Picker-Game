using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Transform[] levelStartPositions;
    [SerializeField] TextMeshProUGUI levelIndicator;

    private void Awake() 
    {
        if(!PlayerPrefs.HasKey("Level_Number"))
            PlayerPrefs.SetInt("Level_Number" , 1);
    }

    private void Update() 
    {
        levelIndicator.text = $"Level: {PlayerPrefs.GetInt("Level_Number").ToString()}";
    }
}
