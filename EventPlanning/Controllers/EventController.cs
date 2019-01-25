using System.Collections.Generic;
using System.Web.Mvc;
using EventPlanning.Models;
using EventPlanning.BLL.Interfaces;
using EventPlanning.BLL.DTO;
using AutoMapper;
using Microsoft.AspNet.Identity;
using System.Net.Mail;
using System;

namespace EventPlanning.Controllers
{
    public class EventController : Controller
    {
        IEventService db;
        IParticipantService ParticipantService;

        public EventController(IEventService eventService, IParticipantService participantService)
        {
            db = eventService;
            ParticipantService = participantService;
        }



        [HttpGet]
        public ActionResult EventForm()
        {
            IEnumerable<EventDTO> eventDTOs = db.GetEvents();
            var events = Mapper.Map<IEnumerable<EventDTO>, List<EventModel>>(eventDTOs);
            ViewBag.Events = events;
            ViewBag.UserId = User.Identity.GetUserId();
            return View();
        }

        [HttpGet]
        public ActionResult FullInformation(int? id)
        {
            EventDTO eventDTO = db.GetById(id);
            if (eventDTO == null )
            {
                return HttpNotFound();
            }

            var @event = Mapper.Map<EventDTO, EventModel>(eventDTO);
            return View(@event);
        }


        [HttpGet]
        public ActionResult Participant(int? id)
        {
            var Participants = ParticipantService.FindByEventId(id);
            ViewBag.Participants = Participants;
            return View();
        }


        #region Registration on the event
        [HttpGet]
        public ActionResult RegistrationOnEvent(int? id)
        {
            EventDTO eventDTO = db.GetById(id);
            if (eventDTO == null)
            {
                return HttpNotFound();
            }

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("ConfirmEmail", "Event", new { Token = eventDTO.Id, Email = User.Identity.GetUserName() });
            }

            var @event = Mapper.Map<EventDTO, EventModel>(eventDTO);
            return View(@event);
        }

        [HttpPost]
        public ActionResult RegistrationOnEvent(ParticipantModel participantModel)
        {
            if (ModelState.IsValid)
            {
                MailAddress from = new MailAddress("EventPlanningTask@gmail.com", "Web Registration");
                MailAddress to = new MailAddress(participantModel.Email);
                MailMessage m = new MailMessage(from, to);
                m.Subject = "Email confirmation";
                m.Body = string.Format("Для завершения регистрации на мероприятие перейдите по ссылке:" +
                                "<a href=\"{0}\" title=\"Подтвердить регистрацию\">{0}</a>",
                    Url.Action("ConfirmEmail", "Event", new { Token = participantModel.EventId, Email = participantModel.Email }, Request.Url.Scheme));
                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential("EventPlanningTask@gmail.com", "qweqaz12345");
                smtp.Send(m);
                return RedirectToAction("Confirm", "Account", new { Email = participantModel.Email });
            }
            else
                return View(participantModel);
        }


        public ActionResult ConfirmEmail(string Token, string Email)
        {
            Int32.TryParse(Token, out int id);
            ParticipantService.AddParticapant(new ParticipantDTO { Email = Email, EventId = id });
            return View();
        }
        #endregion


        #region Add
        [Authorize]
        [HttpGet]
        public ActionResult AddEvent()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddEvent(ViewEventModel viewEventModel)
        {
            if (ModelState.IsValid)
            {
                EventModel @event = EventParser.Parse(viewEventModel);
                @event.UserId = User.Identity.GetUserId();
                var eventDTO = Mapper.Map<EventModel, EventDTO>(@event);
                db.AddEvent(eventDTO);
                //   ParticipantService.AddParticapant(new ParticipantDTO { Email=User.Identity.GetUserName()})
                return RedirectToAction("EventForm");
            }
            else
                return View();
        }
        #endregion

        #region Delete
        [HttpGet]
        public ActionResult DeleteEvent(int? id)
        {
            EventDTO eventDTO = db.GetById(id);
            if (eventDTO == null || eventDTO.UserId != User.Identity.GetUserId())
            {
                return HttpNotFound();
            }

            var @event = Mapper.Map<EventDTO, EventModel>(eventDTO);
            return View(@event);
        }

        [HttpPost, ActionName("DeleteEvent")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (db.GetById(id).UserId != User.Identity.GetUserId() || !db.DeleteEvent(id))
            {
                return HttpNotFound();
            }

            return RedirectToAction("EventForm");
        }
        #endregion

        #region Edit
        [HttpGet]
        [Authorize]
        public ActionResult EditEvent(int? id)
        {
            EventDTO eventDTO = db.GetById(id);
            if (eventDTO == null || eventDTO.UserId != User.Identity.GetUserId())
            {
                return HttpNotFound();
            }

            var @event = Mapper.Map<EventDTO, EventModel>(eventDTO);
            return View(@event);
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditEvent(ViewEventModel viewEventModel)
        {
            EventModel @event = EventParser.Parse(viewEventModel);
            @event.UserId = db.GetById(@event.Id).UserId;
            if (@event.UserId == User.Identity.GetUserId() && ModelState.IsValid)
            {
                var eventDTO = Mapper.Map<EventModel, EventDTO>(@event);
                db.EditEvent(eventDTO);
                return RedirectToAction("EventForm");
            }
            else
                return View(@event);
        }
        #endregion
    }
}