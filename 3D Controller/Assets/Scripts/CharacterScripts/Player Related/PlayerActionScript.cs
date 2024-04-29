using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionScript : MonoBehaviour
{

    private Rigidbody playerRigidbody;
    private GroundCheck collisionDetection;
    private Animator Animator;

    #region Walk
    [SerializeField] private float normalWalkSpeed;
    public float NormalWalkSpeed
    {
        get { return normalWalkSpeed; }
        set { normalWalkSpeed = value; }
    }
    [SerializeField] private float currentWalkSpeed;
    public float CurrentWalkSpeed
    {
        get { return currentWalkSpeed; }
        set { currentWalkSpeed = value; }

    }

    [SerializeField] private float lerpSpeed;


    [SerializeField] private Vector2 MoveInput;
    private Vector3 SmoothMovement;
    private Vector3 MovementVector;
    #endregion

    #region Run
    [SerializeField] private float maxRunSpeed;
    [SerializeField] private float walkSpeedAcceleration;

    private float accelerationMultiplier;
    #endregion

    #region Jump
    [SerializeField] private float jumpPower;
    #endregion

    #region Dash
    [SerializeField] private float dashPower;

    [SerializeField] private float currentDashCoolDown;
    [SerializeField] private float maxDashCooldown;
    #endregion

    #region Rotation
    [SerializeField] private Transform Camera;
    [SerializeField] private float rotationSpeed;
    private Vector3 TargetRotationDirection;

    #endregion

    #region NormalCamera

    private Vector2 cameraInput;
    public Vector2 CameraInput
    {
        get { return cameraInput; }
        set { cameraInput = value; }
    }
    public bool cameraInputActivated;
    #endregion

    #region LockOn Camera
    [SerializeField] private Camera LockOnCamera;
    [SerializeField] private Transform TestEnemy;
    [SerializeField] private bool lockOn;
    #endregion

    private Transform activeCamera;
    private float blockInput;


    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        collisionDetection = GetComponent<GroundCheck>();
        Animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        Walk(activeCamera);
        Run();
        Rotate();
        Block();
        currentDashCoolDown = Mathf.Clamp(currentDashCoolDown - Time.deltaTime, 0, maxDashCooldown);
        if (lockOn) { LockOn(TestEnemy.position); }
    }

    private void Walk(Transform _activeCameraTransform)
    {
         if (!lockOn) 
         {
            _activeCameraTransform = Camera.transform;
         }
         else 
         {
            _activeCameraTransform = LockOnCamera.transform;
         }

        MovementVector = _activeCameraTransform.transform.forward * MoveInput.y;
        MovementVector = MovementVector + _activeCameraTransform.transform.right * MoveInput.x;
        MovementVector.Normalize();

        MovementVector *= currentWalkSpeed;
        MovementVector.y = playerRigidbody.velocity.y;

        SmoothMovement = Vector3.Lerp(playerRigidbody.velocity, MovementVector, lerpSpeed * Time.deltaTime);

        playerRigidbody.velocity = new Vector3(SmoothMovement.x, playerRigidbody.velocity.y, SmoothMovement.z);

        if (MovementVector.x > 0.1f || MovementVector.z > 0.1f || MovementVector.x < -0.1f || MovementVector.z < -0.1f)
        {
            Animator.SetBool("isWalking", true);
        }
        else
        {
            Animator.SetBool("isWalking", false);

        }
    }

    private void Rotate()
    {

        if (!lockOn)
        {
            TargetRotationDirection = Camera.transform.forward * MoveInput.y;
            TargetRotationDirection += Camera.transform.right * MoveInput.x;
        }
        else 
        {
            TargetRotationDirection = LockOnCamera.transform.forward;
        }

            TargetRotationDirection.Normalize();
            TargetRotationDirection.y = 0;
            if (TargetRotationDirection == Vector3.zero) //Comparison between two Vectors works in that Case, because the Values of TargetRotation are bound to Input System.
            {
                TargetRotationDirection = transform.forward;
            }

            Quaternion turnRotation = Quaternion.LookRotation(TargetRotationDirection);
            Quaternion newRotation = Quaternion.Slerp(transform.rotation, turnRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = newRotation;
    }

    private void Run()
    {
        CurrentWalkSpeed = Mathf.Clamp(CurrentWalkSpeed + (accelerationMultiplier * walkSpeedAcceleration) * Time.deltaTime, normalWalkSpeed, maxRunSpeed);
    }

    private void LockOn(Vector3 _target)
    {
        LockOnCamera.gameObject.SetActive(true);
        LockOnCamera.transform.LookAt(_target);
    }

    public void Attack()
    {
        Animator.SetTrigger("Attack Trigger");
    }
    public void Block()
    {

        if (blockInput > 0)
        {
            Debug.Log("Block");
        }

    }




    #region InputCallBackEvents
    public void WalkEvent(InputAction.CallbackContext _context)
    {
        MoveInput = _context.ReadValue<Vector2>();

    }

    public void RunEvent(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            accelerationMultiplier = 1;
        }

        if (_context.canceled)
        {
            accelerationMultiplier = -1;
        }
    }

    public void JumpEvent(InputAction.CallbackContext _context)
    {
        if (_context.started && collisionDetection.CollisionCheck())
        {
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, 0, playerRigidbody.velocity.z);
            playerRigidbody.AddForce(new Vector3(playerRigidbody.velocity.x, 1 * jumpPower, playerRigidbody.velocity.z), ForceMode.Impulse);
            Debug.Log("Jump");
        }


    }


    public void DashEvent(InputAction.CallbackContext _context)
    {
        if (_context.started && currentDashCoolDown <= 0)
        {
            playerRigidbody.AddForce(new Vector3(MovementVector.x, 0, MovementVector.z) * dashPower, ForceMode.Impulse);
            currentDashCoolDown = maxDashCooldown;
        }
    }

    public void AttackEvent(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            Attack();
        }


    }

    public void BlockEvent(InputAction.CallbackContext _context)
    {
        blockInput = _context.ReadValue<float>();
    }

    public void CallCameraInput(InputAction.CallbackContext _context)
    {
        if (cameraInputActivated)
        {
            CameraInput = _context.ReadValue<Vector2>();
        }
        else CameraInput = Vector3.zero;

    }

    public void AllowCameraInput(InputAction.CallbackContext _context)
    {

        if (_context.started)
        {
            cameraInputActivated = true;
        }

        if (_context.canceled)
        {
            cameraInputActivated = false;
        }
    }

    public void CameraLockOnEvent(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            lockOn = !lockOn;
            LockOnCamera.gameObject.SetActive(lockOn);
        }

    }


    #endregion

}
