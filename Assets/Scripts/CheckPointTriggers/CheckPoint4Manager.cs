using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Linq;


public class CheckPoint4Manager : MonoBehaviour
{
    //private void Start()
    //{
    //    PlayerPrefsV.SetBool("checkPoint04Complete", false);
    //}
    private void OnTriggerEnter(Collider other)
    {
        PlayerPrefsV.SetBool("checkPoint04Complete", true);
    }
}
