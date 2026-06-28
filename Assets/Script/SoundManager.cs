using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager Instance { get => _instance; set => _instance = value; }


    [SerializeField] private float _musicTimer = 0;
    private float _musicTime = 0;

    [SerializeField] private AudioSource _music;
    [SerializeField] private AudioSource _buy;
    [SerializeField] private AudioSource _angry;
    [SerializeField] private AudioSource _meeting;
    [SerializeField] private AudioSource _carrot;
    [SerializeField] private AudioSource _stick;

    private void Awake()
    {
        if (SoundManager.Instance == null)
        {
            SoundManager.Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        MusicLogic();
    }

    private void MusicLogic()
    {
        if (_musicTime > 0)
        {
            _musicTime -= Time.deltaTime;
        }
        else
        {
            PlayMusic();
            _musicTime = _musicTimer;
        }
    }

    private void PlayMusic()
    {
        _music.Play();
    }

    public void PlayCarrot()
    {
        _carrot.Play();
    }
    public void PlayStick()
    {
        _stick.Play();
    }
    public void PlayBuy()
    {
        _buy.Play();
    }
    public void PlayMeeting()
    {
        _meeting.Play();
    }
    public void PlayAngry()
    {
        _angry.Play();
    }
}
