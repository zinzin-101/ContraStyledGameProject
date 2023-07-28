using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] PlayerInput playerObject;
    private PlayerHealthScript playerHealthScript;

    [SerializeField] float cameraFollowBoundaryX = 3f;
    [SerializeField] float cameraFollowBoundaryY = 3f;

    [SerializeField] Vector2 minCameraFollowLimit;
    [SerializeField] Vector2 maxCameraFollowLimit;
    [SerializeField] bool limitCameraFollow;

    [SerializeField] float smoothTime = 0.3f;
    private float defaultSmoothTime;
    private Vector3 refVelocity = Vector3.zero;

    private void Awake()
    {
        playerObject.TryGetComponent(out playerHealthScript);

        if (minCameraFollowLimit == Vector2.zero || maxCameraFollowLimit == Vector2.zero)
        {
            limitCameraFollow = false;
        }

        defaultSmoothTime = smoothTime;
    }

    void FixedUpdate()
    {
        if (!playerHealthScript.PlayerAlive)
        {
            return;
        }

        Vector3 newPosition = transform.position;

        if (transform.position.x - playerObject.transform.position.x <= -cameraFollowBoundaryX)
        {
            newPosition.x = transform.position.x + cameraFollowBoundaryX;
        }
        else if (transform.position.x - playerObject.transform.position.x >= cameraFollowBoundaryX)
        {
            newPosition.x = transform.position.x - cameraFollowBoundaryX;
        }

        if (transform.position.y - playerObject.transform.position.y <= -cameraFollowBoundaryY)
        {
            newPosition.y = transform.position.y + cameraFollowBoundaryY;
        }
        else if (transform.position.y - playerObject.transform.position.y >= cameraFollowBoundaryY)
        {
            newPosition.y = transform.position.y - cameraFollowBoundaryY;
        }

        if (limitCameraFollow)
        {
            if (newPosition.x > maxCameraFollowLimit.x || newPosition.x < minCameraFollowLimit.x)
            {
                newPosition.x = transform.position.x;
            }

            if (newPosition.y > maxCameraFollowLimit.y || newPosition.y < minCameraFollowLimit.y)
            {
                newPosition.y = transform.position.y;
            }
        }

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref refVelocity, smoothTime);
    }
}
