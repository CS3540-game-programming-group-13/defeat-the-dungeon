using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    public static float mouseSensitivity = 100;
    private Transform player;
    private Vector3 offset;
    private float pitch = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = player.position - transform.position;
    }

    void Update()
    {
        transform.position = player.position - offset;
        float moveX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float moveY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        player.Rotate(Vector3.up * moveX);
        pitch -= moveY;
        pitch = Mathf.Clamp(pitch, -45f, 45f);
        transform.localRotation = Quaternion.Euler(pitch, 0, 0);
    }
}
