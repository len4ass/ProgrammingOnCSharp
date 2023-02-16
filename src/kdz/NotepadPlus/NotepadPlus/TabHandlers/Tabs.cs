using System;
using System.Windows.Forms;

namespace NotepadPlus.TabHandlers
{
    /// <summary>
    /// Класс для расширения функциональности вкладок.
    /// </summary>
    internal sealed class Tabs : TabPage
    { 
        /// <summary>
        /// Textbox, который привязывается к вкладке.
        /// </summary>
        public RichTextBox TextBox { get; set; }
        
        /// <summary>
        /// Путь к файлу, который привязывается к вкладке.
        /// </summary>
        public string FilePath { get; set; }
        
        /// <summary>
        /// Позиция курсора.
        /// </summary>
        public int CursorPosition { get; set; }
        
        /// <summary>
        /// Указывает, менялось ли содержимое вкладки.
        /// </summary>
        public bool IsChanged { get; set; }
        
        // Гетто-фикс для покраски
        private bool _backChanged;
        
        /// <summary>
        /// Стандартный конструктор: создает пустую вкладку.
        /// </summary>
        /// <param name="name">Имя вкладки.</param>
        /// <param name="contextMenu">Контекстное меню.</param>
        /// <param name="file">Путь к файлу.</param>
        public Tabs(string name, ContextMenuStrip contextMenu, string file = "")
        {
            Text = name;
            FilePath = file;
            CursorPosition = 0;
            TextBox = new RichTextBox();
            TextBox.ContextMenuStrip = contextMenu;
            TextBox.Dock = DockStyle.Fill;
            TextBox.Focus();
            TextBox.BackColorChanged += OnBackChanged;
            TextBox.ForeColorChanged += OnForeChanged;
            TextBox.TextChanged += OnTextChanged;
            Controls.Add(TextBox);
        }

        /// <summary>
        /// Модифицированный конструктор: создает вкладку с текстом.
        /// </summary>
        /// <param name="name">Имя вкладки.</param>
        /// <param name="contextMenu">Контекстное меню.</param>
        /// <param name="text">Содержимое вкладки.</param>
        /// <param name="file">Путь к файлу.</param>
        public Tabs(string name, ContextMenuStrip contextMenu, string text = "", string file = "")
        {
            Text = name;
            FilePath = file;
            CursorPosition = 0;
            TextBox = new RichTextBox();
            TextBox.Text = text;
            TextBox.ContextMenuStrip = contextMenu;
            TextBox.Dock = DockStyle.Fill;
            TextBox.Focus();
            TextBox.BackColorChanged += OnBackChanged;
            TextBox.ForeColorChanged += OnForeChanged;
            TextBox.TextChanged += OnTextChanged;
            Controls.Add(TextBox);
        }

        /// <summary>
        /// Модифицированный конструктор: создает вкладку с текстом (дифференцирует non-rtf и rtf файлы)
        /// </summary>
        /// <param name="name">Имя вкладки.</param>
        /// <param name="contextMenu">Контекстное меню.</param>
        /// <param name="text">Содержимое вкладки.</param>
        /// <param name="pos">Позиция курсора в тексте.</param>
        /// <param name="file">Путь к файлу.</param>
        /// <param name="rtf">Указывает, является ли передаваемый файл rtf или нет.</param>
        public Tabs(string name, ContextMenuStrip contextMenu, string text = "", int pos = 0, string file = "", bool rtf = false)
        {
            Text = name;
            FilePath = file;
            TextBox = new RichTextBox();

            if (rtf)
            {
                TextBox.Rtf = text;
            }
            else
            {
                TextBox.Text = text;
            }
            
            TextBox.ContextMenuStrip = contextMenu;
            TextBox.Dock = DockStyle.Fill;
            TextBox.Focus();
            CursorPosition = pos > TextBox.Text.Length ? TextBox.Text.Length : pos;
            TextBox.Select(CursorPosition, 0);
            TextBox.BackColorChanged += OnBackChanged;
            TextBox.ForeColorChanged += OnForeChanged;
            TextBox.TextChanged += OnTextChanged;
            Controls.Add(TextBox);
        }

        /// <summary>
        /// Обрабатывает BackColorChanged эвент.
        /// </summary>
        /// <param name="sender">Источник эвента.</param>
        /// <param name="e">Параметры эвента.</param>
        private void OnBackChanged(object? sender, EventArgs e)
        {
            _backChanged = true;
            CursorPosition = TextBox.SelectionStart;
        }
        
        /// <summary>
        /// Обрабатывает ForeColorChanged эвент.
        /// </summary>
        /// <param name="sender">Источник эвента.</param>
        /// <param name="e">Параметры эвента.</param>
        private void OnForeChanged(object? sender, EventArgs e)
        {
            _backChanged = true;
            CursorPosition = TextBox.SelectionStart;
        }
        
        /// <summary>
        /// Изменяет состояние файла с сохраненного на несохраненное.
        /// </summary>
        /// <param name="sender">Источник эвента.</param>
        /// <param name="e">Параметры эвента.</param>
        private void OnTextChanged(object? sender, EventArgs e)
        {
            CursorPosition = TextBox.SelectionStart;

            if (!Text.Contains("*") && !_backChanged)
            {
                Text += @"*";
                IsChanged = true;
            }

            if (_backChanged)
            {
                _backChanged = false;
            }
        }
    }
}

