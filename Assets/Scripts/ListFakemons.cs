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

        foreach (Fakemon f in gm.fakemons)
        {
            GameObject obj = Instantiate(prefab, container.transform);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = $"{f.id}º - {f.fakename} ({f.type})  / {f.season}";
        }

    }
    private void OnDisable()
    {
        List<Transform> childs = container.GetComponentsInChildren<Transform>().ToList();
        int qty = childs.Count();

        for (int i = qty - 1; i > 0; i--)
        {
            Destroy(childs[i].gameObject);
        }
    }
}
