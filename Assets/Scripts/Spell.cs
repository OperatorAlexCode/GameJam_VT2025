using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] float LifeTime;

    // Start is called before the first frame update
    void Start()
    {
        SpellLogic();
        StartCoroutine(Destructor());
    }

    public void SpellLogic()
    {

    }

    IEnumerator Destructor()
    {
        yield return new WaitForSeconds(LifeTime);
        Destroy(gameObject);
    }
}
