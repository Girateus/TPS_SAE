using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerItemCollector : MonoBehaviour
{

    [SerializeField] private float _radius;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private Rig _rig;
    [SerializeField] private Transform _aimObject;
    [SerializeField] private float _weightChangeSpeed = 0.001f;
    
    //Collider[] _items = new Collider[1];
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        Collider[] _items = Physics.OverlapSphere(transform.position + _offset, _radius,_layerMask);
        float rigWeight = 0;
        foreach (Collider item in _items)
        {
            if (item.TryGetComponent(out ShipPiece goodItem))
            {
                Debug.Log("Has Item" + goodItem.transform.position);
                SetConstraintSourceObjects(goodItem.transform);
                rigWeight = 1;
            }
        }
        _rig.weight = Mathf.Lerp(_rig.weight, rigWeight, Time.deltaTime *  _weightChangeSpeed);
    }

    private void SetConstraintSourceObjects(Transform obj)
    {
        /*foreach (MultiAimConstraint constraint in _aimConstraints)
        {
            var sources = new WeightedTransformArray();
            WeightedTransform v = new WeightedTransform(obj, 1);
            constraint.data.sourceObjects.Add(v);
        }*/
        
        _aimObject.position = obj.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + _offset, _radius);
    }
}
