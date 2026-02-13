using Unity.Cinemachine;
using UnityEngine;

public class WatchTower : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _playerCam;
    [SerializeField] private CinemachineCamera _watchTowerCam;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _watchTowerCam.Priority = _playerCam.Priority + 1;
        }
        
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _watchTowerCam.Priority = 0;
            Debug.Log("Exit");
        }
        
    }
}
