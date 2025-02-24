using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] private AudioClip coinPickUpSFX;
    [SerializeField] private int coinValue = 100;

    bool isCollected = false;

    protected void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            AudioSource.PlayClipAtPoint(coinPickUpSFX, Camera.main.transform.position);
            FindFirstObjectByType<GameSession>().AddToScore(coinValue);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
