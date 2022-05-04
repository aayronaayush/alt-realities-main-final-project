using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadFinalScene : MonoBehaviour
{

    
   
    [SerializeField]
    private float delayBeforeLoading = 10.0f;
    private string sceneNameToLoad = "Final";
    private string toScene = "XR";
    private float timeElapsed;

    private void Update()
    {
        this.Transfer();
        timeElapsed += Time.deltaTime;
        string active = SceneManager.GetActiveScene().name;
        if (active == sceneNameToLoad)
        {
            return;
        }
        if (timeElapsed > delayBeforeLoading)
        {

            XRSceneTransitionManager.Instance.TransitionTo(sceneNameToLoad);
            gameObject.transform.localScale += new Vector3(0.007f, 0, 0.005f);
        }
    }

    public void Transfer()
    {
        if (gameObject.scene.name == toScene)
        {
            return;
        }
        if (transform.parent != null)
        {
            transform.SetParent(null);

        }
        Scene newScene = SceneManager.GetSceneByName(toScene);
        if (newScene.IsValid())
        {
            SceneManager.MoveGameObjectToScene(gameObject, newScene);
        }
    }
}
