using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    // Variables
    [Tooltip("Force of the jump.")]
    [SerializeField] private float jumpForce = 3f;
    [Tooltip("Radius in which collision with ground will be checked.")]
    [SerializeField] private float checkRadius = 0.3f;
    [Tooltip("What is the ground layer?")]
    [SerializeField] private LayerMask groundMask;
    [Tooltip("Maximum time player can keep airborne.")]
    [SerializeField] private float jumpMaxTime = 0.2f;
    private bool isGrounded;
    private bool isJumpPressed;
    private float jumpTimeCounter;
    private bool canJump;

    // References
    [Tooltip("Position in which collision will be checked.")]
    [SerializeField] private Transform feetPosition;
    private Rigidbody rb;
    private Animator anim;
    private SpriteRenderer sprRend;
    private PlayerInputActions input = null;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        sprRend = GetComponent<SpriteRenderer>();
        input = new PlayerInputActions();
    }

    private void OnEnable() {
        input.Enable();
        input.Player.Jump.Enable();
        input.Player.Jump.started += OnJumpStarted;
        input.Player.Jump.performed += OnJumpPerformed;
        input.Player.Jump.canceled += OnJumpCanceled;

    }

    private void OnDisable() {
        input.Disable();
        input.Player.Jump.Disable();
        input.Player.Jump.started -= OnJumpStarted;
        input.Player.Jump.performed -= OnJumpPerformed;
        input.Player.Jump.canceled -= OnJumpCanceled;
    }

    void Start()
    {
        
    }


    void Update()
    {
        // Timer
        jumpTimeCounter -= Time.deltaTime;

        // Check if player is on the ground 
        Collider[] colliders = Physics.OverlapSphere(feetPosition.position, checkRadius, groundMask);
        isGrounded = colliders.Length > 0;
        anim.SetBool("isGrounded", isGrounded);
    }

    void FixedUpdate() {
        // Jump start handling
        // canJump allows jumping
        if (isGrounded && isJumpPressed) {
            canJump = true;
            jumpTimeCounter = jumpMaxTime;
            anim.SetBool("maxJumpHeight", false);
            rb.AddForce((Vector2.up * jumpForce * Time.fixedDeltaTime), ForceMode.Impulse);
        }

        // Continuous jump handling (if player untaps the key, then 'canJump' becomes false)
        if (isJumpPressed && canJump) {
            if (jumpTimeCounter > 0) {
                //anim.SetBool("isJumping", false);
                rb.AddForce((Vector2.up * jumpForce * Time.fixedDeltaTime), ForceMode.Impulse);
            }
            else {
                canJump = false;
                anim.SetBool("isJumping", false);
                anim.SetBool("maxJumpHeight", true);
            }
        }
    }

    // Methods
    private void OnJumpStarted(InputAction.CallbackContext value) {
        if (!isJumpPressed) {
            isJumpPressed = true;
            anim.SetTrigger("isJumping");
        }
    }
    
    private void OnJumpPerformed(InputAction.CallbackContext value) {
        isJumpPressed = true;
    }

    private void OnJumpCanceled(InputAction.CallbackContext value) {
        //anim.SetBool("isJumping", false);
        isJumpPressed = false;
        canJump = false;
    }
}
