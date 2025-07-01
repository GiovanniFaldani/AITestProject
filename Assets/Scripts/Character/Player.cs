using UnityEngine;

public class Player : Character
{
    private void Update()
    {
        GetInputs();
        TurnTowardsLookDirection();
        ApplyMovement();
        CheckForShot();
    }

    private void GetInputs()
    {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        fire = Input.GetAxisRaw("Fire1");
    }
}
