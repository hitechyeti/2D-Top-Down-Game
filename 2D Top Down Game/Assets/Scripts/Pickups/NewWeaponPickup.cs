using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class NewWeaponPickup : MonoBehaviour
{
    private enum NewWeapon
    {
        WoodenSword,
        StoneSword,
        ShortBow,
        LongBow,
        ElectricStaff,
        FireStaff
    }

    [SerializeField] private NewWeapon newWeapon;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {

    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            DetectNewWeapon();
            Destroy(gameObject);
        }
    }

    private void DetectNewWeapon()
    {
        switch (newWeapon)
        {
            case NewWeapon.WoodenSword:
                ActiveInventory.Instance.SetWoodenSword();
                break;
            case NewWeapon.StoneSword:
                ActiveInventory.Instance.SetStoneSword();
                break;
            case NewWeapon.ShortBow:
                ActiveInventory.Instance.SetShortBow();
                break;
            case NewWeapon.LongBow:
                ActiveInventory.Instance.SetLongBow();
                break;
            case NewWeapon.ElectricStaff:
                ActiveInventory.Instance.SetElectricStaff();
                break;
            case NewWeapon.FireStaff:
                ActiveInventory.Instance.SetFireStaff();
                break;

            default:
                break;
        }
    }
}
