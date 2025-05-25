using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("SoundManager is NULL");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    [Header("Audio")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip menuMusicClip;
    [SerializeField] private AudioClip gameMusicClip;

    [Header("UI - Slider Main Menu")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [Header("UI - Slider Pause Screen")]
    [SerializeField] private Slider musicSliderPause;
    [SerializeField] private Slider sfxSliderPause;

    [Header("UI - Toggle Buttons Main Menu")]
    [SerializeField] private Button musicToggleButton;
    [SerializeField] private Button sfxToggleButton;

    [Header("UI - Toggle Buttons Pause Screen")]
    [SerializeField] private Button musicToggleButtonPause;
    [SerializeField] private Button sfxToggleButtonPause;

    [Header("UI - Images Main Menu")]
    [SerializeField] private Image musicButtonImage;
    [SerializeField] private Image sfxButtonImage;

    [Header("UI - Images Pause Screen")]
    [SerializeField] private Image musicButtonImagePause;
    [SerializeField] private Image sfxButtonImagePause;

    [Header("UI - Sprites")]
    [SerializeField] private Sprite musicOnSprite;
    [SerializeField] private Sprite musicOffSprite;
    [SerializeField] private Sprite sfxOnSprite;
    [SerializeField] private Sprite sfxOffSprite;

    [Header("Default Volume")]
    [Range(0.0001f, 1f)][SerializeField] private float defaultMusicVolume = 0.5f;
    [Range(0.0001f, 1f)][SerializeField] private float defaultSFXVolume = 0.5f;

    private bool isMusicOn = true;
    private bool isSFXOn = true;

    void Start()
    {
        SetMusicVolume(defaultMusicVolume);
        SetSFXVolume(defaultSFXVolume);

        musicSlider.value = defaultMusicVolume;
        sfxSlider.value = defaultSFXVolume;
        musicSliderPause.value = defaultMusicVolume;
        sfxSliderPause.value = defaultSFXVolume;

        musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
        musicSliderPause.onValueChanged.AddListener(OnMusicSliderChanged);

        sfxSlider.onValueChanged.AddListener(OnSFXSliderChanged);
        sfxSliderPause.onValueChanged.AddListener(OnSFXSliderChanged);

        musicToggleButton.onClick.AddListener(ToggleMusic);
        sfxToggleButton.onClick.AddListener(ToggleSFX);

        musicToggleButtonPause.onClick.AddListener(ToggleMusic);
        sfxToggleButtonPause.onClick.AddListener(ToggleSFX);

        PlayMenuMusic();

        UpdateIcons();
    }

    private void OnMusicSliderChanged(float value)
    {
        SetMusicVolume(value);
        if (musicSlider.value != value) musicSlider.value = value;
        if (musicSliderPause.value != value) musicSliderPause.value = value;
    }

    private void OnSFXSliderChanged(float value)
    {
        SetSFXVolume(value);
        if (sfxSlider.value != value) sfxSlider.value = value;
        if (sfxSliderPause.value != value) sfxSliderPause.value = value;
    }

    public void SetMusicVolume(float value)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20);
        isMusicOn = value > 0.0001f;
        UpdateIcons();
    }

    public void SetSFXVolume(float value)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20);
        isSFXOn = value > 0.0001f;
        UpdateIcons();
    }

    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        float volume = isMusicOn ? defaultMusicVolume : 0.0001f;
        musicSlider.value = volume;
        musicSliderPause.value = volume;
        SetMusicVolume(volume);
    }

    public void ToggleSFX()
    {
        isSFXOn = !isSFXOn;
        float volume = isSFXOn ? defaultSFXVolume : 0.0001f;
        sfxSlider.value = volume;
        sfxSliderPause.value = volume;
        SetSFXVolume(volume);
    }

    public void PlayMenuMusic()
    {
        PlayMusic(menuMusicClip);
    }

    public void PlayGameMusic()
    {
        PlayMusic(gameMusicClip);
    }

    private void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip == clip) return;
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    private void UpdateIcons()
    {
        musicButtonImage.sprite = isMusicOn ? musicOnSprite : musicOffSprite;
        sfxButtonImage.sprite = isSFXOn ? sfxOnSprite : sfxOffSprite;

        if (musicButtonImagePause != null)
            musicButtonImagePause.sprite = isMusicOn ? musicOnSprite : musicOffSprite;

        if (sfxButtonImagePause != null)
            sfxButtonImagePause.sprite = isSFXOn ? sfxOnSprite : sfxOffSprite;
    }
}
