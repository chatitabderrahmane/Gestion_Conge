using appGestion_conge.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace appGestion_conge.Controllers
{
    public class employeController : Controller
    {
        // GET: employe
        // GET: employe
        gestion_congeEntities mydb = new gestion_congeEntities();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index([Bind(Include = "noemployer,duree,date_debut ,date_fin,motif,etat")] conge conge)
        {

            if (ModelState.IsValid)
            {
                int ide = (int)Session["ide"];
                conge.noemployer = ide;
                conge.etat = "en cours";
                mydb.conges.Add(conge);
                mydb.SaveChanges();

                //conge co = new conge();
                //co = (from obj in mydb.conges where obj.noemployer == ide select obj).FirstOrDefault();
                //var mail = new MailMessage();
                //var logininfo = new NetworkCredential("abderrahmanechatit63@gmail.com", "Ab212133");
                //mail.From = new MailAddress(co.employer.email);

                //mail.To.Add(new MailAddress("abderrahmanechatit63@gmail.com"));
                //mail.Subject = "Demmande de conge";
                //string body = "vous avez un nouvaux demande de conge non lue de Mr/Me   " + co.employer.nom + "  " + co.employer.prenom
                //        + " duree: " + co.duree + "    date debut : " + co.date_debut + " date fin : " + co.date_fin;
                //mail.Body = body;
                //var smtpClient = new SmtpClient("smtp.gmail.com", 587);
                //smtpClient.EnableSsl = true;
                //smtpClient.Credentials = logininfo;
               
                //smtpClient.UseDefaultCredentials = true;
                //smtpClient.Send(mail);

                ViewBag.Message = ("votre demande envoye avec succe ");
            }


            return View();

        }

        public ActionResult annulerconge(int id)
        {
            conge annulerconge = new conge();
            annulerconge = (from obj in mydb.conges
                            where obj.noconge == id
                            select obj).FirstOrDefault();
            mydb.conges.Remove(annulerconge);
            mydb.SaveChanges();
            return RedirectToAction("mesdemmandes");
        }
        [HttpGet]
        public ActionResult mesdemmandes()
        {
            try
            {
                int ide = (int)Session["ide"];
                //List<conge> mesdemandes = new List<conge>();

                //mesdemandes = (from obj in mydb.conges
                //    where obj.noemployer == ide
                //     select obj).ToList();
                return View(mydb.conges.Where(x => x.noemployer == ide).ToList());

            }
            catch
            {
                ViewBag.result = "vous avez un petit Reconnecter   probleme";
                return View(ViewBag.result);

            }


        }

        public ActionResult login(string username, string motdepasse)
        {
            employer emp = new employer();


            emp = (from obj in mydb.employers
                   where obj.username == username && obj.motdepasse == motdepasse
                   select obj).FirstOrDefault();
            if (emp != null)
            {
                Session["ide"] = emp.noemployer;
                Session["nom"] = emp.nom + " " + emp.prenom;
                Session["E-mail"] = emp.email;
                Session["grade"] = emp.grade;
                Session["telephone"] = emp.telephone;
                return RedirectToAction("Index");
            }
            else
                return View();


        }
        public ActionResult profile()
        {
            int ide = (int)Session["ide"];
            return View(mydb.employers.Where(x => x.noemployer == ide).FirstOrDefault());

        }
        [HttpPost]
        public ActionResult profile([Bind(Include = "noemployer,nom,prenom,datenaissance,grade,telephone,email,username,motdepasse")] employer employe)
        {

            if (ModelState.IsValid)
            {

                mydb.Entry(employe).State = EntityState.Modified;
                mydb.SaveChanges();
                return View();
            }
            return View(employe);

        }

    }
}