using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Parsed;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rb;
    [SerializeField] private float moveSpeed = 2f;

    [Header("Ground Check")]
    bool grounded;
    public float groundDrag = 5;

    private CapsuleCollider playerCollider;

    Vector3 moveDirection;
    private Vector3 input;
    public GameObject cameraPivot;
    private GameObject camerObject;

    [Header("Slope Settings")]
    public float maxSlopeAngle = 45;
    public float minSlopeAngle = 2;
    private RaycastHit slopeHit;

    float horizontalMovement;
    float verticalMovement;
    
    void Start() {
        rb = GetComponent<Rigidbody>();
        camerObject = cameraPivot.transform.GetChild(0).gameObject;
        playerCollider = GetComponent<CapsuleCollider>();
    }
    void Update()
    {
        CameraFollow();
        
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
    }
    void FixedUpdate() {

        Vector3 forwardDirection = new Vector3(camerObject.transform.forward.x, 0f, camerObject.transform.forward.z);
        Vector3 rightDirection = new Vector3(camerObject.transform.right.x, 0f, camerObject.transform.right.z);
        input = new Vector3(horizontalMovement, 0, verticalMovement);

        moveDirection = forwardDirection * verticalMovement + rightDirection * horizontalMovement;
        moveDirection.Normalize();

        rb.useGravity = !OnSlope();

        grounded = Physics.Raycast(transform.position, Vector3.down, playerCollider.height * 0.5f + 0.5f);
        if (!grounded) rb.drag = 0; else {rb.drag = groundDrag;}
        rb.MovePosition(transform.position + GetSlopeMoveDirection() * moveSpeed * Time.deltaTime);
    }

    private bool OnSlope() {Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerCollider.height * 0.5f + 0.5f);
        Debug.DrawRay(transform.position, -slopeHit.normal);
        var angle = Vector3.Angle(Vector3.up, slopeHit.normal);
        return angle < maxSlopeAngle && angle != 0;
    }

    private Vector3 GetSlopeMoveDirection() {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

    void CameraFollow() {
        cameraPivot.transform.position = transform.position;
    }
}
