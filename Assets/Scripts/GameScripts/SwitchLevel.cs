using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class SwitchLevel : MonoBehaviour
{
    [SerializeField] Transform _characterTransform;
    [SerializeField] Text _lvlCountText;
    [SerializeField] Image _switchImage;

    private LevelGenerate _levelGenerateScript;
    private EnemyGenerate _enemyGenerateScript;

    private DataController _dataController;

    private bool canSwitch;

    Tween animation;

    private void Start()
    {
        canSwitch = false;
        _levelGenerateScript = GetComponent<LevelGenerate>();
        _enemyGenerateScript = GetComponent<EnemyGenerate>();

        _dataController = GetComponent<DataController>();
        _dataController.EndLevel += ShowLoadScreen;
    }

    private void Update()
    {
        if (canSwitch)
        {
            canSwitch = false;
            SwitchLevelTextCount();
            ReloadGenerateLevel();
            HideLoadScreen();
        }   
    }

    public void ReloadGenerateLevel()
    {
        _levelGenerateScript.DeleteCreatedPrefab();
        _enemyGenerateScript.DeleteCreatedEnemy();
        _levelGenerateScript.GenerateNewLevel();
        _enemyGenerateScript.GenerateEnemyPosition();
    }

    private void ShowLoadScreen()
    {
        canSwitch = true;
        animation = _switchImage.DOFade(1f, 0.5f);
    }

    private void HideLoadScreen()
    {
        _switchImage.DOFade(0f, 0.5f);
        

    }

    private void SwitchLevelTextCount()
    {
        _dataController._currentFloor++;
        _lvlCountText.text = _dataController._currentFloor.ToString();
    }
}
