using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace NetcodePlus.Demo
{
    public class CameraInitializer : MonoBehaviour
{

    [SerializeField]
    private Camera cam;
    [SerializeField]
    private GameObject virtualPlayerCam;


    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement[] playerMovements = FindObjectsOfType<PlayerMovement>();

        int layer = playerMovements.Length + 9;

        virtualPlayerCam.layer = layer;

        var bitMask = (1 << layer)
            | (1 << 0)
            | (1 << 1)
            | (1 << 2)
            | (1 << 4)
            | (1 << 5)
            | (1 << 8);

        cam.cullingMask = bitMask;
        cam.gameObject.layer = layer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    }
}
