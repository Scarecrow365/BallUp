using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SceneManagerUI : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject loadingScreen;
    public Slider loadingSlider;
    public Text progressText;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void Mute()
    {
        AudioListener.pause = !AudioListener.pause;
    }

    public void StartGame(int sceneIndex)
    {
        StartCoroutine(LoadAsync(sceneIndex));
    }

    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            loadingSlider.value = progress;
            progressText.text = progress * 100f + "%";

            yield return null;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
