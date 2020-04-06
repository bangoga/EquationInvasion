using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    private Vector3 camera_offset;

    void Start()
    {
        camera_offset = this.transform.position - new Vector3(player.transform.position.x,0, player.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(player.transform.position.x,0,0) + camera_offset;
    }
}
