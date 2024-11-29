using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceDesk.Models;

namespace ServiceDesk.ViewModels
{
    public class TicketViewModels
    {
        public Ticket Ticket { get; set; }

        public List<Responsable> Responsables { get; set; }
        public List<Usuario> Usuarios { get; set; }
        public List<Estado> Estados { get; set; }

    }
}