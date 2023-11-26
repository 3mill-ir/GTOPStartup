using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace GPLUSPLUS.Models.Bussiness
{
    public class ScoreManagement
    {
        public int? TotalUserScore(int UserID)
        {
            int? sumCodeScore = 0;
            int freegtopCode = 0;
            using (UsersContext db = new UsersContext())
            {
                UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserId == UserID);
                if (user != null)
                {
                    //using (EntitiesConnection EC_DB = new EntitiesConnection())
                    //{
                    //    var query = from c in EC_DB.ScoreCards
                    //                where c.F_UserID == UserID
                    //                select c;
                    //     sumCodeScore = query.Sum(y => y.CodeScore);
                    //}
                    sumCodeScore = user.U_Score;
                    if (user.FreeCodeGTOP == true)
                    {
                        freegtopCode = 0;
                    }

                }
            }

            int? TotalGiftCodeScore = 0;
            using (EntitiesConnection EC_DB = new EntitiesConnection())
            {
                var query = from c in EC_DB.FreeGiftCodes
                            select c.CodeScore;
                TotalGiftCodeScore = query.Sum(y => y);
            }
            if (sumCodeScore == null)
                sumCodeScore = 0;
            if (TotalGiftCodeScore == null)
                TotalGiftCodeScore = 0;
            sumCodeScore = sumCodeScore + freegtopCode + TotalGiftCodeScore;
            return sumCodeScore;
        }

        public string TrackCode(string TrackCode)
        {
            using (EntitiesConnection EC_DB = new EntitiesConnection())
            {

                ScoreCard Score = EC_DB.ScoreCards.FirstOrDefault(u => u.TrackingCode == TrackCode);
                if (Score == null)
                    return "Code is not exist";
                else
                {
                    if (Score.F_UserID != null)
                        return "Code used before";
                    else
                        return Score.CodeScore.ToString(); ;
                }
            }
        }


        public string NewScore(int UserID, string CodeNumber)
        {

            using (EntitiesConnection EC_DB = new EntitiesConnection())
            {
                ScoreTry ST = EC_DB.ScoreTries.FirstOrDefault(u => u.Id == WebSecurity.CurrentUserId);
                if (ST != null)
                {
                    if (ST.UserStatusForScore == false)
                    {
                        return "User Blocked BrutForced Score";
                    }
                }
            }

            if (CodeNumber.Equals("gtopco"))
            {
                using (UsersContext db = new UsersContext())
                {
                    UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserId == UserID);
                    if (user != null)
                    {
                        if (user.FreeCodeGTOP == false)
                        {
                            user.FreeCodeGTOP = true;
                            user.U_Score = user.U_Score + 2000;
                            user.U_LotteryScore = 2;
                            db.SaveChanges();
                            return user.U_Score.ToString();
                        }
                        else
                        {
                            return "Free Code Used Befor";
                        }
                    }
                }
            }

            if (CodeNumber.Length < 16)
                return "Code Not Found";

            string Plain_CodeNumber = CodeNumber;
            Plain_CodeNumber = Plain_CodeNumber.Insert(4, "-");
            Plain_CodeNumber = Plain_CodeNumber.Insert(9, "-");
            Plain_CodeNumber = Plain_CodeNumber.Insert(14, "-");
            string Cipher_CodeNumber = StringCipher.Encrypt(Plain_CodeNumber, "PARSADORSA");


            char[] arr = Plain_CodeNumber.ToCharArray();
            Array.Reverse(arr);
            string Reverse_Plain = new string(arr);
            Reverse_Plain = Reverse_Plain.Insert(4, "-");
            Reverse_Plain = Reverse_Plain.Insert(9, "-");
            Reverse_Plain = Reverse_Plain.Insert(14, "-");
            string Cipher_Revers_CodeNumber = StringCipher.Encrypt(Reverse_Plain, "PARSADORSA");

            using (EntitiesConnection EC_DB = new EntitiesConnection())
            {

                ScoreCard Score = EC_DB.ScoreCards.FirstOrDefault(u => u.CodeNumber == Cipher_CodeNumber);
                ScoreCard Score_Revers = EC_DB.ScoreCards.FirstOrDefault(u => u.CodeNumber == Cipher_Revers_CodeNumber);
                if (Score == null && Score_Revers == null)
                {
                    isBrutForced(UserID);
                    EC_DB.SaveChanges();
                    return "Code Not Found";
                }
                else
                {
                    if (Score != null)
                    {
                        if (Score.F_UserID == null)
                        {
                            int? newscore = 0;
                            Score.F_UserID = UserID;
                            Score.UsedDate = DateTime.Now;
                            using (UsersContext db = new UsersContext())
                            {
                                UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserId == WebSecurity.CurrentUserId);
                                if (user != null)
                                {
                                    user.U_Score = user.U_Score + Score.CodeScore;
                                    newscore = user.U_Score;
                                }
                                db.SaveChanges();
                            }
                            EC_DB.SaveChanges();
                            return newscore.ToString();
                        }
                        else
                        {
                            isBrutForced(UserID);
                            EC_DB.SaveChanges();
                            return "Code Used Before";
                        }
                    }
                    else
                    {
                        if (Score_Revers.F_UserID == null)
                        {
                            int? newscore = 0;
                            Score_Revers.F_UserID = UserID;
                            Score_Revers.UsedDate = DateTime.Now;
                            using (UsersContext db = new UsersContext())
                            {
                                UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserId == WebSecurity.CurrentUserId);
                                if (user != null)
                                {
                                    user.U_Score = user.U_Score + Score_Revers.CodeScore;
                                    newscore = user.U_Score;
                                }
                                db.SaveChanges();
                            }
                            EC_DB.SaveChanges();
                            return newscore.ToString();
                        }
                        else
                        {
                            isBrutForced(UserID);
                            EC_DB.SaveChanges();
                            return "Code Used Before";
                        }
                    }
                }
            }

        }

        public bool isCodeValid(string CodeNumber)
        {
            if (CodeNumber.Length < 16)
                return false;

            string Plain_CodeNumber = CodeNumber;
            Plain_CodeNumber = Plain_CodeNumber.Insert(4, "-");
            Plain_CodeNumber = Plain_CodeNumber.Insert(9, "-");
            Plain_CodeNumber = Plain_CodeNumber.Insert(14, "-");
            string Cipher_CodeNumber = StringCipher.Encrypt(Plain_CodeNumber, "PARSADORSA");


            char[] arr = Plain_CodeNumber.ToCharArray();
            Array.Reverse(arr);
            string Reverse_Plain = new string(arr);
            Reverse_Plain = Reverse_Plain.Insert(4, "-");
            Reverse_Plain = Reverse_Plain.Insert(9, "-");
            Reverse_Plain = Reverse_Plain.Insert(14, "-");
            string Cipher_Revers_CodeNumber = StringCipher.Encrypt(Reverse_Plain, "PARSADORSA");



            using (EntitiesConnection EC_DB = new EntitiesConnection())
            {

                ScoreCard Score = EC_DB.ScoreCards.FirstOrDefault(u => u.CodeNumber == Cipher_CodeNumber);
                ScoreCard Score_Revers = EC_DB.ScoreCards.FirstOrDefault(u => u.CodeNumber == Cipher_Revers_CodeNumber);
                if (Score == null && Score_Revers == null)
                {
                    return false;
                }
                else
                {
                    if (Score != null)
                    {
                        if (Score.F_UserID == null)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (Score_Revers.F_UserID == null)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
        }

        public bool isBrutForced(int UserID)
        {

            using (EntitiesConnection EC_DB = new EntitiesConnection())
            {
                ScoreTry ST = EC_DB.ScoreTries.FirstOrDefault(u => u.Id == UserID);


                if (ST != null)
                {
                    int LastTryNumber = ST.LastTryNumber ?? 0;
                    int TotaltryNumber = ST.TotalTryNumber ?? 0;
                    DateTime LastTry = ST.LastTryDate ?? DateTime.Now;
                    DateTime current = DateTime.Now;
                    ST.LastTryNumber++;
                    ST.TotalTryNumber++;
                    ST.LastTryDate = DateTime.Now;

                    if ((LastTry - current).TotalDays <= 1)
                    {
                        if (LastTryNumber <= 10)
                        {
                            ST.UserStatusForScore = true;
                            EC_DB.SaveChanges();
                            return false;
                        }
                        else
                        {
                            ST.UserStatusForScore = false;
                            EC_DB.SaveChanges();
                            return true;
                        }
                    }
                    else
                    {
                        ST.UserStatusForScore = true;
                        ST.LastTryNumber = 1;
                        EC_DB.SaveChanges();
                        return false;
                    }

                }
                ScoreTry NewST = new ScoreTry();
                NewST.Id = UserID;
                NewST.LastTryNumber = 1;
                NewST.LastTryDate = DateTime.Now;
                NewST.TotalTryNumber = 1;
                NewST.UserStatusForScore = true;
                EC_DB.ScoreTries.Add(NewST);
                EC_DB.SaveChanges();
                return false;
            }

        }

        public int? TotalUserLotteryScore(int UserID)
        {
            int? sumCodeLottery = 0;
            using (UsersContext db = new UsersContext())
            {
                UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserId == UserID);
                if (user != null)
                {
                    using (EntitiesConnection EC_DB = new EntitiesConnection())
                    {
                        var query = from c in EC_DB.ScoreCards
                                    where c.F_UserID == UserID
                                    select c.LotteryScore;
                        sumCodeLottery = query.Sum(y => y);
                    }
                    if (sumCodeLottery == null)
                    {
                        sumCodeLottery = 0;
                    }
                    sumCodeLottery = sumCodeLottery  + user.U_LotteryScore;
                }
            }
            return sumCodeLottery;
        }


        public int? TotalBonyadKoodakScore()
        {
            int? sumCode = 0;
            using (EntitiesConnection EC_DB = new EntitiesConnection())
            {
                var query = from c in EC_DB.ScoreCards where c.F_UserID!=null
                            select c.BonyadKoodak;
                sumCode = query.Sum(y => y);
            }

            return sumCode;
        }
    }
}