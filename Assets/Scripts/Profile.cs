using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Profile : MonoBehaviour
{
    public Fakemon fk;
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject fakename;
    [SerializeField] private GameObject nickname;
    [SerializeField] private GameObject description;
    [SerializeField] private GameObject map;

    public void SetFakemon(Fakemon fk)
    {
        this.fk = fk;
        SetText(fakename, fk.fakename);
        SetText(nickname, "Es el fakemon " + fk.season);
        SetText(description, fk.info);
    }
    private void SetText(GameObject obj, string text)
    {
        obj.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }
}
