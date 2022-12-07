using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform player;
    private float smoothFollow = .5f;
    private float offsetAxisY = 2.2f;

    private void FixedUpdate()
    {
        Vector3 playerPosition = new Vector3(player.position.x, player.position.y + offsetAxisY, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, playerPosition, smoothFollow);
        transform.position = smoothedPosition;
    }
}
