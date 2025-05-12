using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class CombinationLock : MonoBehaviour
{
    // Actions
    public UnityAction UnlockAction;
    public UnityAction ComboButtonPressed;
    void OnComboButtonPress() => ComboButtonPressed?.Invoke();

    void OnUnlocked()
    {
        UnlockAction?.Invoke();

        if (gameOptionsCanvas != null)
        {
            // Position canvas in front of the player's camera
            Transform camera = Camera.main.transform;
            Vector3 offsetPosition = camera.position + camera.forward * 2f;
            gameOptionsCanvas.transform.position = offsetPosition;

            // Make the canvas face the player without flipping
            Vector3 lookDirection = gameOptionsCanvas.transform.position - camera.position;
            lookDirection.y = 0; // Optional: prevent vertical tilt
            gameOptionsCanvas.transform.rotation = Quaternion.LookRotation(lookDirection);

            gameOptionsCanvas.SetActive(true);
        }

        if (lockUI != null)
        {
            lockUI.SetActive(false); // 👈 Hide the lock UI (buttons + display)
        }
    }

    public UnityAction LockAction;
    void OnLocked() => LockAction?.Invoke();

    // Serialized Fields
    [Header("Combo Lock Properties")]
    [SerializeField] string numberCombination = "0412";
    [SerializeField] ButtonInteractable[] comboButtons = new ButtonInteractable[4];
    [SerializeField] TMP_Text textInput;
    [SerializeField] bool isLocked = true;

    [Header("Audio")]
    [SerializeField] AudioClip lockComboClip;
    public AudioClip GetLockClip => lockComboClip;

    [SerializeField] AudioClip unlockComboClip;
    public AudioClip GetUnlockClip => unlockComboClip;

    [SerializeField] AudioClip comboButtonPressedClip;
    public AudioClip GetComboPressedClip => comboButtonPressedClip;

    [Header("Colors")]
    [SerializeField] Color unlockedButtonColor = Color.green;
    [SerializeField] Color incorrectComboButtonColor = Color.red;

    [Header("UI")]
    [SerializeField] GameObject gameOptionsCanvas; // 👈 Game selection canvas
    [SerializeField] GameObject lockUI;            // 👈 Lock input UI (numbers + buttons)

    // Attributes
    const string DEFAULT_INPUT_TEXT = "0000";
    string userInput = "";

    void Start()
    {
        for (int i = 0; i < comboButtons.Length; i++)
        {
            comboButtons[i].selectEntered.AddListener(OnComboButtonPressed);
        }

        textInput.text = DEFAULT_INPUT_TEXT;

        if (gameOptionsCanvas != null)
        {
            gameOptionsCanvas.SetActive(false);
        }
    }

    void OnComboButtonPressed(SelectEnterEventArgs arg0)
    {
        for (int i = 0; i < comboButtons.Length; i++)
        {
            if (arg0.interactableObject.transform.name == comboButtons[i].transform.name)
            {
                userInput += i.ToString();
                textInput.text = userInput;

                if (userInput.Length == numberCombination.Length)
                {
                    CheckCombination();
                }
                else
                {
                    OnComboButtonPress();
                }
            }

            comboButtons[i].SetColorToNormal();
        }
    }

    void CheckCombination()
    {
        if (numberCombination.CompareTo(userInput) == 0)
        {
            isLocked = false;
            OnUnlocked();

            for (int i = 0; i < comboButtons.Length; i++)
            {
                comboButtons[i].GetComponent<ButtonInteractable>().interactionLayers = InteractionLayerMask.GetMask("Nothing");
                Invoke("SetButtonsToUnlockedColor", 0.1f);
            }
        }
        else
        {
            OnLocked();

            for (int i = 0; i < comboButtons.Length; i++)
            {
                comboButtons[i].SetButtonColor(incorrectComboButtonColor);
            }

            Invoke("ResetCombination", 0.5f);
        }
    }

    void ResetCombination()
    {
        for (int i = 0; i < comboButtons.Length; i++)
        {
            comboButtons[i].SetColorToNormal();
        }

        textInput.text = DEFAULT_INPUT_TEXT;
        userInput = string.Empty;
    }

    void SetButtonsToUnlockedColor()
    {
        for (int i = 0; i < comboButtons.Length; i++)
        {
            comboButtons[i].SetButtonColor(unlockedButtonColor);
        }
    }
}
