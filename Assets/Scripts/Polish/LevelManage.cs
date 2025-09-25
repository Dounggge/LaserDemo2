using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManage : MonoBehaviour
{
    [SerializeField] private float minLoadTime = 2f; // Minimum time to show loading screen
    [SerializeField] private float timer = 0f; // Maximum time to show loading screen
    GameObject pressAnyKey;

    void Start()
    {
        pressAnyKey = GameObject.Find("PressAnyKey");
        if (pressAnyKey != null)
        {
            pressAnyKey.SetActive(false);
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync("Playing_Scene"));
    }
  

    public void OnApplicationQuit()
    {
        Application.Quit();
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        // asyncLoad.progress goes from 0 -> 0.9. It stops at 0.9 until activation.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        // PHASE 1: Load in the background and run the minimum timer
        while (asyncLoad.progress < 0.9f || timer < minLoadTime)
        {
            timer += Time.deltaTime;
            float loadProgress = asyncLoad.progress / 0.9f;
            float timerProgress = timer / minLoadTime;
            float overallProgress = Mathf.Min(loadProgress, timerProgress);

            yield return null;
        }
        // PHASE 2: Minimum time is up, loading is done. Wait for user input.
        pressAnyKey.SetActive(true); // Show the Key

        // Wait for any input
        while (!Input.anyKeyDown)
        {
            yield return null;
        }

        // PHASE 3: Proceed
        pressAnyKey.SetActive(false);
        asyncLoad.allowSceneActivation = true;
    }
}
