using UnityEngine.UI;
using UnityEngine;

public class ShowData : MonoBehaviour
{
    [SerializeField] private Text _recordFloorText;
    [SerializeField] private Text _soulsText;

    const string soulsKeyName = "souls";
    const string floorKeyName = "recordFloor";


    void Start()
    {
        SetDataValue();
    }

    public void SetDataValue()
    {
        int currentSouls = PlayerPrefs.GetInt(soulsKeyName);
        int currentFloorRecord = PlayerPrefs.GetInt(floorKeyName);

        _recordFloorText.text = currentFloorRecord.ToString();
        _soulsText.text = currentSouls.ToString();

    }
}
