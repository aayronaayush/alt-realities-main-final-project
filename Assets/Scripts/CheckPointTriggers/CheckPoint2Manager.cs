using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Linq;

public class CheckPoint2Manager : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefsV.SetBool("checkPoint02Complete", false);
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerPrefsV.SetBool("checkPoint02Complete", true);
    }
}
