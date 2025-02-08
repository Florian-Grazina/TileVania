using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 1f;
    Rigidbody2D myRigidbody;

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
        moveSpeed *= -1;
    }

    private void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(myRigidbody.linearVelocity.x * -1, 1);

    }
}
