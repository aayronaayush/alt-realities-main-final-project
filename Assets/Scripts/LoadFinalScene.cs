using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadFinalScene : MonoBehaviour
{

    
   
    [SerializeField]
    private float delayBeforeLoading = 5.0f;
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
            //Vector3 newScale = new Vector3(0.207f, 0, 0.205f);
            //float speed = 2.0f;

            XRSceneTransitionManager.Instance.TransitionTo(sceneNameToLoad);
            //gameObject.transform.localScale = Vector3.Lerp(transform.localScale, newScale, speed * Time.deltaTime);
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
