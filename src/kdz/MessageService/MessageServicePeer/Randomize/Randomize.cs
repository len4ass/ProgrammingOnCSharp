using System;

namespace MessageServicePeer.Randomize
{
    /// <summary>
    /// Класс отвечающий за генерацию последовательностей.
    /// </summary>
    public sealed partial class Randomize
    {
        private readonly Random _random;
        private static Randomize _instance;
        private Randomize()
        {
            _random = new Random();
        }
        
        /// <summary>
        /// Доступ к синглетону класса.
        /// </summary>
        public static Randomize Instance => _instance ??= new Randomize();
    }
}