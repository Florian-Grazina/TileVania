using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 moveInput;
    private Rigidbody2D redRigidbody;

    protected void Start()
    {
        redRigidbody = GetComponent<Rigidbody2D>();
    }

    protected void Update()
    {
        Run();
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
    #endregion
}
