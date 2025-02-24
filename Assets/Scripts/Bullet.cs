using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float shootSpeed = 10f;
    private float xSpeed;

    private Rigidbody2D myRigidBody;
    private PlayerMovement player;

    protected void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        player = FindFirstObjectByType<PlayerMovement>();

        xSpeed = player.transform.localScale.x < 0 ? -shootSpeed : shootSpeed;
    }

    protected void Update()
    {
        myRigidBody.linearVelocity = new Vector2(xSpeed, 0f);
        if (myRigidBody.IsTouchingLayers(LayerMask.GetMask("Ground")))
            Destroy(gameObject);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
            Destroy(collision.gameObject);
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    protected void OnCollisionStay(Collision collision)
    {
        Destroy(gameObject);
    }
}
