using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace AshenCore.Core
{
    [CreateAssetMenu(menuName = "AshenCore/UI/UI Theme")]
    public class UITheme : ScriptableObject
    {
        [Header("Fonts")]
        public TMP_FontAsset primaryFont;
        public TMP_FontAsset secondaryFont;
        public int fontTitleSize = 24;
        public int fontSize = 12;
        public int fontSizeSecondary = 10;

        [Header("Colors - Base")]
        public Color backgroundColor = Color.black;
        public Color panelColor = Color.gray;
        public Color iconColor = Color.gray;
        public Color inputFieldColor = Color.gray;

        [Header("Colors - Text")]
        public Color textPrimary = Color.white;
        public Color textSecondary = Color.gray;

        [Header("Colors - Positive Buttons")]
        public Color PositivebuttonNormal = Color.white;
        public Color PositivebuttonHover = Color.gray;
        public Color PositivebuttonPressed = Color.black;
        public Color PositivebuttonDisabled = Color.grey;

        [Header("Colors - Negative Buttons")]
        public Color NegativebuttonNormal = Color.white;
        public Color NegativebuttonHover = Color.gray;
        public Color NegativebuttonPressed = Color.black;
        public Color NegativebuttonDisabled = Color.grey;

        [Header("Colors - Other Buttons")]
        public Color OtherbuttonNormal = Color.white;
        public Color OtherbuttonHover = Color.gray;
        public Color OtherbuttonPressed = Color.black;
        public Color OtherbuttonDisabled = Color.grey;

        [Header("Colors - Input Fields")]
        public Color InputFieldColor = Color.white;
        public int InputFieldFontSize = 12;
        public Color InputFieldTextColor = Color.black;
        public Color InputFieldPlaceholderColor = Color.black;
        
        [Header("Sprites (9-Slice Recommended)")]
        public Sprite panelSprite;
        public Sprite buttonSprite;
        public Sprite inputFieldSprite;

        [Header("UI Spacing")]
        public float defaultPadding = 10f;
        public float defaultSpacing = 8f;

        [Header("Rounding / Visual Feel")]
        [Range(0, 50)]
        public float cornerRadius = 12f;

        [Header("Optional Effects")]
        public bool useDropShadow = false;
        public Color shadowColor = new Color(0, 0, 0, 0.5f);
    }

    public static class UIThemeApplicator
    {

        public static void ApplyThemeOkButton(Button button, UITheme theme)
        {
            if (button == null || theme == null)
                return;

            // Sprite
            button.image.sprite = theme.buttonSprite;

            // Colores del Button
            ColorBlock colors = button.colors;

            colors.normalColor      = theme.PositivebuttonNormal;
            colors.highlightedColor = theme.PositivebuttonHover;
            colors.pressedColor     = theme.PositivebuttonPressed;
            colors.disabledColor    = theme.PositivebuttonDisabled;
            colors.selectedColor = theme.PositivebuttonHover;
            colors.colorMultiplier = 1f;
            colors.fadeDuration = 0.08f;
            button.colors = colors;
        }

        public static void ApplyThemeCancelButton(Button button, UITheme theme)
        {
            if (button == null || theme == null)
                return;

            // Sprite
            button.image.sprite = theme.buttonSprite;

            // Colores del Button
            ColorBlock colors = button.colors;

            colors.normalColor      = theme.NegativebuttonNormal;
            colors.highlightedColor = theme.NegativebuttonHover;
            colors.pressedColor     = theme.NegativebuttonPressed;
            colors.disabledColor = theme.NegativebuttonDisabled;
            colors.selectedColor = theme.NegativebuttonHover;
            colors.colorMultiplier = 1f;
            colors.fadeDuration = 0.08f;

            button.colors = colors;

        }

        public static void ApplyThemeOtherButton(Button button, UITheme theme)
        {
            if (button == null || theme == null)
                return;

            // Sprite
            button.image.sprite = theme.buttonSprite;

            // Colores del Button
            ColorBlock colors = button.colors;

            colors.normalColor      = theme.OtherbuttonNormal;
            colors.highlightedColor = theme.OtherbuttonHover;
            colors.pressedColor     = theme.OtherbuttonPressed;
            colors.disabledColor    = theme.OtherbuttonDisabled;
            colors.selectedColor = theme.OtherbuttonHover;
            colors.colorMultiplier = 1f;
            colors.fadeDuration = 0.08f;

            button.colors = colors;

        }

        public static void ApplyThemeTitleText(TMP_Text text, UITheme theme)
        {
            text.font = theme.primaryFont;
            text.color = theme.textPrimary;
            text.fontSize = theme.fontTitleSize;
        }
        public static void ApplyThemePrimaryText(TMP_Text text, UITheme theme)
        {
            text.font = theme.secondaryFont;
            text.color = theme.textPrimary;
            text.fontSize = theme.fontSize;
        }

        public static void ApplyThemeSecondaryText(TMP_Text text, UITheme theme)
        {
            text.font = theme.secondaryFont;
            text.color = theme.textPrimary;
            text.fontSize = theme.fontSizeSecondary;
        }

        public static void ApplyThemeWindow(Image image, UITheme theme)
        {
            image.sprite = theme.panelSprite;
            image.color = theme.panelColor;
        }

        public static void ApplyThemeInputText(TMP_InputField inputField, UITheme theme)
        {
            if (inputField == null || theme == null)
                return;

            inputField.textComponent.font = theme.primaryFont;
            inputField.textComponent.color = theme.InputFieldTextColor;
            inputField.textComponent.fontSize = theme.InputFieldFontSize;

            TMP_Text placeholder = inputField.placeholder as TMP_Text;

            if(placeholder != null)
            {
                placeholder.color = theme.InputFieldPlaceholderColor;
                placeholder.fontSize = theme.InputFieldFontSize;
                placeholder.font = theme.primaryFont;
            }

            if (inputField.image != null)
            {
                inputField.image.sprite = theme.inputFieldSprite;
                inputField.image.color = theme.InputFieldColor;
            }
        }

    }
        
}