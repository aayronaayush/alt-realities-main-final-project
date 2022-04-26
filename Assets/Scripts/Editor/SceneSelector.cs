using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using System.Linq;
public static class SceneSelector  
{

    [MenuItem("Scenes/BedroomApocalypse")]
    static void OpenBedroomApocalypse()
    {
        Load("Bedroom Apocalypse");
    }
    [MenuItem("Scenes/Desert")]
    static void OpenDesert()
    {
        Load("Desert");
    }
    [MenuItem("Scenes/Bedroom")]
    static void OpenBedroom()
    {
        Load("Bedroom");
    }

    static void Load(string scene)
    {

        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        Scene xrScene = EditorSceneManager.OpenScene("Assets/Scenes/XR.unity", OpenSceneMode.Single);
        Scene newScene = EditorSceneManager.OpenScene("Assets/Scenes/" + scene + ".unity", OpenSceneMode.Additive);
         
        XRSceneTransitionManager.PlaceXROrigin(xrScene, newScene);

         

    }

    // static void PlaceXROrigin(Scene xrScene, Scene newScene)
    // {
    //    GameObject[] xrObjects = xrScene.GetRootGameObjects();
    //    GameObject[] newSceneObjects = newScene.GetRootGameObjects();

    //    GameObject xrOrigin = xrObjects.First((obj) =>
    //    {
    //        return obj.CompareTag("XROrigin");
    //    });

    //    GameObject xrOrigin_Origin = newSceneObjects.First((obj) =>
    //    {
    //        return obj.CompareTag("XROriginOrigin");
    //    });

    //    if (xrOrigin && xrOrigin_Origin)
    //    {
    //        xrOrigin.transform.position = xrOrigin_Origin.transform.position;
    //        xrOrigin.transform.rotation = xrOrigin_Origin.transform.rotation;
    //    }
     

    // }
}
