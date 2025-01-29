using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
   
    public bool lostgame = false;

    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

   /* public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;*/

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    Vector3 groundNormal = Vector3.zero;

    Rigidbody rb;

    public ParticleSystem loseFire;
    public GameObject loseFireLight;

    public bool isFrozen;

    private void Start()
    {
        isFrozen = false;
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;

        //readyToJump = true;
    }

    private void Update()
    {
        if (isFrozen)
        {
            return;
        }

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.75f, whatIsGround);
        SpeedControl();
        MyInput();

        if (grounded)
        {
            rb.linearDamping = groundDrag;
            // Store ground normal for movement calculations
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, playerHeight * 0.5f + 0.75f, whatIsGround))
            {
                groundNormal = hit.normal;
            }
        }
        else
        {
            rb.linearDamping = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer(groundNormal);
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        /*if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }*/
    }

    private void MovePlayer(Vector3 groundNormal)
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        Vector3 projectedMoveDir = Vector3.ProjectOnPlane(moveDirection, groundNormal).normalized;

        // Calculate the dot product
        float dotProduct = Vector3.Dot(-orientation.forward, groundNormal);
        float slopeMultiplier = 1.0f; // Default multiplier

        // Only apply the multiplier if the player is facing up the slope
        if (dotProduct > 0.2f) // Adjust the threshold as needed
        {
            slopeMultiplier = 1.5f; // Increase movement speed on slopes
        }

        if (grounded)
        {
            // Check if there's no input
            if (horizontalInput == 0 && verticalInput == 0)
            {
                // Zero out velocity if on a slope and not moving
                rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            }
            else
            {
                rb.linearVelocity = new Vector3(projectedMoveDir.x * moveSpeed * slopeMultiplier, rb.linearVelocity.y, projectedMoveDir.z * moveSpeed * slopeMultiplier);
            }
        }
        else
        {
            //rb.AddForce(projectedMoveDir * moveSpeed * airMultiplier, ForceMode.Acceleration);
            rb.AddForce(projectedMoveDir * moveSpeed * 1, ForceMode.Acceleration);
        }
    }


    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        //rb.AddForce(transform.up * jumpForce, ForceMode.Impulse)
        rb.AddForce(transform.up * 1, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        //readyToJump = true;
    }

    public void setFire()
    {
        Debug.Log("Set Fire PlayerMovement");
        loseFire.Play();
        loseFireLight.gameObject.SetActive(true);
        
        lostgame = true;
    }
}
