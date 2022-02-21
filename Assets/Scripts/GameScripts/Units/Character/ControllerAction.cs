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
    private SpriteRenderer _sprite;
    private CharacterInputController _inputController;
    private Vector3 _reboundDirection;

    const float TrajectoryRebound = 3.2f;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _inputController = GetComponent<CharacterInputController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            _inputController._currentState = CharacterState.onGround;
        else if (collision.gameObject.tag == "Wall" && _inputController._currentState != CharacterState.onGround)
        {
            var collisionPosition = collision.transform.position;
            var contact = collision.GetContact(0);
            var contactPosition = new Vector3(collisionPosition.x, contact.point.y, collisionPosition.z);
            _reboundDirection = transform.position - contactPosition;
            _inputController._currentState = CharacterState.onWall;
        }
    }

    public void MoveHorizontal(Vector3 moveDirection)
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDirection, _speed * Time.deltaTime);
    }

    public void Jump()
    {

        if (_inputController._currentState == CharacterState.onGround && _inputController._currentState != CharacterState.onHit)
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode2D.Impulse);
            _inputController._currentState = CharacterState.onJump;

        }
        else if (_inputController._currentState == CharacterState.onWall)
        {
            _inputController._currentState = CharacterState.onJump;
            var jumpDirection = new Vector3(_reboundDirection.x, _reboundDirection.y + TrajectoryRebound, _reboundDirection.z);
            _rigidbody.AddForce(jumpDirection * _reboundForce, ForceMode2D.Impulse);
        }
    }

    public void HangWall()
    {
        _rigidbody.velocity = Vector3.zero;
    }

    public void FlipSprite(Vector3 moveDirection)
    {
        if (moveDirection.x < 0)
            _sprite.flipX = true;
        else if (moveDirection.x > 0)
            _sprite.flipX = false;
    }
}
