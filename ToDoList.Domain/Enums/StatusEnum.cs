using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Domain.Enums
{
    public class StatusEnum : Enumeration
    {
        public static readonly StatusEnum Pendente = new StatusEnum(1, "Pendente");
        public static readonly StatusEnum Concluida = new StatusEnum(2, "Concluida");
        public static readonly StatusEnum Adiada = new StatusEnum(3, "Adiada");
        public static readonly StatusEnum Cancelada = new StatusEnum(4, "Cancelada");

        public StatusEnum(int id, string name) : base(id, name)
        {
        }
    }
}