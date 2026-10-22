using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool isInvulnerable = false; // Miltillash vaqtida qayta urilib ketmaslik uchun

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Tosh (Enemy) tekkanida va miltillash jarayoni tugagan bo'lsa
        if (collision.CompareTag("Enemy") && !isInvulnerable)
        {
            Destroy(collision.gameObject); // Toshni yo'q qilish

            // Har doim markazga qaytarish
            transform.position = new Vector3(0, -3.5f, 0);

            // Miltillash effektini ishga tushirish (jonlar kamaymaydi va kema o'chmaydi)
            StartCoroutine(DamageEffect());
        }
    }

    IEnumerator DamageEffect()
    {
        isInvulnerable = true;

        for (int i = 0; i < 3; i++)
        {
            if (spriteRenderer != null) spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.15f);
            if (spriteRenderer != null) spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.15f);
        }

        isInvulnerable = false;
    }
}