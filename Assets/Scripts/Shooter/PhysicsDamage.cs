using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsDamage : MonoBehaviour, ITakeDamage
{
    private Rigidbody rb;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void TakeDamage(Weapon weapon, Projectile projectile, Vector3 contactPoint)
    {
        rb.AddForce(projectile.transform.forward * weapon.GetShootingForce(), ForceMode.Impulse);
    }
}
