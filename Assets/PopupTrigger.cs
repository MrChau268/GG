using UnityEngine;
using DG.Tweening;

public class PopupTrigger : MonoBehaviour
{
    [SerializeField] private GameObject popupUI; 

    private void Start()
    {
        popupUI.SetActive(false); // ensure hidden at start
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowPopup();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited trigger with: " + other.name);
        if (other.CompareTag("Player"))
        {
            HidePopup();
        }
    }

    private void ShowPopup()
    {
        popupUI.SetActive(true);
        popupUI.transform.localScale = Vector3.zero;
        popupUI.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
    }

    private void HidePopup()
    {
        popupUI.transform.DOScale(Vector3.zero, 0.3f)
            .SetEase(Ease.InBack)
            .OnComplete(() => popupUI.SetActive(false));
    }
}
