using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GPLUSPLUS.Models.Bussiness
{
    public class AddressManagement
    {
        public static List<SelectListItem> LoadState()
        {
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "استان", Value = "" });
            li.Add(new SelectListItem { Text = "آذربایجان شرقی", Value = "آذربایجان شرقی" });
            li.Add(new SelectListItem { Text = "آذربایجان غربی", Value = "آذربایجان غربی" });
            li.Add(new SelectListItem { Text = "اردبیل", Value = "اردبیل" });
            li.Add(new SelectListItem { Text = "اصفهان", Value = "اصفهان" });
            li.Add(new SelectListItem { Text = "ایلام", Value = "ایلام" });
            li.Add(new SelectListItem { Text = "بوشهر", Value = "بوشهر" });
            li.Add(new SelectListItem { Text = "تهران", Value = "تهران" });
            li.Add(new SelectListItem { Text = "چهارمحال و بختیاری", Value = "چهارمحال و بختیاری" });
            li.Add(new SelectListItem { Text = "خراسان جنوبی", Value = "خراسان جنوبی" });
            li.Add(new SelectListItem { Text = "خراسان رضوی", Value = "خراسان رضوی" });
            li.Add(new SelectListItem { Text = "خراسان شمالی", Value = "خراسان شمالی" });
            li.Add(new SelectListItem { Text = "خوزستان", Value = "خوزستان" });
            li.Add(new SelectListItem { Text = "زنجان", Value = "زنجان" });
            li.Add(new SelectListItem { Text = "سمنان", Value = "سمنان" });
            li.Add(new SelectListItem { Text = "سیستان و بلوچستان", Value = "سیستان و بلوچستان" });
            li.Add(new SelectListItem { Text = "شيراز", Value = "شيراز" });
            li.Add(new SelectListItem { Text = "قزوین", Value = "قزوین" });
            li.Add(new SelectListItem { Text = "قم", Value = "قم" });
            li.Add(new SelectListItem { Text = "کردستان", Value = "کردستان" });
            li.Add(new SelectListItem { Text = "کرمان", Value = "کرمان" });
            li.Add(new SelectListItem { Text = "کرمانشاه", Value = "کرمانشاه" });
            li.Add(new SelectListItem { Text = "کهگیلویه و بویراحمد", Value = "کهگیلویه و بویراحمد" });
            li.Add(new SelectListItem { Text = "گلستان", Value = "گلستان" });
            li.Add(new SelectListItem { Text = "گیلان", Value = "گیلان" });
            li.Add(new SelectListItem { Text = "لرستان", Value = "لرستان" });
            li.Add(new SelectListItem { Text = "مازندران", Value = "مازندران" });
            li.Add(new SelectListItem { Text = "مرکزی", Value = "مرکزی" });
            li.Add(new SelectListItem { Text = "هرمزگان", Value = "هرمزگان" });
            li.Add(new SelectListItem { Text = "همدان", Value = "همدان" });
            li.Add(new SelectListItem { Text = "یزد", Value = "یزد" });

            return li;
        }

        public static List<SelectListItem> GetCity(string id)
        {
            List<SelectListItem> City = new List<SelectListItem>();
            switch (id)
            {
                case "":
                    City.Add(new SelectListItem() { Text = "شهرستان", Value = "" });
                    break;

                case "آذربایجان شرقی":

                    City.Add(new SelectListItem() { Text = "آذرشهر", Value = "آذرشهر" });
                    City.Add(new SelectListItem() { Text = "اسکو", Value = "اسکو" });
                    City.Add(new SelectListItem() { Text = "اهر", Value = "اهر" });
                    City.Add(new SelectListItem() { Text = "بستان آباد", Value = "بستان آباد" });
                    City.Add(new SelectListItem() { Text = "بناب", Value = "بناب" });
                    City.Add(new SelectListItem() { Text = "تبریز", Value = "تبریز" });
                    City.Add(new SelectListItem() { Text = "جلفا", Value = "جلفا" });
                    City.Add(new SelectListItem() { Text = "چاروایماق", Value = "چاروایماق" });
                    City.Add(new SelectListItem() { Text = "خداآفرین", Value = "خداآفرین" });
                    City.Add(new SelectListItem() { Text = "سراب", Value = "سراب" });
                    City.Add(new SelectListItem() { Text = "شبستر", Value = "شبستر" });
                    City.Add(new SelectListItem() { Text = "عجبشیر", Value = "عجبشیر" });
                    City.Add(new SelectListItem() { Text = "کلیبر", Value = "کلیبر" });
                    City.Add(new SelectListItem() { Text = "مرند", Value = "مرند" });
                    City.Add(new SelectListItem() { Text = "ملکان", Value = "ملکان" });
                    City.Add(new SelectListItem() { Text = "میانه", Value = "میانه" });
                    City.Add(new SelectListItem() { Text = "هریس", Value = "هریس" });
                    City.Add(new SelectListItem() { Text = "هشترود", Value = "هشترود" });
                    City.Add(new SelectListItem() { Text = "هوراند", Value = "هوراند" });
                    City.Add(new SelectListItem() { Text = "ورزقان", Value = "ورزقان" });
                    break;

                case "آذربایجان غربی":
                    City.Add(new SelectListItem() { Text = "ارومیه", Value = "ارومیه" });
                    City.Add(new SelectListItem() { Text = "اشنویه", Value = "اشنویه" });
                    City.Add(new SelectListItem() { Text = "بوکان", Value = "بوکان" });
                    City.Add(new SelectListItem() { Text = "پلدشت", Value = "پلدشت" });
                    City.Add(new SelectListItem() { Text = "پیرانشهر", Value = "پیرانشهر" });
                    City.Add(new SelectListItem() { Text = "تکاب", Value = "تکاب" });
                    City.Add(new SelectListItem() { Text = "چالدران", Value = "چالدران" });
                    City.Add(new SelectListItem() { Text = "چایپاره", Value = "چایپاره" });
                    City.Add(new SelectListItem() { Text = "خوی", Value = "خوی" });
                    City.Add(new SelectListItem() { Text = "سردشت", Value = "سردشت" });
                    City.Add(new SelectListItem() { Text = "سرو", Value = "سرو" });
                    City.Add(new SelectListItem() { Text = "سلماس", Value = "سلماس" });
                    City.Add(new SelectListItem() { Text = "شاهین‌دژ", Value = "شاهین‌دژ" });
                    City.Add(new SelectListItem() { Text = "شوط", Value = "شوط" });
                    City.Add(new SelectListItem() { Text = "ماکو", Value = "ماکو" });
                    City.Add(new SelectListItem() { Text = "مهاباد", Value = "مهاباد" });
                    City.Add(new SelectListItem() { Text = "میاندوآب", Value = "میاندوآب" });
                    City.Add(new SelectListItem() { Text = "نقده", Value = "نقده" });
                    break;
                case "اردبیل":
                    City.Add(new SelectListItem() { Text = "اردبیل", Value = "اردبیل" });
                    City.Add(new SelectListItem() { Text = "بیله‌سوار", Value = "بیله‌سوار" });
                    City.Add(new SelectListItem() { Text = "پارس‌آباد", Value = "پارس‌آباد" });
                    City.Add(new SelectListItem() { Text = "خلخال", Value = "خلخال" });
                    City.Add(new SelectListItem() { Text = "سرعین", Value = "سرعین" });
                    City.Add(new SelectListItem() { Text = "کوثر ", Value = "کوثر" });
                    City.Add(new SelectListItem() { Text = "گِرمی", Value = "گِرمی" });
                    City.Add(new SelectListItem() { Text = "مِشگین‌شهر", Value = "مِشگین‌شهر" });
                    City.Add(new SelectListItem() { Text = "نَمین", Value = "نَمین" });
                    City.Add(new SelectListItem() { Text = "نیر", Value = "نیر" });
                    break;
                case "اصفهان":
                    City.Add(new SelectListItem() { Text = "آران و بیدگل", Value = "آران و بیدگل" });
                    City.Add(new SelectListItem() { Text = "اردستان", Value = "اردستان" });
                    City.Add(new SelectListItem() { Text = "اصفهان ", Value = "اصفهان" });
                    City.Add(new SelectListItem() { Text = "برخوار", Value = "برخوار" });
                    City.Add(new SelectListItem() { Text = "بوئین و میاندشت", Value = "بوئین و میاندشت" });
                    City.Add(new SelectListItem() { Text = "تیران و کرون", Value = "تیران و کرون" });
                    City.Add(new SelectListItem() { Text = "چادگان", Value = "چادگان" });
                    City.Add(new SelectListItem() { Text = "خمینی‌شهر", Value = "خمینی‌شهر" });
                    City.Add(new SelectListItem() { Text = "خوانسار", Value = "خوانسار" });
                    City.Add(new SelectListItem() { Text = "خوروبیابانک", Value = "خوروبیابانک" });
                    City.Add(new SelectListItem() { Text = "دهاقان", Value = "دهاقان" });
                    City.Add(new SelectListItem() { Text = "سمیرم", Value = "سمیرم" });
                    City.Add(new SelectListItem() { Text = "شاهین شهر و میمه", Value = "شاهین شهر و میمه" });
                    City.Add(new SelectListItem() { Text = "شهرضا", Value = "شهرضا" });
                    City.Add(new SelectListItem() { Text = "فریدن", Value = "فریدن" });
                    City.Add(new SelectListItem() { Text = "فریدونشهر", Value = "فریدونشهر" });
                    City.Add(new SelectListItem() { Text = "فلاورجان", Value = "فلاورجان" });
                    City.Add(new SelectListItem() { Text = "کاشان", Value = "کاشان" });
                    City.Add(new SelectListItem() { Text = "گلپایگان", Value = "گلپایگان" });
                    City.Add(new SelectListItem() { Text = "لنجان", Value = "لنجان" });
                    City.Add(new SelectListItem() { Text = "مبارکه", Value = "مبارکه" });
                    City.Add(new SelectListItem() { Text = "نائین", Value = "نائین" });
                    City.Add(new SelectListItem() { Text = "نجف‌آباد", Value = "نجف‌آباد" });
                    City.Add(new SelectListItem() { Text = "نطنز", Value = "نطنز" });
                    break;
                case "البرز":
                    City.Add(new SelectListItem() { Text = "اشتهارد", Value = "اشتهارد" });
                    City.Add(new SelectListItem() { Text = "ساوجبلاغ", Value = "ساوجبلاغ" });
                    City.Add(new SelectListItem() { Text = "فردیس", Value = "فردیس" });
                    City.Add(new SelectListItem() { Text = "کرج", Value = "کرج" });
                    City.Add(new SelectListItem() { Text = "نظرآباد", Value = "نظرآباد" });
                    City.Add(new SelectListItem() { Text = "هشتگرد", Value = "هشتگرد" });

                    break;
                case "ایلام":
                    City.Add(new SelectListItem() { Text = "آبدانان", Value = "آبدانان" });
                    City.Add(new SelectListItem() { Text = "ایلام", Value = "ایلام" });
                    City.Add(new SelectListItem() { Text = "ایوان", Value = "ایوان" });
                    City.Add(new SelectListItem() { Text = "بدره", Value = "بدره" });
                    City.Add(new SelectListItem() { Text = "چرداول", Value = "چرداول" });
                    City.Add(new SelectListItem() { Text = "دره‌شهر", Value = "دره‌شهر" });
                    City.Add(new SelectListItem() { Text = "دهلران", Value = "دهلران" });
                    City.Add(new SelectListItem() { Text = "سرابله", Value = "سرابله" });
                    City.Add(new SelectListItem() { Text = "سیروان", Value = "سیروان" });
                    City.Add(new SelectListItem() { Text = "ملکشاهی", Value = "ملکشاهی" });
                    City.Add(new SelectListItem() { Text = "مهران", Value = "مهران" });
                    break;
                case "بوشهر":
                           City.Add(new SelectListItem() { Text = "بوشهر", Value = "بوشهر" });
                    City.Add(new SelectListItem() { Text = "بوشهر", Value = "بوشهر" });
                    City.Add(new SelectListItem() { Text = "تنگستان ", Value = "تنگستان" });
                    City.Add(new SelectListItem() { Text = "جم", Value = "جم" });
                    City.Add(new SelectListItem() { Text = "خورموج", Value = "خورموج" });
                    City.Add(new SelectListItem() { Text = "دشتستان", Value = "دشتستان" });
                    City.Add(new SelectListItem() { Text = "دشتی", Value = "دشتی" });
                    City.Add(new SelectListItem() { Text = "دیر", Value = "دیر" });
                    City.Add(new SelectListItem() { Text = "دیلم", Value = "دیلم" });
                    City.Add(new SelectListItem() { Text = "عسلویه", Value = "عسلویه" });
                    City.Add(new SelectListItem() { Text = "کنگان ", Value = "کنگان" });
                    City.Add(new SelectListItem() { Text = "گناوه", Value = "گناوه" });
                    break;
                case "تهران":
                    City.Add(new SelectListItem() { Text = "اسلام‌شهر", Value = "اسلام‌شهر" });
                    City.Add(new SelectListItem() { Text = "بهارستان", Value = "بهارستان" });
                    City.Add(new SelectListItem() { Text = "پاکدشت", Value = "پاکدشت" });
                    City.Add(new SelectListItem() { Text = "پردیس", Value = "پردیس" });
                    City.Add(new SelectListItem() { Text = "پیشوا", Value = "پیشوا" });
                    City.Add(new SelectListItem() { Text = "تهران", Value = "تهران" });
                    City.Add(new SelectListItem() { Text = "دماوند", Value = "دماوند" });
                    City.Add(new SelectListItem() { Text = "رباط‌کریم", Value = "رباط‌کریم" });
                    City.Add(new SelectListItem() { Text = "ری", Value = "ری" });
                    City.Add(new SelectListItem() { Text = "شمیرانات", Value = "شمیرانات" });
                    City.Add(new SelectListItem() { Text = "شهریار", Value = "شهریار" });
                    City.Add(new SelectListItem() { Text = "فیروزکوه", Value = "فیروزکوه" });
                    City.Add(new SelectListItem() { Text = "قدس", Value = "قدس" });
                    City.Add(new SelectListItem() { Text = "قرچک", Value = "قرچک" });
                    City.Add(new SelectListItem() { Text = "ملارد", Value = "ملارد" });
                    City.Add(new SelectListItem() { Text = "ورامین", Value = "ورامین" });
                    break;
                case "چهارمحال و بختیاری":
                    City.Add(new SelectListItem() { Text = "اردل", Value = "اردل" });
                    City.Add(new SelectListItem() { Text = "بروجن", Value = "بروجن" });
                    City.Add(new SelectListItem() { Text = "بن", Value = "بن" });
                    City.Add(new SelectListItem() { Text = "جونقان", Value = "جونقان" });
                    City.Add(new SelectListItem() { Text = "سامان", Value = "سامان" });
                    City.Add(new SelectListItem() { Text = "سورشجان", Value = "سورشجان" });
                    City.Add(new SelectListItem() { Text = "شهرکرد", Value = "شهرکرد" });
                    City.Add(new SelectListItem() { Text = "فارسان", Value = "فارسان" });
                    City.Add(new SelectListItem() { Text = "فرخشهر", Value = "فرخشهر" });
                    City.Add(new SelectListItem() { Text = "کوهرنگ", Value = "کوهرنگ" });
                    City.Add(new SelectListItem() { Text = "کیار", Value = "کیار" });
                    City.Add(new SelectListItem() { Text = "لردگان", Value = "لردگان" });
                    break;
                case "خراسان جنوبی":
                    City.Add(new SelectListItem() { Text = "بشرویه", Value = "بشرویه" });
                    City.Add(new SelectListItem() { Text = "بیرجند", Value = "بیرجند" });
                    City.Add(new SelectListItem() { Text = "خوسف", Value = "خوسف" });
                    City.Add(new SelectListItem() { Text = "درمیان", Value = "درمیان" });
                    City.Add(new SelectListItem() { Text = "زیرکوه", Value = "زیرکوه" });
                    City.Add(new SelectListItem() { Text = "سرایان", Value = "سرایان" });
                    City.Add(new SelectListItem() { Text = "سربیشه", Value = "سربیشه" });
                    City.Add(new SelectListItem() { Text = "طبس", Value = "طبس" });
                    City.Add(new SelectListItem() { Text = "فردوس", Value = "فردوس" });
                    City.Add(new SelectListItem() { Text = "قائنات", Value = "قائنات" });
                    City.Add(new SelectListItem() { Text = "نهبندان", Value = "نهبندان" });
                    break;
                case "خراسان رضوی":
                    City.Add(new SelectListItem() { Text = "باخرز", Value = "باخرز" });
                    City.Add(new SelectListItem() { Text = "بجستان", Value = "بجستان" });
                    City.Add(new SelectListItem() { Text = "بردسکن", Value = "بردسکن" });
                    City.Add(new SelectListItem() { Text = "تایباد", Value = "تایباد" });
                    City.Add(new SelectListItem() { Text = "تخت جلگه", Value = "تخت جلگه" });
                    City.Add(new SelectListItem() { Text = "تربت جام", Value = "تربت جام" });
                    City.Add(new SelectListItem() { Text = "تربت حیدریه", Value = "تربت حیدریه" });
                    City.Add(new SelectListItem() { Text = "جغتای", Value = "جغتای" });
                    City.Add(new SelectListItem() { Text = "جوین", Value = "جوین" });
                    City.Add(new SelectListItem() { Text = "چناران", Value = "چناران" });
                    City.Add(new SelectListItem() { Text = "خلیل‌آباد", Value = "خلیل‌آباد" });
                    City.Add(new SelectListItem() { Text = "خواف", Value = "خواف" });
                    City.Add(new SelectListItem() { Text = "خوشاب", Value = "خوشاب" });
                    City.Add(new SelectListItem() { Text = "داورزن", Value = "داورزن" });
                    City.Add(new SelectListItem() { Text = "درگز", Value = "درگز" });
                    City.Add(new SelectListItem() { Text = "رشتخوار", Value = "رشتخوار" });
                    City.Add(new SelectListItem() { Text = "زاوه", Value = "زاوه" });
                    City.Add(new SelectListItem() { Text = "سبزوار ", Value = "سبزوار" });
                    City.Add(new SelectListItem() { Text = "سرخس", Value = "سرخس" });
                    City.Add(new SelectListItem() { Text = "طرقبه شاندیز", Value = "طرقبه شاندیز" });
                    City.Add(new SelectListItem() { Text = "فریمان", Value = "فریمان" });
                    City.Add(new SelectListItem() { Text = "قوچان", Value = "قوچان" });
                    City.Add(new SelectListItem() { Text = "کاشمر", Value = "کاشمر" });
                    City.Add(new SelectListItem() { Text = "کلات", Value = "کلات" });
                    City.Add(new SelectListItem() { Text = "گناباد", Value = "گناباد" });
                    City.Add(new SelectListItem() { Text = "مشهد", Value = "مشهد" });
                    City.Add(new SelectListItem() { Text = "مه ولات", Value = "مه ولات" });
                    City.Add(new SelectListItem() { Text = "نیشابور", Value = "نیشابور" });
                    break;
                case "خراسان شمالی":
                    City.Add(new SelectListItem() { Text = "اسفراین", Value = "اسفراین" });
                    City.Add(new SelectListItem() { Text = "بجنورد ", Value = "بجنورد" });
                    City.Add(new SelectListItem() { Text = "جاجرم", Value = "جاجرم" });
                    City.Add(new SelectListItem() { Text = "شیروان", Value = "شیروان" });
                    City.Add(new SelectListItem() { Text = "فاروج", Value = "فاروج" });
                    City.Add(new SelectListItem() { Text = "گرمه", Value = "گرمه" });
                    City.Add(new SelectListItem() { Text = "مانه و سملقان", Value = "مانه و سملقان" });
                    break;
                case "خوزستان":
                    City.Add(new SelectListItem() { Text = "آبادان", Value = "آبادان" });
                    City.Add(new SelectListItem() { Text = "امیدیه", Value = "امیدیه" });
                    City.Add(new SelectListItem() { Text = "اندیکا", Value = "اندیکا" });
                    City.Add(new SelectListItem() { Text = "اندیمشک", Value = "اندیمشک" });
                    City.Add(new SelectListItem() { Text = "اهواز", Value = "اهواز" });
                    City.Add(new SelectListItem() { Text = "ایذه", Value = "ایذه" });
                    City.Add(new SelectListItem() { Text = "باغ‌ملک", Value = "باغ‌ملک" });
                    City.Add(new SelectListItem() { Text = "باوی", Value = "باوی" });
                    City.Add(new SelectListItem() { Text = "بهبهان", Value = "بهبهان" });
                    City.Add(new SelectListItem() { Text = "خرمشهر", Value = "خرمشهر" });
                    City.Add(new SelectListItem() { Text = "دزفول", Value = "دزفول" });
                    City.Add(new SelectListItem() { Text = "دشت آزادگان", Value = "دشت آزادگان" });
                    City.Add(new SelectListItem() { Text = "رامشیر", Value = "رامشیر" });
                    City.Add(new SelectListItem() { Text = "رامهرمز", Value = "رامهرمز" });
                    City.Add(new SelectListItem() { Text = "شادگان", Value = "شادگان" });
                    City.Add(new SelectListItem() { Text = "شوش", Value = "شوش" });
                    City.Add(new SelectListItem() { Text = "شوشتر", Value = "شوشتر" });
                    City.Add(new SelectListItem() { Text = "کارون", Value = "کارون" });
                    City.Add(new SelectListItem() { Text = "گتوند", Value = "گتوند" });
                    City.Add(new SelectListItem() { Text = "لالی", Value = "لالی" });
                    City.Add(new SelectListItem() { Text = "ماهشهر", Value = "ماهشهر" });
                    City.Add(new SelectListItem() { Text = "مسجد سلیمان", Value = "مسجد سلیمان" });
                    City.Add(new SelectListItem() { Text = "هفتکل", Value = "هفتکل" });
                    City.Add(new SelectListItem() { Text = "هندیجان", Value = "هندیجان" });
                    City.Add(new SelectListItem() { Text = "هویزه", Value = "هویزه" });
                    break;
                case "زنجان":
                    City.Add(new SelectListItem() { Text = "ابهر", Value = "ابهر" });
                    City.Add(new SelectListItem() { Text = "ایجرود", Value = "ایجرود" });
                    City.Add(new SelectListItem() { Text = "خدابنده", Value = "خدابنده" });
                    City.Add(new SelectListItem() { Text = "خرمدره", Value = "خرمدره" });
                    City.Add(new SelectListItem() { Text = "زنجان ", Value = "زنجان" });
                    City.Add(new SelectListItem() { Text = "سلطانیه ", Value = "سلطانیه" });
                    City.Add(new SelectListItem() { Text = "طارم", Value = "طارم" });
                    City.Add(new SelectListItem() { Text = "ماه‌نشان", Value = "ماه‌نشان" });
                    break;
                case "سمنان":
                    City.Add(new SelectListItem() { Text = "آرادان", Value = "آرادان" });
                    City.Add(new SelectListItem() { Text = "دامغان", Value = "دامغان" });
                    City.Add(new SelectListItem() { Text = "سرخه", Value = "سرخه" });
                    City.Add(new SelectListItem() { Text = "سمنان", Value = "سمنان" });
                    City.Add(new SelectListItem() { Text = "شاهرود", Value = "شاهرود" });
                    City.Add(new SelectListItem() { Text = "گرمسار", Value = "گرمسار" });
                    City.Add(new SelectListItem() { Text = "مهدی‌شهر", Value = "مهدی‌شهر" });
                    City.Add(new SelectListItem() { Text = "میامی", Value = "میامی" });
                    break;
                case "سیستان و بلوچستان":
                    City.Add(new SelectListItem() { Text = "ایرانشهر ", Value = "ایرانشهر" });
                    City.Add(new SelectListItem() { Text = "چابهار", Value = "چابهار" });
                    City.Add(new SelectListItem() { Text = "خاش", Value = "خاش" });
                    City.Add(new SelectListItem() { Text = "دلگان", Value = "دلگان" });
                    City.Add(new SelectListItem() { Text = "زابل", Value = "زابل" });
                    City.Add(new SelectListItem() { Text = "زاهدان", Value = "زاهدان" });
                    City.Add(new SelectListItem() { Text = "زهک", Value = "زهک" });
                    City.Add(new SelectListItem() { Text = "سراوان", Value = "سراوان" });
                    City.Add(new SelectListItem() { Text = "سرباز", Value = "سرباز" });
                    City.Add(new SelectListItem() { Text = "سیب و سوران", Value = "سیب و سوران" });
                    City.Add(new SelectListItem() { Text = "فنوج", Value = "فنوج" });
                    City.Add(new SelectListItem() { Text = "قصرقند", Value = "قصرقند" });
                    City.Add(new SelectListItem() { Text = "کنارک", Value = "کنارک" });
                    City.Add(new SelectListItem() { Text = "مهرستان", Value = "مهرستان" });
                    City.Add(new SelectListItem() { Text = "میرجاوه", Value = "میرجاوه" });
                    City.Add(new SelectListItem() { Text = "نیک‌شهر", Value = "نیک‌شهر" });
                    City.Add(new SelectListItem() { Text = "نیمروز", Value = "نیمروز" });
                    City.Add(new SelectListItem() { Text = "هامون", Value = "هامون" });
                    City.Add(new SelectListItem() { Text = "هیرمند", Value = "هیرمند" });
                    break;
                case "شيراز":
                    City.Add(new SelectListItem() { Text = "آباده ", Value = "آباده" });
                    City.Add(new SelectListItem() { Text = "ارسنجان", Value = "ارسنجان" });
                    City.Add(new SelectListItem() { Text = "استهبان ", Value = "استهبان" });
                    City.Add(new SelectListItem() { Text = "اقلید", Value = "اقلید" });
                    City.Add(new SelectListItem() { Text = "بوانات", Value = "بوانات" });
                    City.Add(new SelectListItem() { Text = "پاسارگاد", Value = "پاسارگاد" });
                    City.Add(new SelectListItem() { Text = "جهرم", Value = "جهرم" });
                    City.Add(new SelectListItem() { Text = "خرامه", Value = "خرامه" });
                    City.Add(new SelectListItem() { Text = "خرم‌بید", Value = "خرم‌بید" });
                    City.Add(new SelectListItem() { Text = "خنج", Value = "خنج" });
                    City.Add(new SelectListItem() { Text = "داراب", Value = "داراب" });
                    City.Add(new SelectListItem() { Text = "زرین‌دشت", Value = "زرین‌دشت" });
                    City.Add(new SelectListItem() { Text = "سپیدان", Value = "سپیدان" });
                    City.Add(new SelectListItem() { Text = "سروستان", Value = "سروستان" });
                    City.Add(new SelectListItem() { Text = "شیراز", Value = "شیراز" });
                    City.Add(new SelectListItem() { Text = "فراشبند", Value = "فراشبند" });
                    City.Add(new SelectListItem() { Text = "فسا", Value = "فسا" });
                    City.Add(new SelectListItem() { Text = "فیروزآباد", Value = "فیروزآباد" });
                    City.Add(new SelectListItem() { Text = "قیر و کارزین", Value = "قیر و کارزین" });
                    City.Add(new SelectListItem() { Text = "کازرون", Value = "کازرون" });
                    City.Add(new SelectListItem() { Text = "لارستان", Value = "لارستان" });
                    City.Add(new SelectListItem() { Text = "لامِرد", Value = "لامِرد" });
                    City.Add(new SelectListItem() { Text = "مرودشت ", Value = "مرودشت" });
                    City.Add(new SelectListItem() { Text = "ممسنی", Value = "ممسنی" });
                    City.Add(new SelectListItem() { Text = "مهر", Value = "مهر" });
                    City.Add(new SelectListItem() { Text = "نی‌ریز", Value = "نی‌ریز" });
                    break;
                case "قزوین":
                    City.Add(new SelectListItem() { Text = "آبیک", Value = "آبیک" });
                    City.Add(new SelectListItem() { Text = "البرز", Value = "البرز" });
                    City.Add(new SelectListItem() { Text = "آوج", Value = "آوج" });
                    City.Add(new SelectListItem() { Text = "بوئین‌زهرا", Value = "بوئین‌زهرا" });
                    City.Add(new SelectListItem() { Text = "تاکستان", Value = "تاکستان" });
                    City.Add(new SelectListItem() { Text = "شریفیه", Value = "شریفیه" });
                    City.Add(new SelectListItem() { Text = "قزوین", Value = "قزوین" });
                    break;
                case "قم":
                    City.Add(new SelectListItem() { Text = "قم", Value = "قم" });
                    City.Add(new SelectListItem() { Text = "کهک", Value = "کهک" });
                    break;
                case "کردستان":
                    City.Add(new SelectListItem() { Text = "بانه ", Value = "بانه" });
                    City.Add(new SelectListItem() { Text = "بیجار", Value = "بیجار" });
                    City.Add(new SelectListItem() { Text = "دهگلان", Value = "دهگلان" });
                    City.Add(new SelectListItem() { Text = "دیواندره", Value = "دیواندره" });
                    City.Add(new SelectListItem() { Text = "سروآباد", Value = "سروآباد" });
                    City.Add(new SelectListItem() { Text = "سقز ", Value = "سقز" });
                    City.Add(new SelectListItem() { Text = "سنندج", Value = "سنندج" });
                    City.Add(new SelectListItem() { Text = "قروه", Value = "قروه" });
                    City.Add(new SelectListItem() { Text = "کامیاران", Value = "کامیاران" });
                    City.Add(new SelectListItem() { Text = "مریوان", Value = "مریوان" });
                    break;
                case "کرمان":
                    City.Add(new SelectListItem() { Text = "ارزوئیه", Value = "ارزوئیه" });
                    City.Add(new SelectListItem() { Text = "انار", Value = "انار" });
                    City.Add(new SelectListItem() { Text = "بافت", Value = "بافت" });
                    City.Add(new SelectListItem() { Text = "بردسیر", Value = "بردسیر" });
                    City.Add(new SelectListItem() { Text = "بم", Value = "بم" });
                    City.Add(new SelectListItem() { Text = "جیرفت", Value = "جیرفت" });
                    City.Add(new SelectListItem() { Text = "رابر", Value = "رابر" });
                    City.Add(new SelectListItem() { Text = "راور", Value = "راور" });
                    City.Add(new SelectListItem() { Text = "رفسنجان", Value = "رفسنجان" });
                    City.Add(new SelectListItem() { Text = "رودبار", Value = "رودبار" });
                    City.Add(new SelectListItem() { Text = "ریگان", Value = "ریگان" });
                    City.Add(new SelectListItem() { Text = "زرند", Value = "زرند" });
                    City.Add(new SelectListItem() { Text = "سیرجان", Value = "سیرجان" });
                    City.Add(new SelectListItem() { Text = "شهر بابک", Value = "شهر بابک" });
                    City.Add(new SelectListItem() { Text = "عنبرآباد", Value = "عنبرآباد" });
                    City.Add(new SelectListItem() { Text = "فاریاب", Value = "فاریاب" });
                    City.Add(new SelectListItem() { Text = "فهرج", Value = "فهرج" });
                    City.Add(new SelectListItem() { Text = "قلعه گنج", Value = "قلعه گنج" });
                    City.Add(new SelectListItem() { Text = "کرمان", Value = "کرمان" });
                    City.Add(new SelectListItem() { Text = "کوهبنان", Value = "کوهبنان" });
                    City.Add(new SelectListItem() { Text = "کهنوج", Value = "کهنوج" });
                    City.Add(new SelectListItem() { Text = "منوجان", Value = "منوجان" });
                    City.Add(new SelectListItem() { Text = "نرماشیر", Value = "نرماشیر" });
                    break;
                case "کرمانشاه":
                    City.Add(new SelectListItem() { Text = "اسلام آباد غرب", Value = "اسلام آباد غرب" });
                    City.Add(new SelectListItem() { Text = "پاوه", Value = "پاوه" });
                    City.Add(new SelectListItem() { Text = "ثلاث باباجانی", Value = "ثلاث باباجانی" });
                    City.Add(new SelectListItem() { Text = "جوانرود", Value = "جوانرود" });
                    City.Add(new SelectListItem() { Text = "دالاهو", Value = "دالاهو" });
                    City.Add(new SelectListItem() { Text = "روانسر", Value = "روانسر" });
                    City.Add(new SelectListItem() { Text = "سرپل ذهاب", Value = "سرپل ذهاب" });
                    City.Add(new SelectListItem() { Text = "سنقر", Value = "سنقر" });
                    City.Add(new SelectListItem() { Text = "صحنه", Value = "صحنه" });
                    City.Add(new SelectListItem() { Text = "قصر شیرین", Value = "قصر شیرین" });
                    City.Add(new SelectListItem() { Text = "کرمانشاه ", Value = "کرمانشاه" });
                    City.Add(new SelectListItem() { Text = "کنگاور", Value = "کنگاور" });
                    City.Add(new SelectListItem() { Text = "گیلان غرب", Value = "گیلان غرب" });
                    City.Add(new SelectListItem() { Text = "هرسین", Value = "هرسین" });
                    break;
                case "کهگیلویه و بویراحمد":
                    City.Add(new SelectListItem() { Text = "باشت", Value = "باشت" });
                    City.Add(new SelectListItem() { Text = "بهمئی", Value = "بهمئی" });
                    City.Add(new SelectListItem() { Text = "بویراحمد", Value = "بویراحمد" });
                    City.Add(new SelectListItem() { Text = "چرام", Value = "چرام" });
                    City.Add(new SelectListItem() { Text = "دنا", Value = "دنا" });
                    City.Add(new SelectListItem() { Text = "کهگیلویه", Value = "کهگیلویه" });
                    City.Add(new SelectListItem() { Text = "گچساران", Value = "گچساران" });
                    City.Add(new SelectListItem() { Text = "لنده", Value = "لنده" });
                    break;
                case "گلستان":
                    City.Add(new SelectListItem() { Text = "آزادشهر", Value = "آزادشهر" });
                    City.Add(new SelectListItem() { Text = "آق‌قلا", Value = "آق‌قلا" });
                    City.Add(new SelectListItem() { Text = "بندر گز", Value = "بندر گز" });
                    City.Add(new SelectListItem() { Text = "ترکمن ", Value = "ترکمن" });
                    City.Add(new SelectListItem() { Text = "رامیان", Value = "رامیان" });
                    City.Add(new SelectListItem() { Text = "علی‌آباد", Value = "علی‌آباد" });
                    City.Add(new SelectListItem() { Text = "کردکوی", Value = "کردکوی" });
                    City.Add(new SelectListItem() { Text = "کلاله", Value = "کلاله" });
                    City.Add(new SelectListItem() { Text = "گالیکش", Value = "گالیکش" });
                    City.Add(new SelectListItem() { Text = "گرگان ", Value = "گرگان" });
                    City.Add(new SelectListItem() { Text = "گمیشان ", Value = "گمیشان" });
                    City.Add(new SelectListItem() { Text = "گنبد کاووس", Value = "گنبد کاووس" });
                    City.Add(new SelectListItem() { Text = "مراوه‌تپه", Value = "مراوه‌تپه" });
                    City.Add(new SelectListItem() { Text = "مینودشت", Value = "مینودشت" });
                    break;
                case "گیلان":
                    City.Add(new SelectListItem() { Text = "آستارا", Value = "آستارا" });
                    City.Add(new SelectListItem() { Text = "آستانه اشرفیه", Value = "آستانه اشرفیه" });
                    City.Add(new SelectListItem() { Text = "اَملَش", Value = "اَملَش" });
                    City.Add(new SelectListItem() { Text = "بندر انزلی", Value = "بندر انزلی" });
                    City.Add(new SelectListItem() { Text = "رشت", Value = "رشت" });
                    City.Add(new SelectListItem() { Text = "رضوانشهر", Value = "رضوانشهر" });
                    City.Add(new SelectListItem() { Text = "رودبار", Value = "رودبار" });
                    City.Add(new SelectListItem() { Text = "رودسر", Value = "رودسر" });
                    City.Add(new SelectListItem() { Text = "سیاهکل", Value = "سیاهکل" });
                    City.Add(new SelectListItem() { Text = "شَفت", Value = "شَفت" });
                    City.Add(new SelectListItem() { Text = "صومعه‌سرا", Value = "صومعه‌سرا" });
                    City.Add(new SelectListItem() { Text = "طوالش", Value = "طوالش" });
                    City.Add(new SelectListItem() { Text = "فومَن", Value = "فومَن" });
                    City.Add(new SelectListItem() { Text = "لاهیجان ", Value = "لاهیجان" });
                    City.Add(new SelectListItem() { Text = "لنگرود", Value = "لنگرود" });
                    City.Add(new SelectListItem() { Text = "ماسال", Value = "ماسال" });
                    City.Add(new SelectListItem() { Text = "منجیل", Value = "منجیل" });
                    break;
                case "لرستان":
                    City.Add(new SelectListItem() { Text = "ازنا", Value = "ازنا" });
                    City.Add(new SelectListItem() { Text = "اليگودرز", Value = "اليگودرز" });
                    City.Add(new SelectListItem() { Text = "بروجرد", Value = "بروجرد" });
                    City.Add(new SelectListItem() { Text = "پل‌دختر", Value = "پل‌دختر" });
                    City.Add(new SelectListItem() { Text = "چگنی", Value = "چگنی" });
                    City.Add(new SelectListItem() { Text = "خرم‌آباد", Value = "خرم‌آباد" });
                    City.Add(new SelectListItem() { Text = "دلفان ", Value = "دلفان" });
                    City.Add(new SelectListItem() { Text = "دورود", Value = "دورود" });
                    City.Add(new SelectListItem() { Text = "رومشگان", Value = "رومشگان" });
                    City.Add(new SelectListItem() { Text = "سلسله", Value = "سلسله" });
                    City.Add(new SelectListItem() { Text = "کوهدشت", Value = "کوهدشت" });
                    City.Add(new SelectListItem() { Text = "نورآباد", Value = "نورآباد" });
                    break;
                case "مازندران":
                    City.Add(new SelectListItem() { Text = "آمل", Value = "آمل" });
                    City.Add(new SelectListItem() { Text = "بابل", Value = "بابل" });
                    City.Add(new SelectListItem() { Text = "بابلسر", Value = "بابلسر" });
                    City.Add(new SelectListItem() { Text = "بهشهر", Value = "بهشهر" });
                    City.Add(new SelectListItem() { Text = "تنکابن", Value = "تنکابن" });
                    City.Add(new SelectListItem() { Text = "جويبار", Value = "جويبار" });
                    City.Add(new SelectListItem() { Text = "چالوس", Value = "چالوس" });
                    City.Add(new SelectListItem() { Text = "رامسر", Value = "رامسر" });
                    City.Add(new SelectListItem() { Text = "ساري", Value = "ساري" });
                    City.Add(new SelectListItem() { Text = "سوادکوه", Value = "سوادکوه" });
                    City.Add(new SelectListItem() { Text = "سوادکوه شمالی", Value = "سوادکوه شمالی" });
                    City.Add(new SelectListItem() { Text = "سیمرغ", Value = "سیمرغ" });
                    City.Add(new SelectListItem() { Text = "عباس آباد", Value = "عباس آباد" });
                    City.Add(new SelectListItem() { Text = "فریدونکنار", Value = "فریدونکنار" });
                    City.Add(new SelectListItem() { Text = "قائم‌شهر", Value = "قائم‌شهر" });
                    City.Add(new SelectListItem() { Text = "کلاردشت", Value = "کلاردشت" });
                    City.Add(new SelectListItem() { Text = "گلوگاه", Value = "گلوگاه" });
                    City.Add(new SelectListItem() { Text = "محمودآباد", Value = "محمودآباد" });
                    City.Add(new SelectListItem() { Text = "میاندرود", Value = "میاندرود" });
                    City.Add(new SelectListItem() { Text = "نکا", Value = "نکا" });
                    City.Add(new SelectListItem() { Text = "نور", Value = "نور" });
                    City.Add(new SelectListItem() { Text = "نوشهر", Value = "نوشهر" });
                    break;
                case "مرکزی":
                    City.Add(new SelectListItem() { Text = "آشتيان", Value = "آشتيان" });
                    City.Add(new SelectListItem() { Text = "اراک", Value = "اراک" });
                    City.Add(new SelectListItem() { Text = "تفرش", Value = "تفرش" });
                    City.Add(new SelectListItem() { Text = "خمين", Value = "خمين" });
                    City.Add(new SelectListItem() { Text = "خنداب", Value = "خنداب" });
                    City.Add(new SelectListItem() { Text = "دليجان ", Value = "دليجان" });
                    City.Add(new SelectListItem() { Text = "زرنديه", Value = "زرنديه" });
                    City.Add(new SelectListItem() { Text = "ساوه", Value = "ساوه" });
                    City.Add(new SelectListItem() { Text = "شازند", Value = "شازند" });
                    City.Add(new SelectListItem() { Text = "فراهان", Value = "فراهان" });
                    City.Add(new SelectListItem() { Text = "کميجان", Value = "کميجان" });
                    City.Add(new SelectListItem() { Text = "محلات", Value = "محلات" });
                    break;
                case "هرمزگان":
                    City.Add(new SelectListItem() { Text = "ابوموسي", Value = "ابوموسي" });
                    City.Add(new SelectListItem() { Text = "بستک", Value = "بستک" });
                    City.Add(new SelectListItem() { Text = "بشاگرد", Value = "بشاگرد" });
                    City.Add(new SelectListItem() { Text = "بندر عباس", Value = "بندر عباس" });
                    City.Add(new SelectListItem() { Text = "بندر لنگه", Value = "بندر لنگه" });
                    City.Add(new SelectListItem() { Text = "پارسیان", Value = "پارسیان" });
                    City.Add(new SelectListItem() { Text = "جاسک", Value = "جاسک" });
                    City.Add(new SelectListItem() { Text = "حاجي‌آباد", Value = "حاجي‌آباد" });
                    City.Add(new SelectListItem() { Text = "خمير ", Value = "خمير" });
                    City.Add(new SelectListItem() { Text = "رودان", Value = "رودان" });
                    City.Add(new SelectListItem() { Text = "سیریک", Value = "سیریک" });
                    City.Add(new SelectListItem() { Text = "قشم", Value = "قشم" });
                    City.Add(new SelectListItem() { Text = "ميناب", Value = "ميناب" });
                    break;
                case "همدان":
                    City.Add(new SelectListItem() { Text = "اسدآباد", Value = "اسدآباد" });
                    City.Add(new SelectListItem() { Text = "بهار", Value = "بهار" });
                    City.Add(new SelectListItem() { Text = "تويسرکان", Value = "تويسرکان" });
                    City.Add(new SelectListItem() { Text = "رزن ", Value = "رزن" });
                    City.Add(new SelectListItem() { Text = "فامنین ", Value = "فامنین" });
                    City.Add(new SelectListItem() { Text = "کبودرآهنگ", Value = "کبودرآهنگ" });
                    City.Add(new SelectListItem() { Text = "ملایر", Value = "ملایر" });
                    City.Add(new SelectListItem() { Text = "نهاوند", Value = "نهاوند" });
                    City.Add(new SelectListItem() { Text = "همدان", Value = "همدان" });
                    break;
                case "یزد":
                    City.Add(new SelectListItem() { Text = "ابرکوه", Value = "ابرکوه" });
                    City.Add(new SelectListItem() { Text = "اردکان", Value = "اردکان" });
                    City.Add(new SelectListItem() { Text = "اشکذر", Value = "اشکذر" });
                    City.Add(new SelectListItem() { Text = "بافق ", Value = "بافق" });
                    City.Add(new SelectListItem() { Text = "بهاباد ", Value = "بهاباد" });
                    City.Add(new SelectListItem() { Text = "تفت", Value = "تفت" });
                    City.Add(new SelectListItem() { Text = "خاتم", Value = "خاتم" });
                    City.Add(new SelectListItem() { Text = "مهريز", Value = "مهريز" });
                    City.Add(new SelectListItem() { Text = "مِيبُد", Value = "مِيبُد" });
                    City.Add(new SelectListItem() { Text = "يزد", Value = "يزد" });
                    break;

            }

            return City;
        }

    }
}