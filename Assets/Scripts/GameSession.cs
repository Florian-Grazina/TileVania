using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] private int playerLives = 3;

    #region unity methods
    protected void Awake()
    {
        int numberOfGameSessions = FindObjectsByType<GameSession>(FindObjectsSortMode.None).Length;
        if(numberOfGameSessions > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
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
    #endregion

    #region
    private void TakeLife()
    {
        playerLives--;

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
