//using System.Runtime.Remoting.Messaging;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainManager : MonoBehaviour
{
    public bool isStartMenu = false;
    public bool isHighScoreMenu = false;

    public Brick BrickPrefab;
    public int LineCount = 8;
    public float LineSize = 4;
    public float Step = 0.6f;
    public Rigidbody Ball;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI PlayerText;
    public TextMeshProUGUI BestScoreText;
    public TextMeshProUGUI BestPlayerText;
    public GameObject GameOverText;
    
    public bool m_Started { get; private set; } = false;
    private int m_Points = 0;  
        
    public bool m_GameOver { get; private set; } = false;


    void Start()
    {
        InitializeScore();

        if (isHighScoreMenu)
        {
            if(GameManager.Instance != null) { GameManager.Instance.isGameActive = true; }
            m_Started= true;
            m_GameOver= true;
            return;
        }
            

        InitializeBricks();        

        if (isStartMenu)
        {
            InitializeBall();
        }

    }

    private void Update()
    {
        if ((GameManager.Instance != null ? GameManager.Instance.isGameActive : true))
        {
            if (!m_Started)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    m_Started = true;
                    InitializeBall();

                }
            }
            else if (m_GameOver)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    m_GameOver = false;
                    m_Started = false;
                    m_Points = 0;
                    AddPoint(0);

                    SceneManager.LoadScene(1);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.Exit();

        }

    }

    void AddPoint(int point)
    {
        m_Points += point;
        //ScoreText.text = $"Score : {m_Points}";
        ScoreText.text = m_Points.ToString("D3");

    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        
        if(GameManager.Instance != null ? m_Points >= GameManager.Instance.highScores[9] : false) 
        {
            GameManager.Instance.NewHighScore(m_Points);

            InitializeScore();

            GameOverText.GetComponent<Text>().text = "NEW HIGH SCORE";

            StartCoroutine(LoadHighScores());

        }        

    }

    private void InitializeBricks() 
    {
        //const float step = 0.6f;
        int perLine = Mathf.FloorToInt(LineSize / Step);

        int[] pointCountArray = new[] { 1, 1, 3, 3, 5, 5, 10, 10 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-2.4f + Step * x, 3f + i * 0.15f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

    }

    private void InitializeBall() 
    {
        float randomDirection = Random.Range(-1.0f, 1.0f);
        Vector3 forceDir = new Vector3(randomDirection, 1, 0);
        forceDir.Normalize();

        Ball.transform.SetParent(null);
        Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);

    }

    private void InitializeScore()
    {        
        if (GameManager.Instance != null)
        {   
            if(!isHighScoreMenu && !m_GameOver)
            {
                GameManager.Instance.UpdateScore(0);
            }
            PlayerText.text = GameManager.Instance.playerName;
            ScoreText.text = GameManager.Instance.playerScore.ToString("D3");
            BestPlayerText.text = GameManager.Instance.highScoresName[0];
            BestScoreText.text = GameManager.Instance.highScores[0].ToString("D3");

        }

    }

    private IEnumerator LoadHighScores()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(2);

    }

}
