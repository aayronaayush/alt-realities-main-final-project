using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LoadFinalScene : MonoBehaviour
{

    public VideoPlayer player;
   
    [SerializeField]
    private float delayBeforeLoading = 5.0f;
    private string sceneNameToLoad = "Final";
    private string toScene = "XR";
    private float timeElapsed = 0.0f;

    public bool allowTransition = false;

    private bool hasTransitioned = false;

    public void AllowTransition()
    {
        allowTransition = true;
        if (!player.isPlaying) player.Play();
    }

    private void Start()
    {
        Transfer();
        timeElapsed = 0.0f;
    }

    private void Update()
    {
        if (!allowTransition) return;

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

            if (!hasTransitioned)
            {
                XRSceneTransitionManager.Instance.TransitionTo(sceneNameToLoad);
                hasTransitioned = true;
            }
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
