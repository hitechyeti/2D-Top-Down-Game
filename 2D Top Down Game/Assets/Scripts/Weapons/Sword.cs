using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{

    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private GameObject circleSlashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    [SerializeField] private float swordAttackCD = 0.5f;
    [SerializeField] private WeaponInfo weaponInfo;
    
    private PolygonCollider2D weaponCollider;
    private Animator anim;
    private GameObject slashAnim;

    private bool swordStabing = false;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        weaponCollider = GetComponent<PolygonCollider2D>();
    }

    private void Start()
    {
        slashAnimSpawnPoint = GameObject.Find("SlashAnimSpawnPoint").transform;
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
        slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
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
    }

    public void SwingUpFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180f, 0f, 0f);

        if (PlayerController.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        if (PlayerController.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
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
                weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
            }
            else
            {
                ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
                weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
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
