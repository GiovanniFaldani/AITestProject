using UnityEngine;

public class Player : Character
{
    [SerializeField] GameObject respawnUI;

    private void Start()
    {
        spawnPoint = transform.position;
    }

    protected override void Update()
    {
        base.Update();
        if (Time.timeScale != 0.0f && isActive)
        {
            GetInputs();
            TurnTowardsLookDirection();
            ApplyMovement();
            CheckForShot();
        }
    }

    private void GetInputs()
    {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        fire = Input.GetAxisRaw("Fire1");
        // get mouse look direction
        lookDirection = Vector3.ProjectOnPlane(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position, Vector3.up).normalized;
    }

    protected override void DeactivateCharacter()
    {
        base.DeactivateCharacter();
        respawnUI.SetActive(true);
    }

    protected override void ActivateCharacter()
    {
        base.ActivateCharacter();
        respawnUI.SetActive(false);
    }
}
