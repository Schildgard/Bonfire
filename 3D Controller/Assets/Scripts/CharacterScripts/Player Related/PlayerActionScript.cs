using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerActionScript : MonoBehaviour
{
    private StaminaScript Stamina;
    public float staminaExhaustion;
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

    #region NewWalkUsing BlendTree


    private float velocity;
    [SerializeField] float acceleration;
    [SerializeField] private float deceleration;
    private int velocityHash;
    private float accelerationThreshold = 0.4f;
    #endregion

    #region NewWalk2DBlendTree

    float velocityZ;
    float velocityX;

    private bool runButtonPressed;

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
        Stamina = GetComponent<StaminaScript>();

        velocityHash = Animator.StringToHash("Velocity");


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

    private void NewWalkUsingBlendTree()
    {// NO USE
        if (MoveInput.x > 0.1f || MoveInput.x < -0.1f || MoveInput.y > 0.1f || MoveInput.y < -0.1f)
        {
            if (velocity <= accelerationThreshold)
            {
                velocity += Time.deltaTime * acceleration;
            }
            else velocity -= Time.deltaTime * deceleration;

        }

        else if (velocity > 0)
        {
            velocity -= Time.deltaTime * deceleration;
        }

        Animator.SetFloat(velocityHash, velocity);
    }

    private void NewWalk2DBlendTree()
    { // NO USE

        // Forward Movement (MoveInput equals the Vector2 Parameter of the Walk Event)
        if (MoveInput.y > 0.1f && velocityZ < 0.5f && !runButtonPressed)
        {
            Debug.Log("Acceleration");
            velocityZ += Time.deltaTime * acceleration;
        }
        if (MoveInput.y < -0.1f && velocityZ < 0.5f && !runButtonPressed)
        {
            velocityZ += Time.deltaTime * acceleration;
        }
        //Sidewards
        if (MoveInput.x > 0.1f && velocityX < 0.5f && !runButtonPressed)
        {
            velocityZ += Time.deltaTime * acceleration;
        }
        if (MoveInput.x < -0.1f && velocityX > -0.5f && !runButtonPressed)
        {
            velocityZ -= Time.deltaTime * acceleration;
        }





        //Sidewards
        if (MoveInput.x > 0.1f && velocityX < 0.5f && !runButtonPressed && lockOn)
        {
            velocityX += Time.deltaTime * acceleration;
        }
        if (MoveInput.x < -0.1f && velocityX > -0.5f && !runButtonPressed && lockOn)
        {
            velocityX -= Time.deltaTime * acceleration;
        }


        // SlowDown and Stop Movement

        //Forward
        if (MoveInput.y < 0.1f && velocityZ > 0 && MoveInput.y > -0.1f && velocityZ > 0)
        {
            Debug.Log("Deceleration");
            velocityZ -= Time.deltaTime * acceleration;
        }
        if (MoveInput.y < 0.1f && velocityZ < 0 && MoveInput.y > -0.1f && velocityZ < 0)
        {
            velocityZ = 0;
        }

        //sidewards
        if (MoveInput.x < 0.1f && velocityX > 0 && MoveInput.x > -0.1f && velocityX > 0 && lockOn)
        {
            velocityX -= Time.deltaTime * acceleration;
        }
        if (MoveInput.x < 0.1f && velocityX < 0 && MoveInput.x > -0.1f && velocityX < 0 && lockOn)
        {
            velocityX = 0;
        }


        Animator.SetFloat("Velocity Z", velocityZ);
        Animator.SetFloat("Velocity X", velocityX);



    }

    private void AnimationOnlyBlendTree2D()
    { // NO USE
        //Vertical Movement Acceleration
        if (MoveInput.y > 0.1f && velocityZ < 0.1f && !runButtonPressed)
        {
            velocityZ += Time.deltaTime * acceleration;
        }
        if (MoveInput.y < -0.1f && velocityZ < 0.1f && !runButtonPressed)
        {
            velocityZ += Time.deltaTime * acceleration;
        }
        //Vertical Movement Deceleration
        if (MoveInput.y < 0.1f && velocityZ > 0 && MoveInput.y > -0.1f)
        {
            velocityZ -= Time.deltaTime * acceleration;
        }
        if (MoveInput.y < 0.1f && velocityZ < 0 && MoveInput.y > -0.1f)
        {
            velocityZ = 0;
        }


        //Horizontal Acceleration
        if (MoveInput.x > 0.1f && velocityX < 0.1f && !runButtonPressed)
        {
            velocityX += Time.deltaTime * acceleration;
        }
        if (MoveInput.x < -0.1f && velocityX > -0.1f && !runButtonPressed)
        {
            velocityX -= Time.deltaTime * acceleration;
        }
        //Horizontal Deceleraiton
        if (MoveInput.x < 0.1f && velocityX > 0 && MoveInput.x > -0.1f)
        {
            velocityX -= Time.deltaTime * acceleration;
        }
        if (MoveInput.x < 0.1f && velocityX < 0 && MoveInput.x > -0.1f)
        {
            velocityX += Time.deltaTime * acceleration;
        }

        Animator.SetFloat("Velocity Z", velocityZ);
        Animator.SetFloat("Velocity X", velocityX);
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
        if (acceleration > 0) 
        {
            Stamina.CurrentStamina -= staminaExhaustion * Time.deltaTime;
            if (Stamina.CurrentStamina < 0) 
            {
                Stamina.CurrentStamina = 0;
            }
        }
    }

    private void LockOn(Vector3 _target)
    {
        LockOnCamera.gameObject.SetActive(true);
        LockOnCamera.transform.LookAt(_target);
    }

    public void Attack()
    {
        if (Stamina.CurrentStamina > 30)
        {
            Animator.SetTrigger("Attack Trigger");
            Stamina.CurrentStamina -= 30;
            Stamina.UpdateStaminaBar();
        }
        else Debug.Log("Not Enough Stamina");
    }
    public void Block()
    {

        if (blockInput > 0)
        {
            Debug.Log("Block");
        }

    }

    public void RestAtFire() 
    {
        Animator.SetTrigger("Resting");
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
            
            runButtonPressed = true;
            //accelerationThreshold = 1f;

            Animator.SetBool("isRunning", true);


        }

        if (_context.canceled)
        {
            accelerationMultiplier = -1;
             runButtonPressed = false;
            //accelerationThreshold = 0.4f;
            Animator.SetBool("isRunning", false);
        }
    }

    public void JumpEvent(InputAction.CallbackContext _context)
    {
        if (_context.started && collisionDetection.CollisionCheck())
        {
            Animator.SetTrigger("Jump");
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, 0, playerRigidbody.velocity.z);
            playerRigidbody.AddForce(new Vector3(playerRigidbody.velocity.x, 1 * jumpPower, playerRigidbody.velocity.z), ForceMode.Impulse);
            
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
