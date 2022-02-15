using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private int Health;

    public void GetDamage(int DamageCount)
    {
        Health -= DamageCount;
    }

    public void AddHealth(int HealthCount)
    {
        Health += HealthCount;
    }
}
