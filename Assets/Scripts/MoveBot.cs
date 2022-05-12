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

    //public GameObject[] checkPointList;
    public GameObject checkPoint1;
    public GameObject checkPoint2;
    public GameObject checkPoint3;
    public GameObject checkPoint4;
    public Vector3 targetPoint;
    public AudioSource robot1;
    public AudioSource player1;
    public AudioSource robot2;
    public AudioSource player2;
    public AudioSource robot3;


    //RaycastHit hitInfo = new RaycastHit();

    private void Start()
    {
        PlayerPrefsV.SetBool("checkPoint1Complete", false);
        PlayerPrefsV.SetBool("checkPoint2Complete", false);
        PlayerPrefsV.SetBool("checkPoint3Complete", false);
        PlayerPrefsV.SetBool("checkPoint4Complete", false);

        robot1= GetComponent<AudioSource>();
        player1= GetComponent<AudioSource>();
        robot2 = GetComponent<AudioSource>();
        player2 = GetComponent<AudioSource>();
        robot3 = GetComponent<AudioSource>();
}

    void Update()
    {
        //StartCoroutine(TraverseCheckpoints());

        if (PlayerPrefsV.GetBool("checkPoint1Complete", true) && PlayerPrefsV.GetBool("checkPoint2Complete", true)
            && PlayerPrefsV.GetBool("checkPoint3Complete", true) && PlayerPrefsV.GetBool("checkPoint4Complete", true))
        {
            StopCoroutine(SetCheckpoint(checkPoint4));
            StartCoroutine(SetCheckpoint(checkPoint4));
        }
        else if (PlayerPrefsV.GetBool("checkPoint1Complete", true) && PlayerPrefsV.GetBool("checkPoint2Complete", true)
            && PlayerPrefsV.GetBool("checkPoint3Complete", true))
        {
            //targetPoint = checkPoint4.transform.position;
            StopCoroutine(SetCheckpoint(checkPoint3));
            StartCoroutine(SetCheckpoint(checkPoint4));
            //SetCheckpoint(checkPoint4);


        }
        else if (PlayerPrefsV.GetBool("checkPoint1Complete", true) && PlayerPrefsV.GetBool("checkPoint2Complete", true))
        {
            //targetPoint = checkPoint3.transform.position;
            StopCoroutine(SetCheckpoint(checkPoint2));
            StartCoroutine(SetCheckpoint(checkPoint3));
            //SetCheckpoint(checkPoint3);
        }
        else if (PlayerPrefsV.GetBool("checkPoint1Complete", true))
        {
            StopCoroutine(SetCheckpoint(checkPoint1));
            
            StartCoroutine(SetCheckpoint(checkPoint2));
            targetPoint = checkPoint2.transform.position;
            //SetCheckpoint(checkPoint2);
        }
        else
        {
            StartCoroutine(SetCheckpoint(checkPoint1));
            //targetPoint = checkPoint1.transform.position;
            //SetCheckpoint(checkPoint1);
        }



        //GameObject checkpoint_test = this.FindCheckpoint("BotCheckpoint");
        //agent.SetDestination(checkpoint_test.transform.position);
    }

    IEnumerator SetCheckpoint(GameObject check)
    {
        yield return new WaitForSeconds(2f);
        targetPoint = check.transform.position;
        //agent.SetDestination(targetPoint);
        string journeyName = check.tag.ToString() + "Complete";
        while (PlayerPrefsV.GetBool(journeyName) == false)
        {
            agent.SetDestination(targetPoint);
            yield return null;

        }
    }

    IEnumerator TraverseCheckpoints()
    {
        yield return new WaitForSeconds(2f);
        SetCheckpoint(checkPoint1);
        yield return new WaitForSeconds(1f);
        SetCheckpoint(checkPoint2);
        yield return new WaitForSeconds(1f);
        SetCheckpoint(checkPoint3);
        yield return new WaitForSeconds(1f);
        SetCheckpoint(checkPoint4);
        //yield return new WaitForSeconds(6f);
        yield return null;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "checkPoint1")
        {
            PlayerPrefsV.SetBool("checkPoint1Complete", true);
            //robot1.Play(0);
            Debug.Log("started");
            //player1.Play(0);
            Debug.Log("started");
            //robot2.Play(0);
            Debug.Log("started");
            //player2.Play(0);
            Debug.Log("started");
            //robot3.Play(0);
            Debug.Log("started");
        }
        else if (other.gameObject.tag == "checkPoint2")
        {
            PlayerPrefsV.SetBool("checkPoint2Complete", true);
        }
        else if (other.gameObject.tag == "checkPoint3")
        {
            PlayerPrefsV.SetBool("checkPoint3Complete", true);
        }
        else if (other.gameObject.tag == "checkPoint4")
        {
            PlayerPrefsV.SetBool("checkPoint4Complete", true);
        }
    }

    //public GameObject FindCheckpoint(string checkpointName)
    //{

    //    GameObject[] newSceneObjects = SceneManager.GetActiveScene().GetRootGameObjects();


    //    GameObject checkpointObject = newSceneObjects.First((obj) =>
    //    {
    //        return obj.CompareTag(checkpointName);
    //    });

    //    return checkpointObject;

    //}
    

    //void OnGUI()
    //{
    //    if (GUI.Button(new Rect(10, 70, 150, 30), "Pause"))
    //    {
    //        audioData.Pause();
    //        Debug.Log("Pause: " + audioData.time);
    //    }

    //    if (GUI.Button(new Rect(10, 170, 150, 30), "Continue"))
    //    {
    //        audioData.UnPause();
    //    }
    //}
}


