using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private GameObject icon;

    public void SetFakemon(Fakemon fk)
    {
        this.fk = fk;
        SetText(fakename, fk.fakename);
        SetText(nickname, fk.info.Split('|')[0]);
        SetText(description, fk.info.Split('|')[1]);

        List<Sprite> result = GameManager.instance.GetComponent<GameManager>().pixelArts.Where(item => item.name.ToLower().Contains(fk.fakename.ToLower())).ToList();

        icon.GetComponent<UnityEngine.UI.Image>().sprite = result.Count > 0 ? result[0] : null;
        icon.GetComponent<UnityEngine.UI.Image>().color = result.Count > 0 ? Color.white : new Color(0,0,0,0);

        SetText(icon, "");
        GameManager.instance.SetModel(fk.fakename);
    }
    private void SetText(GameObject obj, string text)
    {
        obj.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }
}
