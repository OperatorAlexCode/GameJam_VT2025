using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int CurrentHealth;
    [SerializeField] int MaxHealth;
    [SerializeField] float Speed;
    Vector2 MoveDirection;
    CrystalRing CrystalRing;
    public Queue<GameObject> StoredCrystals = new();
    public bool CanTakeDamage = true;
    public bool IsDead;
    [SerializeField] float InvincibilityDuration = 1f;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
        CrystalRing = GetComponentInChildren<CrystalRing>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsDead)
            transform.position += (Vector3)MoveDirection * Time.deltaTime;

        GetComponent<Animator>().SetBool("Walking", MoveDirection != Vector2.zero);

    }

    public void Move(CallbackContext context)
    {
        if (!IsDead)
        {
            MoveDirection = context.ReadValue<Vector2>() * Speed;

            // Face sprite left
            if (MoveDirection.x < 0)
                GetComponent<SpriteRenderer>().flipX = true;

            // Face sprite right
            else if (MoveDirection.x > 0)
                GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public void CycleCrystal(CallbackContext context)
    {
        if (context.started)
        {
            float value = context.ReadValue<float>();
            
            CrystalRing.CycleCrystal(value);
        }
    }

    public void UseCrystal(CallbackContext context)
    {
        if (context.started)
            CrystalRing.UseCrystal();
    }

    public void TakeDamage(int dmg)
    {
        if (CanTakeDamage)
        {
            CurrentHealth = Mathf.Max(CurrentHealth - dmg, 0);

            if (CurrentHealth <= 0)
            {
                IsDead = true;
                CrystalRing.gameObject.SetActive(false);
                GetComponent<Animator>().SetTrigger("Death");
            }

            else
                StartCoroutine(SpriteFlicker());
        }
    }

    public void Heal(int hpToHeal)
    {
        CurrentHealth = Mathf.Min(CurrentHealth+hpToHeal,MaxHealth);
    }

    public void DrawNewCrystal()
    {
        CrystalRing.AddCrystal(StoredCrystals.Dequeue());
    }

    IEnumerator SpriteFlicker()
    {
        int flickers = 8;
        CanTakeDamage = false;

        for (float x = 0; x < InvincibilityDuration; x += 2)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(InvincibilityDuration / flickers);
            GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(InvincibilityDuration / flickers);
        }

        CanTakeDamage = true;
    }
}