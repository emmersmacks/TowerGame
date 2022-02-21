using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class ExitScript : MonoBehaviour
{
    [SerializeField] private DataController _dataController;

    public void ExitInMenu()
    {
        _dataController.SaveData();
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
