using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : PawnInput
{
    private MovementScript movementScript;
    private PlayerProjectileSpawner projectileSpawner;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpForce = 5f;

    float horizontalInput = 0f;
    bool jumpInput;

    void Start()
    {
        TryGetComponent(out movementScript);
        TryGetComponent(out  projectileSpawner);
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal") * moveSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jumpInput = true;
        }

        if (Input.GetButton("Fire1"))
        {
            projectileSpawner.SpawnProjectile();
        }
    }

    private void FixedUpdate()
    {
        movementScript.Move(horizontalInput);

        if (jumpInput)
        {
            jumpInput = false;
            movementScript.Jump(jumpForce);
        }
    }
}
