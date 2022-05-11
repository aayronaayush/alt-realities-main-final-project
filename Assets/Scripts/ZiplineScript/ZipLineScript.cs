using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

using Unity.XR.CoreUtils;

public class ZipLineScript : MonoBehaviour
{
    public Transform finish;
    public float speed = 0.015f;
    public float fallSpeed = 1.0f;

    private XROrigin origin;
    private Vector3 start;
    private float lerping = 0;
    private Coroutine zipline;

    private void Start()
    {
        origin = FindObjectOfType<XROrigin>();
    }

    public void Zipline_Activate()
    {
        lerping = 0;
        start = origin.transform.position;
        zipline = StartCoroutine(DoZip());
    }

     

    public void CancelZip()
    {
        StopCoroutine(zipline);
        StartCoroutine(Fall());
    }
    IEnumerator Fall()
    {

        Vector3 fall = origin.transform.position;
        fall.y -= 20.0f;
        while (!Mathf.Approximately(origin.transform.position.y, fall.y+2))
        {
            lerping = Mathf.MoveTowards(lerping, 1.0f, fallSpeed * Time.deltaTime);
            origin.transform.position = Vector3.Lerp(start, fall, lerping);
            yield return null;
        }
        XRSceneTransitionManager.Instance.TransitionTo("Bedroom");
    }

    IEnumerator DoZip()
    {
        yield return new WaitForSeconds(.25f);

        while (!Mathf.Approximately(Vector3.Distance(origin.transform.position, finish.transform.position), 0))
        {
            lerping = Mathf.MoveTowards(lerping, 1.0f, speed * Time.deltaTime);
            origin.transform.position = Vector3.Lerp(start, finish.transform.position, lerping);
            yield return null;
        }
    }

}
