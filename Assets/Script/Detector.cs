using System;
using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _layer;
    
    public bool Detected = false;
    private readonly Collider[] _hits = new Collider[1];
    
    // Update is called once per frame
    void Update()
    {
        int hitCount= Physics.OverlapSphereNonAlloc(transform.position, _radius, _hits, _layer);
        Detected = hitCount > 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Detected ? Color.green : Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
