using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] private int playerLives = 3;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;


    #region unity methods
    protected void Awake()
    {
        int numberOfGameSessions = FindObjectsByType<GameSession>(FindObjectsSortMode.None).Length;
        if(numberOfGameSessions > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }

    protected void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }
    #endregion

    #region public methods
    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
            TakeLife();
        else
            ResetGameSession();
    }

    public void AddToScore(int v)
    {
        score += v;
        scoreText.text = score.ToString();
    }
    #endregion

    #region
    private void TakeLife()
    {
        playerLives--;
        livesText.text = playerLives.ToString();

        int activeScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeScene);
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    #endregion
}
