using UnityEngine;

[RequireComponent(typeof(CharacterInputs))]
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 5;
    [SerializeField] private Detector _gndDetector;
    [SerializeField] private float _delay = 0.05f;
    private CharacterInputs _inputs;
    private CharacterController _controller;
    private Animator _animator;
    
    private Vector3 _verticalVelocity;
    
    private Camera  _mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _inputs = GetComponent<CharacterInputs>();
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float moveMagnitude = _inputs.InputMove.magnitude;
        
        Vector3 horizontalVelocity = transform.forward * (moveMagnitude * horizontalSpeed * Time.deltaTime);
        if (_gndDetector.Detected)
        {
            _verticalVelocity = Vector3.zero;
        }
        else
        {
            _verticalVelocity += Physics.gravity * Time.deltaTime;
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
        
    }
    
}
