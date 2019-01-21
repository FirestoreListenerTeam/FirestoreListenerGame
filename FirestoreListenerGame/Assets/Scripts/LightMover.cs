using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMover : MonoBehaviour
{
    public float speed = 1.0f;
    public float radius = 1.0f;
    public float minX, maxX = 0.0f;
    public float minZ, maxZ = 0.0f;
    public float defaultY = 0.0f;

    private float posX, posZ = 0.0f;
    private bool needPoint = true;

    void Update()
    {
        if (needPoint)
        {
            NewPoint();
            needPoint = false;
        }

        Vector3 direction = new Vector3(posX, defaultY, posZ) - transform.position;
        if (direction.magnitude <= radius)
            needPoint = true;
        else
        {
            direction.Normalize();
            direction *= speed * Time.deltaTime;
            transform.position += direction;
        }
    }

    void NewPoint()
    {
        posX = Random.Range(minX, maxX);
        posZ = Random.Range(minZ, maxZ);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(Vector3.zero, new Vector3(Mathf.Abs(maxX - minX), 1.0f, Mathf.Abs(maxZ - minZ)));
    }
}