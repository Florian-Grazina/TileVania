using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected float jumpForce = 5f;

    private Vector2 moveInput;
    private Rigidbody2D myRigidbody;
    private Coroutine flipCoroutine;
    private Animator myAnimator;

    protected void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
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
    }

    protected void OnJump(InputValue value)
    {
        if(value.isPressed)
            Jump();
    }
    #endregion

    #region movement methods
    protected void Run()
    {
        Vector2 playerVelocity = new (moveInput.x * moveSpeed, myRigidbody.linearVelocityY);
        myRigidbody.linearVelocity = playerVelocity;

        bool playerHasHorizontalSpeed = Math.Abs(myRigidbody.linearVelocityX) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    protected void Jump()
    {
        myRigidbody.linearVelocityY += jumpForce;
    }

    protected void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Math.Abs(myRigidbody.linearVelocityX) > Mathf.Epsilon;

        if(!playerHasHorizontalSpeed)
            return;

        if (flipCoroutine != null)
            StopCoroutine(flipCoroutine);

        float xScale = Mathf.Sign(myRigidbody.linearVelocityX);
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
