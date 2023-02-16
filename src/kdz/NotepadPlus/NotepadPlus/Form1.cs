using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using NotepadPlus.Overrides;
using NotepadPlus.Properties;
using NotepadPlus.TabHandlers;
using NotepadPlus.Workers;

namespace NotepadPlus
{
    public partial class Form1 : Form
    {
        private static bool s_firstInstanceCreated;
        private static System.Windows.Forms.Timer s_autoSave = new();

        /// <summary>
        /// Конструктор окна.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            Icon = Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }
        
        /// <summary>
        /// Вызывается после отображения формы.
        /// </summary>
        /// <param name="sender">Источник эвента.</param>
        /// <param name="e">Параметры эвента.</param>
        private void Form1_Shown(object sender, EventArgs e)
        {
            if (!s_firstInstanceCreated)
            {
                if (!File.Exists("objects.json"))
                {
                    FileWorker.WriteFile("objects.json", "[]");
                }
                
                // Автосохранение.
                s_autoSave.Interval = 2147483 * 1000;
                s_autoSave.Tick += SaveAllExistingFiles;
                toolStripTextBoxAutoSaveDuration.Text = Settings.Default.AutoSave.ToString();

                if (Settings.Default.AutoSave != -1 && Settings.Default.AutoSave >= 5 && Settings.Default.AutoSave <= 2147483)
                {
                    s_autoSave.Interval = Settings.Default.AutoSave * 1000;
                    s_autoSave.Start();
                }
                
                s_firstInstanceCreated = true;

                try
                {
                    // Инициализация вкладок.
                    InstantiateTabs();
                
                    // Покраска.
                    OnUpdate(Color.White, Color.Black);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(@"Не удалось корректно инициализировать вкладки или тему:" +
                                    $@"{Environment.NewLine}{exception.Message}",
                        @"Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Инициализация вкладок.
        /// </summary>
        private void InstantiateTabs()
        {
            var jsonParse = new JsonWorker("objects.json");
            var tabs = jsonParse.CollectJson<MinTabs>();
            if (tabs is null || tabs.Count < 1)
            {
                SetNewTab($@"Untitled_1");
                return;
            }

            foreach (var tab in tabs)
            {
                if (!File.Exists(tab.Path) && tab.Existed)
                {
                    continue;
                }
                
                if (File.Exists(tab.Path))
                {
                    SetNewTab(tab.Name, tab.Path, FileWorker.ReadFile(tab.Path) ?? "", tab.CursorPosition, tab.Rtf);
                }
            }

            // Очистка файла.
            for (int i = tabs.Count - 1; i >= 0; i--)
            {
                jsonParse.RemoveObjectJson<MinTabs>(tabs[i]);
            }
        }

        /// <summary>
        /// Сохранение вкладок.
        /// </summary>
        private void SaveTabs()
        {
            var jsonParse = new JsonWorker("objects.json");
            foreach (Tabs element in TabControl.TabPages)
            {
                if (element is null || !File.Exists(element.FilePath))
                {
                    continue;
                }

                bool useRtf = File.Exists(element.FilePath) && Path.GetExtension(element.FilePath) == ".rtf";
                var tabToSerialize = new MinTabs
                {
                    Name = element.Text,
                    Path = element.FilePath,
                    Rtf = useRtf,
                    CursorPosition = element.CursorPosition,
                    Existed = File.Exists(element.FilePath)
                };

                jsonParse.UpdateJson<MinTabs>(tabToSerialize);
            }
        }

        /// <summary>
        /// Сохранение всех файлов.
        /// </summary>
        private bool SaveAllFiles()
        {
            foreach (Tabs element in TabControl.TabPages)
            {
                if (element is null)
                {
                    continue;
                }

                if (element.IsChanged)
                {
                    SaveFile(element, out bool cancel);
                    if (cancel)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Автосохранение всех существующих файлов.
        /// </summary>
        /// <param name="sender">Источник эвента.</param>
        /// <param name="e">Параметры эвента.</param>
        private void SaveAllExistingFiles(object? sender, EventArgs e)
        {
            foreach (Tabs element in TabControl.TabPages)
            {
                if (element is null)
                {
                    continue;
                }

                if (File.Exists(element.FilePath) && element.IsChanged)
                {
                    SaveFile(element, out _);
                }
            }
        }

        /// <summary>
        /// Сохранение всех файлов при выходе.
        /// </summary>
        /// <param name="allowContinuation">Показывает, продолжить исполенение программы или нет.</param>
        private void SaveAllFilesQuit(out bool allowContinuation)
        {
            allowContinuation = false;
            for (int i = TabControl.TabPages.Count - 1; i >= 0; i--)
            {
                var element = TabControl.TabPages[i] as Tabs;
                if (element is null)
                {
                    continue;
                }

                if (!element.IsChanged)
                {
                    continue;
                }

                int ask = AskUserBeforeClosing($@"Хотите сохранить {element.Text.TrimEnd('*')} перед закрытием?");
                if (ask == 2)
                {
                    allowContinuation = true;
                    return;
                }

                if (ask == 1)
                {
                    continue;
                }
                
                SaveFile(element, out allowContinuation);
                if (allowContinuation)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Закрытие всех файлов.
        /// </summary>
        /// <param name="allowContinuation">Показывает, продолжить исполенение программы или нет.</param>
        private void CloseAllFiles(out bool allowContinuation)
        {
            allowContinuation = false;
            for (int i = TabControl.TabPages.Count - 1; i >= 0; i--)
            {
                var element = TabControl.TabPages[i] as Tabs;
                if (element is null)
                {
                    continue;
                }

                if (!element.IsChanged)
                {
                    TabControl.TabPages.RemoveAt(i);
                    continue;
                }

                int ask = AskUserBeforeClosing($@"Хотите сохранить {element.Text.TrimEnd('*')} перед закрытием?");
                if (ask == 2)
                {
                    allowContinuation = true;
                    return;
                }

                if (ask == 1)
                {
                    TabControl.TabPages.RemoveAt(i);
                    continue;
                }

                SaveFile(element, out allowContinuation);
                if (allowContinuation)
                {
                    return;
                }
                
                TabControl.TabPages.RemoveAt(i);
            }
        }

        /// <summary>
        /// Спрашивает пользователя о закрытии файла.
        /// </summary>
        /// <param name="text">Текст для задания мессаджбокса.</param>
        /// <returns>Число, определяющее тип выбранной операции.</returns>
        private int AskUserBeforeClosing(string text)
        {
            DialogResult = MessageBox.Show(text,
                @"Сохранить файл",
                MessageBoxButtons.YesNoCancel);

            if (DialogResult == DialogResult.Yes)
            {
                return 0;
            }

            if (DialogResult == DialogResult.No)
            {
                return 1;
            }

            if (DialogResult == DialogResult.Cancel)
            {
                return 2;
            }

            return 2;
        }

        /// <summary>
        /// Ищет новосозданные вкладки.
        /// </summary>
        /// <returns>Количество новосозданных вкладок.</returns>
        private int GetUntitledTabsAmount()
        {
            if (TabControl.TabPages.Count == 0)
            {
                return 0;
            }

            TabControl.TabPageCollection collection = TabControl.TabPages;
            return collection.Cast<TabPage>().Count(element => element.Text.StartsWith("Untitled"));
        }

        /// <summary>
        /// Создает новую вкладку.
        /// </summary>
        /// <param name="name">Имя вкладки.</param>
        private void CreateNewTab(string name)
        {
            TabControl.TabPages.Add(new Tabs(name, RightClickMenuStrip, ""));
            TabControl.SelectedTab = TabControl.TabPages[^1];

            var currentTab = TabControl.SelectedTab as Tabs;
            currentTab!.TextBox.ShortcutsEnabled = true;
        }

        /// <summary>
        /// Ищет вкладку по имени.
        /// </summary>
        /// <param name="name">Имя вкладки.</param>
        /// <param name="foundTab">Ссылка на найденную вкладку (если она была найдена).</param>
        /// <returns>true, если удалось найти вкладку по имени; иначе false.</returns>
        private bool TabContains(string name, out TabPage foundTab)
        {
            foreach (TabPage tab in TabControl.TabPages)
            {
                if (tab.Name == name)
                {
                    foundTab = tab;
                    return true;
                }
            }

            foundTab = new Tabs($"Untitled_{GetUntitledTabsAmount()}", ContextMenuStrip, "");
            return false;
        }

        /// <summary>
        /// Создает новую вкладку.
        /// </summary>
        /// <param name="name">Имя вкладки.</param>
        /// <param name="file">Путь к файлу.</param>
        /// <param name="text">Содержимое вкладки.</param>
        /// <param name="pos">Позиция курсора</param>
        /// <param name="rtf">Показывает rtf файл или нет.</param>
        private void SetNewTab(string name, string file = "", string text = "", int pos = 0, bool rtf = false)
        {
            var tabToAdd = new Tabs(name, RightClickMenuStrip, text, pos, file, rtf);
            if (!TabContains(name, out _))
            {
                TabControl.TabPages.Add(tabToAdd);
                TabControl.SelectedTab = TabControl.TabPages[^1];

                var currentTab = TabControl.SelectedTab as Tabs;
                currentTab!.TextBox.ShortcutsEnabled = true;
            }
        }

        /// <summary>
        /// Открывает выбранный файл.
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        private void OpenFile(string path)
        {
            var extension = Path.GetExtension(path);
            if (extension == ".rtf")
            {
                SetNewTab(Path.GetFileName(path), path, FileWorker.ReadFile(path) ?? "", 0, true);
            }
            else
            {
                SetNewTab(Path.GetFileName(path), path, FileWorker.ReadFile(path) ?? "");
            }
        }
        
        /// <summary>
        /// Сохраняет выбранный файл.
        /// </summary>
        /// <param name="tab">Вкладка для сохранения.</param>
        /// <param name="cancel">Дает информацию, было ли закрыто диалоговое окно.</param>
        private void SaveFile(Tabs tab, out bool cancel)
        {
            if (File.Exists(tab.FilePath))
            {
                var extension = Path.GetExtension(tab.FilePath);
                if (extension == ".rtf")
                {
                    FileWorker.WriteFile(tab.FilePath, tab.TextBox.Rtf ?? "");
                    tab.Text = Path.GetFileName(tab.FilePath);
                }
                else
                {
                    FileWorker.WriteFile(tab.FilePath, tab.TextBox.Text ?? "");
                    tab.Text = Path.GetFileName(tab.FilePath);
                }
                
                tab.IsChanged = false;
                cancel = false;
                return;
            }

            var dialog = new SaveFileDialog();
            dialog.FileName = $"{tab.Text.TrimEnd('*')}";
            dialog.Filter = @"Текстовые файлы (*.txt)|*.txt|Rich Text Document (*.rtf)|*.rtf|Исходный код C# (*.cs)|*.cs|Все файлы (*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.Cancel)
            {
                cancel = true;
                return;
            }

            cancel = false;
            SaveFileAs(tab, dialog.FileName);
        }

        /// <summary>
        /// Сохраняет файл по выбранному пути.
        /// </summary>
        /// <param name="tab">Вкладка для сохранения.</param>
        /// <param name="path">Путь к файлу.</param>
        private void SaveFileAs(Tabs tab, string path)
        {
            var fileName = Path.GetFileName(path);
            var extension = Path.GetExtension(path);

            if (extension == ".rtf")
            {
                FileWorker.WriteFile(tab.FilePath, tab.TextBox.Rtf ?? "");
                tab.Text = fileName;
                tab.FilePath = path;
            }
            else
            {
                FileWorker.WriteFile(path, tab.TextBox.Text ?? "");
                tab.Text = fileName;
                tab.FilePath = path;
            }
            
            tab.IsChanged = false;
        }

        /// <summary>
        /// Вызов при нажатии "Открыть".
        /// </summary>
        /// <param name="sender">Источник эвента.</param>
        /// <param name="e">Параметры эвента.</param>
        private void OpenFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = @"Текстовые файлы (*.txt)|*.txt|Rich Text Document (*.rtf)|*.rtf|Исходный код C# (*.cs)|*.cs|Все файлы (*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            try
            {
                var path = dialog.FileName;
                OpenFile(path);
                OnUpdate(Color.White, Color.Black);
            }
            catch (Exception exception)
            {
                MessageBox.Show($@"Не удалось открыть файл:{Environment.NewLine}{exception.Message}",
                    @"Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Сохраняет файл при нажатии "Сохранить".
        /// </summary>
        /// <param name="sender">Источник эвента.</param>
        /// <param name="e">Параметры эвента.</param>
        private void SaveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TabControl.SelectedTab is not Tabs currentTab)
            {
                return;
            }

            try
            {
                SaveFile(currentTab, out _);
            }
            catch (Exception exception)
            {
                MessageBox.Show($@"Не удалось сохранить файл:{Environment.NewLine}{exception.Message}",
                    @"Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Сохраняет файл при нажатии "Сохранить как".
        /// </summary>
        /// <param name="sender">Источник эвента.</param>
        /// <param name="e">Параметры эвента.</param>
        private void SaveFileAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TabControl.SelectedTab is not Tabs currentTab)
            {
                return;
            }

            var dialog = new SaveFileDialog();
            dialog.FileName = $"{currentTab.Text.TrimEnd('*')}";
            dialog.Filter = @"Текстовые файлы (*.txt)|*.txt|Rich Text Document (*.rtf)|*.rtf|Исходный код C# (*.cs)|*.cs|Все файлы (*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            try
            {
                var path = dialog.FileName;
                SaveFileAs(currentTab, path);
                OnUpdate(Color.White, Color.Black);
            }
            catch (Exception exception)
            {
                MessageBox.Show($@"Не удалось сохранить файл:{Environment.NewLine}{exception.Message}",
                    @"Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Создает новое окно при нажатии "Новое окно".
        /// </summary>
        /// <param name="sender">Источник эвента.</param>
        /// <param name="e">Параметры эвента.</param>
        private void NewWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");
            var formToShow = new Form1();
            formToShow.Show();
            formToShow.CreateNewTab($@"Untitled_{formToShow.GetUntitledTabsAmount() + 1}");
            formToShow.OnUpdate(Color.White, Color.Black);
        }

        /// <summary>
        /// Создает новую вкладку при нажатии "Новая вкладка".
        /// </summary>
        /// <param name="sender">Источник эвента.</param>
        /// <param name="e">Параметры эвента.</param>
        private void NewTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var untitledTabsAmount = GetUntitledTabsAmount();
            CreateNewTab($@"Untitled_{untitledTabsAmount + 1}");
            OnUpdate(Color.White, Color.Black);
        }

        /// <summary>
        /// Открывает вкладку с шрифтом и форматированием при нажатии "Шрифт и текст", а также сохраняет изменения.
        /// </summary>
        /// <param name="sender">Источник эвента.</param>
        /// <param name="e">Параметры эвента.</param>
        private void FontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FontDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            if (TabControl.SelectedTab is Tabs currentTab)
            {
                currentTab.TextBox.Font = FontDialog.Font;
            }
        }

        /// <summary>
        /// Отменяет предыдущее действие.
        /// </summary>
        /// <param name="sender">Источник эвента.</param>
        /// <param name="e">Параметры эвента.</param>
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var currentTab = TabControl.SelectedTab as Tabs;
            currentTab?.TextBox.Undo();
        }

        /// <summary>
        /// Повторяет предыдущее действие.
        /// </summary>
        /// <param name="sender">Источник эвента.</param>
        /// <param name="e">Параметры эвента.</param>
        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var currentTab = TabControl.SelectedTab as Tabs;
            currentTab?.TextBox.Redo();
        }

        /// <summary>
        /// Вырезает выделенный текст.
        /// </summary>
        /// <param name="sender">Источник эвента.</param>
        /// <param name="e">Параметры эвента.</param>
        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var currentTab = TabControl.SelectedTab as Tabs;
            currentTab?.TextBox.Cut();
        }

        /// <summary>
        /// Копирует выделенный текст.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var currentTab = TabControl.SelectedTab as Tabs;
            currentTab?.TextBox.Copy();
        }

        /// <summary>
        /// Вставляет текст из буфера.
        /// </summary>
        /// <param name="sender">Источник эвента.</param>
        /// <param name="e">Параметры эвента.</param>
        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var currentTab = TabControl.SelectedTab as Tabs;
            currentTab?.TextBox.Paste();
        }

        /// <summary>
        /// Выделяет весь текст.
        /// </summary>
        /// <param name="sender">Источник эвента.</param>
        /// <param name="e">Параметры эвента.</param>
        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var currentTab = TabControl.SelectedTab as Tabs;
            currentTab?.TextBox.SelectAll();
        }

        /// <summary>
        /// Удаляет выделенный текст.
        /// </summary>
        /// <param name="sender">Источник эвента.</param>
        /// <param name="e">Параметры эвента.</param>
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TabControl.SelectedTab is Tabs currentTab)
            {
                currentTab.TextBox.SelectedText = string.Empty;
            }
        }

        /// <summary>
        /// Обновляет тему приложения.
        /// </summary>
        private void UpdateFullTheme()
        {
            foreach (Control control in Controls)
            {
                UpdateMenuControl(control, Settings.Default.BackColor, Settings.Default.ForeColor);
            }
            
            UpdateMenuItems(menuStrip1.Items, Settings.Default.BackColor, Settings.Default.ForeColor);
            ToolStripProfessionalRenderer renderer = Settings.Default.BackColor == Color.FromArgb(45, 45, 45)
                ? new ToolStripProfessionalRenderer(new OverrideColors())
                : new ToolStripProfessionalRenderer();
            
            menuStrip1.Renderer = renderer;
            RightClickMenuStrip.Renderer = renderer;

            foreach (Tabs tab in TabControl.Controls)
            {
                tab.BackColor = Settings.Default.BackColor;
                tab.ForeColor = Settings.Default.ForeColor;
                tab.ContextMenuStrip = RightClickMenuStrip;
                tab.ContextMenuStrip.BackColor = Settings.Default.BackColor;
                tab.ContextMenuStrip.ForeColor = Settings.Default.ForeColor;
            }
        }

        /// <summary>
        /// Форсит обновление темы приложения на указанные цвета.
        /// </summary>
        /// <param name="back">Цвет бэкграунда.</param>
        /// <param name="fore">Цвет форграунда.</param>
        private void ForceUpdateFullTheme(Color back, Color fore)
        {
            foreach (Control control in Controls)
            {
                UpdateMenuControl(control, back, fore);
            }

            UpdateMenuItems(menuStrip1.Items, back, fore);
            ToolStripProfessionalRenderer renderer = new();

            menuStrip1.Renderer = renderer;
            RightClickMenuStrip.Renderer = renderer;

            foreach (Tabs tab in TabControl.Controls)
            {
                tab.BackColor = Settings.Default.BackColor;
                tab.ForeColor = Settings.Default.ForeColor;
                tab.ContextMenuStrip = RightClickMenuStrip;
            }
        }

        /// <summary>
        /// Рекурсивное обновление подэлементов переданного контрола.
        /// </summary>
        /// <param name="control">Контрол для покраски.</param>
        /// <param name="back">Цвет бэкграунда.</param>
        /// <param name="fore">Цвет форграунда.</param>
        private void UpdateMenuControl(Control control, Color back, Color fore)
        {
            control.BackColor = back;
            control.ForeColor = fore;

            foreach (Control subControl in control.Controls)
            {
                UpdateMenuControl(subControl, back, fore);
            }
        }

        /// <summary>
        /// Рекурсивное обновление ToolStripItems.
        /// </summary>
        /// <param name="items">Элементы коллекции ToolStripItem.</param>
        /// <param name="back">Цвет бэкграунда.</param>
        /// <param name="fore">Цвет форграунда.</param>
        private void UpdateMenuItems(ToolStripItemCollection items, Color back, Color fore)
        {
            foreach (var item in items)
            {
                if (item is not ToolStripMenuItem itemCast)
                {
                    continue;
                }
                
                itemCast.BackColor = back;
                itemCast.ForeColor = fore;
                
                if (itemCast.DropDownItems.Count > 0)
                {
                    UpdateMenuItems(itemCast.DropDownItems, Settings.Default.BackColor, Settings.Default.ForeColor);
                }
            }
        }
        
        /// <summary>
        /// Обновление темы.
        /// </summary>
        /// <param name="back">Цвет бэкграунда.</param>
        /// <param name="fore">Цвет форграунда.</param>
        private void OnUpdate(Color back, Color fore)
        {
            try
            {
                UpdateFullTheme();
            }
            catch
            {
                ForceUpdateFullTheme(back, fore);
            }
        }
        
        /// <summary>
        /// Обновляет тему на стандартную.
        /// </summary>
        /// <param name="sender">Источник эвента.</param>
        /// <param name="e">Параметры эвента.</param>
        private void StandardThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.BackColor = Color.White;
            Settings.Default.ForeColor = Color.Black;
            Settings.Default.Save();

            OnUpdate(Color.White, Color.Black);
        }
        
        /// <summary>
        /// Обновляет тему на темную.
        /// </summary>
        /// <param name="sender">Источник эвента.</param>
        /// <param name="e">Параметры эвента.</param>
        private void DarkThemeToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            Settings.Default.BackColor = Color.FromArgb(45, 45, 45);
            Settings.Default.ForeColor = Color.FromArgb(230, 230, 230);
            Settings.Default.Save();

            OnUpdate(Color.White, Color.Black);
        }

        /// <summary>
        /// Обновляет таймер автосохранения.
        /// </summary>
        /// <param name="sender">Источник эвента.</param>
        /// <param name="e">Параметры эвента.</param>
        private void ToolStripTextBoxAutoSaveDuration_TextChanged(object sender, EventArgs e)
        {
            var text = toolStripTextBoxAutoSaveDuration.TextBox?.Text;
            if (text is null || !int.TryParse(text, out int a))
            {
                return;
            }

            if (a == -1)
            {
                Settings.Default.AutoSave = a;
                Settings.Default.Save();
                if (s_autoSave.Interval == 2147483 * 1000)
                {
                    return;
                }
                
                s_autoSave.Stop();
                s_autoSave.Interval = 2147483 * 1000;
                return;
            }

            // Делаем лимит на таймере
            a = a < 5 || a > 2147483 ? 10 : a;
            Settings.Default.AutoSave = a;
            Settings.Default.Save();
            if (s_autoSave.Interval == 2147483 * 1000)
            {
                s_autoSave.Interval = Settings.Default.AutoSave * 1000;
                s_autoSave.Start();
                return;
            }
                
            s_autoSave.Stop();
            s_autoSave.Interval = Settings.Default.AutoSave * 1000;
            s_autoSave.Start();
        }
        
        /// <summary>
        /// Выделяет весь текст.
        /// </summary>
        /// <param name="sender">Источник эвента.</param>
        /// <param name="e">Параметры эвента.</param>
        private void SelectAllContextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var currentTab = TabControl.SelectedTab as Tabs;
            currentTab?.TextBox.SelectAll();
        }
        
        /// <summary>
        /// Вырезает выделенный текст.
        /// </summary>
        /// <param name="sender">Источник эвента.</param>
        /// <param name="e">Параметры эвента.</param>
        private void CutContextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var currentTab = TabControl.SelectedTab as Tabs;
            currentTab?.TextBox.Cut();
        }

        /// <summary>
        /// Копирует выделенный текст.
        /// </summary>
        /// <param name="sender">Источник эвента.</param>
        /// <param name="e">Параметры эвента.</param>
        private void CopyContextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var currentTab = TabControl.SelectedTab as Tabs;
            currentTab?.TextBox.Copy();
        }

        /// <summary>
        /// Вставляет текст из буфера.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasteContextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var currentTab = TabControl.SelectedTab as Tabs;
            currentTab?.TextBox.Paste();
        }

        /// <summary>
        /// Выбирает форматирование для выделенного текста.
        /// </summary>
        /// <param name="sender">Источник эвента.</param>
        /// <param name="e">Параметры эвента.</param>
        private void SelectFormatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FontDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            if (TabControl.SelectedTab is Tabs currentTab)
            {
                currentTab.TextBox.SelectionFont = FontDialog.Font;
            }
        }

        /// <summary>
        /// Выдает информацию о программе.
        /// </summary>
        /// <param name="sender">Источник эвента.</param>
        /// <param name="e">Параметры эвента.</param>
        private void AboutMeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var process = new Process();
                process.StartInfo.UseShellExecute = true; 
                process.StartInfo.FileName = "https://telegra.ph/Notepad-12-01";
                process.Start();
            }
            catch (Exception exception)
            {
                MessageBox.Show($@"Не удалось открыть браузер:{Environment.NewLine}{exception.Message}" +
                                $@"{Environment.NewLine}Перейдите по ссылке https://telegra.ph/Notepad-12-01",
                    @"Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Хэндлит выход из программы.
        /// </summary>
        /// <param name="sender">Источник эвента.</param>
        /// <param name="e">Параметры эвента.</param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveAllFilesQuit(out bool allow);
                if (allow)
                {
                    return;
                }
                
                SaveTabs();
                s_autoSave.Stop();
                Application.Exit();
            }
            catch (Exception exception)
            {
                MessageBox.Show($@"Не удалось сохранить файлы при выходе:{Environment.NewLine}{exception.Message}
                    {Environment.NewLine}{exception}",
                    @"Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Хэндлит закрытие формы.
        /// </summary>
        /// <param name="sender">Источник эвента.</param>
        /// <param name="e">Параметры эвента.</param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (e.Cancel)
                {
                    return;
                }

                SaveAllFilesQuit(out bool allow);
                if (allow)
                {
                    e.Cancel = true;
                }
                
                SaveTabs();
                s_autoSave.Stop();
            }
            catch (Exception exception)
            {
                MessageBox.Show($@"Не удалось сохранить файлы при выходе:{Environment.NewLine}{exception.Message}
                    {Environment.NewLine}{exception}",
                    @"Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Сохраняет все файлы.
        /// </summary>
        /// <param name="sender">Источник эвента.</param>
        /// <param name="e">Параметры эвента.</param>
        private void SaveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                _ = SaveAllFiles();
            }
            catch (Exception exception)
            {
                MessageBox.Show($@"Не удалось сохранить файл:{Environment.NewLine}{exception.Message}",
                    @"Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Закрывает текущую вкладку.
        /// </summary>
        /// <param name="sender">Источник эвента.</param>
        /// <param name="e">Параметры эвента.</param>
        private void CloseCurrentTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TabControl.SelectedTab is not Tabs currentTab)
            {
                return;
            }

            try
            {
                var currentIndex = TabControl.SelectedIndex;
                if (!currentTab.IsChanged)
                {
                    TabControl.TabPages.RemoveAt(currentIndex);
                    return;
                }
                
                int ask = AskUserBeforeClosing($@"Хотите сохранить {currentTab.Text.TrimEnd('*')} перед закрытием?");
                if (ask == 2)
                {
                    return;
                }

                if (ask == 1)
                {
                    TabControl.TabPages.RemoveAt(currentIndex);
                    return;
                }

                SaveFile(currentTab, out bool allowContinuation);
                if (!allowContinuation)
                {
                    TabControl.TabPages.RemoveAt(currentIndex);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($@"Не удалось закрыть файл:{Environment.NewLine}{exception.Message}",
                    @"Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error); 
            }
        }

        /// <summary>
        /// Закрывает все вкладки.
        /// </summary>
        /// <param name="sender">Источник эвента.</param>
        /// <param name="e">Параметры эвента.</param>
        private void CloseAllTabsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CloseAllFiles(out _);
                OnUpdate(Color.White, Color.Black);
            }
            catch (Exception exception)
            {
                MessageBox.Show($@"Не удалось закрыть файл:{Environment.NewLine}{exception.Message}",
                    @"Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
