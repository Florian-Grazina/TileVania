using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float shootSpeed = 10f;
    private Rigidbody2D myRigidBody;


    protected void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();    
    }

    protected void Update()
    {
        myRigidBody.linearVelocity = new Vector2(shootSpeed, 0f);
    }
}
