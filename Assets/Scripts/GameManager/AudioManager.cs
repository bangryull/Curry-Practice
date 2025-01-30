using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // �̱��� ����
    private AudioSource audioSource;

    void Awake()
    {
        // AudioManager �̱��� ����
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı����� ����
        }
        else
        {
            Destroy(gameObject);
        }

        // Audio Source ������Ʈ ��������
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (AudioManager.instance != null)
        {
            Destroy(AudioManager.instance.gameObject); // ���� ������Ʈ ����
            AudioManager.instance = null; // �̱��� �ʱ�ȭ
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

