using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransfer : MonoBehaviour
{
    public static SceneTransfer Instance;
    public string toScene = "Final";
 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

        public void Transfer()
    {
        if(gameObject.scene.name == toScene){
            return;
        }
        if (transform.parent != null){
            transform.SetParent(null);

        }
        Scene newScene = SceneManager.GetSceneByName(toScene);
        if (newScene.IsValid())
        {
            SceneManager.MoveGameObjectToScene(gameObject, newScene);
        }
    }
    
}
