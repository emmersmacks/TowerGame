using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class FlyingEye : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Vector3 _direction;
    private SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        _direction = transform.right;
    }

    void Update()
    {
        FlipSprite();
        Move();
    }

    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.5f + transform.right * _direction.x * 1f, 0.2f);
        if (colliders.Length > 0 && colliders.All(n => !n.GetComponent<CharacterInputController>()))
            _direction *= -1f;

        transform.position = Vector3.MoveTowards(transform.position, transform.position + _direction, speed * Time.deltaTime);
    }

    private void FlipSprite()
    {
        if (_direction.x < 0)
            sprite.flipX = true;
        else
            sprite.flipX = false;
    }
}
