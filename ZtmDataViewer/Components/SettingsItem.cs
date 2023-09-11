﻿using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ZtmDataViewer.Components
{
    public class SettingsItem : Control, INotifyPropertyChanged
    {

        //  DEPENDENCY PROPERTIES

        #region Font Dependency Properties

        //  Title

        public static readonly DependencyProperty TitleFontFamilyProperty = DependencyProperty.Register(
            nameof(TitleFontFamily),
            typeof(FontFamily),
            typeof(SettingsItem),
            new PropertyMetadata(new FontFamily("Seoge UI")));

        public static readonly DependencyProperty TitleFontSizeProperty = DependencyProperty.Register(
            nameof(TitleFontSize),
            typeof(double),
            typeof(SettingsItem),
            new PropertyMetadata(14d));

        public static readonly DependencyProperty TitleFontStretchProperty = DependencyProperty.Register(
            nameof(TitleFontStretch),
            typeof(FontStretch),
            typeof(SettingsItem),
            new PropertyMetadata(FontStretches.Normal));

        public static readonly DependencyProperty TitleFontStyleProperty = DependencyProperty.Register(
            nameof(TitleFontStyle),
            typeof(FontStyle),
            typeof(SettingsItem),
            new PropertyMetadata(FontStyles.Normal));

        public static readonly DependencyProperty TitleFontWeightProperty = DependencyProperty.Register(
            nameof(TitleFontWeight),
            typeof(FontWeight),
            typeof(SettingsItem),
            new PropertyMetadata(FontWeights.SemiBold));

        //  Description

        public static readonly DependencyProperty DescriptionFontFamilyProperty = DependencyProperty.Register(
            nameof(DescriptionFontFamily),
            typeof(FontFamily),
            typeof(SettingsItem),
            new PropertyMetadata(new FontFamily("Seoge UI")));

        public static readonly DependencyProperty DescriptionFontSizeProperty = DependencyProperty.Register(
            nameof(DescriptionFontSize),
            typeof(double),
            typeof(SettingsItem),
            new PropertyMetadata(12d));

        public static readonly DependencyProperty DescriptionFontStretchProperty = DependencyProperty.Register(
            nameof(DescriptionFontStretch),
            typeof(FontStretch),
            typeof(SettingsItem),
            new PropertyMetadata(FontStretches.Normal));

        public static readonly DependencyProperty DescriptionFontStyleProperty = DependencyProperty.Register(
            nameof(DescriptionFontStyle),
            typeof(FontStyle),
            typeof(SettingsItem),
            new PropertyMetadata(FontStyles.Normal));

        public static readonly DependencyProperty DescriptionFontWeightProperty = DependencyProperty.Register(
            nameof(DescriptionFontWeight),
            typeof(FontWeight),
            typeof(SettingsItem),
            new PropertyMetadata(FontWeights.Normal));

        #endregion Font Dependency Properties

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            nameof(CornerRadius),
            typeof(CornerRadius),
            typeof(SettingsItem),
            new PropertyMetadata(new CornerRadius(4d)));

        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(
            nameof(Content),
            typeof(object),
            typeof(SettingsItem),
            new PropertyMetadata(null));

        public static readonly DependencyProperty IconKindProperty = DependencyProperty.Register(
            nameof(IconKind),
            typeof(PackIconKind),
            typeof(SettingsItem),
            new PropertyMetadata(PackIconKind.None));

        public static readonly DependencyProperty IconMarginProperty = DependencyProperty.Register(
            nameof(IconMargin),
            typeof(Thickness),
            typeof(SettingsItem),
            new PropertyMetadata(new Thickness(0, 0, 4, 0)));

        public static readonly DependencyProperty IconSizeProperty = DependencyProperty.Register(
            nameof(IconSize),
            typeof(double),
            typeof(SettingsItem),
            new PropertyMetadata(28d));

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            nameof(Title),
            typeof(string),
            typeof(SettingsItem),
            new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
            nameof(Description),
            typeof(string),
            typeof(SettingsItem),
            new PropertyMetadata(string.Empty));


        //  EVENTS

        public event PropertyChangedEventHandler PropertyChanged;


        //  GETTERS & SETTERS

        #region Font

        //  Title

        public FontFamily TitleFontFamily
        {
            get => (FontFamily)GetValue(TitleFontFamilyProperty);
            set
            {
                SetValue(TitleFontFamilyProperty, value);
                OnPropertyChanged(nameof(TitleFontFamily));
            }
        }

        public double TitleFontSize
        {
            get => (double)GetValue(TitleFontSizeProperty);
            set
            {
                SetValue(TitleFontSizeProperty, value);
                OnPropertyChanged(nameof(TitleFontSize));
            }
        }

        public FontStretch TitleFontStretch
        {
            get => (FontStretch)GetValue(TitleFontStretchProperty);
            set
            {
                SetValue(TitleFontStretchProperty, value);
                OnPropertyChanged(nameof(TitleFontStretch));
            }
        }

        public FontStyle TitleFontStyle
        {
            get => (FontStyle)GetValue(TitleFontStyleProperty);
            set
            {
                SetValue(TitleFontStyleProperty, value);
                OnPropertyChanged(nameof(TitleFontStyle));
            }
        }

        public FontWeight TitleFontWeight
        {
            get => (FontWeight)GetValue(TitleFontWeightProperty);
            set
            {
                SetValue(TitleFontWeightProperty, value);
                OnPropertyChanged(nameof(TitleFontWeight));
            }
        }

        //  Description

        public FontFamily DescriptionFontFamily
        {
            get => (FontFamily)GetValue(DescriptionFontFamilyProperty);
            set
            {
                SetValue(DescriptionFontFamilyProperty, value);
                OnPropertyChanged(nameof(DescriptionFontFamily));
            }
        }

        public double DescriptionFontSize
        {
            get => (double)GetValue(DescriptionFontSizeProperty);
            set
            {
                SetValue(DescriptionFontSizeProperty, value);
                OnPropertyChanged(nameof(DescriptionFontSize));
            }
        }

        public FontStretch DescriptionFontStretch
        {
            get => (FontStretch)GetValue(DescriptionFontStretchProperty);
            set
            {
                SetValue(DescriptionFontStretchProperty, value);
                OnPropertyChanged(nameof(DescriptionFontStretch));
            }
        }

        public FontStyle DescriptionFontStyle
        {
            get => (FontStyle)GetValue(DescriptionFontStyleProperty);
            set
            {
                SetValue(DescriptionFontStyleProperty, value);
                OnPropertyChanged(nameof(DescriptionFontStyle));
            }
        }

        public FontWeight DescriptionFontWeight
        {
            get => (FontWeight)GetValue(DescriptionFontWeightProperty);
            set
            {
                SetValue(DescriptionFontWeightProperty, value);
                OnPropertyChanged(nameof(DescriptionFontWeight));
            }
        }

        #endregion Font

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set
            {
                SetValue(CornerRadiusProperty, value);
                OnPropertyChanged(nameof(CornerRadius));
            }
        }

        public object Content
        {
            get => GetValue(ContentProperty);
            set
            {
                SetValue(ContentProperty, value);
                OnPropertyChanged(nameof(Content));
            }
        }

        public PackIconKind IconKind
        {
            get => (PackIconKind)GetValue(IconKindProperty);
            set
            {
                SetValue(IconKindProperty, value);
                OnPropertyChanged(nameof(CornerRadius));
            }
        }

        public Thickness IconMargin
        {
            get => (Thickness)GetValue(IconMarginProperty);
            set
            {
                SetValue(IconMarginProperty, value);
                OnPropertyChanged(nameof(IconMargin));
            }
        }

        public double IconSize
        {
            get => (double)GetValue(IconSizeProperty);
            set
            {
                SetValue(IconSizeProperty, value);
                OnPropertyChanged(nameof(IconSize));
            }
        }

        public string Title
        {
            get => (string)GetValue(DescriptionProperty);
            set
            {
                SetValue(DescriptionProperty, value);
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Description
        {
            get => (string)GetValue(TitleProperty);
            set
            {
                SetValue(TitleProperty, value);
                OnPropertyChanged(nameof(Description));
            }
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Static SettingsItem class constructor. </summary>
        static SettingsItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SettingsItem),
                new FrameworkPropertyMetadata(typeof(SettingsItem)));
        }

        #endregion CLASS METHODS

        #region NOTIFY PROPERTIES CHANGED INTERFACE METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method for invoking PropertyChangedEventHandler event. </summary>
        /// <param name="propertyName"> Changed property name. </param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion NOTIFY PROPERTIES CHANGED INTERFACE METHODS

    }
}
