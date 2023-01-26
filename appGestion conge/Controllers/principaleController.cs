using appGestion_conge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace appGestion_conge.Controllers
{
    public class principaleController : Controller
    {
        // GET: pricioale
        public ActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public ActionResult contact(Contactmodel contact)
        {
            var mail = new MailMessage();
            var logininfo = new NetworkCredential("abderrahmanechatit63@gmail.com", "Ab212133");
            mail.From = new MailAddress(contact.Email);

            mail.To.Add(new MailAddress("abderrahmanechatit63@gmail.com"));
            mail.Subject = contact.name;
            mail.Body = contact.name +
                    "Phone : " + contact.phone + contact.message;

            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = logininfo;
            smtpClient.Send(mail);

            //MailMessage msg = new MailMessage();

            //msg.From = new MailAddress(contact.Email);
            //msg.To.Add(new MailAddress("abderrahmanechatit63@gmail.com"));
            //msg.Subject = contact.name;
            //msg.Body = contact.name +
            //        "Phone : " + contact.phone + contact.message;
            //msg.Priority = MailPriority.High;

            //SmtpClient client = new SmtpClient();

            //client.Credentials = new NetworkCredential("abderrahmanechatit63@gmail.com", "Ab212133", "smtp.gmail.com");
            //client.Host = "smtp.gmail.com";
            //client.Port = 587;
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.EnableSsl = true;
            //client.UseDefaultCredentials = true;

            //client.Send(msg);
            //ViewBag.result = "envoyer avec succe , thanks ";
            return RedirectToAction("Index");
        }
    }
    }
 