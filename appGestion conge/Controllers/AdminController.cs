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
    public class AdminController : Controller
    {
      
 
            gestion_congeEntities mydb = new gestion_congeEntities();

        

        [HttpPost]
        public void Export()
        {
         //   Response.ContentType = "Application/pdf";
         //   Response.AddHeader("Content-Disposition", "attachement; filename=YourFileName.pdf");
         //   Response.Cache.SetCacheability(HttpCacheability.NoCache);
         //   StringWriter sw = new StringWriter();
         //   HtmlTextWriter hw = new HtmlTextWriter(sw);
           
         ////   DivToPrint.RenderControl(hw);
         //   Document doc = new Document(PageSize.A4, 50f, 50f, 100f, 30f);
         //   HTMLWorker htw = new HTMLWorker(doc);
         //   PdfWriter.GetInstance(doc, Response.OutputStream);
         //   doc.Open();
         //   StringReader sr = new StringReader(sw.ToString());
         //   htw.Parse(sr);
         //   doc.Close();
         //   Response.Write(doc);
         //   Response.End();
        }
      public ActionResult loginadmin(string username, string password)
        {
            admin ad = new admin();
            ad = (from obj in mydb.admins
                  where obj.username == username && obj.passe == password
                  select obj).FirstOrDefault();
            if (ad != null)
                return RedirectToAction("employe");
            else
                return View();

        }
        public ActionResult employe()
        {

            List<employer> listemploye = new List<employer>();
            listemploye = (from a in mydb.employers select a).ToList();

            notif();
            return View(listemploye);
        }

        [HttpGet]
        public ActionResult modifieremploye(int id)
        {
            //employer modifieremp = new employer();
            //modifieremp = (from obj in mydb.employers
            //               where obj.noemployer == id
            //               select obj).FirstOrDefault();
            ////mydb.employers.update(modifieremp);
            ///


            return View(mydb.employers.Find(id));
        }
        [HttpPost]
        public ActionResult modifieremploye([Bind(Include = "noemployer,nom,prenom,datenaissance,grade,telephone,email,username,motdepasse")] employer employe)
        {

            if (ModelState.IsValid)
            {

                mydb.Entry(employe).State = EntityState.Modified;
                mydb.SaveChanges();
                return RedirectToAction("employe");
            }
            return View(employe);

        }
        public ActionResult ajouteremploye(employer employe)
        {

            //employer addemploye = new employer();
            //addemploye.nom = nom;
            //addemploye.prenom = prenom;
            //addemploye.datenaissance = datenaissance;
            //addemploye.grade = grade;
            //addemploye.telephone = telephone;
            //addemploye.username = username;
            //addemploye.motdepasse = password;
            mydb.employers.Add(employe);

            mydb.SaveChanges();

            return RedirectToAction("employe");


        }

        //
        public ActionResult conge()
        {
            List<conge> listesdemandes = new List<conge>();
            //var listconges= (from emp in mydb.employers
            // join con in mydb.conges on emp.noemployer equals con.noemployer
            // select new {emp.nom,emp.prenom,con.duree,con.date_debut,con.date_fin,con.etat }).ToList();    
            listesdemandes = (from obj in mydb.conges where (obj.etat == "en cours") select obj).ToList();
            notif();
            return View(listesdemandes);
        }
        [HttpPost]
        public ActionResult conge(string keywords)
        {
            ViewBag.keywords = keywords;
            return View(mydb.conges.Where(x => x.etat == "en cours" && (x.etat.Contains(keywords) || x.motif.Contains(keywords) || x.employer.prenom.Contains(keywords)
           || x.duree.Contains(keywords) || x.employer.nom.Contains(keywords))).ToList());
        }

        public ActionResult supprimeremploye(int id)
        {
            employer supprimeremp = new employer();
            supprimeremp = (from a in mydb.employers
                            where a.noemployer == id
                            select a).FirstOrDefault();


            mydb.employers.Remove(supprimeremp);
            mydb.SaveChanges();
            return RedirectToAction("employe");

        }

        public int notif()
        {
            ViewBag.motif = mydb.conges.Where(x => x.etat == "en cours").Count();
            return ViewBag.motif;
        }
        public ActionResult accepterconge(int id)
        {
            conge okconge = new conge();
            okconge = (from obj in mydb.conges
                       where obj.noconge == id
                       select obj).FirstOrDefault();
            okconge.etat = "accepter";
            mydb.SaveChanges();
            notif();


            //var mail = new MailMessage();
            //var logininfo = new NetworkCredential("abderrahmanechatit63@gmail.com", "Ab212133");
            //mail.From = new MailAddress("abderrahmanechatit63@gmail.com");

            //mail.To.Add(new MailAddress(okconge.employer.email));
            //mail.Subject = "demande conge";
            //mail.Body = "bonjour " + okconge.employer.nom + "  " + okconge.employer.prenom + "  felicitation votre demande a été  Accepté  le  " + DateTime.Now + " details : "
            //       + "durée " + okconge.duree + "date debut : " + okconge.date_debut + " date_fin : " + okconge.date_fin + " motif : " + okconge.etat + "  Merci a bientôt";
            //var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            //smtpClient.EnableSsl = true;
            //smtpClient.Credentials = logininfo;
            //smtpClient.Send(mail);

            ViewBag.accepter = "demande ccepter avec succe ";
            return RedirectToAction("conge");

        }

        public ActionResult refuserconge(int id)
        {
            conge noconge = new conge();
            noconge = (from obj in mydb.conges
                       where obj.noconge == id
                       select obj).FirstOrDefault();
            notif();
            noconge.etat = "refuser";
            mydb.SaveChanges();


            var mail = new MailMessage();
            var logininfo = new NetworkCredential("abderrahmanechatit63@gmail.com", "Ab212133");
            mail.From = new MailAddress("abderrahmanechatit63@gmail.com");

            mail.To.Add(new MailAddress(noconge.employer.email));
            mail.Subject = "demande conge";
            mail.Body = "bonjour " + noconge.employer.nom + "  " + noconge.employer.prenom + "   votre demande a été  refusé     le  " + DateTime.Now + "  details :   "
                   + "durée " + noconge.duree + "date debut : " + noconge.date_debut + " date_fin : " + noconge.date_fin + "   motif : " + noconge.etat + "   Merci a bientôt";
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = logininfo;
           // smtpClient.Send(mail);
            ViewBag.refuser = "demande refuser avec succe ";
            return RedirectToAction("conge");

        }



        [HttpGet]
        public ActionResult generatepdf(int id)
        {

            conge c = new conge();
            c = (from obj in mydb.conges
                 where obj.noconge == id
                 select obj).FirstOrDefault();
            return View(c);
        }
        public ActionResult congerefuser()
        {
            List<conge> demmandesrefuser = new List<conge>();

            demmandesrefuser = (from obj in mydb.conges
                                where obj.etat == "refuser"
                                select obj).ToList();
            notif();
            return View(demmandesrefuser);
        }
        public ActionResult congeaccepter()
        {
            List<conge> demandesaccepter = new List<conge>();
            demandesaccepter = (from obj in mydb.conges
                                where obj.etat == "accepter"
                                select obj).ToList();
            notif();
            return View(demandesaccepter);
        }
        public ActionResult annulerconge(int id)
        {
            conge annulerconge = new conge();
            annulerconge = (from obj in mydb.conges
                            where obj.noconge == id
                            select obj).FirstOrDefault();
            annulerconge.etat = "en cours";
            mydb.SaveChanges();
            notif();
            return RedirectToAction("congerefuser", annulerconge);
        }
        public ActionResult annulerconge2(int id)
        {
            conge annulerconge = new conge();
            annulerconge = (from obj in mydb.conges
                            where obj.noconge == id
                            select obj).FirstOrDefault();
            annulerconge.etat = "en cours";
            mydb.SaveChanges();
            notif();
            return RedirectToAction("congeaccepter", annulerconge);
        }

    }
}