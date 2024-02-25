using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProfileButton : MonoBehaviour
{
    public Fakemon fk;
    
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }
    private void OnClick()
    {
        GameObject profile = GameObject.Find("Canvas").transform.Find("Profile").gameObject;

        profile.GetComponent<Profile>().SetFakemon(fk);
        profile.SetActive(true);

        GameObject.Find("Canvas").transform.Find("Dex").gameObject.SetActive(false);
    }
}
