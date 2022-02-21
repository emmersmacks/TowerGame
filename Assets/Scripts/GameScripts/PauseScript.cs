using UnityEngine;


public class PauseScript : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;

    public void StartPause()
    {
        Time.timeScale = 0;
        _pausePanel.SetActive(true);
    }

    public void EndPause()
    {
        Time.timeScale = 1;
        _pausePanel.SetActive(false);
    }
}
