using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsWindow_UI : MonoBehaviour
{
    [SerializeField]
    private GameObject _optionsWindow;
    [SerializeField]
    private Sprite[] _musicMuteIcons, _sfxMuteIcons;

    public static OptionsWindow_UI instance;
    public static Action onResetScore;
    public static Action onOptionsWindowClose;

    private GameObject _optionsWindowClone;

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void ShowOptionsWindow()
    {
        _optionsWindowClone = Instantiate(_optionsWindow);
        _optionsWindowClone.transform.SetParent(transform, false);

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
        {
            var backgroundImage = _optionsWindowClone.transform.GetChild(0);
            backgroundImage.gameObject.SetActive(true);
        }

        var buttons = _optionsWindowClone.GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++)
        {
            switch (i)
            {
                case 0: ExitButton(buttons, i); break;
                case 1: MusicMuteButton(buttons, i); break;
                case 2: SfxMuteButton(buttons, i); break;
                case 3: ResetScoreButton(buttons, i); break;
            }
        }

        var sliders = _optionsWindowClone.GetComponentsInChildren<Slider>();
        for (int i = 0; i < sliders.Length; i++)
        {
            switch (i)
            {
                case 0: MusicVolumeSlider(sliders, i); break;
                case 1: SfxVolumeSlider(sliders, i); break;
            }
        }
    }

    public void HideOptionsWindow()
    {
        Destroy(_optionsWindowClone);
    }

    public Sprite ToggleMusicMuteIcon()
    {
        return _musicMuteIcons[PlayerPrefs.GetInt("isMusicMuted")];
    }

    public Sprite ToggleSfxMuteIcon()
    {
        return _sfxMuteIcons[PlayerPrefs.GetInt("isSfxMuted")];
    }

    private void ExitButton(Button[] buttons, int index)
    {
        buttons[index].onClick.AddListener(HideOptionsWindow);
        
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
        {
            buttons[index].onClick.AddListener(() => GameManager.instance.SetPause(false));
        }
        else
        {
            buttons[index].onClick.AddListener(() => onOptionsWindowClose());
        }
    }

    private void MusicMuteButton(Button[] buttons, int index)
    {
        buttons[index].GetComponent<Image>().sprite = _musicMuteIcons[PlayerPrefs.GetInt("isMusicMuted")];
        buttons[index].onClick.AddListener(AudioManager.instance.ToggleMusic);
        buttons[index].onClick.AddListener(() => buttons[index].GetComponent<Image>().sprite = ToggleMusicMuteIcon());
    }

    private void SfxMuteButton(Button[] buttons, int index)
    {
        buttons[index].GetComponent<Image>().sprite = _sfxMuteIcons[PlayerPrefs.GetInt("isSfxMuted")];
        buttons[index].onClick.AddListener(AudioManager.instance.ToggleSfx);
        buttons[index].onClick.AddListener(() => buttons[index].GetComponent<Image>().sprite = ToggleSfxMuteIcon());
    }

    private void ResetScoreButton(Button[] buttons, int index)
    {
        if (onResetScore != null && SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            buttons[index].onClick.AddListener(() => onResetScore());
        }
        else
        {
            buttons[index].gameObject.SetActive(false);
        }
    }

    private void MusicVolumeSlider(Slider[] sliders, int index)
    {
        sliders[index].value = PlayerPrefs.GetFloat("MusicVolume");
        sliders[index].onValueChanged.AddListener(delegate { AudioManager.instance.MusicVolume(sliders[index].value); });
    }

    private void SfxVolumeSlider(Slider[] sliders, int index)
    {
        sliders[index].value = PlayerPrefs.GetFloat("SfxVolume");
        sliders[index].onValueChanged.AddListener(delegate { AudioManager.instance.SfxVolume(sliders[index].value); });
    }
}
