using System.Collections;
using UnityEngine;
using System;
using System.Linq;

public class FlyingEye : Monsters
{
    [SerializeField] private float speed;

    private Vector3 _direction;
    private SpriteRenderer sprite;
    private Animator _animator;
    private MonsterState _currentState;

    public static Action Dead;

    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        _direction = transform.right;
        _animator = GetComponentInChildren<Animator>();
    }

    MonsterAnimationState CurrentAnimation
    {
        get { return (MonsterAnimationState)_animator.GetInteger("State"); }
        set { _animator.SetInteger("State", (int)value); }
    }

    void Update()
    {
        FlipSprite();
        if(_currentState != MonsterState.isDead)
        {
            if (_currentState == MonsterState.isHit)
                CurrentAnimation = MonsterAnimationState.hit;
            else
                CurrentAnimation = MonsterAnimationState.fly;
            Move();
        }
        else
            CurrentAnimation = MonsterAnimationState.dead;
    }

    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.5f + transform.right * _direction.x * 1f, 0.2f);
        if (colliders.Length > 0 && colliders.All(n => !n.GetComponent<CharacterInputController>()))
            _direction *= -1f;

        transform.position = Vector3.MoveTowards(transform.position, transform.position + _direction, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Unit>())
        {
            StartCoroutine(HitAnimation());
            collision.GetComponent<HealthController>().GetDamage(20);
        }
    }

    private void FlipSprite()
    {
        if (_direction.x < 0)
            sprite.flipX = true;
        else
            sprite.flipX = false;
    }

    public override void DeadAction()
    {
        if(Dead != null)
            Dead();
        Destroy(gameObject);
    }

    IEnumerator HitAnimation()
    {
        _currentState = MonsterState.isHit;
        yield return new WaitForSeconds(0.3f);
        _currentState = MonsterState.isFly;
    }

    enum MonsterState
    {
        isFly,
        isHit,
        isDead,
    }

    enum MonsterAnimationState
    {
        fly,
        hit,
        dead,
    }
}
