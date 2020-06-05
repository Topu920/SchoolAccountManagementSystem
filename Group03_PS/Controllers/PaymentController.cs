﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Group03_PS.Models;
using Group03_PS.Data;
using Group03_PS.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Group03_PS.Controllers
{
    public class PaymentController : Controller
    {
        // GET: /<controller>/

        private readonly dataContext _context;
        public PaymentController(dataContext context)
        {
            _context = context;
        }

        public IActionResult Students(Int64 Id, StudentPayment studentPayment)
        {

            var data = _context.StudentPayment.ToList();

            var model = new List<StudentPayment>();
            foreach (var i in data)
            {
                if (Id != i.Id)
                {
                    continue;
                }
               
                var s = new StudentPayment();
                s.Id = i.Id;
                s.Date = i.Date;
                s.AdmissionFee = i.AdmissionFee;
                s.MonthName = i.MonthName;
                s.TuitionFee = i.TuitionFee;
                s.ReAdmission = i.ReAdmission;
                s.YearlyCharge = i.YearlyCharge;
                s.Examination = i.Examination;
                s.Lab = i.Lab;
                s.IdCard = i.IdCard;
                s.Fine = i.Fine;
                s.Other = i.Other;
                s.AdvancePayment = i.AdvancePayment;
                model.Add(s);
            }
            return View(model);
        }

        public IActionResult StudentPaymentAdd( Int64 Id, StudentPayment studentPayment)
        {
            studentPayment.Id = Id; 
            return View();
        }


        [HttpPost]
        public IActionResult StudentPaymentAdd(StudentPayment studentPayment)
        {
            Int64 Id = studentPayment.Id;
            _context.StudentPayment.Add(studentPayment);
            _context.SaveChanges();



            return RedirectToAction("Students",new{ id =Id });

        }

        public IActionResult StudentPaymentEdit(Int64 Id, DateTime Date)
        {
            var t = Date;

            var model = _context.StudentPayment.Where(u => u.Id == Id && u.Date == Date).FirstOrDefault();

            //var data = _context.StudentPayment.ToList();
            //if(data.Id==Id )
            return View(model);
        }

        [HttpPost]
        public IActionResult StudentPaymentEdit(StudentPayment i)
        {

            var s = _context.StudentPayment.Where(u => u.Id == i.Id && u.No==i.No).FirstOrDefault();

            s.Id = i.Id;
            s.Date = i.Date;
            s.AdmissionFee = i.AdmissionFee;
            s.MonthName = i.MonthName;
            s.TuitionFee = i.TuitionFee;
            s.ReAdmission = i.ReAdmission;
            s.YearlyCharge = i.YearlyCharge;
            s.Examination = i.Examination;
            s.Lab = i.Lab;
            s.IdCard = i.IdCard;
            s.Fine = i.Fine;
            s.Other = i.Other;
            s.AdvancePayment = i.AdvancePayment;
          //  _context.StudentPayment.Update(s);
            _context.SaveChanges();
            return RedirectToAction("Students", new { id = i.Id });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
