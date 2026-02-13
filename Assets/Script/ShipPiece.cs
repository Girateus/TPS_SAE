using UnityEngine;
using System;

public class ShipPiece : MonoBehaviour
{
    
    public event Action<ShipPiece> OnPieceCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.GetComponent<PlayerController>() != null)
        {
            Collect();
        }
    }

    private void Collect()
    {
        
        OnPieceCollected?.Invoke(this);
        Destroy(gameObject);
    }
}