using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using TMPro;

public class ButtonInteractable : XRBaseInteractable
{
    [Header("Visuals")]
    [SerializeField] private Image buttonImage;
    [SerializeField] private Color[] buttonColors;

    [Header("UI Prompt")]
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private string newPromptMessage;

    // Cached Colors
    private Color buttonNormalColor;
    private Color buttonHighlightedColor;
    private Color buttonPressedColor;
    private Color buttonSelectedColor;

    // State
    private bool isPressed;

    protected override void Awake()
    {
        base.Awake();

        if (buttonColors.Length >= 4)
        {
            buttonNormalColor = buttonColors[0];
            buttonHighlightedColor = buttonColors[1];
            buttonPressedColor = buttonColors[2];
            buttonSelectedColor = buttonColors[3];
        }

        if (buttonImage != null)
            buttonImage.color = buttonNormalColor;
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
        isPressed = false;

        if (buttonImage != null)
            buttonImage.color = buttonHighlightedColor;
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);

        if (isPressed) return;

        if (buttonImage != null)
            buttonImage.color = buttonNormalColor;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (isPressed) return;
        isPressed = true;

        if (buttonImage != null)
            buttonImage.color = buttonPressedColor;

        if (promptText != null && !string.IsNullOrEmpty(newPromptMessage))
        {
            promptText.text = newPromptMessage;
        }

        // 🎯 Call StartGame on the GameManager when button is selected
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartGame();
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        if (buttonImage != null)
            buttonImage.color = buttonSelectedColor;
    }

    public void SetColorToNormal()
    {
        SetButtonColor(buttonNormalColor);
    }

    public void SetButtonColor(Color newColor)
    {
        if (buttonImage != null)
            buttonImage.color = newColor;
    }
}
