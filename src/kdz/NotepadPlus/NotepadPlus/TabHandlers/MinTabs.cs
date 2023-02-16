namespace NotepadPlus.TabHandlers
{
    /// <summary>
    /// Класс для сериализации информации о вкладках.
    /// </summary>
    internal sealed class MinTabs
    {
        /// <summary>
        /// Имя вкладки.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Путь к файлу, привязанному к вкладке.
        /// </summary>
        public string Path { get; set; }
        
        /// <summary>
        /// Позиция курсора.
        /// </summary>
        public int CursorPosition { get; set; }
        
        /// <summary>
        /// Указывает, является ли файл RTF.
        /// </summary>
        public bool Rtf { get; set; }

        /// <summary>
        /// Указывается, существовал ли такой файл на момент закрытия программы.
        /// </summary>
        public bool Existed { get; set; }
    }
}