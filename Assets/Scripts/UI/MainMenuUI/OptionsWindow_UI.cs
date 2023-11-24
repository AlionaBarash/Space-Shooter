using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsWindow_UI : MonoBehaviour
{
    [SerializeField]
    private GameObject _optionsWindow;
    [SerializeField]
    private Sprite[] _musicVolumeIcons;
    [SerializeField]
    private Icons _currentIcon;

    private GameObject _optionsWindowClone;
  
    public void ShowOptionsWindow()
    {
        _optionsWindowClone = Instantiate(_optionsWindow);
        _optionsWindowClone.transform.SetParent(transform, false);

        var buttons = _optionsWindowClone.GetComponentsInChildren<Button>();

        buttons[0].onClick.AddListener(HideOptionsWindow);

        buttons[1].GetComponent<Image>().sprite = _currentIcon.icon;
        buttons[1].onClick.AddListener(AudioManager.instance.ToggleMusic);
        buttons[1].onClick.AddListener(() => buttons[1].GetComponent<Image>().sprite = ToggleMusicIcon(AudioManager.instance.musicAudioSource.mute));
    }

    public void HideOptionsWindow()
    {
        Destroy(_optionsWindowClone);
    }

    public Sprite ToggleMusicIcon(bool isMute)
    {
        if (isMute) 
        {
            _currentIcon.icon = _musicVolumeIcons[0];
            return _musicVolumeIcons[0];
        }
        else
        {
            _currentIcon.icon = _musicVolumeIcons[1];
            return _musicVolumeIcons[1];
        }
    }
}
