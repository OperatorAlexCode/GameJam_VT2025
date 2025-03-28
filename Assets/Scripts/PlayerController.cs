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

    // Start is called before the first frame update
    void Start()
    {
        CrystalRing = GetComponentInChildren<CrystalRing>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)MoveDirection * Time.deltaTime;
    }

    public void Move(CallbackContext context)
    {
        MoveDirection = context.ReadValue<Vector2>() * Speed;
    }

    public void CycleCrystal(CallbackContext context)
    {
        if (context.started)
        {
            float value = context.ReadValue<float>();

           CrystalRing.CycleCrystal(value);
        }
    }
}
