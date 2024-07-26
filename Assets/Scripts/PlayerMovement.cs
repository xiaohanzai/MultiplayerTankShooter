using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float turningSpeed;
    [SerializeField] private Rigidbody myRigidbody;
    private float horizontalInput;
    private float verticalInput;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        //NEW START()
        //OR
        //NEW AWAKE()

    }

    void Update()
    {
        if(IsOwner)
        {
            if(IsServer && IsLocalPlayer)
            {
                horizontalInput = Input.GetAxis("Horizontal");
                verticalInput = Input.GetAxis("Vertical");
            }
            else if(IsClient && IsLocalPlayer)
            {
                ReceiveInputServerRpc(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            } 
        }

    }

    [ServerRpc]
    void ReceiveInputServerRpc(float h, float v)
    {

        horizontalInput = h;
        verticalInput = v;
    }

    private void FixedUpdate()
    {
        //MOVE RIGIDBODY HERE
        myRigidbody.velocity = transform.forward * verticalInput * speed;
        myRigidbody.rotation = Quaternion.Euler(0, transform.eulerAngles.y + (horizontalInput * turningSpeed), 0);
    }
}
