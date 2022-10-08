using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Transform[] levelStartPositions;
    [SerializeField] TextMeshProUGUI levelIndicator;
    [SerializeField] GameObject player;

    private void Awake() 
    {
        if(!PlayerPrefs.HasKey("Level_Number"))
            PlayerPrefs.SetInt("Level_Number" , 1);

        player.transform.position = levelStartPositions[0].position;
    }

    private void Update() 
    {
        levelIndicator.text = $"Level: {PlayerPrefs.GetInt("Level_Number").ToString()}";
    }
}
