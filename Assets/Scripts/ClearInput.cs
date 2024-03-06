using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClearInput : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<TMP_InputField>().text = "";
    }
}
