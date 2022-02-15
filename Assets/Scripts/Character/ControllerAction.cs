using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAction : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _jumpForce;
    [SerializeField]
    private float _reboundForce;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer sprite;
    private CharacterInputController _inputController;

    const float TrajectoryRebound = 60;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        _inputController = GetComponent<CharacterInputController>();
    }

    public void MoveHorizontal(Vector3 moveDirection)
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDirection, _speed * Time.deltaTime);
    }

    public void Jump()
    {

        if (_inputController._currentState == CharacterState.onGround)
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode2D.Impulse);
            _inputController._currentState = CharacterState.onJump;

        }
        else if (_inputController._currentState == CharacterState.onWall)
        {
            _inputController._currentState = CharacterState.onJump;
            _rigidbody.AddForce((_inputController._reboundDirection * _reboundForce) + transform.up * TrajectoryRebound, ForceMode2D.Impulse);
        }
    }

    public void HangWall()
    {
        _rigidbody.velocity = Vector3.zero;
    }

    public void FlipSprite(Vector3 moveDirection)
    {
        if (moveDirection.x < 0)
            sprite.flipX = true;
        else if (moveDirection.x > 0)
            sprite.flipX = false;
    }
}
