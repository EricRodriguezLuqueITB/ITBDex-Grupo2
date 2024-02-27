using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Profile : MonoBehaviour
{
    public Fakemon fk;
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject fakename;
    [SerializeField] private GameObject nickname;
    [SerializeField] private GameObject description;
    [SerializeField] private GameObject map;
    [SerializeField] private GameObject icon;

    public void SetFakemon(Fakemon fk)
    {
        this.fk = fk;
        SetText(fakename, fk.fakename);
        SetText(nickname, $"{fk.season} fakemon");
        SetText(description, fk.info);

        List<Sprite> result = GameObject.Find("GameManager").GetComponent<GameManager>().pixelArts.Where(item => item.name.Contains(fk.fakename)).ToList();

        icon.GetComponent<UnityEngine.UI.Image>().sprite = result.Count > 0 ? result[0] : null;

        SetText(icon, "");

    }
    private void SetText(GameObject obj, string text)
    {
        obj.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }
}
