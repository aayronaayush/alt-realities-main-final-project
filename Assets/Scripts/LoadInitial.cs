using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadInitial : MonoBehaviour
{
    

    public void OnTriggerEnter()
    {
        XRSceneTransitionManager.Instance.TransitionTo("BedroomApocalypse");
    }
   

}
