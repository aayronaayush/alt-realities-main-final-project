using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

using Unity.XR.CoreUtils;

public class ZipLineScript : MonoBehaviour
{
    private XROrigin origin;
    public Transform start;
    public Transform finish;
    public float speed = 1.0f;

    private float lerping = 0;

    private void Start()
    {
        origin = FindObjectOfType<XROrigin>();
    }

    void Zipline_Activate()
    {
        lerping = 0;
        StartCoroutine(DoZip());
    }

    IEnumerator DoZip()
    {
        while (!Mathf.Approximately(Vector3.Distance(origin.transform.position, finish.transform.position), 0))
        {
            lerping = Mathf.MoveTowards(lerping, 1.0f, speed * Time.deltaTime);
            origin.transform.position = Vector3.Lerp(start.transform.position, finish.transform.position, lerping);
            yield return null;
        }
    }

}
