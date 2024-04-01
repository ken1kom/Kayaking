using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NetcodePlus.Demo
{

    [RequireComponent(typeof(CharacterController))]
public class LandBasedMovement : SNetworkPlayer
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    private PlayerControl playerControl;

    private float xMove, zMove;

    private void Awake()
    {
        playerControl = new PlayerControl();
    }
    private void OnEnable()
    {
        playerControl.Enable();
    }

    private void OnDisable()
    {
        playerControl.Disable();
    }


    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        playerMovmentControler();
        playerRotationControler();
    }
    // Update is called once per frame
    void playerMovmentControler()
    {
        xMove = playerControl.PlayerLandMovement.Movement.ReadValue<Vector2>().x;
        zMove = playerControl.PlayerLandMovement.Movement.ReadValue<Vector2>().x;
        float gravity = 9.8f;
        Vector3 moveAxis = new Vector3(xMove, -gravity, zMove);
        controller.Move(((moveAxis) * playerSpeed * Time.deltaTime));
    }
    void playerRotationControler()
    {
        if (xMove > 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 2f * Time.deltaTime);
        else if (xMove < 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 2f * Time.deltaTime);

        if (zMove > 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 2f * Time.deltaTime);
        else if (zMove < 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 2f * Time.deltaTime);
    }
    }
}
