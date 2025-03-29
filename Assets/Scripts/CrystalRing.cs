using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class CrystalRing : MonoBehaviour
{
    [SerializeField] List<GameObject> Crystals;
    //[SerializeField] GameObject Crystal;
    //[SerializeField] int MaxCrystals;
    [SerializeField] float Radius = 1.5f;
    [SerializeField] float RotationSpeed = 1;
    float Rotation;
    public int RotationDirection = 1;

    [SerializeField] float NormalScale = 0.5f;
    [SerializeField] float SelectedScale = 1.5f;
    [SerializeField] int SelectedCrystal = 0;

    // Start is called before the first frame update
    void Start()
    {
        //for (int x = 0; x < MaxCrystals; x++)
        //    AddCrystal(this.Crystal);

        Crystals[SelectedCrystal].transform.localScale = Vector3.one * SelectedScale;
    }

    // Update is called once per frame
    void Update()
    {
        for (int x = 0; x < Crystals.Count; x++)
        {
            float angle = x * (360/Crystals.Count) + Rotation;

            angle = (angle%360) * Mathf.Deg2Rad;

            Crystals[x].transform.position = (Vector3)GetForwardVector(transform.position.x, transform.position.y,angle,Radius);

            if (x != SelectedCrystal)
                Crystals[x].transform.localScale = Vector3.one * NormalScale;
        }

        Rotation += RotationSpeed*Time.deltaTime*RotationDirection;

        if (RotationDirection < 0 && Rotation < 0)
            Rotation = 360f + Rotation;
        
        else
            Rotation = Rotation%360;

        ScaleSelectedCrystal();
    }

    public void CycleCrystal(float direction)
    {
        if (Crystals.Count > 0)
        {
            if (RotationDirection != direction)
            {
                RotationDirection = (int)direction;
            }

            else
            {
                Crystals[SelectedCrystal].transform.localScale /= SelectedScale;

                SelectedCrystal += (int)direction;

                if (SelectedCrystal < 0)
                    SelectedCrystal = Crystals.Count - 1;

                else
                    SelectedCrystal = SelectedCrystal % Crystals.Count;

                Crystals[SelectedCrystal].transform.localScale *= SelectedScale;
            }
        }
    }

    public void AddCrystal(GameObject newCrystal)
    {
        //if (Crystals.Count < MaxCrystals)
        Crystals.Insert(0,Instantiate(newCrystal, transform));
        Crystals[0].GetComponent<SpriteRenderer>().enabled = true;

        if (Crystals.Count == 1)
            ScaleSelectedCrystal();

        else
            CycleCrystal(1);
    }

    public void UseCrystal()
    {
        if (Crystals.Count > 0)
        {
            GameObject crystalUsed = Crystals[SelectedCrystal];
            crystalUsed.transform.localScale = Vector3.one * NormalScale;

            float angle = SelectedCrystal * (360 / Crystals.Count) + Rotation;
            angle = (angle % 360) * Mathf.Deg2Rad;

            GetComponentInParent<PlayerController>().StoredCrystals.Enqueue(crystalUsed);
            crystalUsed.GetComponent<Crystal>().UseCrystal(GetForwardVector(0, 0, angle, 1));
            
            Crystals.RemoveAt(SelectedCrystal);
            SelectedCrystal = SelectedCrystal % Crystals.Count;
            ScaleSelectedCrystal();
        }
    }

    void ScaleSelectedCrystal()
    {
        Crystals[SelectedCrystal].transform.localScale = Vector3.one * SelectedScale;
    }

    Vector2 GetForwardVector(float x, float y, float a, float r)
    {
        return new Vector2(x + Mathf.Sin(a) * r, y + Mathf.Cos(a) * r);
    }
}