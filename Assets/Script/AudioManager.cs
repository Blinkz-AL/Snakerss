using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager'� ekleyin

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Y�neticiyi sahne de�i�ikliklerinden koru
        }
        else
        {
            Destroy(gameObject); // Birden fazla kopyay� engelle
        }
    }

    // T�m sesleri durdurma fonksiyonu
    public void StopAllSounds()
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.Stop();
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Sahne y�klendi�inde devam eden sesleri durdur
        StopAllSounds();
    }
}
