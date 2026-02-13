using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private PiecesManager _piecesManager;

    void Update()
    {
        SetCounter(_piecesManager.CollectedCount, _piecesManager.MaxPieces);
    }

    public void SetCounter(int count, int maxCount)
    {
     
        _text.text = $"{count.ToString("D2")} / {maxCount.ToString("D2")}";
    }
}