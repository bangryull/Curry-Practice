using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // 싱글톤 패턴
    private AudioSource audioSource;

    void Awake()
    {
        // AudioManager 싱글톤 설정
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않음
        }
        else
        {
            Destroy(gameObject);
        }

        // Audio Source 컴포넌트 가져오기
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (AudioManager.instance != null)
        {
            Destroy(AudioManager.instance.gameObject); // 기존 오브젝트 삭제
            AudioManager.instance = null; // 싱글톤 초기화
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}

