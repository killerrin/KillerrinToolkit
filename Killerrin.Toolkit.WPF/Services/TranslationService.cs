﻿/// Base code provided by
/// https://codinginfinity.me/post/2015-05-10/localization_of_a_wpf_app_the_simple_approach
/// https://gist.github.com/jakubfijalkowski/0771bfbd26ce68456d3e
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Killerrin.Toolkit.WPF.Services
{
    public class TranslationService : INotifyPropertyChanged
    {
        public static TranslationService Instance { get; protected set; }

        public Dictionary<string, CultureInfo> LoadedLanguages { get; protected set; } = new Dictionary<string, CultureInfo>();
        public ResourceManager TranslationResources { get; protected set; }
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Retrieves the Translation string from the Translation Resources
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string this[string key]
        {
            get { return TranslationResources?.GetString(key, m_currentCulture); }
        }

        private CultureInfo m_currentCulture = CultureInfo.GetCultureInfo("en");
        public CultureInfo CurrentCulture
        {
            get { return m_currentCulture; }
            set
            {
                if (m_currentCulture != value)
                {
                    m_currentCulture = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
                }
            }
        }

        public TranslationService(ResourceManager translationResources) : this(translationResources, new KeyValuePair<string, CultureInfo>()) { }
        public TranslationService(ResourceManager translationResources, params KeyValuePair<string, CultureInfo>[] languages )
        {
            // Set the Instance
            Instance = this;

            // Set the Translation Resources
            TranslationResources = translationResources;

            // Add the Loaded Languages
            LoadedLanguages["en"] = CultureInfo.GetCultureInfo("en");
            foreach (var language in languages)
            {
                LoadedLanguages[language.Key] = language.Value;
            }
        }

        /// <summary>
        /// Whether the CurrentCulture info is set
        /// </summary>
        /// <returns>A boolean containing if the CurrentCulture is set</returns>
        public bool IsLoaded() { return CurrentCulture != null; }

        /// <summary>
        /// Checks if a language code is for the CurrentCulture
        /// </summary>
        /// <param name="languageCode">The Language Code</param>
        /// <returns>Whether the Language Code is for the CurrentCulture</returns>
        public bool IsLanguage(string languageCode) { return CurrentCulture?.Name == languageCode; }

        /// <summary>
        /// Switches the Translations to a given Language
        /// </summary>
        /// <param name="languageCode">The Language Code to switch to</param>
        public void SwitchToLanguage(string languageCode) { CurrentCulture = LoadedLanguages[languageCode]; }
    }

    public class LocExtension : Binding
    {
        public LocExtension(string name)
            : base("[" + name + "]")
        {
            this.Mode = BindingMode.OneWay;
            this.Source = TranslationService.Instance;
        }
    }
}
