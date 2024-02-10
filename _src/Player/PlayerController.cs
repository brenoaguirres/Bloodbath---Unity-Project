using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public void EnableInput() {
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<PlayerJump>().enabled = true;
        GetComponent<PlayerAttack>().enabled = true;
        GetComponent<PlayerInteraction>().enabled = true;
    }

    public void DisableInput() {
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerJump>().enabled = false;
        GetComponent<PlayerAttack>().enabled = false;
        GetComponent<PlayerInteraction>().enabled = false;
    }
}
