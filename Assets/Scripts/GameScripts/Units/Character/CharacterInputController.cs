using System;
using UnityEngine;

public class CharacterInputController : Unit
{
    [SerializeField]
    private Joystick _joystick;

    private Vector3 _moveDirection;
    public CharacterState _currentState;
    private ControllerAction _actionController;
    public bool hitAnimationStart;
    protected Animator animator;


    protected AnimationState CurrentAnimation
    {
        get { return (AnimationState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    public event Action OnDeadCharacter = default;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        hitAnimationStart = false;
        _currentState = CharacterState.onGround;
        _actionController = GetComponent<ControllerAction>();
    }

    private void Update()
    {
        if (_currentState == CharacterState.onWall)
        {
            CurrentAnimation = AnimationState.hangWall;
            _actionController.HangWall();
        }
        else if (!hitAnimationStart)
        {
            if (_currentState == CharacterState.onJump)
            {
                CurrentAnimation = AnimationState.jump;
            }
            else if (_joystick.Horizontal != 0)
            {
                CurrentAnimation = AnimationState.go;
            }
            else CurrentAnimation = AnimationState.idle;
        }
        else if(_currentState != CharacterState.isDead)
            CurrentAnimation = AnimationState.hit;
        
        _actionController.FlipSprite(_moveDirection);
    }

    private void FixedUpdate()
    {
        if(_currentState != CharacterState.onWall)
        {
            _moveDirection = transform.right * _joystick.Horizontal;
            _actionController.MoveHorizontal(_moveDirection);
        }
    }

    public override void DeadAction()
    {
        if (_currentState != CharacterState.isDead)
            OnDeadCharacter();
        _currentState = CharacterState.isDead;
        CurrentAnimation = AnimationState.dead;
    }
}
public enum AnimationState
{
    idle,
    go,
    jump,
    hit,
    hangWall,
    dead,
}

public enum CharacterState
{
    onGround,
    onWall,
    onJump,
    onHit,
    isDead,
}



