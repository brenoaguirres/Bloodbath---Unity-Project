using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractibleObject : MonoBehaviour
{
    public abstract void Interact();
    public abstract void Interact(GameObject i);
}
