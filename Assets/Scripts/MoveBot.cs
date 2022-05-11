// ClickToMove.cs
using UnityEngine;
using UnityEngine.AI;
//using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class MoveBot : MonoBehaviour
{
    public NavMeshAgent agent;

    public GameObject[] checkPointList;
    public GameObject checkPoint1;
    public GameObject checkPoint2;
    public GameObject checkPoint3;
    public GameObject checkPoint4;
    private Vector3 targetPoint;

    //RaycastHit hitInfo = new RaycastHit();

    private void Start()
    {

        //checkpoint1 = FindCheckpoint("checkPoint1");
        //checkPointList[0] = checkpoint1;
        //checkPointList[1] = FindCheckpoint("checkPoint2");
        //checkPointList[2] = FindCheckpoint("checkPoint3");
        //checkPointList[3] = FindCheckpoint("checkPoint4");

        targetPoint = checkPoint1.transform.position;

        //targetPoint = checkPointList[0].transform.position;
        PlayerPrefsV.SetBool("checkPoint01Complete", false);
        PlayerPrefsV.SetBool("checkPoint02Complete", false);
        PlayerPrefsV.SetBool("checkPoint03Complete", false);
        PlayerPrefsV.SetBool("checkPoint04Complete", false);
        agent.SetDestination(targetPoint);
    }

    void Update()
    {

        if (PlayerPrefsV.GetBool("checkPoint01Complete", true) && PlayerPrefsV.GetBool("checkPoint02Complete", true)
            && PlayerPrefsV.GetBool("checkPoint03Complete", true))
        {
            agent.SetDestination(checkPoint4.transform.position);
        }
        else if (PlayerPrefsV.GetBool("checkPoint01Complete", true) && PlayerPrefsV.GetBool("checkPoint02Complete", true))
        {
            agent.SetDestination(checkPoint3.transform.position);
        }
        else if (PlayerPrefsV.GetBool("checkPoint01Complete", true))
        {
            agent.SetDestination(checkPoint2.transform.position);
        }

        //GameObject checkpoint_test = this.FindCheckpoint("BotCheckpoint");
        //agent.SetDestination(checkpoint_test.transform.position);

    }

    public GameObject FindCheckpoint(string checkpointName)
    {

        GameObject[] newSceneObjects = SceneManager.GetActiveScene().GetRootGameObjects();


        GameObject checkpointObject = newSceneObjects.First((obj) =>
        {
            return obj.CompareTag(checkpointName);
        });

        return checkpointObject;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "checkPoint1")
        {
            PlayerPrefsV.SetBool("checkPoint01Complete", true);
        }
        else if (other.gameObject.tag == "checkPoint2")
        {
            PlayerPrefsV.SetBool("checkPoint02Complete", true);
        }
        else if (other.gameObject.tag == "checkPoint3")
        {
            PlayerPrefsV.SetBool("checkPoint03Complete", true);
        }
        else
        {
            PlayerPrefsV.SetBool("checkPoint04Complete", true);
        }
    }
}


