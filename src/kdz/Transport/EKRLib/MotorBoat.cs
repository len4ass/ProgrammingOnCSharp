namespace EKRLib
{
    /// <summary>
    /// Класс наследник Transport (тип MotorBoat).
    /// </summary>
    public sealed class MotorBoat : Transport
    {
        /// <summary>
        /// Конструктор для объектов типа MotorBoat (наследник Transport).
        /// </summary>
        /// <param name="model">Название модели транспортного средства.</param>
        /// <param name="power">Мощность транспортного средства.</param>
        public MotorBoat(string model, uint power) : base(model, power)
        {
        }

        /// <summary>
        /// Получает тип ТС, модель и мощность.
        /// </summary>
        /// <returns>Строковое представление типа ТС, модели и ее мощности.</returns>
        public override string ToString()
        {
            return $"MotorBoat. {base.ToString()}";
        }
        
        /// <summary>
        /// Получает звук соответствующий модели ТС.
        /// </summary>
        /// <returns>Строковое представление модели ТС и ее звук.</returns>
        public override string StartEngine()
        {
            return $"{Model}: Brrrbrr";
        }
    }
}