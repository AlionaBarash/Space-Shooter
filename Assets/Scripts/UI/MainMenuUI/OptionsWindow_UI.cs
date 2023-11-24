using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsWindow_UI : MonoBehaviour
{
    [SerializeField]
    private GameObject _optionsWindow;

    private GameObject _optionsWindowClone;

    public void ShowOptionsWindow()
    {
        _optionsWindowClone = Instantiate(_optionsWindow);
        _optionsWindowClone.transform.SetParent(transform, false);

        var buttons = _optionsWindowClone.GetComponentsInChildren<Button>();

        buttons[0].onClick.AddListener(HideOptionsWindow);
        
    }

    public void HideOptionsWindow()
    {
        Destroy(_optionsWindowClone);
    }
}
