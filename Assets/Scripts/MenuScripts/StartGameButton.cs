using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
