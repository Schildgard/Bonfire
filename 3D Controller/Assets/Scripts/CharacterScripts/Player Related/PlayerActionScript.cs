using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionScript : MonoBehaviour
{
    private StaminaScript Stamina;
    private Rigidbody playerRigidbody;
    private GroundCheck collisionDetection;
    private Animator Animator;
    private Spelllist Spelllist;
    private bool blockMovement;

    #region Walk
    [Header("Walking Parameters")]
    [SerializeField] private Camera MainCamera;
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


    private Vector2 MoveInput;
    private Vector3 SmoothMovement;
    private Vector3 movementVector;

    public Vector3 MovementVector { get { return movementVector; } }
    #endregion

    #region Run
    [Header("Running Parameters")]
    [SerializeField] private float maxRunSpeed;
    [SerializeField] private float walkSpeedAcceleration;
    [SerializeField] private float staminaExhaustion;

    private bool runButtonPressed;
    private float accelerationMultiplier;
    #endregion
    #region Jump
    [Header("Jump Parameters")]
    [SerializeField] private float jumpPower;
    #endregion

    #region Dash
    [Header("Dash Parameters")]
    [SerializeField] private float dashPower;

    [SerializeField] private float currentDashCoolDown;
    [SerializeField] private float maxDashCooldown;
    #endregion

    [SerializeField] private WeaponScript currentWeapon;


    //Wasted
    private float blockInput;

    private PlayerRotation RotationScript;



    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        collisionDetection = GetComponent<GroundCheck>();
        Animator = GetComponent<Animator>();
        Stamina = GetComponent<StaminaScript>();
        Spelllist = GetComponent<Spelllist>();

        RotationScript = GetComponent<PlayerRotation>();


    }

    // Update is called once per frame
    void Update()
    {

        if (!blockMovement)
        {
            Walk();
        }
        Run();

        Block();

        //currentDashCoolDown = Mathf.Clamp(currentDashCoolDown - Time.deltaTime, 0, maxDashCooldown);
    }



    private void Walk()
    {
        movementVector = MainCamera.transform.forward * MoveInput.y;
        movementVector += MainCamera.transform.right * MoveInput.x;


        movementVector.Normalize();

        movementVector *= currentWalkSpeed;
        movementVector.y = playerRigidbody.velocity.y;

        SmoothMovement = Vector3.Lerp(playerRigidbody.velocity, movementVector, lerpSpeed * Time.deltaTime);

        playerRigidbody.velocity = new Vector3(SmoothMovement.x, playerRigidbody.velocity.y, SmoothMovement.z);


        if (!RotationScript.LockOn)
        {

            if (movementVector.x > 0.1f || movementVector.z > 0.1f || movementVector.x < -0.1f || movementVector.z < -0.1f)
            {
                Animator.SetBool("isWalking", true);
            }
            else
            {
                Animator.SetBool("isWalking", false);

            }
        }

        else // if Lock On
        {
            if (MoveInput.x > 0 && MoveInput.y == 0)
            {
                Animator.SetBool("rightStrafe", true);
                Animator.SetBool("leftStrafe", false);
            }
            else if (MoveInput.x < 0 && MoveInput.y == 0)
            {
                Animator.SetBool("leftStrafe", true);
                Animator.SetBool("rightStrafe", false);
            }
            else if (MoveInput.y > 0 || MoveInput.y < 0)
            {
                Animator.SetBool("isWalking", true);
                Animator.SetBool("rightStrafe", false);
                Animator.SetBool("leftStrafe", false);
            }
            else
            {
                Animator.SetBool("isWalking", false);
                Animator.SetBool("rightStrafe", false);
                Animator.SetBool("leftStrafe", false);
            }
        }
    }


    private void Run()
    {

        if (playerRigidbody.velocity.x > 0.1f || playerRigidbody.velocity.x < -0.1f || playerRigidbody.velocity.z > 0.1f || playerRigidbody.velocity.z < -0.1f)
        {



            if (runButtonPressed && Stamina.CurrentStamina > 0)
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
            //Stamina.CurrentStamina = 0;
        }
        CurrentWalkSpeed = Mathf.Clamp(CurrentWalkSpeed + (accelerationMultiplier * walkSpeedAcceleration) * Time.deltaTime, normalWalkSpeed, maxRunSpeed);


    }

    public void Attack()
    {
        if (Stamina.CurrentStamina > 1f)
        {
            Animator.SetTrigger("Attack Trigger");
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

    public void AttackDrainStamina()
    {
        Stamina.DrainStamina(currentWeapon.StaminaAttackCost);
       // Stamina.CurrentStamina -= currentWeapon.StaminaAttackCost;
       // Stamina.UpdateStaminaBar();
    }

    public void BlockMovement()
    {
        blockMovement = true;
    }

    public void UnblockMovement()
    {
        blockMovement = false;
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




    #endregion

}
