using UnityEngine;

public class HardGameModeScript : MonoBehaviour
{
    public HardSnakeMovement hardSnakeMovement;
    public int previousScore = 0; // �nceki puan� tutmak i�in
    private int speedIncreaseCount = 0; // H�z art��� sayac�

    void Start()
    {
        InvokeRepeating("ScoreCheck", 0f, 3f);
    }

    void ScoreCheck()
    {
        if (hardSnakeMovement != null && hardSnakeMovement.score % 3 == 0 && hardSnakeMovement.score != previousScore)
        {
            // H�z art��� sayac�n� art�r
            speedIncreaseCount++;

            // E�er h�z art��� say�s� 6'ya ula�mad�ysa, h�z art���n� ger�ekle�tir
            if (speedIncreaseCount <= 5)
            {
                // Oyun h�z�n� art�ran fonksiyonu �a��r
                IncreaseGameSpeed(0.1f); // %10 art��
            }

            // �nceki skoru g�ncelle
            previousScore = hardSnakeMovement.score;
        }
    }


    private void IncreaseGameSpeed(float speedIncrease)
    {
        // Mevcut oyun h�z�n� al
        float currentSpeed = Time.timeScale;

        // Yeni h�z� hesapla (mevcut h�z�n %10'u kadar art�r)
        float newSpeed = currentSpeed + (currentSpeed * speedIncrease);

        // Yeni h�z� uygula
        Time.timeScale = newSpeed;
    }
}
