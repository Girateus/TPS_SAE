using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TrueEndingManager : MonoBehaviour
{
    [SerializeField] private UnityEvent _commander1;
    [SerializeField] private UnityEvent _recruit1;
    [SerializeField] private UnityEvent _commander2;
    [SerializeField] private UnityEvent _astley;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            
            StartCoroutine(TrueRoutine());
            Debug.Log("Bye bye");
        }
    }

    private IEnumerator TrueRoutine()
    {
        _commander1.Invoke();
        yield return new WaitForSeconds(3.5f);
        _recruit1.Invoke();
        yield return new WaitForSeconds(3.5f);
        _commander2.Invoke();
        yield return new WaitForSeconds(0.5f);
        _astley.Invoke();
    }
}
