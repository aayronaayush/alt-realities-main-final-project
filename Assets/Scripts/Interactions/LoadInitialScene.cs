using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadInitialScene : MonoBehaviour
{
    public void LoadInitial()
    {
        XRSceneTransitionManager.Instance.TransitionTo("BedroomApocalypse");
    }
}
