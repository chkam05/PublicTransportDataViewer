using chkam05.Tools.ControlsEx.Events;
using chkam05.Tools.ControlsEx.InternalMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZtmDataViewer.Data.Config;

namespace ZtmDataViewer.Utilities
{
    public static class InternalMessagesHelper
    {

        //  METHODS

        #region VISUAL STYLES

        //  --------------------------------------------------------------------------------
        /// <summary> Apply visual styles for base files selector InternalMessageEx. </summary>
        /// <param name="internalMessage"> Base files selector InternalMessageEx. </param>
        public static void ApplyVisualStyle(BaseFilesSelectorInternalMessageEx internalMessage)
        {
            var appearanceConfig = ConfigManager.Instance.ConfigData.AppearanceConfig;

            ApplyVisualStyle(internalMessage, appearanceConfig);

            internalMessage.TextBoxMouseOverBackground = appearanceConfig.AccentMouseOverColorBrush;
            internalMessage.TextBoxMouseOverBorderBrush = appearanceConfig.AccentMouseOverColorBrush;
            internalMessage.TextBoxMouseOverForeground = appearanceConfig.AccentForegroundColorBrush;
            internalMessage.TextBoxSelectedBackground = appearanceConfig.AccentSelectedColorBrush;
            internalMessage.TextBoxSelectedBorderBrush = appearanceConfig.AccentSelectedColorBrush;
            internalMessage.TextBoxSelectedForeground = appearanceConfig.AccentForegroundColorBrush;
            internalMessage.TextBoxSelectedTextBackground = appearanceConfig.AccentForegroundColorBrush;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Apply visual styles for base files selector InternalMessageEx. </summary>
        /// <param name="internalMessage"> Base files selector InternalMessageEx. </param>
        public static void ApplyVisualStyle(BaseAwaitInternalMessageEx internalMessage)
        {
            var appearanceConfig = ConfigManager.Instance.ConfigData.AppearanceConfig;

            ApplyVisualStyle(internalMessage, appearanceConfig);

            internalMessage.IndicatorFill = appearanceConfig.AccentColorBrush;
            internalMessage.IndicatorPen = appearanceConfig.AccentColorBrush;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Apply visual styles for InternalMessageEx. </summary>
        /// <param name="internalMessage"> InternalMessageEx. </param>
        public static void ApplyVisualStyle(InternalMessageEx internalMessage)
        {
            var appearanceConfig = ConfigManager.Instance.ConfigData.AppearanceConfig;

            ApplyVisualStyle(internalMessage, appearanceConfig);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Apply visual styles for BaseInternalMessageEx. </summary>
        /// <param name="internalMessage"> BaseInternalMessageEx. </param>
        /// <param name="configManager"> Configuration manager. </param>
        private static void ApplyVisualStyle<T>(BaseInternalMessageEx<T> internalMessage,
            AppearanceConfig appearanceConfig) where T : InternalMessageCloseEventArgs
        {
            internalMessage.Background = appearanceConfig.ThemeBackgroundColorBrush;
            internalMessage.BorderBrush = appearanceConfig.ThemeShadeBackgroundColorBrush;
            internalMessage.BottomBackground = appearanceConfig.ThemeShadeBackgroundColorBrush;
            internalMessage.BottomBorderBrush = appearanceConfig.AccentColorBrush;
            internalMessage.BottomPadding = new Thickness(8);
            internalMessage.ButtonBackground = appearanceConfig.AccentColorBrush;
            internalMessage.ButtonBorderBrush = appearanceConfig.AccentColorBrush;
            internalMessage.ButtonForeground = appearanceConfig.AccentForegroundColorBrush;
            internalMessage.ButtonMouseOverBackground = appearanceConfig.AccentMouseOverColorBrush;
            internalMessage.ButtonMouseOverBorderBrush = appearanceConfig.AccentMouseOverColorBrush;
            internalMessage.ButtonMouseOverForeground = appearanceConfig.AccentForegroundColorBrush;
            internalMessage.ButtonPressedBackground = appearanceConfig.AccentPressedColorBrush;
            internalMessage.ButtonPressedBorderBrush = appearanceConfig.AccentPressedColorBrush;
            internalMessage.ButtonPressedForeground = appearanceConfig.AccentForegroundColorBrush;
            internalMessage.ButtonPressedForeground = appearanceConfig.AccentForegroundColorBrush;
            internalMessage.Foreground = appearanceConfig.ThemeForegroundColorBrush;
            internalMessage.HeaderBackground = appearanceConfig.ThemeShadeBackgroundColorBrush;
            internalMessage.HeaderBorderBrush = appearanceConfig.AccentColorBrush;
            internalMessage.HeaderPadding = new Thickness(8);
            internalMessage.Padding = new Thickness(0);
            internalMessage.UseCustomSectionBreaksBorderBrush = true;
        }

        #endregion VISUAL STYLES

    }
}
