using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : Singleton<PlayerHealth>
{
    public bool IsDead {  get; private set; }

    [SerializeField] private Sprite fullHeartImage, emptyHeartImage, noHeartImage;
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 10;
    [SerializeField] private float damageRecoveryTime = 1f;

    private Transform heartContainer;
    private int maxPossibleHealth = 10;
    private int currentHealth;
    private bool canTakeDamage = true;

    private Knockback knockback;
    private Flash flash;

    const string HEART_CONTAINER_TEXT = "New Heart Container";
    const string RESPAWN_AREA_TEXT = "Lvl_0";
    readonly int DEATH_HASH = Animator.StringToHash("Death");

    protected override void Awake()
    {
        base.Awake();

        knockback = GetComponent<Knockback>();
        flash = GetComponent<Flash>();
    }

    private void Start()
    {
        heartContainer = GameObject.Find(HEART_CONTAINER_TEXT).transform;

        IsDead = false;
        currentHealth = maxHealth;
        UpdateHeartImages();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

        if (enemy)
        {
            TakeDamage(1, other.transform);
        }
    }

    public void HealPlayer()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += 1;
            UpdateHeartImages();
        } 
    }

    public void TakeDamage(int damageAmount, Transform hitTransform)
    {
        if (!canTakeDamage) { return;  }

        ScreenShakeManager.Instance.ShakeScreen();
        knockback.GetKnockedBack(hitTransform, knockBackThrustAmount);
        StartCoroutine(flash.FlashRoutine());
        canTakeDamage = false;
        currentHealth -= damageAmount;
        StartCoroutine(DamageRecoveryRoutine());

        UpdateHeartImages();

        CheckIfPlayerDeath();
    }

    private void CheckIfPlayerDeath()
    {
        if (currentHealth <= 0 && !IsDead)
        {
            IsDead = true;
            Destroy(ActiveWeapon.Instance.gameObject);
            currentHealth = 0;
            GetComponent<Animator>().SetTrigger(DEATH_HASH);
            StartCoroutine(DeathLoadSceneRoutine());
        }
    }

    private IEnumerator DeathLoadSceneRoutine()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
        Stamina.Instance.ReplenishStaminaOnDeath();
        SceneManager.LoadScene(RESPAWN_AREA_TEXT);
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

    private void UpdateHeartImages()
    {
        for (int i = 0; i < maxPossibleHealth; i++)
        {
            Transform child = heartContainer.GetChild(i);
            Image image = child?.GetComponent<Image>();

                if (i <= currentHealth - 1)
                {
                    image.sprite = fullHeartImage;
                }
                else if(i <= maxHealth - 1)
                {
                    image.sprite = emptyHeartImage;
                }
                else
                {
                    image.sprite = noHeartImage;
                } 
        }
    }

}
