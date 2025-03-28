using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalRing : MonoBehaviour
{
    [SerializeField] List<GameObject> Crystals;
    [SerializeField] float Radius = 1.5f;
    [SerializeField] float RotationSpeed = 1;
    float Rotation;

    [SerializeField] float SelectedScale = 1.5f;
    [SerializeField]
    int SelectedCrystal = 0;

    // Start is called before the first frame update
    void Start()
    {
        Crystals[SelectedCrystal].transform.localScale *= SelectedScale;
    }

    // Update is called once per frame
    void Update()
    {
        for (int x = 0; x < Crystals.Count; x++)
        {
            float angle = x * (360/Crystals.Count) + Rotation;

            angle = (angle%360) * Mathf.Deg2Rad;

            Vector2 pos = Vector2.zero;
            pos.x = transform.position.x + Mathf.Sin(angle) * Radius;
            pos.y = transform.position.y + Mathf.Cos(angle) * Radius;

            Crystals[x].transform.position = (Vector3)pos;
        }

        Rotation += RotationSpeed*Time.deltaTime;
        Rotation = Rotation%360;
    }

    public void CycleCrystal(float direction)
    {
        Crystals[SelectedCrystal].transform.localScale /= SelectedScale;

        SelectedCrystal += (int)direction;

        if (SelectedCrystal < 0)
            SelectedCrystal = Crystals.Count-1;
        
        else
            SelectedCrystal = SelectedCrystal % Crystals.Count;

        Crystals[SelectedCrystal].transform.localScale *= SelectedScale;
    }
}