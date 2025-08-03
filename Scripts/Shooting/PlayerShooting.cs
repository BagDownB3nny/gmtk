using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : Shooting
{
    void Update()
    {
        if (InputSystem.actions.FindAction("Attack").triggered)
        {
            Shoot();
        }
    }

    public override void Shoot()
    {
        base.Shoot();
        RecorderManager.instance.RecordFire();
    }
}
