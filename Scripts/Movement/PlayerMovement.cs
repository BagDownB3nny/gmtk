using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Movement
{
    InputAction moveAction;
    InputAction lookAction;

    public Camera cam;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Point");

        moveAction.performed += OnMovePerformed;
        moveAction.canceled += OnMoveCancelled;
        lookAction.performed += OnLookPerformed;
    }

    void OnDestroy()
    {
        moveAction.performed -= OnMovePerformed;
        moveAction.canceled -= OnMoveCancelled;
        lookAction.performed -= OnLookPerformed;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
        RecorderManager.instance.RecordMove(movement);
    }

    private void OnMoveCancelled(InputAction.CallbackContext context)
    {
        movement = Vector2.zero;
        RecorderManager.instance.RecordMove(movement);
    }
    private void OnLookPerformed(InputAction.CallbackContext context)
    {
        Vector2 currentMousePos = cam.ScreenToWorldPoint(context.ReadValue<Vector2>());
        mousePos = currentMousePos;
        RecorderManager.instance.RecordAim(currentMousePos);
    }
}
