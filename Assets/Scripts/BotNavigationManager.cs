using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class BotNavigationManager : MonoBehaviour
{
    static public void PlaceXROrigin( Scene newScene)
    {
        
        GameObject[] newSceneObjects = newScene.GetRootGameObjects();

        GameObject xrOrigin = newSceneObjects.First((obj) =>
        {
            return obj.CompareTag("BotCheckpoint");
        });

    }
}
