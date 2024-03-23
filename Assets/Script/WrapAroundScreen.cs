using UnityEngine;

public class WrapAroundArea : MonoBehaviour
{
    private float minX = -7f; // Minumum X koordinat�
    private float maxX = 7;  // Maksimum X koordinat�
    private float minY = -5.25f; // Minumum Y koordinat�
    private float maxY = 11.25f;  // Maksimum Y koordinat�

    private void Update()
    {
        // Y�lan�n pozisyonunu kontrol et
        Vector3 currentPosition = transform.position;

        // E�er y�lan s�n�rlar�n d���na ��karsa, s�n�rlar i�inde kalmas�n� sa�la
        currentPosition.x = WrapAround(currentPosition.x, minX, maxX);
        currentPosition.y = WrapAround(currentPosition.y, minY, maxY);

        // Y�lan�n pozisyonunu g�ncelle
        transform.position = currentPosition;
    }

    // Belirli bir koordinat�n s�n�rlar i�ine s��d�r�lmas�n� sa�layan fonksiyon
    private float WrapAround(float value, float min, float max)
    {
        // E�er de�er s�n�rlar�n d���na ��karsa, di�er tarafa ta��
        if (value < min)
        {
            return max - (min - value);
        }
        else if (value > max)
        {
            return min + (value - max);
        }
        else
        {
            return value;
        }
    }
}
