// ClickToMove.cs
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    public NavMeshAgent agent;

    RaycastHit hitInfo = new RaycastHit();

    public bool allowClickToMove = true;

    void Update()
    {
        if (!allowClickToMove) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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