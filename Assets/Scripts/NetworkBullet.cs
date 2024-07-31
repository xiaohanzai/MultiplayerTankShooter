using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class NetworkBullet : NetworkBehaviour
{
    public ulong shooterOrigin;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float bulletSpeed;
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        rb.AddForce(rb.transform.forward * bulletSpeed, ForceMode.VelocityChange);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(IsServer && IsSpawned)
        {

            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if(playerHealth && playerHealth.OwnerClientId != shooterOrigin)
            {
                Debug.Log(shooterOrigin + " damaged " + playerHealth.OwnerClientId);
                
                playerHealth.GetDamaged(shooterOrigin);
            }

            NetworkObject.Despawn();
        }
    }
}
