using System.Collections;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    [SerializeField] private GameObject _hitBox;
    [SerializeField] private float RechargeAttack;

    private CharacterInputController _inputController;
    private SpriteRenderer _sprite;
    private AudioSource _swordSound;

    private void Start()
    {
        _inputController = GetComponent<CharacterInputController>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _swordSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        FlipHitBox();
    }

    public void Attack()
    {
        if(_inputController.hitAnimationStart == false)
        {
            _swordSound.enabled = true;
            _inputController.hitAnimationStart = true;
            ActivateHitBox();
        }
    }

    public void ActivateHitBox()
    {
        StartCoroutine(WaitAttackAnimation());
    }

    public void FlipHitBox()
    {
        if (_sprite.flipX == true)
            _hitBox.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        else
            _hitBox.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }

    IEnumerator WaitAttackAnimation()
    {
        _hitBox.SetActive(true);
        yield return new WaitForSeconds(RechargeAttack);
        _hitBox.SetActive(false);
        _inputController.hitAnimationStart = false;
        _swordSound.enabled = false;

    }
}
