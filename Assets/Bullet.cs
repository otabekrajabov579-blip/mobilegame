using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;

    void Update()
    {
        // O'qni yuqoriga harakatlantirish
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        // Ekrandan chiqib ketsa, xotirani band qilmasligi uchun o'chirish
        if (transform.position.y > 15f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Dushmanga tekkan paytda yo'q qilish uchun
        if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}