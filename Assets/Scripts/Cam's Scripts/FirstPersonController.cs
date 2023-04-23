using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class FirstPersonController : MonoBehaviour
{
    /// <summary>
    /// Boolean based on whether player can move or not
    /// </summary>
    public bool CanMove { get; set; } = true;
    // Check player can sprint, and sprint key is held

    /// <summary>
    /// Boolean based on whether player is currently sprinting or not
    /// </summary>
    private bool IsSprinting => canSprint && Input.GetKey(sprintKey);
    // Check player's on the ground, and jump key is pressed

    /// <summary>
    /// Boolean based on whether player should be able to jump, or not
    /// </summary>
    private bool ShouldJump => Input.GetKeyDown(jumpKey) && characterController.isGrounded;
    // Check player's on the ground, not during their crouch animation and also crouch key is pressed

    /// <summary>
    /// Boolean based on whether player should be able to crouch or not
    /// </summary>
    private bool ShouldCrouch => Input.GetKeyDown(crouchKey) && !duringCrouchAnim && characterController.isGrounded;

    /// <summary>
    /// Boolean based on whether torch is on or not
    /// </summary>
    private bool TorchOn { get; set; } = false;

    private bool IsDead { get; set; } = false;

    [Header("Functional options")]
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canCrouch = true;
    [SerializeField] private bool canUseHeadBob = true;
    [SerializeField] private bool willSlideOnSlope = true;

    [Header("Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;
    [SerializeField] private KeyCode torchKey = KeyCode.T;

    [Header("Movement parameters")]
    [SerializeField] private float walkSpeed = 4.0f;
    [SerializeField] private float sprintSpeed = 8.0f;
    [SerializeField] private float crouchSpeed = 2f;
    [SerializeField] private float slopeSpeed = 6.0f;

    [Header("Jumping parameters")]
    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField] private float gravity = 30.0f;

    [Header("Crouching parameters")]
    [SerializeField] private float crouchHeight = 1f;
    [SerializeField] private float standingHeight = 2f;
    [SerializeField] private float timeToCrouch = 0.1f;
    [SerializeField] private Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);
    [SerializeField] private Vector3 standingCenter = new Vector3(0, 0, 0);
    private bool isCrouching;
    private bool duringCrouchAnim;

    [Header("Headbob parameters")]
    [SerializeField] private float walkBobSpeed = 14f;
    [SerializeField] private float walkBobAmount = 0.02f;
    [SerializeField] private float sprintBobSpeed = 18f;
    [SerializeField] private float sprintBobAmount = 0.09f;
    [SerializeField] private float crouchBobSpeed = 8f;
    [SerializeField] private float crouchBobAmount = 0.015f;
    private float defaultYPos = 0;
    private float timer;

    [Header("PlayerDeath")]
    [SerializeField] private Canvas crossHair;
    [SerializeField] private Canvas deathScreen;
    private static int currentLives = 0;
    public static bool firstBoot = true;
    private static int maxLives = 3;

    // Torch params
    public float torchBatteryPercent { get; private set; } = 100f;
    private float torchOnTime = 0.0f;
    private float torchOnPeriod = 1f;

    private float torchOffTime = 0.0f;
    private float torchOffPeriod = 1f;

    [SerializeField] private GameObject health1;
    [SerializeField] private GameObject health2;
    [SerializeField] private GameObject health3;

    // Sliding params
    private Vector3 hitPointNormal;

    // Check if angle > slope limit with Raycast
    private bool IsSliding
    {
        get
        {
            if (characterController.isGrounded && Physics.Raycast(transform.position, Vector3.down, out RaycastHit slopeHit, 2f))
            {
                hitPointNormal = slopeHit.normal;
                return Vector3.Angle(hitPointNormal, Vector3.up) > characterController.slopeLimit;
            }
            else
            {
                return false;
            }
        }
    }

    private Camera playerCamera;
    private CharacterController characterController;
    private GameObject torch;

    private Vector3 moveDirection;
    private Vector2 currentInput;

    private GameObject spawnPoint;

    // Called when the component is set to active
    void Awake()
    {
        // Set sprint speed to double the walk speed
        sprintSpeed = walkSpeed * 2;
        // Set params
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        torch = GameObject.Find("Torch");
        defaultYPos = playerCamera.transform.localPosition.y;
        // Set cursor to locked and invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (firstBoot)
        {
            currentLives = maxLives;
            firstBoot = false;
        }
        
    }

    // Called every frame
    void Update()
    {
        // If player can move (not locked in cut scene or 'snare')
        if (CanMove)
        {
            HandleMovementInput();
            if (canJump) HandleJump();
            if (canCrouch) HandleCrouch();
            if (canUseHeadBob) HandleHeadBob();
            ToggleTorch();
            ApplyFinalMovements();
        }

        TorchBattery();
        CheckForEnemies();


        if(currentLives == 3) 
        {
            health1.SetActive(true);
            health2.SetActive(true);
            health3.SetActive(true);
        }
        if (currentLives == 2)
        {
            health1.SetActive(false);
        }

        if (currentLives == 1)
        {
            health1.SetActive(false);
            health2.SetActive(false);
        }
        if(currentLives == 0)
        {
            health1.SetActive(false);
            health2.SetActive(false);
            health3.SetActive(false);
        }
        
    }

    /// <summary>
    /// Movement input handler
    /// </summary>
    private void HandleMovementInput()
    {
        // Get the current input angle as a Vector2
        currentInput = new Vector2((isCrouching ? crouchSpeed : IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Vertical"), (isCrouching ? crouchSpeed : IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Horizontal"));

        float moveDirectionY = moveDirection.y;
        // Set direction to move towards
        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);
        moveDirection.y = moveDirectionY;
    }

    /// <summary>
    /// Jump input handler
    /// </summary>
    private void HandleJump()
    {
        // Just go up
        if (ShouldJump) moveDirection.y = jumpForce;
    }

    /// <summary>
    /// Crouch input handler
    /// </summary>
    private void HandleCrouch()
    {
        if (ShouldCrouch)
            StartCoroutine(CrouchStand());
    }

    /// <summary>
    /// Player walk/sprint/crouch head-bob handler
    /// </summary>
    private void HandleHeadBob()
    {
        if (!characterController.isGrounded) return;

        // Check player is actually moving
        if (Mathf.Abs(moveDirection.x) > 0.1f || Mathf.Abs(moveDirection.z) > 0.1f)
        {
            // Get the timer based on whether player is walking/sprinting/crouching
            timer += Time.deltaTime * (isCrouching ? crouchBobSpeed : IsSprinting ? sprintBobSpeed : walkBobSpeed);
            // Local pos
            playerCamera.transform.localPosition = new Vector3( // Move Camera up and down
                playerCamera.transform.localPosition.x, // Camera's local position X
                defaultYPos + Mathf.Sin(timer) * (isCrouching ? crouchBobAmount : IsSprinting ? sprintBobAmount : walkBobAmount), // change y pos based on state
                playerCamera.transform.localPosition.z); // Camera's local position Y
        }
    }

    /// <summary>
    /// Torch toggling logic
    /// </summary>
    private void ToggleTorch()
    {
        if (Input.GetKeyDown(torchKey))
            TorchOn = !TorchOn;
        torch.GetComponent<Light>().range = TorchOn ? TorchBrightness() : 0;
    }

    /// <summary>
    /// Torch battery logic
    /// </summary>
    private void TorchBattery()
    {
        if (TorchOn)
        {
            torchOnTime += Time.deltaTime;
            if(torchOnTime >= torchOnPeriod)
            {
                torchOnTime = 0f;
                if(torchBatteryPercent > 0.25f)
                    torchBatteryPercent -= 0.5f;
            }
        }

        if (!TorchOn)
        {
            torchOffTime += Time.deltaTime;
            if(torchOffTime >= torchOffPeriod)
            {
                torchOffTime = 0f;
                if(torchBatteryPercent < 100)
                    torchBatteryPercent += 1f;
            }
        }
    }

    /// <summary>
    /// Torch brightness logic
    /// </summary>
    /// <returns>float</returns>
    private float TorchBrightness()
        => torchBatteryPercent >= 50 ? 20 : torchBatteryPercent >= 10 && torchBatteryPercent < 50 ? 10 : 5;

    /// <summary>
    /// Applies all final movements to player
    /// </summary>
    private void ApplyFinalMovements()
    {
        // Gravity check
        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;

        // Slope-slide check
        if (willSlideOnSlope && IsSliding)
            moveDirection += new Vector3(hitPointNormal.x, -hitPointNormal.y, hitPointNormal.z) * slopeSpeed;

        // Move player
        characterController.Move(moveDirection * Time.deltaTime);
    }

    /// <summary>
    /// Handler for crouching and standing back up, with roof detection
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator CrouchStand()
    {

        // Ensure the player's not got anything above their head when crouched, if something is above, do nothing
        if (isCrouching && Physics.Raycast(playerCamera.transform.position, Vector3.up, 1f))
            yield break;

        // Set during
        duringCrouchAnim = true;

        // Params
        float timeElapsed = 0;
        float targetHeight = isCrouching ? standingHeight : crouchHeight;
        float currentHeight = characterController.height;
        Vector3 targetCenter = isCrouching ? standingCenter : crouchingCenter;
        Vector3 currentCenter = characterController.center;

        while (timeElapsed < timeToCrouch)
        {
            // Move character height and center down, based on amount of crouch per timeElapsed
            characterController.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCrouch);
            characterController.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / timeToCrouch);
            timeElapsed += Time.deltaTime;

            yield return null; // Move next
        }

        // Sanity check height setter
        characterController.height = targetHeight;
        characterController.center = targetCenter;

        // Swap isCrouching
        isCrouching = !isCrouching;

        // Set during
        duringCrouchAnim = false;
    }

    /// <summary>
    /// Checks the distance between enemy and player, and acts accordingly
    /// </summary>
    public void CheckForEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        enemies.ToList().ForEach(x =>
        {
            if ((Vector3.Distance(x.transform.position, gameObject.transform.position) < 1.5f) && (x.GetComponent<SeenMeter>().Seen))
            {
                if (currentLives > 0)
                    RemoveLife();
                else
                    PlayerDeath();
            }
        });
    }

    /// <summary>
    /// Resets the player's lives, and resets the scene
    /// </summary>
    static void ResetPlayer()
    {
        currentLives = maxLives;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    

    /// <summary>
    /// Player death logic
    /// </summary>
    private void PlayerDeath()
    {
        CanMove = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        crossHair.gameObject.SetActive(false);
        deathScreen.gameObject.SetActive(true);
    }

    /// <summary>
    /// Remove life logic
    /// </summary>
    private void RemoveLife()
    {
        currentLives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Add life logic
    /// </summary>
    private void AddLife() => currentLives++;
}
