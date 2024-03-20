using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI elementlerine eri�mek i�in gerekli k�t�phane

public class HardSnakeMovement : MonoBehaviour
{
    private float initialTimeScale; // Ba�lang��ta kaydedilecek zaman �l�e�i de�eri
    private Vector2 _direction = Vector2.zero;
    public List<Transform> _segments;
    public Button RestartGame; // StartGame butonuna referans
    public Text scoreText, highScoreText; // Text elementine referans
    public GameObject BigFood;
    private int highScore = 0; // Ba�lang��ta en y�ksek skor s�f�r olacak
    private FoodRandomizer foodRandomizer;
    public SoundEffect soundEffect;
    public HardGameModeScript hardGameModeScript;
    public void Start()
    {
        _segments = new List<Transform>();
        _segments.Add(this.transform);
        RestartGame.gameObject.SetActive(false);
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateHighScoreText();
        BigFood.SetActive(false);
        // foodRandomizer de�i�kenini ba�lat
        foodRandomizer = FindObjectOfType<FoodRandomizer>(); // FoodRandomizer scriptini sahnedeki nesneler aras�nda bul
        initialTimeScale = Time.timeScale; // Ba�lang�� zaman �l�e�i de�erini kaydet
    }

    public void GameStarter()
    {
        _segments = new List<Transform>();
        _segments.Add(this.transform);
    }

    public void GameRestarter()
    {
        RestartGame.gameObject.SetActive(false);
        Time.timeScale = initialTimeScale; // Ba�lang�� h�z�na geri d�n
        hardGameModeScript.previousScore = 0;
    }

    public Transform segmentPrefab;

    private void Update()
    {
    }
    // Android i�in kontrolleri buttonlara atamak i�in olu�turuldu
    public void SnakeGoRight()
    {
        // E�er mevcut y�n sol de�ilse, sa�a hareket etmeye izin ver
        if (_direction != Vector2.left)
        {
            _direction = Vector2.right;
        }
    }

    public void SnakeGoLeft()
    {
        // E�er mevcut y�n sol de�ilse, sa�a hareket etmeye izin ver
        if (_direction != Vector2.right)
        {
            _direction = Vector2.left;
        }
    }

    public void SnakeGoUp()
    {
        // E�er mevcut y�n sol de�ilse, sa�a hareket etmeye izin ver
        if (_direction != Vector2.down)
        {
            _direction = Vector2.up;
        }
    }

    public void SnakeGoDown()
    {
        // E�er mevcut y�n sol de�ilse, sa�a hareket etmeye izin ver
        if (_direction != Vector2.up)
        {
            _direction = Vector2.down;
        }
    }

    private void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }
        Vector3 newPosition = transform.position + new Vector3(_direction.x * 0.5f, _direction.y * 0.5f, 0f);

        // Her bile�eni 0.5 birimden tam say�ya yuvarlayarak y�lan�n her ad�mda 0.5 birim hareket etmesini sa�lar
        float newX = Mathf.Round(newPosition.x * 2) / 2;
        float newY = Mathf.Round(newPosition.y * 2) / 2;

        transform.position = new Vector3(newX, newY, 0f);
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);
    }

    public void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);
        this.transform.position = Vector3.zero;
        RestartGame.gameObject.SetActive(true);
        BigFood.SetActive(false);
        foodRandomizer.SpawnCount = 0;
        foodRandomizer.FoodSpawner();
        _direction = Vector2.zero; //yand�ktan sonra head objesinin sabit kalmas� i�in

    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString(); // Text elementini g�ncelle
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            UpdateHighScoreText();
        }
    }

    public int score = 0; // Ba�lang��ta score de�eri

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            soundEffect.PlayFoodEatSound();
            Grow();
            score++; // Score de�erini artt�r
            UpdateScoreText(); // Score de�erini g�ncelleyerek ekrana yazd�
        }
        else if (other.tag == "Obstacle")
        {
            soundEffect.PlayWallCrashSound();
            ResetState();
            score = 0; // Score de�erini s�f�rla
            UpdateScoreText(); // Score de�erini g�ncelleyerek ekrana yazd�r
            Time.timeScale = 0;

        }
        else if (other.tag == "BigFood")
        {
            // BigFood tetiklendi�inde 4 kez Grow fonksiyonunu �a��r ve skora +4 ekle
            for (int i = 0; i < 4; i++)
            {
                Grow();
                score++;
                UpdateScoreText();
            }
            soundEffect.PlayBigFoodEatSound();
            BigFood.SetActive(false);//yem yendikten sonra sabit kalmamas� i�in
        }
    }

    private void UpdateHighScoreText()
    {
        highScoreText.text = "High Score: " + highScore.ToString();
    }
}
