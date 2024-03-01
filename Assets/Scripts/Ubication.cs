using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Ubication : MonoBehaviour
{
    [SerializeField] private List<Transform> ubications;
    [SerializeField] private Transform marker;
    [SerializeField] private GameObject profile;
    private string season;
    private float clock;
    private int movement;
    private void OnEnable()
    {
        movement = 5;
        transform.localPosition = new Vector3(0, transform.localPosition.y + movement, 0);
        season = profile.GetComponent<Profile>().fk.season;
        SetMarker();
    }
    private void Update()
    {
        if (Time.time - clock > 0.2f)
        {
            switch (transform.localPosition.y)
            {
                case <= 0:
                case >= 20:
                    movement *= -1;
                    break;
            }

            transform.localPosition = new Vector3 ( 0, transform.localPosition.y + movement, 0);

            clock = Time.time;
        }
    }
    public void SetMarker()
    {
        List<Transform> ubis = ubications.Where(item => item.name == "Ubi" + season).ToList();
        if (ubis.Count() == 0)
        {
            marker.position = new( 0, 0, 5);
            return;
        }
        marker.position = ubis[0].position;
    }
}
