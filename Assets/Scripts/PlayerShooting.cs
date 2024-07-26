using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class PlayerShooting : NetworkBehaviour
{
    [SerializeField] private Rigidbody myRigidbody; //this is optional
    [SerializeField] private float shootingStrength;
    [SerializeField] private NetworkBullet bulletPrefab;
    [SerializeField] private Transform shootingPoint;

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ShootRPC();
        }
    }

    [Rpc(SendTo.Server)]
    void ShootRPC()
    {

        NetworkBullet temporaryBullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
        temporaryBullet.shooterOrigin = OwnerClientId;
        temporaryBullet.NetworkObject.Spawn();

        //adding the tank velocity is optional
        //temporaryBullet.GetComponent<Rigidbody>().AddForce(myRigidbody.velocity + temporaryBullet.transform.forward * shootingStrength, ForceMode.VelocityChange);

        //Destroy(temporaryBullet, 3f);

    }
}
