using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using DG.Tweening;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private bool isPaused = false;

    [Header("UI Elements")]
    public GameObject instruccionOnscreen;

    [Header("Ease Settings")]
    public float easeDuration = 0.5f;
    public float easeDelay = 0.5f;

    [Header("Input")]
    public InputAction anyButtonAction; // Referencia desde el InputActionAsset

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        
    }

    private void OnEnable()
    {
        anyButtonAction.performed += OnAnyButtonPressed;
        anyButtonAction.Enable();
    }

    private void OnDisable()
    {
        anyButtonAction.performed -= OnAnyButtonPressed;
        anyButtonAction.Disable();
    }

    private void OnAnyButtonPressed(InputAction.CallbackContext context)
    {
        instruccionOnscreen.SetActive(false);
        anyButtonAction.Disable(); // Deja de escuchar después del primer toque
        
    }

    private IEnumerator EaseInOut(Transform target, float duration, float delay)
    {
        yield return new WaitForSeconds(delay);
        Vector3 originalScale = target.localScale;
        target.localScale = Vector3.zero;
        target.gameObject.SetActive(true);

        target.DOScale(originalScale, duration).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(duration);

        yield return new WaitForSeconds(2f);
        target.DOScale(Vector3.zero, duration).SetEase(Ease.InBack);
        yield return new WaitForSeconds(duration);

        target.gameObject.SetActive(false);
    }

    public void ShowInstructions(string message)
    {
        instruccionOnscreen.SetActive(true);
        StartCoroutine(EaseInOut(instruccionOnscreen.transform, easeDuration, easeDelay));

        anyButtonAction.Enable(); // Activa la escucha justo aquí si no está en OnEnable
    }
}
