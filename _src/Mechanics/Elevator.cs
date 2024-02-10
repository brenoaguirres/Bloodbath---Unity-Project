using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : InteractibleObject
{
    // Variables
    [SerializeField] private float elevatorSpeed = 2.0f;
    [SerializeField] private int currentFloor = 1;
    private bool hasInteraction;
    private bool moveDirection = false;
    private float moveRate;

    // References
    private GameObject player;
    [SerializeField] private Door myDoor;
    [SerializeField] private List<Transform> floorExitPosition = new List<Transform>();

    // Methods
    public override void Interact() {
        try {
            throw new NotImplementedException();
        }
        catch {
            Debug.Log("Please use Interact(GameObject i) instead.");
        }
    }

    public override void Interact(GameObject gameObj) {
        hasInteraction = true;
        player = gameObj;
    }

    private void MoveFloorUp(Vector3 goalPos) {
        StartCoroutine(MoveFloor(goalPos));
        currentFloor++;
    }

    private void MoveFloorDown(Vector3 goalPos) {
        StartCoroutine(MoveFloor(goalPos));
        currentFloor--;
    }

private IEnumerator MoveFloor(Vector3 goalPos) {
    Vector3 newPos = new Vector3(transform.position.x, goalPos.y, transform.position.z);
    
    float rate = 1.0f/Vector3.Distance(transform.position, newPos) * elevatorSpeed;
    float t = 0.0f;

    while (t < 1.0f) {
        t += Time.deltaTime * rate;
        transform.position = Vector3.Lerp(transform.position, newPos, t);
        yield return null;
    }

    transform.position = newPos;
}

    public void ToggleFloorDirection() {
        moveDirection = !moveDirection;
    }
    
    // Logic

    private void Awake() {
        myDoor = GetComponentInChildren<Door>();
    }

    private void Update() {
        if (hasInteraction) {
            myDoor.UseDoor();
            if (!moveDirection) {
                if (currentFloor + 1 < floorExitPosition.Count) {
                    MoveFloorUp(floorExitPosition[currentFloor + 1].position);
                }
            }
            else {
                if (currentFloor - 1 >= 0) {
                    MoveFloorDown(floorExitPosition[currentFloor - 1].position);
                }
            }
            hasInteraction = false;
        }
    }
}
