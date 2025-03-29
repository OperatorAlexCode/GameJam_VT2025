using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    [SerializeField] GameObject SpellPrefab;
    GameObject SpellInEffect = null;

    //private void Update()
    //{
    //    if (!GetComponent<SpriteRenderer>().enabled && SpellInEffect == null)
    //        Destroy(gameObject);
    //}

    public void UseCrystal(Vector2 forward)
    {
        SpellInEffect = Instantiate(SpellPrefab);
        SpellInEffect.transform.position = transform.position;
        SpellInEffect.transform.up = forward;
        GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(Destructor());
    }

    IEnumerator Destructor()
    {
        yield return new WaitUntil(() => SpellInEffect == null);
        yield return new WaitForSeconds(0.5f);
        GetComponentInParent<PlayerController>().DrawNewCrystal();
        Destroy(gameObject);
    }
}