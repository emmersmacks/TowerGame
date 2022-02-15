using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxScript : MonoBehaviour
{
    HealthController monsterHealth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
            if(collision.transform.GetComponent<Monsters>())
            {
                monsterHealth = collision.transform.GetComponent<HealthController>();
                monsterHealth.GetDamage(10);
            }
    }
}
