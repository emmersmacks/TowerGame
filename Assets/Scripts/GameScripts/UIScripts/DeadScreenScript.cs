using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DeadScreenScript : MonoBehaviour
{
    [SerializeField] CharacterInputController _actionScript;
    [SerializeField] Image DeadScreenImage;
    [SerializeField] GameObject InfoPanel;
    [SerializeField] Text _soulsText;
    [SerializeField] Text _floorText;

    private DataController _dataScript;
    private bool _showDeadPanel;

    Tween DOTweenAnim;

    private void Start()
    {
        _showDeadPanel = false;
        _dataScript = GetComponent<DataController>();
        _actionScript.OnDeadCharacter += ShowDeadScreen;
    }

    private void Update()
    {
        if(_showDeadPanel)
        {
            _showDeadPanel = false;
            InfoPanel.SetActive(true);
            SetInformationLevel();
        }
    }

    private void ShowDeadScreen()
    {
        DeadScreenImage.DOFade(1f, 0.5f);
        DOTweenAnim = DeadScreenImage.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 1, 1, 1f);
        if(DOTweenAnim != null)
            if(DOTweenAnim.active)
                _showDeadPanel = true;
    }

    private void SetInformationLevel()
    {
        int dataSouls = PlayerPrefs.GetInt("souls");
        _soulsText.text = dataSouls.ToString() + "+" + _dataScript._soulsSum.ToString();

        _floorText.text = _dataScript._currentFloor.ToString();
    }
}
