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
    private void OnEnable()
    {
        season = profile.GetComponent<Profile>().fk.season;
        SetMarker();
    }
    public void SetMarker()
    {
        List<Transform> ubis = ubications.Where(item => item.name == "Ubi" + season).ToList();
        Debug.Log("Buscando: Ubi" + season);
        if (ubis.Count() == 0)
        {
            marker.position = new( 0, 0, 5);
            return;
        }
        marker.position = ubis[0].position;
    }
}
