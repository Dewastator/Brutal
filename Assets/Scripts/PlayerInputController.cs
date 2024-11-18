using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputController : MonoBehaviour
{
    public Vector2 movePostion;


    public void OnMove(CallbackContext callbackContext)
    {
        movePostion = callbackContext.ReadValue<Vector2>();
    }
}
