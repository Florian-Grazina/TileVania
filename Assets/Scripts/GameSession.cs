using UnityEngine;

public class GameSession : MonoBehaviour
{
    protected void Awake()
    {
        int numberOfGameSessions = FindObjectsByType<GameSession>(FindObjectsSortMode.None).Length;
        if(numberOfGameSessions > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }

    protected void Update()
    {
        
    }
}
