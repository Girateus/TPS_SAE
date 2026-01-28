using UnityEngine;

[RequireComponent(typeof(CharacterInputs))]
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 5;
    [SerializeField] private float _jumpHeight = 10;
    [SerializeField] private float _fallFactor = 10;
    [SerializeField] private Detector _gndDetector;
    [SerializeField] private float _delay = 0.05f;
    [SerializeField] private Animator _animator;
    [SerializeField] private float crouchSpeed = 2.5f;
    
    private CharacterInputs _inputs;
    private CharacterController _controller;
    private float _originalHeight;
    private Vector3 _originalCenter;
    
    private Vector3 _verticalVelocity;
    private bool _landingDone = true;
    
    private Camera  _mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _inputs = GetComponent<CharacterInputs>();
        _controller = GetComponent<CharacterController>();
        _mainCamera = Camera.main;
        
        _originalHeight = _controller.height;
        _originalCenter = _controller.center;
    }

    // Update is called once per frame
    void Update()
    {
        float moveMagnitude = _inputs.InputMove.magnitude;
        
        float currentSpeed = _inputs.Crouch ? crouchSpeed : horizontalSpeed;
        
        if (_inputs.Crouch)
        {
            _controller.height = _originalHeight * 0.5f; 
            _controller.center = _originalCenter * 0.5f;
            
        }
        else
        {
            _controller.height = _originalHeight;
            _controller.center = _originalCenter;
        }
        
        Vector3 horizontalVelocity;
        if (_landingDone)
        {
            horizontalVelocity = transform.forward * (moveMagnitude * currentSpeed/*horizontalSpeed*/ * Time.deltaTime);
            
        }
        else
        {
            horizontalVelocity = Vector3.zero;
        }
        if (_gndDetector.Detected)
        {
            if (_inputs.Jump && _landingDone)
            {
                //_verticalVelocity = Vector3.up  * _jumpVelocity;
                _verticalVelocity = Vector3.up * Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y);
                _inputs.Jump = false;
            }
            
        }
        else
        {
            //Is Not grounded : Jumping, Falling
            if (_controller.velocity.y > 0)
            {
                _verticalVelocity += Physics.gravity * Time.deltaTime;
            }
            else
            {
                _verticalVelocity += Physics.gravity * (_fallFactor * Time.deltaTime);
            }
           
        }
        
        Quaternion inputRotation = Quaternion.LookRotation(new Vector3(_inputs.InputMove.x, 0, _inputs.InputMove.y), Vector3.up);
        Quaternion cameraRotation = _mainCamera.transform.rotation;
        
        
        Quaternion rotation = Quaternion.Euler(0, cameraRotation.eulerAngles.y, 0) * inputRotation;
        
        _controller.Move(horizontalVelocity + _verticalVelocity * Time.deltaTime);
        
        if (horizontalVelocity.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _delay);
        }
        
        _animator.SetFloat("velocity", moveMagnitude);
        _animator.SetBool("isFalling", !_gndDetector.Detected && _controller.velocity.y < 0.01);
        _animator.SetBool("IsJumping", !_gndDetector.Detected && _controller.velocity.y > 0.01);
        _animator.SetBool("IsCrouching", _inputs.Crouch);
        
    }

    private void OnLandingBegin()
    {
        _landingDone = false;
        Debug.Log("OnLandingBegin");
    }

    private void OnLandingEnd()
    {
        _landingDone = true;
        Debug.Log("OnLandingEnd");
    }
    
}
