using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _rotationspeed;

    private Rigidbody2D _rb;
    private PlayerAwarenessController _playerAwarenessController;
    private Vector2 _targetDirection;
    private float newSpeed;
    [SerializeField] private float baseSpeed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }

    private void UpdateTargetDirection()
    {
        if (_playerAwarenessController.AwareOfPlayer)
        {
            _targetDirection = _playerAwarenessController.DirectionToPlayer;
        }
        else
        {
            _targetDirection = Vector2.zero;
        }

    }

    private void RotateTowardsTarget()
    {
        if (_targetDirection == Vector2.zero)
        {
            return;
        }
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationspeed * Time.deltaTime);

        _rb.SetRotation(rotation);
    }

    private void SetVelocity()
    {
        if (_targetDirection == Vector2.zero)
        {
            _rb.velocity = Vector2.zero;
        }
        else
        {
            _rb.velocity = _targetDirection * _speed;
        }
    }
    public void UpdateSpeed(float newSpeed)
    {
        _speed = newSpeed;

    }
    public void ResetSpeed()
    {
        _speed = baseSpeed;

    }
}
