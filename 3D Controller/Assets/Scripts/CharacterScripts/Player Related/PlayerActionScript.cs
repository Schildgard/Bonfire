using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerActionScript : MonoBehaviour
{
    private StaminaScript Stamina;
    private Rigidbody playerRigidbody;
    private GroundCheck collisionDetection;
    private Animator Animator;
    private Spelllist Spelllist;
    private AbilityBar AbilityBar;

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
    [SerializeField] private float staminaExhaustion;

    private bool runButtonPressed;
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
    // private GameObject ActiveSpell;
    // private GameObject ActiveSpell2;
    [SerializeField] private Transform SpawnPosition;


    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        collisionDetection = GetComponent<GroundCheck>();
        Animator = GetComponent<Animator>();
        Stamina = GetComponent<StaminaScript>();
        Spelllist = GetComponent<Spelllist>();
        AbilityBar = GetComponentInChildren<AbilityBar>();


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

        if (playerRigidbody.velocity.x > 0.1f || playerRigidbody.velocity.x < -0.1f || playerRigidbody.velocity.z > 0.1f || playerRigidbody.velocity.z < -0.1f)
        {



            if (runButtonPressed && Stamina.CurrentStamina > 1)
            {
                accelerationMultiplier = 1;
                Stamina.CurrentStamina -= staminaExhaustion * Time.deltaTime;
                Animator.SetBool("isRunning", true);
            }
            else
            {
                accelerationMultiplier = -1;
                Animator.SetBool("isRunning", false);
            }
        }
        else
        {
            accelerationMultiplier = -1;
            Animator.SetBool("isRunning", false);
        }

        if (Stamina.CurrentStamina < 0)
        {
            Stamina.CurrentStamina = 0;
        }
        CurrentWalkSpeed = Mathf.Clamp(CurrentWalkSpeed + (accelerationMultiplier * walkSpeedAcceleration) * Time.deltaTime, normalWalkSpeed, maxRunSpeed);


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


    public void PlaySFX(int _index)
    {
        AudioManager.instance.SFX[_index].source.Play();
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
            runButtonPressed = true;
        }


        if (_context.canceled)
        {
            runButtonPressed = false;
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


    public void CastSpellEvent(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            int spellIndex = (int)_context.ReadValue<float>();  // ReadValue returns a float (1 =pressed, 0 = not pressed), the Num =InputMap has a ScaleFactor, according to its spellIndex in the spelllist, so by pressing 3, this line retuns: spellindex =  1 x 2 = 2. So Spell of Index 2 is selected.

            Spelllist.CastSpell(spellIndex);

            //To prevent a wrong InputEvent applied to this Method, I could set Index 1 an Placeholder, which returns an Debug. Since no Scale factor is applied, the Index of a non cast spell will be 1.
        }
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
