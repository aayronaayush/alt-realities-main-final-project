using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]
public class Weapon : MonoBehaviour
{
    [SerializeField] public float shootingForce;
    [SerializeField] public Transform bulletSpawn;
    [SerializeField] public float recoilForce;
    [SerializeField] public float damage;

    private Rigidbody rigidBody;
    private XRGrabInteractable interactableWeapon;

    protected virtual void Awake()
    {
        interactableWeapon = GetComponent<XRGrabInteractable>();
        rigidBody = GetComponent<Rigidbody>();

        SetupInteractableWeaponEvents();
    }

    private void SetupInteractableWeaponEvents()
    {
        interactableWeapon.selectEntered.AddListener(PickUpWeapon);
        interactableWeapon.selectExited.AddListener(DropWeapon); 
        interactableWeapon.activated.AddListener(StartShooting); 
        interactableWeapon.deactivated.AddListener(StopShooting); 
    }

    protected virtual void StopShooting(DeactivateEventArgs arg0)
    {
        // throw new NotImplementedException();
    }

    protected virtual void StartShooting(ActivateEventArgs arg0)
    {
        // throw new NotImplementedException();
    }

    private void DropWeapon(SelectExitEventArgs arg0)
    {
        // throw new NotImplementedException();
    }

    private void PickUpWeapon(SelectEnterEventArgs arg0)
    {
        // throw new NotImplementedException();
    }

    protected virtual void Shoot()
    {
        ApplyRecoil();
    }

    private void ApplyRecoil()
    {
        rigidBody.AddRelativeForce(Vector3.back * recoilForce, ForceMode.Impulse);
    }

    public float GetShootingForce()
    {
        return shootingForce;
    }

    public float GetDamage()
    {
        return damage;
    }
}
