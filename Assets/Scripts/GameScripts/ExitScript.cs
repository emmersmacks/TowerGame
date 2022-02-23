using UnityEngine.SceneManagement;
using UnityEngine;


public class ExitScript : MonoBehaviour
{
    [SerializeField] private DataController _dataController;

    public void ExitInMenu()
    {
        _dataController.SaveData();
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
