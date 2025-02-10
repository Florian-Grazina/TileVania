using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected float jumpForce = 5f;
    [SerializeField] protected float climbSpeed = 3f;

    private float defaultGravity;
    private bool isClimbing = false;
    private bool isAlive = true;

    private Vector2 moveInput;
    private Rigidbody2D myRigidbody;
    private CapsuleCollider2D myBodyCollider;
    private BoxCollider2D myFeetCollider;
    private Coroutine flipCoroutine;
    private Animator myAnimator;

    protected void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        defaultGravity = myRigidbody.gravityScale;
    }

    protected void Update()
    {
        if (!isAlive) return;

        Run();
        ClimbTree();
        FlipSprite();
        Die();
    }

    #region input system methods
    protected void OnMove(InputValue value)
    {
        if (!isAlive) return;

        moveInput = value.Get<Vector2>();
    }

    protected void OnJump(InputValue value)
    {
        if (!isAlive) return;

        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) &&
           !myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Trees"))) return;

        if (value.isPressed)
            Jump();
    }
    #endregion

    #region movement methods
    protected void Run()
    {
        Vector2 playerVelocity = new(moveInput.x * moveSpeed, myRigidbody.linearVelocityY);
        myRigidbody.linearVelocity = playerVelocity;

        bool playerHasHorizontalSpeed = Math.Abs(myRigidbody.linearVelocityX) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    protected void Jump()
    {
        myRigidbody.linearVelocityY += jumpForce;
    }

    protected void ClimbTree()
    {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Trees")) && Math.Abs(moveInput.y) > Mathf.Epsilon)
            isClimbing = true;

        if(!myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Trees"))) isClimbing = false;
        
        if (!isClimbing) 
        {
            myRigidbody.gravityScale = defaultGravity;
            myAnimator.SetBool("isClimbing", false);
            return;
        }

        myRigidbody.gravityScale = 0f;

        Vector2 climbVelocity = new(myRigidbody.linearVelocityX, moveInput.y * climbSpeed);
        myRigidbody.linearVelocity = climbVelocity;

        bool playerHasVerticalSpeed = Math.Abs(myRigidbody.linearVelocityY) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
    }

    protected void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Math.Abs(myRigidbody.linearVelocityX) > Mathf.Epsilon;

        if (!playerHasHorizontalSpeed)
            return;

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

    private void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            myAnimator.SetBool("isDead", true);
            isAlive = false;
        }
    }
    #endregion
}
