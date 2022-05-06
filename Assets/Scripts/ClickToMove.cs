// ClickToMove.cs
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Linq;

public class ClickToMove : MonoBehaviour
{
    public NavMeshAgent agent;

    RaycastHit hitInfo = new RaycastHit();

    public bool allowClickToMove = true;

    void Update()
    {
        if (!allowClickToMove) return;

         
            GameObject checkpoint_test = this.FindCheckpoint();
            Ray ray = Camera.main.ScreenPointToRay(checkpoint_test.transform.position);
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {
                agent.SetDestination(hitInfo.point);
            }
        
    }

    public GameObject FindCheckpoint( )
    {

        GameObject[] newSceneObjects = SceneManager.GetActiveScene().GetRootGameObjects();

        GameObject checkpointObject = newSceneObjects.First((obj) =>
        {
            return obj.CompareTag("BotCheckpoint");
        });

        return checkpointObject;

    }
}