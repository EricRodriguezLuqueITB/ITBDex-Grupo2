using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dex : MonoBehaviour
{
    [SerializeField] private GameObject prefabList;
    private Transform container;

    [SerializeField] private GameObject pixelArtCage;

    private void OnEnable()
    {
        container = transform.GetComponentsInChildren<Transform>().Where(item => item.name == "container").ToList()[0];
        CreateList(GameManager.instance.fakemons);
    }
    public void CreateList(List<Fakemon> fks)
    {
        ClearList();
        foreach (Fakemon f in fks)
        {
            GameObject obj = Instantiate(prefabList, container.transform);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = $"{f.id}º - {f.fakename} ({f.type})  / {f.season}";

            Image img = obj.GetComponent<Image>();

            switch (f.type)
            {
                case "Fire":
                    img.color = new Color(0.97f, 0.41f, 0.41f);
                    break;
                case "Water":
                    img.color = new Color(0.2f, 0.4f, 1);
                    break;
                case "Ground":
                    img.color = new Color(0.6f, 0.4f, 0.2f);
                    break;
                case "Wind":
                    img.color = new Color(0.8f, 1, 1);
                    break;
                case "Electric":
                    img.color = new Color(1, 1, 0.6f);
                    break;
            }

            foreach (var item in obj.GetComponentsInChildren<TextMeshProUGUI>())
            {
                switch (item.name)
                {
                    case "TextID":
                        item.text = f.id.ToString();
                        break;
                    case "TextName":
                        item.text = f.fakename;
                        break;
                    case "TextType":
                        item.text = f.type;
                        break;
                }
            }


            ProfileButton pb = obj.GetComponent<ProfileButton>();

            pb.fk = f;
            pb.GetComponent<Button>().onClick.AddListener(pb.OnClick);
        }
    }
    public void SetPixelArt(string name)
    {
        List<Sprite> result = GameManager.instance.pixelArts.Where(item => item.name.Contains(name)).ToList();

        pixelArtCage.GetComponent<Image>().sprite = result.Count > 0 ? result[0] : GameManager.instance.pixelArts[0];
        pixelArtCage.GetComponentInChildren<TextMeshProUGUI>().text = "";
    }
    private void OnDisable()
    {
        ClearList();
    }
    private void ClearList()
    {
        List<Transform> childs = container.GetComponentsInChildren<Transform>().ToList();
        int qty = childs.Count();

        for (int i = qty - 1; i > 0; i--)
        {
            Destroy(childs[i].gameObject);
        }
    }
}
