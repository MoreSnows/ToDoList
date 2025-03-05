using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Domain.Enums
{
    public class PriorityEnum : Enumeration
    {
        public static readonly PriorityEnum Baixa = new PriorityEnum(1, "Baixa");
        public static readonly PriorityEnum Media = new PriorityEnum(2, "Média");
        public static readonly PriorityEnum Alta = new PriorityEnum(3, "Alta");
        public static readonly PriorityEnum Urgente = new PriorityEnum(4, "Urgente");

        public PriorityEnum(int id, string name) : base(id, name)
        {
        }
    }
}