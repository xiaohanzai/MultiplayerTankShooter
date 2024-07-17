using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float turningSpeed;
    [SerializeField] private Rigidbody myRigidbody;
    private float horizontalInput;
    private float verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        //MOVE RIGIDBODY HERE
        myRigidbody.velocity = transform.forward * verticalInput * speed;
        myRigidbody.rotation = Quaternion.Euler(0, transform.eulerAngles.y + (horizontalInput * turningSpeed), 0);
    }
}
