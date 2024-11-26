using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceDesk.Models;
using ServiceDesk.ViewModels;

namespace ServiceDesk.Controllers
{
    public class TicketController : Controller
    {
        private TicketsContext context = new TicketsContext();
        // GET: Ticket
        public ActionResult Index()
        {
           
                var listado = context.Ticket;
                return View(listado);
            
        }
        [HttpGet]
        public ActionResult Nuevo()
        {
            var viewmodel = new TicketViewModels();
            viewmodel.Responsables = context.Responsable.ToList();
            viewmodel.Estados = context.Estado.ToList();   
            viewmodel.Usuarios = context.Usuario.ToList();
            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult Nuevo(Ticket ticket)
        {
            context.Ticket.Add(ticket);
            context.SaveChanges();
            return RedirectToAction("Index"); 
        }
        [HttpGet]
        public ActionResult Detalle (int id)
        {
            var viewModel = new TicketViewModels();
            viewModel.Ticket = context.Ticket.FirstOrDefault(x => x.Id == id);
            return View(viewModel);

        }

        [HttpGet]
        public ActionResult Actualizar(int id)
        {
            var viewModel = new TicketViewModels();
            viewModel.Ticket = context.Ticket.FirstOrDefault(x => x.Id == id);
            viewModel.Responsables = context.Responsable.ToList();
            viewModel.Estados = context.Estado.ToList();
            viewModel.Usuarios = context.Usuario.ToList();
            return View(viewModel);

        }

        [HttpPost]
        public ActionResult Actualizar(Ticket ticket)
        {
            context.Entry(ticket).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            var ticket = context.Ticket.FirstOrDefault(x => x.Id == id);
            context.Ticket.Remove(ticket);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}