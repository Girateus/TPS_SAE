using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInputs : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector2 InputMove;

    public void OnMove(InputAction.CallbackContext ctx) => InputMove = ctx.ReadValue<Vector2>();
    
}
