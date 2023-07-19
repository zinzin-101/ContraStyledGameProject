using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private MovementScript movementScript;
    private PlayerProjectileSpawner projectileSpawner;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpForce = 5f;

    float horizontalInput = 0f;
    bool jumpInput;

    [SerializeField] AnimController animController;

    void Start()
    {
        TryGetComponent(out movementScript);
        TryGetComponent(out  projectileSpawner);
        TryGetComponent(out  animController);
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

        animController.SetFloat("Speed", Mathf.Abs(horizontalInput));

        if (jumpInput)
        {
            jumpInput = false;
            movementScript.Jump(jumpForce);
        }
    }
}
