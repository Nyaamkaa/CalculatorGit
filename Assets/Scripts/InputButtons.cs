using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputButtons : MonoBehaviour
{
    [SerializeField] private Text text1;
    private CalcManager calcManager;

    private void Awake()
    {
        calcManager = GameObject.Find("MainHolder").GetComponent<CalcManager>();
    }

    // Update is called once per frame
    public void InputButton()
    {
        calcManager.ButtonInput(text1.text[0]);
    }
}
