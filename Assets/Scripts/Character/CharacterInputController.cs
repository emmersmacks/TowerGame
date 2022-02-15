using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputController : MonoBehaviour
{
    [SerializeField]
    private Joystick _joystick;

    private Vector3 _moveDirection;
    private Animator animator;
    public Vector3 _reboundDirection;
    public CharacterState _currentState;
    private ControllerAction _actionController;

    public AnimationState State
    {
        get { return (AnimationState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        _currentState = CharacterState.onGround;
        _actionController = GetComponent<ControllerAction>();
    }

    void Update()
    {
        
        if (_currentState == CharacterState.onWall)
        {
            State = AnimationState.hangWall;
            _actionController.HangWall();
        }
        else if (_currentState == CharacterState.onJump)
            State = AnimationState.jump;
        else if (_joystick.Horizontal != 0)
        {
            State = AnimationState.go;
        }
        else State = AnimationState.hit;//State = AnimationState.idle;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _reboundDirection = transform.position - collision.transform.position;
        if (collision.gameObject.tag == "Ground")
            _currentState = CharacterState.onGround;
        else if (collision.gameObject.tag == "Wall")
            _currentState = CharacterState.onWall;
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
}
