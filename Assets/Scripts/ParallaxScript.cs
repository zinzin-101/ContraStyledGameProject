using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] float parallaxAmount;
    Vector2 length;

    Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
        length = GetComponentInChildren<SpriteRenderer>().bounds.size;
    }

    void Update()
    {
        Vector3 relativePos = _camera.transform.position * parallaxAmount;
        Vector3 distance = _camera.transform.position - relativePos;
        if (distance.x > startPos.x + length.x)
        {
            startPos.x += length.x;
        }
        if (distance.x < startPos.x - length.x)
        {
            startPos.x -= length.x;
        }
        relativePos.z = startPos.z;
        transform.position = startPos + relativePos;

    }
}
