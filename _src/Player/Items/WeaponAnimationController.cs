using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationController : MonoBehaviour
{
    // Variables
    private bool facingRight = true;
    // References
    private Animator anim;


    // Methods
    public void ChangeWeaponDirection(bool facingRight) {
        this.facingRight = facingRight;
        if (facingRight) {
            anim.SetBool("Facing_R", true);
        }
        else {
            anim.SetBool("Facing_R", false);
        }
    }

    public void CallAttackAnim() {
        anim.SetTrigger("Attack");
    }

    // Logic
    private void Awake() {
        anim = GetComponent<Animator>();
    }
}
