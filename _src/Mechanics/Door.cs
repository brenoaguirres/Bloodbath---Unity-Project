using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Variables
    [Tooltip("Speed in which door opens.")]
    [SerializeField] private float doorSpeed = 1.0f;
    private bool isOpened = false;
    private float destinationThreshold = 0.1f;
    private bool haveInteraction = false;

    // References
    [Tooltip("Position in which the door is considered opened.")]
    [SerializeField] private Transform openDoorPos;
    private Vector3 closedDoorPos;

    // Methods
    public void UseDoor() {
        haveInteraction = true;
    }

    public bool GetDoorStatus() {
        return isOpened;
    }

    private void MoveDoor(Vector3 goalPos) {
        float dist = Vector3.Distance(transform.position, goalPos);
        if (dist > destinationThreshold) {
            transform.position = Vector3.Lerp(transform.position, goalPos, doorSpeed * Time.deltaTime);
        }
        else {
            isOpened = !isOpened;
            haveInteraction = false;
        }
    }

     // Logic
    private void Awake() {
        closedDoorPos = transform.position;
    }

    private void Update() {
        if (haveInteraction) {
            if (isOpened) {
                MoveDoor(closedDoorPos);
            }
            else {
                MoveDoor(openDoorPos.position);
            }
        }
    }
}
