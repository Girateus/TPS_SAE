using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class PiecesManager : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _shipCamera;
    [SerializeField] private CinemachineCamera _playerCamera;
    [SerializeField] private CharacterInputs _inputs;
    [SerializeField] private AIMouvement _mouvement;
    
    [SerializeField] private UnityEvent _onAllPiecesCollected;
    [SerializeField] private UnityEvent _RunYouFool;
    
    private List<ShipPiece> _pieces;
    
    public int CollectedCount { get; private set; }
    public int MaxPieces { get; private set; }
    
    

    void Start()
    {
        _pieces = GetComponentsInChildren<ShipPiece>().ToList();
        MaxPieces = _pieces.Count;
        CollectedCount = 0;

        foreach (ShipPiece piece in _pieces)
        {
            piece.OnPieceCollected += RemovePiece;
        }
    }

    void RemovePiece(ShipPiece piece)
    {
        if (_pieces.Contains(piece))
        {
            _pieces.Remove(piece);
            CollectedCount++;

            if (_pieces.Count <= 0)
            {
                ShipFixed();
                
            }
        }
    }

    private void ShipFixed()
    {
        _onAllPiecesCollected.Invoke();
        StartCoroutine(SwitchToShipCamera());
    }

    private IEnumerator SwitchToShipCamera()
    {
        //yield return new WaitForSeconds(1f);
        _shipCamera.Priority = _playerCamera.Priority +2;
        yield return new WaitForSeconds(5f);
        _shipCamera.Priority = 0;
        RunAway();
    }

    private void RunAway()
    {
        _RunYouFool.Invoke();
    }
}