using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform playerPos;
    public Vector3 offset = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = new Vector3(playerPos.position.x, transform.position.y, playerPos.position.z);
        transform.position = position + offset;
    }

    void SetUpCameraInSceneView()
    {
        playerPos = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
        Vector3 position = new Vector3(playerPos.position.x, transform.position.y, playerPos.position.z);
        transform.position = position + offset;
    }

    private void OnValidate()
    {
        SetUpCameraInSceneView();
    }
}
