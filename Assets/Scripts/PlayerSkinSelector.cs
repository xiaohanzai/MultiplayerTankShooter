using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerSkinSelector : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown colorDropdown;

    [SerializeField] private Color[] availableColors = { Color.white, Color.red, Color.green, Color.blue, Color.yellow };
    [SerializeField] private string[] colorNames = { "Original", "Red", "Green", "Blue", "Yellow" };

    public UnityAction<int> OnColorChanged;

    private void OnEnable()
    {
        PopulateDropdownOptions();
        colorDropdown.onValueChanged.AddListener(OnColorSelected);
    }

    private void OnDisable()
    {
        colorDropdown.onValueChanged.RemoveListener(OnColorSelected);
    }

    private void PopulateDropdownOptions()
    {
        // Clear any existing options
        colorDropdown.ClearOptions();

        // Create a list to hold the dropdown options (names of colors)
        List<TMP_Dropdown.OptionData> dropdownOptions = new List<TMP_Dropdown.OptionData>();

        // Loop through available colors and add them to the dropdown options
        for (int i = 0; i < availableColors.Length; i++)
        {
            // Create a new TMP option with the color name
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData(colorNames[i]);
            dropdownOptions.Add(option);
        }

        // Add the options to the TMP Dropdown
        colorDropdown.AddOptions(dropdownOptions);
    }

    public void OnColorSelected(int index)
    {
        OnColorChanged(index);
    }

    public Color GetColorByIndex(int index)
    {
        if (index >= 0 && index < availableColors.Length)
        {
            return availableColors[index];
        }
        return Color.white; // Default in case of an invalid index
    }
}
