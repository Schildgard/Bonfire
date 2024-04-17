using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionScript : MonoBehaviour
{

    private Rigidbody playerRigidbody;
    private CollisionDetection collisionDetection;

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


    [SerializeField]private Vector2 Input;
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


    [SerializeField] private Vector2 cameraInput;
    public Vector2 CameraInput 
    {
        get { return cameraInput; }
        set { cameraInput = value; }
    }
     private Vector2 cameraVector;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        collisionDetection = GetComponent<CollisionDetection>();

    }

    // Update is called once per frame
    void Update()
    {
        Walk();
        Run();
        currentDashCoolDown = Mathf.Clamp(currentDashCoolDown - Time.deltaTime, 0, maxDashCooldown);
        Camera();
    }


    private void Walk()
    {
        MovementVector.x = Input.x;
        MovementVector.z = Input.y;
        MovementVector.Normalize();
        MovementVector *= currentWalkSpeed;
        MovementVector.y = playerRigidbody.velocity.y;

        SmoothMovement = Vector3.Lerp(playerRigidbody.velocity, MovementVector, lerpSpeed * Time.deltaTime);

        playerRigidbody.velocity = new Vector3(SmoothMovement.x, playerRigidbody.velocity.y, SmoothMovement.z);
    }

    private void Run()
    {
        CurrentWalkSpeed = Mathf.Clamp(CurrentWalkSpeed + (accelerationMultiplier * walkSpeedAcceleration) * Time.deltaTime, normalWalkSpeed, maxRunSpeed);
    }
    public void Camera() 
    {
        cameraVector = cameraInput;
    }

    public void WalkEvent(InputAction.CallbackContext _context)
    {
        Input = _context.ReadValue<Vector2>();

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
            playerRigidbody.AddForce(new Vector3(playerRigidbody.velocity.x,1 * jumpPower, playerRigidbody.velocity.z), ForceMode.Impulse);
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

    public void TestCamInput(InputAction.CallbackContext _context) 
    { 
        CameraInput = _context.ReadValue<Vector2>();
      
    }



}
