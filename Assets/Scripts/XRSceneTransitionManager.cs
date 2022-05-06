using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

[DisallowMultipleComponent]

public class XRSceneTransitionManager : MonoBehaviour
{
    public static XRSceneTransitionManager Instance;
    public Material transitionMaterial;
    public float transitionSpeed = 1.0f;

    public bool isLoading { get; private set; } = false;

    public string initialScene;

    Scene xrScene;
    Scene currentScene;

    float currentTransitionAmount = 0.0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Detected rouge XRSceneTransitionManager"); 
            Destroy(this.gameObject);
            return;
        }
        xrScene = SceneManager.GetActiveScene();
        SceneManager.sceneLoaded += OnNewSceneAdded;

        if (!Application.isEditor)
        {
            TransitionTo(initialScene);
        }

    }

    public void TransitionTo(string scene)
    {
        if (!isLoading)
        {
            StartCoroutine(Load(scene));
        }

    }

    void OnNewSceneAdded(Scene newScene, LoadSceneMode mode)
    {
        if (newScene != xrScene)
        {
            currentScene = newScene;
            SceneManager.SetActiveScene(currentScene);
            PlaceXROrigin(xrScene, currentScene);
        }
        
    }

    IEnumerator Load(string scene)
    {
        isLoading  = true;
        yield return StartCoroutine(Fade(1.0f));
        yield return StartCoroutine(UnloadCurrentScene());

        yield return StartCoroutine(LoadNewScene(scene));
        yield return StartCoroutine(Fade(0.0f));

        isLoading = false;
    }

    IEnumerator UnloadCurrentScene()
    {
        AsyncOperation unload = SceneManager.UnloadSceneAsync(currentScene);
        while (!unload.isDone) {
            yield return null;
        }
    }
    IEnumerator LoadNewScene(string scene)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        while (!load.isDone)
        {
            yield return null;
        }
    }

    static public void PlaceXROrigin(Scene xrScene, Scene newScene)
    {
        GameObject[] xrObjects = xrScene.GetRootGameObjects();
        GameObject[] newSceneObjects = newScene.GetRootGameObjects();

        GameObject xrOrigin = xrObjects.First((obj) =>
        {
            return obj.CompareTag("XROrigin");
        });

        GameObject xrOrigin_Origin = newSceneObjects.First((obj) =>
        {
            return obj.CompareTag("XROriginOrigin");
        });

        if (xrOrigin && xrOrigin_Origin)
        {
            xrOrigin.transform.position = xrOrigin_Origin.transform.position;
            xrOrigin.transform.rotation = xrOrigin_Origin.transform.rotation;
        }


    }

    IEnumerator Fade(float target)
    {
        while (!Mathf.Approximately(currentTransitionAmount, target))
        {
            currentTransitionAmount = Mathf.MoveTowards(currentTransitionAmount, target, transitionSpeed * Time.deltaTime);
            transitionMaterial.SetFloat("_FadeAmount", currentTransitionAmount);
            yield return null;
        }
    }
}
