using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyMaker
{
    public class PartyModel
    {
        /// <summary>
        /// Организационные моменты
        /// </summary>
        public string TypeofEvent { get; set; }
        /// <summary>
        /// Тип мероприятия
        /// </summary>
        public string Distination { get; set; }
        /// <summary>
        /// Место проведения
        /// </summary>
        public string Services { get; set; }
        /// <summary>
        /// Услуги
        /// </summary>
        public override string ToString()
        {
            return $"Тип мероприятия: {TypeofEvent}, Где будет проходить мероприятие: {Distination}, Услуги: {Services}";
        }
        public List<Workers> Workers { get; set; }
        /// <summary>
        /// Кто работает на этом мероприятии
        /// </summary>
    }

    public class Workers
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Должность
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// Стаж
        /// </summary>
        public int Experience { get; set; }

        public override string ToString()
        {
            return $"Имя: {Name}, Должность: {Position}, Стаж: {Experience}";
        }
    }
}
