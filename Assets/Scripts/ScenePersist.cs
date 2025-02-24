using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    protected void Awake()
    {
        int numberOfScenePersist = FindObjectsByType<ScenePersist>(FindObjectsSortMode.None).Length;
        if (numberOfScenePersist > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }

    public void DestroyScenePersist()
    {
        Destroy(gameObject);
    }
}
