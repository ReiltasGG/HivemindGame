using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
        [SerializeField]
        private float _speed;
        public float slowAmount = 3f;
        public bool slowPlayerBool = false;

        private Rigidbody2D _rigidbody;
        private Vector2 _movementInput;
        private Vector2 _smoothedMovementInput;
        private Vector2 _movementInputSmoothVelocity;
        
       

        private void Awake()
        {
                _rigidbody = GetComponent<Rigidbody2D>();
        }



    private void FixedUpdate()
    {
        if(slowPlayerBool == true && slowAmount>=0)
        {
            _speed = 3;
            slowAmount -= Time.deltaTime;

        }
        else
        {
            slowPlayerBool = false;
            _speed = 5;
        }
        _smoothedMovementInput = Vector2.SmoothDamp(
                _smoothedMovementInput,
                _movementInput,
                ref _movementInputSmoothVelocity,
                0.1f
        );
        _rigidbody.velocity = _smoothedMovementInput * _speed;

    }
   /*private void RotateInDirectionOfInput()
    {
        if (_movementInput != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _smoothedMovementInput);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

            _rigidbody.MoveRotation(rotation);
        }
    }*/
    private void OnMove(InputValue inputValue)
        {
                _movementInput = inputValue.Get<Vector2>();
        }
  
    public void slowPlayer()
    {
        slowPlayerBool = true;
        slowAmount = 3f;

    }
    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.layer == LayerMask.NameToLayer("Border")){
            _rigidbody.velocity = Vector2.zero;
        }
    }
}
