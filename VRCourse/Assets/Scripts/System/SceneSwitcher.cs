using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // This method now takes a scene name as a parameter.
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
