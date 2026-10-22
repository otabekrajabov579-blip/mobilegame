using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TouchController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float smoothTime = 0.05f;
    private Vector3 velocity = Vector3.zero;

    [Header("Screen Boundaries")]
    public float minX = -2.2f;
    public float maxX = 2.2f;
    public float minY = -4.5f;
    public float maxY = 4.5f;

    [Header("Blink Effect")]
    public SpriteRenderer shipSprite;
    private bool isBlinking = false;

    [Header("Swipe Settings")]
    public float minSwipeDistance = 50f;
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    public TextMeshProUGUI swipeTextTMP;
    public Text swipeTextLegacy;

    [Header("Camera Reference")]
    public Camera mainCamera;

    void Start()
    {
        if (shipSprite == null) shipSprite = GetComponent<SpriteRenderer>();
        if (mainCamera == null) mainCamera = Camera.main;
    }

    void Update()
    {
        // -------------------------------------------------------------
        // HARAKAT VA SWIPE ANIQLASH (TELEFON)
        // -------------------------------------------------------------
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
                touchEndPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Ended)
            {
                touchEndPos = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                DetectSwipe();
            }

            Vector3 touchWorldPos = mainCamera.ScreenToWorldPoint(touch.position);
            touchWorldPos.z = 0f;

            MoveAndClampShip(touchWorldPos);
        }
        // KOMPYUTER SICHQONCHASI UCHUN
        else if (Input.GetMouseButton(0))
        {
            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;
            MoveAndClampShip(mouseWorldPos);
        }
    }

    void MoveAndClampShip(Vector3 targetWorldPos)
    {
        // Kema belgilangan min/max chegaralar ichida qolishini ta'minlaymiz
        float clampedX = Mathf.Clamp(targetWorldPos.x, minX, maxX);
        float clampedY = Mathf.Clamp(targetWorldPos.y, minY, maxY);
        Vector3 targetPosition = new Vector3(clampedX, clampedY, 0f);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    void DetectSwipe()
    {
        float swipeDistanceX = Mathf.Abs(touchEndPos.x - touchStartPos.x);
        float swipeDistanceY = Mathf.Abs(touchEndPos.y - touchStartPos.y);

        if (swipeDistanceX > minSwipeDistance || swipeDistanceY > minSwipeDistance)
        {
            string swipeDirection = "";

            if (swipeDistanceX > swipeDistanceY)
            {
                swipeDirection = (touchEndPos.x > touchStartPos.x) ? "Swipe Right" : "Swipe Left";
            }
            else
            {
                swipeDirection = (touchEndPos.y > touchStartPos.y) ? "Swipe Up" : "Swipe Down";
            }

            Debug.Log(swipeDirection);
            ShowSwipeOnScreen(swipeDirection);
        }
    }

    void ShowSwipeOnScreen(string textMessage)
    {
        if (swipeTextTMP != null) swipeTextTMP.text = textMessage;
        if (swipeTextLegacy != null) swipeTextLegacy.text = textMessage;
    }
}