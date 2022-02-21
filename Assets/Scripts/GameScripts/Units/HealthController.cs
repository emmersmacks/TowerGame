using UnityEngine;
using System;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private float _maxHealth;
    [SerializeField]
    private GameObject _healthBarImage;

    private float _currentHealth;
    private float _protectingShiftBar;
    private float _fillPercent;
    private Unit _unitScript;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _fillPercent = 2 / _maxHealth;
        _protectingShiftBar = 4.2f / _maxHealth;
        _unitScript = GetComponent<Unit>();
    }

    private void Update()
    {
        ShowHealthBar();
        CheckCurrentHealth();
    }

    private void CheckCurrentHealth()
    {
        if(_currentHealth <= 0)
        {
            _unitScript.DeadAction();
        }
    }

    public void GetDamage(int damageCount)
    {
        _currentHealth -= damageCount;
    }

    public void AddHealth(int healthCount)
    {
        _currentHealth += healthCount;
    }

    private void ShowHealthBar()
    {
        _healthBarImage.transform.localScale = new Vector3(_currentHealth * _fillPercent, 1, -1);
        _healthBarImage.transform.localPosition = new Vector3(_currentHealth * _protectingShiftBar, 0, -1);
    }
}
