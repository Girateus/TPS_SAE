using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{ 
    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private string animationTriggerName = "Flying away";
    [SerializeField] private float animationWaitTime = 2f;

    private bool _isExiting;

    private void OnTriggerEnter(Collider other)
    {
        if (_isExiting) return;

        if (other.CompareTag("Player") || other.GetComponent<PlayerController>() != null)
        {
            _isExiting = true;
            StartCoroutine(ExitRoutine());
            Debug.Log("Bye bye");
        }
    }

    private IEnumerator ExitRoutine()
    {
        if (animator != null && !string.IsNullOrWhiteSpace(animationTriggerName))
            animator.SetTrigger(animationTriggerName);
        yield return new WaitForSeconds(animationWaitTime);
        
        var sceneName = TrueEnding.SpecialEnding ? "True" : "Normal";
        SceneManager.LoadScene(sceneName);
    }
}
