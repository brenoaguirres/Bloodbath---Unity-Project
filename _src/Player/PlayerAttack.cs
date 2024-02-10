using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    // References
    [SerializeField] private PlayerInputActions input = null;
    private WeaponAnimationController weapon;
    private Animator anim;

    // Logic

    private void Awake() {
        input = new PlayerInputActions();
        anim = GetComponent<Animator>();
        weapon = GetComponentInChildren<WeaponAnimationController>();
    }

    private void OnEnable() {
        input.Enable();
        input.Player.Attack.Enable();
        input.Player.Attack.started += OnAttackStarted;
    }

    private void OnDisable() {
        input.Disable();
        input.Player.Attack.Disable();
        input.Player.Attack.started -= OnAttackStarted;       
    }

    private void OnAttackStarted(InputAction.CallbackContext value) {
        anim.SetTrigger("Attack");
        weapon.CallAttackAnim();
    }
}
