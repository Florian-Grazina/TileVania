using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] private AudioClip coinPickUpSFX;

    protected void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(coinPickUpSFX, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
