using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Linq;

public class CheckPoint1Manager : MonoBehaviour
{
    //private void Start()
    //{
    //    PlayerPrefsV.SetBool("checkPoint01Complete", false);
    //}
    private void OnTriggerEnter(Collider other)
    {
        PlayerPrefsV.SetBool("checkPoint01Complete", true);
    }
}
