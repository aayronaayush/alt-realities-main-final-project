// ClickToMove.cs
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class ClickToMove : MonoBehaviour
{
    public NavMeshAgent agent;

    RaycastHit hitInfo = new RaycastHit();

    public bool allowClickToMove = true;

    void Update()
    {
        if (!allowClickToMove) return;

        if (Mouse.current.leftButton.isPressed)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {
                agent.SetDestination(hitInfo.point);
            }
        }
    }

    public void DisableClickToMove()
    {
        allowClickToMove = false;
    }

    public void EnableClickToMove()
    {
        allowClickToMove = true;
    }
}