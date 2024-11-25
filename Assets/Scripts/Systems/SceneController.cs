using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    private void Awake()
    {
        
        instance = this;
            
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SwitchScene(string sceneName)
    {
        
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}