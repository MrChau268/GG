using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class PopupTrigger : MonoBehaviour
{
    [Header("Popup Settings")]
    [SerializeField] private GameObject popupUI;
    [SerializeField] private Transform player;
    [SerializeField] private float showDistance = 4f;
    [SerializeField] private float hideDistance = 5f;

    [Header("Text Settings")]
    [SerializeField] private Text messageText;       // assign your UI Text here
    [SerializeField] [TextArea] private string message = "Welcome, traveler."; // customizable message
    [SerializeField] private float typeSpeed = 0.05f; // speed of typing

    private bool isShown = false;
    private Coroutine typingCoroutine;

    void Start()
    {
        popupUI.SetActive(false);
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (!isShown && distance <= showDistance)
        {
            ShowPopup();
        }
        else if (isShown && distance >= hideDistance)
        {
            HidePopup();
        }
    }

    private void ShowPopup()
    {
        isShown = true;
        popupUI.SetActive(true);
        popupUI.transform.localScale = Vector3.zero;
        popupUI.transform
            .DOScale(Vector3.one, 1f)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                // Start the typing animation after popup fully shown
                if (typingCoroutine != null)
                    StopCoroutine(typingCoroutine);
                typingCoroutine = StartCoroutine(TypeMessage());
            });
    }

    private void HidePopup()
    {
        isShown = false;

        // Stop text animation if still typing
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        popupUI.transform
            .DOScale(Vector3.zero, 0.5f)
            .SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                popupUI.SetActive(false);
                messageText.text = ""; // clear text when hidden
            });
    }

    private IEnumerator TypeMessage()
    {
        messageText.text = "";
        foreach (char c in message)
        {
            messageText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }
    }
}
