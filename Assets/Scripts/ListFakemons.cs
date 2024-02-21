using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;

public class ListFakemons : MonoBehaviour
{
    private GameManager gm;

    [SerializeField] private GameObject prefab;
    private Transform container;
    private void OnEnable()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        container = transform.GetComponentsInChildren<Transform>().Where(item => item.name == "container").ToList()[0];
        CreateList(gm.fakemons);
    }
    private void CreateList(List<Fakemon> fks)
    {
        foreach (Fakemon f in fks)
        {
            GameObject obj = Instantiate(prefab, container.transform);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = $"{f.id}� - {f.fakename} ({f.type})  / {f.season}";
            obj.GetComponent<ProfileButton>().fk = f;
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
