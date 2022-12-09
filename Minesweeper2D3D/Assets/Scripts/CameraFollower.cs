using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public GameObject character;

    public float smoothSpeed = 5f;

    private Vector3 dist;

    // Start is called before the first frame update
    void Start()
    {
        dist = character.transform.position - transform.position;
    }

    void LateUpdate()
    {
        Vector3 nextPos = character.transform.position - dist;
        transform.position = Vector3.Lerp(transform.position, nextPos, smoothSpeed * Time.deltaTime);
    }
}
