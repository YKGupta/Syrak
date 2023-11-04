using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public int index;
    private AsyncOperation operation;

    public void SetSceneToLoad(int _index)
    {
        index = _index;
        operation = null;
    }

    public void StartLoad()
    {
        operation = SceneManager.LoadSceneAsync(index);
        operation.allowSceneActivation = false;
    }

    public void FinishLoad()
    {
        if(operation == null)
            return;
        operation.allowSceneActivation = true;
    }
}
