using System.Collections.Generic;
using UnityEngine;

public class SceneMusicPlayer : MonoBehaviour
{
    [Header("Music Path")]
    [SerializeField] private string musicFolderPath;

    private AudioSource audioSrc;
    private List<AudioClip> musics;

    void Start()
    {
        audioSrc = gameObject.AddComponent<AudioSource>();
        audioSrc.loop = false;

        LoadMusic();
        PlayRandomTrack();
    }

    void Update()
    {
        // verify if music is finished before playing another
        if (!audioSrc.isPlaying && musics.Count > 0)
        {
            PlayRandomTrack();
        }
    }

    private void LoadMusic()
    {
        // load all clips from folder to the list
        musics = new List<AudioClip>(Resources.LoadAll<AudioClip>(musicFolderPath));

        if (musics.Count == 0)
        {
            Debug.LogWarning($"No music found in path: {musicFolderPath}");
        }
    }

    private void PlayRandomTrack()
    {
        if (musics.Count > 0)
        {
            AudioClip randomClip = musics[Random.Range(0, musics.Count)];
            audioSrc.clip = randomClip;
            audioSrc.Play();
        }
    }
}
