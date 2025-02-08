using System;
using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = -1f;
    Rigidbody2D myRigidbody;

    private Coroutine flipCoroutine;

    protected void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    protected void Update()
    {
        myRigidbody.linearVelocity = new Vector2(moveSpeed, 0);
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Ground"))
            return;

        moveSpeed *= -1;
        FlipSprite();
    }

    protected void FlipSprite()
    {
        if (flipCoroutine != null)
            StopCoroutine(flipCoroutine);

        float xScale = Mathf.Sign(myRigidbody.linearVelocityX);
        flipCoroutine = StartCoroutine(SmoothFlip(xScale));
    }

    private IEnumerator SmoothFlip(float xScale)
    {
        Vector2 currentScale = transform.localScale;
        Vector2 targetScale = new(xScale, currentScale.y);

        float progress = 0f;
        float duration = 1f;

        while (progress < duration)
        {
            progress += Time.deltaTime * 10f;
            transform.localScale = Vector2.Lerp(currentScale, targetScale, progress);
            yield return null;
        }

        transform.localScale = targetScale;
    }
}
