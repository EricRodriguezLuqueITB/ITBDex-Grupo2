using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProfileButton : MonoBehaviour
{
    [SerializeField] private Button button;
    public Fakemon fk;
    
    private void Start()
    {
        button.onClick.AddListener(OnClick);
    }
    private void OnClick()
    {
        //Debug.Log($"prof: {GameObject.Find("Canvas").transform.Find("Profile").name} - dex: {GameObject.Find("Canvas").transform.Find("Dex").name}");
        GameObject profile = GameObject.Find("Canvas").transform.Find("Profile").gameObject;

        profile.GetComponent<Profile>().SetFakemon(fk);
        profile.SetActive(true);

        GameObject.Find("Canvas").transform.Find("Dex").gameObject.SetActive(false);
    }
}
