using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 moveInput;
    private Rigidbody2D redRigidbody;
    private Coroutine flipCoroutine;

    protected void Start()
    {
        redRigidbody = GetComponent<Rigidbody2D>();
    }

    protected void Update()
    {
        Run();
        FlipSprite();
    }

    #region input system methods
    protected void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }
    #endregion

    #region movement methods
    protected void Run()
    {
        Vector2 playerVelocity = new (moveInput.x * moveSpeed, redRigidbody.linearVelocityY);
        redRigidbody.linearVelocity = playerVelocity;
    }

    protected void FlipSprite()
    {
        if(flipCoroutine != null)
            StopCoroutine(flipCoroutine);

        float xScale = Mathf.Sign(redRigidbody.linearVelocityX);
        flipCoroutine = StartCoroutine(SmoothFlip(xScale));
    }

    private IEnumerator SmoothFlip(float xScale)
    {
        Vector2 currentScale = transform.localScale;
        Vector2 targetScale = new (xScale, currentScale.y);

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
    #endregion
}
