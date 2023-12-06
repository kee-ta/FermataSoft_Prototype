using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.ProBuilder;

public class SimplePlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Settings")]
    public bool showDebug = true;

    [Header("Player")]
    public float playerHeight = 2f;

    [Header("Movement")]
    public float speed = 5f;
    public float maxSlopeAngle = 20f;
    public float groundCheckDepth = 0.5f;
    public float slowdownDrag = 10f;
    float slopeAngle;
    Vector3 moveDir = Vector3.zero;
    float horizontalInput;
    float verticalInput;
    RaycastHit hit;
    bool groundCheck = false;
    float groundDrag = 5;
    float airDrag = 0;
    RigidbodyConstraints lockedConstraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    RigidbodyConstraints unlockedConstraints = RigidbodyConstraints.FreezeRotation;

    [Header("Camera")]
    public Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update() 
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // cast ray to floor, and also use that hit as grounded check
        // RaycastHit hit;
        groundCheck = Physics.Raycast(transform.position, -transform.up, out hit, playerHeight * 0.5f + groundCheckDepth);
        slopeAngle = Vector3.Angle(transform.up, hit.normal);
        // adjust move direction to match slope normal
        moveDir = Vector3.ProjectOnPlane(cameraTransform.right, hit.normal) * horizontalInput + Vector3.ProjectOnPlane(cameraTransform.forward, hit.normal) * verticalInput;
        moveDir.Normalize();
        // set drag if grounded, and no drag if in air
        if (groundCheck) rb.drag = groundDrag; else rb.drag = airDrag;
        // add proportional drag when idle to prevent sliding
        if (slopeAngle < maxSlopeAngle && slopeAngle != 0 && moveDir.magnitude == 0) rb.drag = slopeAngle * 4;
        if (slopeAngle == 0 && moveDir.magnitude == 0) rb.drag = 10;
        // move
        rb.AddForce(moveDir * speed * 20f, ForceMode.Force);
        // limit velocity
        if (rb.velocity.magnitude > speed) { rb.velocity = rb.velocity.normalized * speed; }

        if (showDebug) 
        {
            Debug.Log(rb.velocity.magnitude);
            Debug.DrawRay(transform.position, moveDir, Color.white);
            Debug.DrawRay(transform.position, rb.velocity, Color.blue);
            Debug.DrawRay(transform.position, -rb.velocity, Color.red);
        }
    }

    void OnDrawGizmos() {
        if (showDebug) 
        {
            RaycastHit debugHit;
            Physics.Raycast(transform.position, -transform.up, out debugHit, playerHeight * 0.5f + groundCheckDepth);
            Debug.DrawRay(transform.position, Vector3.ProjectOnPlane(cameraTransform.forward, debugHit.normal), Color.red);
            Debug.DrawRay(transform.position, Vector3.ProjectOnPlane(cameraTransform.right, debugHit.normal), Color.green);
            Debug.DrawRay(transform.position, -Vector3.ProjectOnPlane(cameraTransform.forward, debugHit.normal), Color.green);
            Debug.DrawRay(transform.position, -Vector3.ProjectOnPlane(cameraTransform.right, debugHit.normal), Color.green);
        }
    }
}
