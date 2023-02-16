using System.Text.RegularExpressions;

namespace EKRLib
{
    /// <summary>
    /// Абстрактный класс для представления различных видов транспортных средств.
    /// </summary>
    public abstract class Transport
    {
        /// <summary>
        /// Модель транспортного средства.
        /// </summary>
        private readonly string _model;
        
        /// <summary>
        /// Мощность транспортного средства.
        /// </summary>
        private readonly uint _power;

        /// <summary>
        /// Свойство для ограниченного доступа к приватному полю <see cref="_model"/>.
        /// </summary>
        /// <exception cref="TransportException">Исключение, которое будет вызвано в одном из двух случаев:
        /// длина строки не равна 5 символам или строка состоит не из заглавных букв латинского алфавита/цифр.</exception>
        protected string Model
        {
            get => _model;
            private init
            {
                if (Regex.IsMatch(value, @"[^A-Z0-9]") || value.Length != 5)
                {
                    throw new TransportException($"Недопустимая модель {value}");
                }

                _model = value;
            } 
        }

        /// <summary>
        /// Свойство для ограниченного доступа к приватному полю <see cref="_power"/>.
        /// </summary>
        /// <exception cref="TransportException">Исключение, которое будет вызвано, если мощность меньше 20</exception>
        private uint Power
        {
            get => _power;
            init
            {
                if (value < 20)
                {
                    throw new TransportException($"Мощность не может быть меньше 20 л.с.");
                }

                _power = value;
            }
        }

        /// <summary>
        /// Конструктор для объектов типов, наследующихся от Transport.
        /// </summary>
        /// <param name="model">Название модели транспортного средства.</param>
        /// <param name="power">Мощность транспортного средства.</param>
        protected Transport(string model, uint power)
        {
            Model = model;
            Power = power;
        }

        /// <summary>
        /// Получает название модели ТС и ее мощность.
        /// </summary>
        /// <returns>Строковое представление модели ТС и ее мощность.</returns>
        public override string ToString()
        {
            return $"Model: {Model}, Power: {Power}";
        }

        /// <summary>
        /// Получает звук соответствующий модели ТС.
        /// </summary>
        /// <returns>Строковое представление модели ТС и ее звук.</returns>
        public abstract string StartEngine();
    }
}