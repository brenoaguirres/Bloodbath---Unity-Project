using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGizmo : MonoBehaviour
{
    // Debug
    private Color gizmoColor = Color.yellow;
    [SerializeField] private float gizmoRadius = 0.3f;

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, gizmoRadius);
    }
}
