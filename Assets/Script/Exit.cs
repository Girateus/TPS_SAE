using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{ 
    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private string animationTriggerName = "FlyAway";
    [SerializeField] private float animationWaitTime = 2f;
    [SerializeField] private CinemachineCamera _shipCamera;
    [SerializeField] private CinemachineCamera _playerCamera;
    [SerializeField] private UnityEvent _exit;

    private bool _isExiting;

    private void OnTriggerEnter(Collider other)
    {
        if (_isExiting) return;

        if (other.CompareTag("Player") || other.GetComponent<PlayerController>() != null)
        {
            _isExiting = true;
            StartCoroutine(ExitRoutine());
            _exit.Invoke();
            Debug.Log("Bye bye");
        }
    }

    private IEnumerator ExitRoutine()
    {
        if (animator != null && !string.IsNullOrWhiteSpace(animationTriggerName))
        {
            _shipCamera.Priority = _playerCamera.Priority +2;
            animator.SetTrigger(animationTriggerName);
        }
        yield return new WaitForSeconds(animationWaitTime);
        
        var sceneName = TrueEnding.SpecialEnding ? "True" : "Normal";
        SceneManager.LoadScene(sceneName);
    }
}
