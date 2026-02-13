using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInputs : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector2 InputMove;
    public bool Jump;
    public bool Crouch;
    public bool Flashlight;

    public void OnMove(InputAction.CallbackContext ctx) => InputMove = ctx.ReadValue<Vector2>();
    public void OnJump(InputAction.CallbackContext ctx) => Jump = ctx.ReadValueAsButton();
    public void OnCrouch(InputAction.CallbackContext ctx) 
    {
        if (ctx.started) Crouch = !Crouch; 
    }

    public void OnFlashlight(InputAction.CallbackContext ctx)
    {
        if (ctx.started) Flashlight = !Flashlight;
    }
    private void Update()
    {
        Debug.Log("CharacterInputs" + InputMove);
    }
}
