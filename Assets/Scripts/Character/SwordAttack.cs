using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject _hitBox;
    private Animator animator;
    private CharacterInputController _inputController;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        FlipHitBox();
    }

    public void Attack()
    {
        
        ActivateHitBox();
    }

    public void ActivateHitBox()
    {
        StartCoroutine(WaitAttackAnimation());
    }

    public void FlipHitBox()
    {
        if (transform.GetComponentInChildren<SpriteRenderer>().flipX == true)
            _hitBox.transform.Rotate(0, 180, 0);
        else
            _hitBox.transform.Rotate(0, 0, 0);
    }

    IEnumerator WaitAttackAnimation()
    {
        _hitBox.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        _hitBox.SetActive(false);
    }
}
