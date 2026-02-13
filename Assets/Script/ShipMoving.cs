using UnityEngine;

public class ShipMoving : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private bool _isMoving = true;

    void Update()
    {
        if (_isMoving)
        {
            transform.Translate(Vector3.right * (_speed * Time.deltaTime));
        }
    }

    
    public void StartMoving() => _isMoving = true;
    public void StopMoving() => _isMoving = false;
}
