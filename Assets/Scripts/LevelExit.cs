using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private float levelLoadDelay = 1f;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            StartCoroutine(LoadNextLevel());
    }

    private IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            nextSceneIndex = 0;

        FindFirstObjectByType<ScenePersist>().DestroyScenePersist();
        SceneManager.LoadScene(nextSceneIndex);
    }
}
