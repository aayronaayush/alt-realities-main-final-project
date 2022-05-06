using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToBedroomScene : MonoBehaviour
{
    [SerializeField]
    private float delayBeforeLoading = 10.0f;
    private string sceneNameToLoad = "Bedroom";
 
    private float timeElapsed;

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        string active = SceneManager.GetActiveScene().name;
        if (active == sceneNameToLoad)
        {
            return;
        }
        if (timeElapsed > delayBeforeLoading)
        {
  
            XRSceneTransitionManager.Instance.TransitionTo(sceneNameToLoad);
       
        }
    }

    
}
