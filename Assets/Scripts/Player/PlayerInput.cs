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

    [SerializeField] GameObject walkParticleObject;
    private ParticleSystem walkParticle;
    private Transform walkParticleTransform;

    void Start()
    {
        TryGetComponent(out movementScript);
        TryGetComponent(out  projectileSpawner);
        TryGetComponent(out  animController);

        if (walkParticleObject != null)
        {
            walkParticleObject.TryGetComponent(out walkParticle);
            walkParticleObject.TryGetComponent(out walkParticleTransform);
        }

        SoundManager.PlayerWalk.Play();
        walkParticle.Stop();
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal") * moveSpeed;

        EffectsHandler();

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

        if (movementScript.OnWall)
        {
            movementScript.SetRigidbodyVelocity(new Vector3(movementScript.GetCurrentVelocity().x, -movementScript.GetCurrentGravity(),0f));
        }

        animController.SetFloat("Speed", Mathf.Abs(horizontalInput));

        if (jumpInput)
        {
            jumpInput = false;
            movementScript.Jump(jumpForce);
        }
    }

    void EffectsHandler()
    {
        if (horizontalInput == 0f || !movementScript.Grounded)
        {
            SoundManager.PlayerWalk.Pause();

            if (walkParticleObject != null)
            {
                if (walkParticle.isPlaying)
                {
                    walkParticle.Stop();
                }
            }
        }
        else if (Mathf.Abs(horizontalInput) > 0 && movementScript.Grounded)
        {
            if (horizontalInput > 0f)
            {
                walkParticleTransform.rotation = Quaternion.Euler(0, 0, 135);
            }
            else if (horizontalInput < 0f)
            {
                walkParticleTransform.rotation = Quaternion.Euler(0, 180, 135);
            }

            SoundManager.PlayerWalk.UnPause();

            if (walkParticleObject != null)
            {
                if (!walkParticle.isPlaying)
                {
                    walkParticle.Play();
                }
            }
        }
    }
}
