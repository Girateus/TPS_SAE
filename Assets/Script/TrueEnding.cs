using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TrueEnding : MonoBehaviour
{
    [SerializeField] private UnityEvent _trueEndingBegin;
    [SerializeField] private UnityEvent _trueEndingend;
    
    public MeshRenderer _meshRenderer;
    public static bool SpecialEnding { get; private set; }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.GetComponent<PlayerController>() != null)
        {
            Collect();
        }
    }

    private void Collect()
    {
        StartCoroutine(FoundTape());
        _trueEndingBegin.Invoke();
       // _meshRenderer.enabled = false; 
        SpecialEnding = true;
       
    }

    private IEnumerator FoundTape()
    {
        yield return new WaitForSeconds(2.5f);
        TrueEndingBegin();
    }

    private void TrueEndingBegin()
    {
        _trueEndingend.Invoke();
       // Destroy(gameObject);
    }
}
