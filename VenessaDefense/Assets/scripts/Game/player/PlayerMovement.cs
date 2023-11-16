/*using System.Collections;
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
   private void RotateInDirectionOfInput()
    {
        if (_movementInput != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _smoothedMovementInput);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

            _rigidbody.MoveRotation(rotation);
        }
    }
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
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//New Code for movement
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float normalAcceleration;
    [HideInInspector] public float acceleration;
    [HideInInspector] public Vector2 movementInput;

    public float slowAmount = 3f;
    public bool slowPlayerBool = false;
    public bool hasBeenGrabbed = false;


      private Vector2 _smoothedMovementInput;
    private Vector2 _movementInputSmoothVelocity;
        
    public Transform arrow;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        acceleration = normalAcceleration;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        

       // arrow.up = (mousePos - (Vector2)transform.position.normalized);
    }

    void FixedUpdate()
    {
        
       if(!hasBeenGrabbed)
       {
        _smoothedMovementInput = Vector2.SmoothDamp(
                _smoothedMovementInput,
                movementInput,
                ref _movementInputSmoothVelocity,
                0.1f
        );


         if(slowPlayerBool == true && slowAmount>=0)
        {
            
            movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            slowAmount -= Time.deltaTime;
            rb.velocity = new (Input.GetAxisRaw("Horizontal")/2, Input.GetAxisRaw("Vertical")/2);
          

        }
        else
        {
            slowPlayerBool = false;
           movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
           rb.velocity += (movementInput * acceleration * Time.fixedDeltaTime);

        }
           
       }
    }

       public void slowPlayer()
    {
          //Debug.Log("Slowed Down");
        slowPlayerBool = true;
        slowAmount = 3f;

    }

     private void RotateInDirectionOfInput()
    {
        if (movementInput != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _smoothedMovementInput);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 5.0f * Time.deltaTime);

            rb.MoveRotation(rotation);
        }
    }
    public void changeGrabFalse()
    {
        hasBeenGrabbed = false;
        
    }
     public void changeGrabTrue()
    {
        hasBeenGrabbed = true;
        
    }
}
