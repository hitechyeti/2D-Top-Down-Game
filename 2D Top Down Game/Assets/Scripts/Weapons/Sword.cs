using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private float swordAttackCD = 0.5f;
    [SerializeField] private WeaponInfo weaponInfo;

    private PolygonCollider2D weaponCollider;
    private Animator anim;

    private bool swordStabing = false;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        weaponCollider = GetComponent<PolygonCollider2D>();
    }

    private void Start()
    {
        weaponCollider.enabled = false;
    }

    private void Update()
    {
        MouseFollowWithOffset();
        MouseFollowStabbing();
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
        weaponCollider.enabled = true;
        SwordTrail.Instance.SwordTrailOn();
    }

    public void AltAttack()
    {
        anim.SetTrigger("AltAttack");
        weaponCollider.enabled = true;
        swordStabing = true;
    }

    public void EndAttackStabing()
    {
        swordStabing = false;
        weaponCollider.enabled = false;
    }

    public void DoneAttackingAnimEvent()
    {
        weaponCollider.enabled = false;
        SwordTrail.Instance.SwordTrailOff();
    }

    private void MouseFollowWithOffset()
    {
        if (!swordStabing)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

            if (mousePos.x < playerScreenPoint.x)
            {
                ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            }
            else
            {
                ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }

    private void MouseFollowStabbing()
    {
        if (swordStabing)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            Vector2 direction = transform.position - mousePosition;

            transform.right = -direction;
        }
    }

   
}
