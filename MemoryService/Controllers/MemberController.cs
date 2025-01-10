using App.Domain.Core.Memory.Contract.AppServices;
using App.Domain.Core.Memory.Entities;
using App.Domain.Core.Memory.Entities.Enum;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MemoryService.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberAppService _memberAppService;

        public MemberController(IMemberAppService memberAppService)
        {
            _memberAppService = memberAppService;
        }

        public IActionResult Index()
        {
            var members = _memberAppService.GetAll();
            foreach (var member in members)
            {
                member.RegisterDateString = ConvertToPersianDate(member.RegisterDate);
                member.BirthDateString = ConvertToPersianDate(member.BirthDate);
            }
            return View(members);
        }

        public IActionResult Details(int id)
        {
            var member = _memberAppService.GetById(id);
            if (member == null)
            {
                TempData["Error"] = "عضو مورد نظر یافت نشد.";
                return RedirectToAction(nameof(Index));
            }

            member.RegisterDateString = ConvertToPersianDate(member.RegisterDate);
            member.BirthDateString = ConvertToPersianDate(member.BirthDate);

            return View(member);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Member member)
        {
            if (!IsValidMember(member))
            {
                return View(member);
            }

            member.PhoneNumber = null;
            member.RegisterDate = DateTime.Now;
            member.MembershipType = _memberAppService.GetMembershipTypeBasedOnScore(member.Score);
            _memberAppService.Add(member);

            TempData["Success"] = "عضو جدید با موفقیت اضافه شد.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var member = _memberAppService.GetById(id);
            if (member == null)
            {
                TempData["Error"] = "عضو مورد نظر یافت نشد.";
                return RedirectToAction(nameof(Index));
            }

            return View(member);
        }

        [HttpPost]
        public IActionResult Edit(Member member)
        {
            if (!IsValidMember(member))
            {
                return View(member);
            }

            member.MembershipType = _memberAppService.GetMembershipTypeBasedOnScore(member.Score);

            _memberAppService.Update(member);

            TempData["Success"] = "اطلاعات عضو با موفقیت به‌روزرسانی شد.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var member = _memberAppService.GetById(id);
            if (member == null)
            {
                TempData["Error"] = "عضو مورد نظر یافت نشد.";
                return RedirectToAction(nameof(Index));
            }

            _memberAppService.Delete(id);
            TempData["Success"] = "عضو با موفقیت حذف شد.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Search(string searchQuery, Gender? gender = null, MembershipType? membershipType = null, int? score = null)
        {
            var members = _memberAppService.GetAll();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                members = members.Where(m => m.FirstName.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)
                                               || m.LastName.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (gender.HasValue)
            {
                members = members.Where(m => m.Gender == gender.Value).ToList();
            }

            if (membershipType.HasValue)
            {
                members = members.Where(m => m.MembershipType == membershipType.Value).ToList();
            }

            if (score.HasValue)
            {
                members = members.Where(m => m.Score == score.Value).ToList();
            }

            if (!members.Any())
            {
                TempData["Error"] = "هیچ عضوی مطابق با جستجوی شما یافت نشد.";
                return RedirectToAction("Index");
            }
            TempData["Success"] = "جستجوی شما با موفقیت انجام شد.";

            ViewData["SearchQuery"] = searchQuery;
            ViewData["Gender"] = gender;
            ViewData["MembershipType"] = membershipType;
            ViewData["Score"] = score;

            return View("Index", members);
        }


        private string ConvertToPersianDate(DateTime date)
        {
            try
            {
                var persianCalendar = new System.Globalization.PersianCalendar();
                return $"{persianCalendar.GetYear(date)}/{persianCalendar.GetMonth(date):00}/{persianCalendar.GetDayOfMonth(date):00}";
            }
            catch (ArgumentOutOfRangeException)
            {
                return "تاریخ نامعتبر";
            }
        }

        private bool IsValidMember(Member member)
        {
            if (string.IsNullOrEmpty(member.FirstName) || string.IsNullOrEmpty(member.LastName))
            {
                TempData["Error"] = "نام و نام خانوادگی الزامی است.";
                return false;
            }

            if (string.IsNullOrEmpty(member.NationalCode) || member.NationalCode.Length != 10)
            {
                TempData["Error"] = "کد ملی باید دقیقاً 10 رقم باشد.";
                return false;
            }

            if (_memberAppService.IsNationalCodeDuplicate(member.NationalCode) && member.Id == 0)
            {
                TempData["Error"] = "کد ملی وارد شده قبلاً ثبت شده است.";
                return false;
            }

            if (member.BirthDate == DateTime.MinValue || member.BirthDate > DateTime.Now)
            {
                TempData["Error"] = "تاریخ تولد وارد شده معتبر نیست.";
                return false;
            }

            if (member.Score < 0)
            {
                TempData["Error"] = "امتیاز وارد شده معتبر نیست.";
                return false;
            }

            return true;
        }
    }
}
