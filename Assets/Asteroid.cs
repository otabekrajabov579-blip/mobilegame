using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public int health = 3;            // Toshning joni (Inspector'da har bir prefab uchun o'zgartirasiz)
    public float speed = 3f;          // Toshning tushish tezligi
    public float rotationSpeed = 50f; // Toshning aylanish tezligi

    void Update()
    {
        // Toshni pastga harakatlantirish va aylantirish
        transform.Translate(Vector2.down * speed * Time.deltaTime, Space.World);
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        // Tosh ekrandan pastga chiqib ketsa, xotiradan o'chirish (o'yin qotishining oldini oladi)
        if (transform.position.y < -7f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Agar o'q (Bullet tagiga ega ob'ekt) tekkan bo'lsa
        if (collision != null && collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject); // O'qni darhol yo'q qilish
            health--;                      // Toshning jonini kamaytirish

            // Joni tugasa, toshni yo'q qilish
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}