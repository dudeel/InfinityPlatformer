using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float gravity = -20f;
    [SerializeField] private Vector2 velocity;

    [SerializeField] private float groundHeight = 0f;
    [SerializeField] private bool isGrounded = false;
    

    private float startJumpVelocity = 10f;
    [SerializeField] private float currentJumpVelocity = 10f;
    private int startJumpAmount = 1;
    [SerializeField] private int currentJumpAmount = 0;



    private void Start()
    {
        currentJumpAmount = startJumpAmount;
        currentJumpVelocity = startJumpVelocity;
    }

    private void Update()
    {
        if (isGrounded)
        {
            if (currentJumpAmount <= 0) currentJumpAmount = startJumpAmount;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGrounded = false;
                velocity.y = currentJumpVelocity;
                currentJumpAmount--;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) && currentJumpAmount > 0)
        {
            velocity.y = currentJumpVelocity;
            currentJumpAmount--;
        }
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        if (!isGrounded)
        {
            pos.y += velocity.y * Time.fixedDeltaTime;
            velocity.y += gravity * Time.fixedDeltaTime;

            if (pos.y <= groundHeight)
            {
                pos.y = groundHeight;
                isGrounded = true;
            }
        }

        transform.position = pos;
    }
}
