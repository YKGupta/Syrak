using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject loadingPanel;
    public Image lockBase;
    public Image lockHead;
    public int sceneIndex = 1;

    private AsyncOperation loadOperation;

    private void Start()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        loadOperation = null;
    }

    private void Update()
    {
        if(loadOperation == null)
            return;
        
        float progress = (loadOperation.progress / 0.9f);
        if(progress <= 0.8f)
            lockBase.fillAmount =  progress / 0.8f;
        else
        {
            lockBase.fillAmount = 1f;
            lockHead.fillAmount = (progress - 0.8f) / 0.2f;
        }

        if(progress >= 1)
            SetSceneActivation();
    }

    public void PlayButton()
    {
        loadingPanel.SetActive(true);
        LoadScene(sceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene(int sceneIndex)
    {
        loadOperation = SceneManager.LoadSceneAsync(sceneIndex);
        loadOperation.allowSceneActivation = false;
    }

    public void SetSceneActivation()
    {
        loadOperation.allowSceneActivation = true;
    }
}
