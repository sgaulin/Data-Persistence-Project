using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
       
    public string playerName { get; private set; } = "AAA";
    public int playerScore { get; private set; } = 0;
    public int playerRank { get; private set; } = 0;
    public List<int> highScores { get; private set; } = new List<int>() {0,0,0,0,0,0,0,0,0,0};
    public List<string> highScoresName { get; private set; } = new List<string>() { "AAA", "AAA", "AAA", "AAA", "AAA", "AAA", "AAA", "AAA", "AAA", "AAA"};
    public bool isGameActive { get; set; } = false;

    [SerializeField] private TMP_InputField PlayerName;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
            
        LoadHighScores();

    }

    private void Start()
    {
        if (!isGameActive)
        {
            PlayerName.Select();

        }
        
    }

    public void EnterName()
    {
        playerName = PlayerName.text;
        PlayerName.gameObject.SetActive(false);
        isGameActive = true;

        SceneManager.LoadScene(1);

    }

    public void UpdateScore(int score)
    {
        playerScore = score;
    }

    public void NewHighScore(int score)
    {
        playerScore = score;
        string newPlayerName = playerName;
        bool ranked = false;
               
        for (int i = 0;i < highScores.Count; i++) 
        {         
            if(score >= highScores[i]) 
            {
                int lowScore;
                string lowName;
                lowScore = highScores[i];
                lowName = highScoresName[i];

                highScores[i] = score;
                highScoresName[i] = newPlayerName;                

                score= lowScore;
                newPlayerName = lowName;

                if(!ranked) 
                { 
                    playerRank = i;  
                    ranked = true;
                }
            }
        }

        SaveHighScores();

        isGameActive= false;

    }


    [System.Serializable]
    class SaveData
    {
        public List<int> highScores = new List<int>() {0,0,0,0,0,0,0,0,0,0};
        public List<string> highScoresName = new List<string>();

    }

    void SaveHighScores()
    {
        SaveData score= new SaveData();

        score.highScores = highScores;
        score.highScoresName = highScoresName;

        string json = JsonUtility.ToJson(score);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);        

    }

    void LoadHighScores()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData score = JsonUtility.FromJson<SaveData>(json);

            highScores = score.highScores;
            highScoresName = score.highScoresName;    

        }

    }

    public void Exit()
    {

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();        
#else
        Application.Quit();
#endif       

    }
        

}
