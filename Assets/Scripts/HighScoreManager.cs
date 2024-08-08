using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    [SerializeField] private GameObject scoreText;
    [SerializeField] private GameObject scoreTextPrefab;
    private List<GameObject> scores = new List<GameObject>();

    //dev
    [SerializeField] private List<int> highScores = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    private void Start()
    {
        InitializeHighScore();
    }

    private void InitializeHighScore()
    {
        if(GameManager.Instance != null)
        {
            for (int i = 0; i < highScores.Count; i++)
            {
                Vector3 posOffset = scoreText.transform.position - new Vector3(0, i * 50 + 100, 0);
                GameObject instance = Instantiate(scoreTextPrefab, posOffset, scoreTextPrefab.transform.rotation, scoreText.transform);
                instance.GetComponent<TextMeshProUGUI>().text = GameManager.Instance.highScoresName[i] + ".........." + GameManager.Instance.highScores[i].ToString("D3"); ;

                if(i == GameManager.Instance.playerRank) 
                {
                    instance.GetComponent<TextBlink>().isActive = true;
                }
            }

        }
     
    }


}
