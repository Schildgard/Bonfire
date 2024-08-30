using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionScript : MonoBehaviour
{
    private StaminaScript stamina;
    private Rigidbody playerRigidbody;
    private Animator animator;
    private Spelllist spelllist;
    private bool movementIsBlocked;
    private Camera mainCamera;

    #region Walk
    [Header("Walking Parameters")]
    [SerializeField] private float normalWalkSpeed;
    [SerializeField] private float currentWalkSpeed;
    [SerializeField] private float lerpSpeed;

    private Vector2 moveInput;
    private Vector3 smoothMovement;
    private Vector3 movementVector;

    public Vector3 MovementVector { get { return movementVector; } } //Unnötig?
    #endregion

    #region Run
    [Header("Running Parameters")]
    [SerializeField] private float maxRunSpeed;
    [SerializeField] private float walkSpeedAcceleration;
    [SerializeField] private float staminaExhaustion;

    private float runningThreshold = 2f;
    private bool runButtonPressed;
    private float accelerationMultiplier;
    #endregion

    #region BlendTree Parameters
    [Header("Blend Tree Parameters")]
    [SerializeField] private float blendTreeAcceleration;
    [SerializeField] private float blendTreeDecceleration;

    private float velocityX;
    private float velocityZ;

    private int velocityHashX;
    private int velocityHashZ;

    private float maxVelocity;
    #endregion

    [SerializeField] private WeaponScript currentWeapon;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        stamina = GetComponent<StaminaScript>();
        spelllist = GetComponent<Spelllist>();

        mainCamera = Camera.main;

        velocityHashX = Animator.StringToHash("VelocityX");
        velocityHashZ = Animator.StringToHash("VelocityZ");


    }

    void Update()
    {
        animator.SetFloat(velocityHashX, velocityX);
        animator.SetFloat(velocityHashZ, velocityZ);
        CalculateMovement();

        CalculateRunning();
    }



    private void CalculateMovement()

    {
        movementVector = mainCamera.transform.forward * moveInput.y;
        movementVector += mainCamera.transform.right * moveInput.x;

        movementVector.Normalize();

        movementVector *= currentWalkSpeed;
        movementVector.y = playerRigidbody.velocity.y;

        smoothMovement = Vector3.Lerp(playerRigidbody.velocity, movementVector, lerpSpeed * Time.deltaTime);

        if (!movementIsBlocked)
        {
            playerRigidbody.velocity = new Vector3(smoothMovement.x, playerRigidbody.velocity.y, smoothMovement.z);
        }
        if (moveInput.x > 0.01f || moveInput.x < -0.01f)
        {
            velocityX = Mathf.Clamp(velocityX + Time.deltaTime * blendTreeAcceleration, velocityX, maxVelocity);
        }
        else
        {
            velocityX = Mathf.Clamp(velocityX - Time.deltaTime * blendTreeDecceleration, 0f, velocityX);
        }
        if (moveInput.y > 0.01f || moveInput.y < -0.01f)
        {
            velocityZ = Mathf.Clamp(velocityZ + Time.deltaTime * blendTreeAcceleration, velocityZ, maxVelocity);
        }
        else
        {
            velocityZ = Mathf.Clamp(velocityZ - Time.deltaTime * blendTreeDecceleration, 0f, velocityZ);
        }
    }

    private void MoveCharacter()
    {
        if (!movementIsBlocked)
        {
            playerRigidbody.velocity = new Vector3(smoothMovement.x, playerRigidbody.velocity.y, smoothMovement.z);
        }
    }
    private void CalculateRunning()
    {
        if (runButtonPressed && stamina.CurrentStamina > 0)
        {
            if (playerRigidbody.velocity.x > runningThreshold || playerRigidbody.velocity.x < -runningThreshold ||
                playerRigidbody.velocity.z > runningThreshold || playerRigidbody.velocity.z < -runningThreshold)
            {
                accelerationMultiplier = 1;
                stamina.CurrentStamina -= staminaExhaustion * Time.deltaTime;
                maxVelocity = 2f;
            }
        }
        else
        {
            accelerationMultiplier = -1;
            maxVelocity = Mathf.Clamp(maxVelocity - Time.deltaTime * blendTreeDecceleration, 0.5f, maxVelocity);
        }

        currentWalkSpeed = Mathf.Clamp(currentWalkSpeed + (accelerationMultiplier * walkSpeedAcceleration) * Time.deltaTime, normalWalkSpeed, maxRunSpeed);


    }

    public void Attack()
    {
        if (stamina.CurrentStamina > 1f)
        {
            animator.SetTrigger("Attack Trigger");
        }
        else Debug.Log("Not Enough Stamina");
    }

    public void RestAtFire()
    {
        animator.SetTrigger("Resting");
    }


    public void PlaySFX(int _index)
    {
        if (AudioManager.instance == null)
        {
            Debug.Log("Player ActionScript tried to play an Sound Effect, but the AudioManager is Null. Please check if there is an AudioManager in the Scene, and if so, if there is an SFX List of Sound Effects");
            return;
        }
        AudioManager.instance.EnvironmentalSFX[_index].source.Play();
    }

    public void DrainStamina()
    {
        stamina.DrainStamina(currentWeapon.StaminaAttackCost);
    }

    public void BlockMovement()
    {
        if (movementIsBlocked) { return; }
        movementIsBlocked = true;
    }

    public void UnblockMovement()
    {
        if (!movementIsBlocked) { return; }
        movementIsBlocked = false;
    }

    public void ActivateRootMotion()
    {
        animator.applyRootMotion = true;
    }

    public void DeactivateRootMotion()
    {
        animator.applyRootMotion = false;
    }

    #region InputCallBackEvents
    public void WalkEvent(InputAction.CallbackContext _context)
    {
        moveInput = _context.ReadValue<Vector2>();
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


    public void AttackEvent(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            Attack();
        }
    }

    public void CastSpellEvent(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            int spellIndex = (int)_context.ReadValue<float>();  // ReadValue returns a float, the Num =InputMap has a ScaleFactor, according to its spellIndex in the spelllist, so by pressing 3, this line retuns: spellindex =  1 x 2 = 2. So Spell of Index 2 is selected.
            spelllist.CastSpell(spellIndex);
            //To prevent a wrong InputEvent applied to this Method, I could set Index 1 an Placeholder, which returns an Debug. Since no Scale factor is applied, the Index of a non cast spell will be 1.
        }
    }




    #endregion

}
