using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRSceneTransitionController : MonoBehaviour
{
     public string scene;

    // only let the user transport if the story is finished
    public bool canTransport = true;

    public void Transition(){
        if(canTransport)
        {
            XRSceneTransitionManager.Instance.TransitionTo(scene);
        }
    }
}
