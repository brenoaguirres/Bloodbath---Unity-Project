using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    // Variables
    private bool canInteract;
    private bool isInteracting;
    private float interactionInput;
    [Tooltip("Max radius to check for interactions.")]
    [SerializeField] private float checkRadius = 1.0f;
    [Tooltip("Layer of interactible objects.")]
    [SerializeField] private LayerMask interactibleLayer;
    
    // References
    private PlayerInputActions input = null;

    private void Awake() {
        input = new PlayerInputActions();
    }

    private void Update() {
        if (interactionInput > 0.1)
            isInteracting = true;

        if (isInteracting) {
            Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, interactibleLayer);
            canInteract = colliders.Length > 0;

            if (canInteract) {
                try {
                    colliders[0].GetComponentInParent<InteractibleObject>().Interact(this.gameObject);
                }
                catch {
                    Debug.Log("InteractibleObject component not found!");
                }
            }
        }
    }

    private void OnEnable() {
        input.Enable();
        input.Player.Movement.Enable();
        input.Player.Movement.started += OnMovementStarted;
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCanceled;
    }

    private void OnDisable() {
        input.Disable();
        input.Player.Movement.Disable();
        input.Player.Movement.started -= OnMovementStarted;
        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovementCanceled;
    }

    // Methods
    private void OnMovementStarted(InputAction.CallbackContext value) {
        Vector2 vet = value.ReadValue<Vector2>();
        interactionInput = vet.y;
    }
    
    private void OnMovementPerformed(InputAction.CallbackContext value) {
        Vector2 vet = value.ReadValue<Vector2>();
        interactionInput = vet.y;
    }

    private void OnMovementCanceled(InputAction.CallbackContext value) {
        Vector2 vet = value.ReadValue<Vector2>();
        interactionInput = vet.y;
        isInteracting = false;
    }

}
