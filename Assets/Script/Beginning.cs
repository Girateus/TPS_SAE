using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Beginning : MonoBehaviour
{
   [SerializeField] private UnityEvent _beginning;
   [SerializeField] private UnityEvent _start;
   [SerializeField] private TMP_Text _text;
   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         StartCoroutine(BeginSpeech());
      }
   }

   private IEnumerator BeginSpeech()
   {
      _beginning.Invoke();
      yield return new WaitForSeconds(3.5f);
      Start();
   }
   
   private void Start()
    {
        _start.Invoke();
        
    }

   private void OnTriggerExit(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         Destroy(gameObject);
         Destroy(_text);
      }
         
   }

  
}
