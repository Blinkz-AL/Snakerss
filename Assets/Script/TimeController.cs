using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeScaleController : MonoBehaviour
{
    private float defaultTimeScale = 1f; // Varsay�lan zaman �l�e�i de�eri

    private void Start()
    {
        // Varsay�lan zaman �l�e�i de�erini kaydedin
        defaultTimeScale = Time.timeScale;

        // SceneManager'� sahne y�kleme olaylar�na abone yap�n
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Aboneli�i kald�r�n
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Yeni bir sahne y�klendi�inde zaman �l�e�ini varsay�lan de�ere geri d�nd�r�n
        Time.timeScale = defaultTimeScale;
    }
}
