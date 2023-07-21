using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] Transform pos1, pos2;
    private Vector3 next_pos;

    void Start()
    {
        next_pos = pos1.localPosition;

    }

    void FixedUpdate()
    {
        if (transform.localPosition == pos1.localPosition)
        {
            next_pos = pos2.localPosition;
        }

        if (transform.localPosition == pos2.localPosition)
        {
            next_pos = pos1.localPosition;
        }

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, next_pos, moveSpeed * 0.1f);
    }
}
