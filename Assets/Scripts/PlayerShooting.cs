using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class PlayerShooting : NetworkBehaviour
{
    [SerializeField] private NetworkBullet bulletPrefab;
    [SerializeField] private Transform shootingPoint;

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
    }
}
