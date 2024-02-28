using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dex : MonoBehaviour
{
    private GameManager gm;

    [SerializeField] private GameObject prefab;
    private Transform container;

    private void OnEnable()
    {
        gm = GameManager.instance.GetComponent<GameManager>();
        container = transform.GetComponentsInChildren<Transform>().Where(item => item.name == "container").ToList()[0];
        CreateList(gm.fakemons);
    }
    private void CreateList(List<Fakemon> fks)
    {
        foreach (Fakemon f in fks)
        {
            GameObject obj = Instantiate(prefab, container.transform);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = $"{f.id}º - {f.fakename} ({f.type})  / {f.season}";

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
