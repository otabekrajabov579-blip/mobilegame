using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 0.5f); // 0.5 sekunddan keyin portlash obyekti o'chib ketadi
    }
}