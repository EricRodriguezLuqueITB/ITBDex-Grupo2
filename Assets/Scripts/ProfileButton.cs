using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ProfileButton : MonoBehaviour, IPointerUpHandler
{
    public Fakemon fk;
    public Button btn;

    public void OnClick()
    {
        GameObject profile = GameObject.Find("Canvas").transform.Find("Profile").gameObject;

        profile.GetComponent<Profile>().SetFakemon(fk);
        profile.SetActive(true);

        GameObject.Find("Canvas").transform.Find("Dex").gameObject.SetActive(false);
    }
    public void OnPointerUp(PointerEventData a)
    {
        transform.parent.parent.parent.GetComponent<Dex>().SetPixelArt(fk.fakename);
    }
}
