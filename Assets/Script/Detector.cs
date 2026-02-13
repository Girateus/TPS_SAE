using System;
using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _layer;
    
    public bool Detected = false;
    
    // Update is called once per frame
    void Update()
    {
        Detected = Physics.CheckSphere(transform.position, _radius, _layer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Detected ? Color.green : Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
