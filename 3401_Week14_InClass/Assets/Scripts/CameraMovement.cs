using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 pointer;
    Vector3 lastPosition;
    Vector3 offset;
    public Transform player;
    public float mouseSensitivity = .01f;
    public bool willFollow = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (willFollow == false)
        {
            if (Input.GetMouseButtonDown(1))
            {
                lastPosition = Input.mousePosition;
            }
            if (Input.GetMouseButton(1))
            {
                pointer = lastPosition - Input.mousePosition;
                transform.Translate(pointer.x * mouseSensitivity, pointer.y * mouseSensitivity, 0);
                lastPosition = Input.mousePosition;
            }
           
        }
        if (willFollow == true)
        {
            FollowPlayer();
        }
    }
    public void FollowPlayer()
    {
        transform.position = new Vector3(player.transform.position.x, player.position.y, -10);
    }
    public void WillFollow()
    {
        transform.position = new Vector3(0, 0, -10);
        willFollow = true;
    }
}