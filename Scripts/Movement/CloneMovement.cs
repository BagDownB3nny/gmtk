using UnityEngine;

public class CloneMovement : Movement
{
    public void SetMovement(Vector2 movement)
    {
        this.movement = movement;
    }

    public void SetMousePosition(Vector2 mousePos)
    {
        this.mousePos = mousePos;
    }
}
