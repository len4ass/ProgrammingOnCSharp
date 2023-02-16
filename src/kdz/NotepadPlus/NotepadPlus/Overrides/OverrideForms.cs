using System;
using System.Windows.Forms;

namespace NotepadPlus.Overrides
{
    /// <summary>
    /// Класс для изменения поведения формы на завершение программы при закрытии последнего окна.
    /// </summary>
    internal sealed class OverrideForms
    {
        private static ApplicationContext s_appContext;
        
        /// <summary>
        /// Обычное поведение.
        /// </summary>
        public static void Run()
        {
            s_appContext = new ApplicationContext();
            Application.Run(s_appContext);
        }
        
        /// <summary>
        /// Модифицированное поведение для ApplicationContext.
        /// </summary>
        /// <param name="context">Контекст для формы.</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, если context null.</exception>
        public static void Run(ApplicationContext context)
        {
            s_appContext = context ?? throw new ArgumentNullException(nameof(context));
 
            if (context.MainForm is not null)
            {
                context.MainForm.Closed += ClosedEventHandler;
            }
 
            Application.Run(context);
        }
        
        /// <summary>
        /// Модифицированное поведение для формы.
        /// </summary>
        /// <param name="form">Ссылка на объект формы.</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, если форма null.</exception>
        public static void Run(Form form)
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }
 
            form.Closed += ClosedEventHandler;
            s_appContext = new ApplicationContext
            {
                MainForm = form
            };
            
            Application.Run(s_appContext);
        }
        
        /// <summary>
        /// Модификауия поведения при закрытии главного окна.
        /// </summary>
        /// <param name="sender">Источник эвента.</param>
        /// <param name="e">Параметры эвента.</param>
        /// <exception cref="ArgumentNullException">Выбрасывается если sender или e null.</exception>
        private static void ClosedEventHandler(object? sender, EventArgs e)
        {
            if (sender is null)
            {
                throw new ArgumentNullException(nameof(sender));
            }

            if (e is null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            if (s_appContext.MainForm != sender || Application.OpenForms.Count == 0)
            {
                return;
            }
            
            // Поиск нового окна, чтобы переназначить главное
            foreach (Form form in Application.OpenForms)
            {
                if (form == sender)
                {
                    continue;
                }
                 
                s_appContext.MainForm = form;
                s_appContext.MainForm.Closed += ClosedEventHandler;
                return;
            }
        }
    }
}