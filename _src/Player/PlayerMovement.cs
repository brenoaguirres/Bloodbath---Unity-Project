using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    // Variables
    private Vector2 movement;
    [SerializeField] private float moveSpeed = 1500;


    // References
    private Rigidbody rb;
    private Animator anim;
    private SpriteRenderer sprRend;
    private WeaponAnimationController weapon;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        sprRend = GetComponent<SpriteRenderer>();
        weapon = GetComponentInChildren<WeaponAnimationController>();
    }

    public void SetMovement(InputAction.CallbackContext value) {
        movement = value.ReadValue<Vector2>();
        SetMovementAnim(movement.x);
    }

    private void SetMovementAnim(float mov) {
        // Set animation movement
        if (mov > 0.1 || mov < -0.1) {
            anim.SetBool("isRunning", true);
        }
        else {
            anim.SetBool("isRunning", false);
        }

        // Facing direction
        if (mov < -0.1) {
            sprRend.flipX = true;
            weapon.ChangeWeaponDirection(false);
        }
        else if (mov > 0.1) {
            sprRend.flipX = false;
            weapon.ChangeWeaponDirection(true);
        }
    }

    private void FixedUpdate() {
        rb.MovePosition(transform.position + (new Vector3(movement.x, 0, 0) * moveSpeed * Time.fixedDeltaTime));
    }
}
