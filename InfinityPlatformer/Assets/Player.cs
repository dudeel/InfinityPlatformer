using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public float distance {get; private set;}
    [SerializeField] private float gravity = -20f;
    [SerializeField] private Vector2 velocity;

    [SerializeField] private float  currentAcceleration = 1f;
    [SerializeField] private float  maxAcceleration = 50f;
    [SerializeField] private float  maxVelocity = 50f;

    [SerializeField] private float groundHeight = 0f;
    private bool isGrounded = false;

    private float startJumpVelocity = 10f;
    [SerializeField] private float currentJumpVelocity = 10f;

    private int startJumpAmount = 1;
    private int maxJumpAmount = 1;
    [SerializeField] private int currentJumpAmount = 1;

    private bool isHoldingJump = false;
    private float maxHoldJumpTime = .4f;
    private float holdJumpTimer = .0f;

    private void Start()
    {
        ResetParameters();
    }

    private void ResetParameters()
    {
        maxJumpAmount = startJumpAmount;
        currentJumpAmount = maxJumpAmount;

        currentJumpVelocity = startJumpVelocity;

        distance = 0f; 
    }

    private void Update()
    {
        if (isGrounded)
        {
            currentJumpAmount = maxJumpAmount;

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                isGrounded = false;
                velocity.y = currentJumpVelocity;
                currentJumpAmount--;
                isHoldingJump = true;
            }
        }
        else if ( (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && currentJumpAmount > 0)
        {
            velocity.y = currentJumpVelocity;
            currentJumpAmount--;
        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            isHoldingJump = false;
        }
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        if (!isGrounded)
        {
            if (isHoldingJump)
            {
                holdJumpTimer += Time.fixedDeltaTime;
                if (holdJumpTimer >= maxHoldJumpTime)
                    isHoldingJump = false;
            }

            pos.y += velocity.y * Time.fixedDeltaTime;
            
            if (!isHoldingJump)
                velocity.y += gravity * Time.fixedDeltaTime;

            if (pos.y <= groundHeight)
            {
                pos.y = groundHeight;
                isGrounded = true;
                holdJumpTimer = .0f;
            }
        }
        else
        {
            float velocyRatio = velocity.x / maxVelocity;
            currentAcceleration = maxAcceleration * (1 - velocyRatio);
            if (velocity.x < maxVelocity)
                velocity.x += currentAcceleration * Time.fixedDeltaTime;
        }

        distance += velocity.x * Time.fixedDeltaTime;

        transform.position = pos;
    }
}
