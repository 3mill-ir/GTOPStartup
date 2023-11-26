using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace GPLUSPLUS.Models.Bussiness
{
    public class CodeManagement
    {
        private List<string[]> CodesList;

        public List<string[]> GenerateCodeMethod(int NumberOf1Score,int NumberOf2Score, int NumberOf5Score, int NumberOf10Score, int NumberOf20Score, int NumberOf25Score, int NumberOf50Score, int NumberOf100Score, int NumberOf200Score)
        {
            CodesList = new List<string[]>();
            
            /////////1
            List<string> Codes1_1Score = new List<string>();
            List<string> Codes2_1Score = new List<string>();
            for (int i = 0; i < NumberOf1Score; i++)
            {
                string[] Result = new string[3];
                byte[] buffer1 = Guid.NewGuid().ToByteArray();
                byte[] buffer2 = Guid.NewGuid().ToByteArray();
                Result[0] = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
                Result[1] = BitConverter.ToUInt64(buffer2, 8).ToString().Substring(0, 8);
                Result[2] = "1";
                string temp_0 = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
                while (temp_0 == Result[1])
                {
                    buffer1 = Guid.NewGuid().ToByteArray();
                    buffer2 = Guid.NewGuid().ToByteArray();
                    Result[0] = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
                    Result[1] = BitConverter.ToUInt64(buffer2, 8).ToString().Substring(0, 8);
                    temp_0 = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
                }
                Result[0] = Result[0].Insert(4, "-");
                Result[0] = Result[0].Insert(9, "-");
                Result[0] = Result[0].Insert(14, "-");

                bool again = false;

                if (Codes1_1Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_1Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
                using (EntitiesConnection EC_DB = new EntitiesConnection()) {

                    var query = (from c in EC_DB.ScoreCards select c);

                    List<string> temp_Codes1_Score = new List<string>();
                    List<string> temp_Codes2_Score = new List<string>();
                    foreach (var q in query) {
                        temp_Codes1_Score.Add(StringCipher.Decrypt(q.CodeNumber,"PARSADORSA"));
                        temp_Codes2_Score.Add(q.TrackingCode);
                    }
                    if (temp_Codes1_Score.Any(m => m == Result[0]))
                    {
                        again = true;
                    }
                    if (temp_Codes2_Score.Any(m => m == Result[1]))
                    {
                        again = true;
                    }
                }

                if (again == false)
                {
                    Codes1_1Score.Add(Result[0]);
                    Codes2_1Score.Add(Result[1]);
                    CodesList.Add(Result);
                }
                else
                {
                    i--;
                }


            }


            /////////2
  /*          List<string>   Codes1_2Score = new List<string>();
             List<string>  Codes2_2Score = new List<string>();
            for (int i = 0; i < NumberOf2Score; i++)
            {
                string[] Result = new string[3];
                byte[] buffer1 = Guid.NewGuid().ToByteArray();
                byte[] buffer2 = Guid.NewGuid().ToByteArray();
                Result[0] = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
                Result[1] = BitConverter.ToUInt64(buffer2, 8).ToString().Substring(0, 8);
                Result[2] = "2";
                string temp_0 = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16); 
                while (temp_0 == Result[1])
                {
                    buffer1 = Guid.NewGuid().ToByteArray();
                    buffer2 = Guid.NewGuid().ToByteArray();
                    Result[0] = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
                    Result[1] = BitConverter.ToUInt64(buffer2, 8).ToString().Substring(0, 8);
                    temp_0 = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
                }
                Result[0] = Result[0].Insert(4, "-");
                Result[0] = Result[0].Insert(9, "-");
                Result[0] = Result[0].Insert(14, "-");

                bool again = false;

                if (Codes1_2Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_2Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
                if (Codes1_1Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_1Score.Any(m => m == Result[1]))
                {
                    again = true;
                }

                if (again == false)
                {
                    Codes1_2Score.Add(Result[0]);
                    Codes2_2Score.Add(Result[1]);
                    CodesList.Add(Result);
                }
                else
                {
                    i--;
                }


            }

//////*********5 Score
            List<string> Codes1_5Score = new List<string>();
            List<string> Codes2_5Score = new List<string>();
            for (int i = 0; i < NumberOf5Score; i++)
            {
                string[] Result = new string[3];
                byte[] buffer1 = Guid.NewGuid().ToByteArray();
                byte[] buffer2 = Guid.NewGuid().ToByteArray();
                Result[0] = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
                Result[1] = BitConverter.ToUInt64(buffer2, 8).ToString().Substring(0, 8);
                Result[2] = "5";
                string temp_0 = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
                while (temp_0 == Result[1])
                {
                    buffer1 = Guid.NewGuid().ToByteArray();
                    buffer2 = Guid.NewGuid().ToByteArray();
                    Result[0] = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
                    Result[1] = BitConverter.ToUInt64(buffer2, 8).ToString().Substring(0, 8);
                    temp_0 = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
                }
                Result[0] = Result[0].Insert(4, "-");
                Result[0] = Result[0].Insert(9, "-");
                Result[0] = Result[0].Insert(14, "-");

                bool again = false;

                if (Codes1_5Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_5Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
                if (Codes1_2Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_2Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
                if (Codes1_1Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_1Score.Any(m => m == Result[1]))
                {
                    again = true;
                }

                if (again == false)
                {
                    Codes1_5Score.Add(Result[0]);
                    Codes2_5Score.Add(Result[1]);
                    CodesList.Add(Result);
                }
                else
                {
                    i--;
                }


            }


            //////********************10

            List<string> Codes1_10Score = new List<string>();
            List<string> Codes2_10Score = new List<string>();
            for (int i = 0; i < NumberOf10Score; i++)
            {
                string[] Result = new string[3];
                byte[] buffer1 = Guid.NewGuid().ToByteArray();
                byte[] buffer2 = Guid.NewGuid().ToByteArray();
                Result[0] = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
                Result[1] = BitConverter.ToUInt64(buffer2, 8).ToString().Substring(0, 8);
                Result[2] = "10";
                string temp_0 = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
                while (temp_0 == Result[1])
                {
                    buffer1 = Guid.NewGuid().ToByteArray();
                    buffer2 = Guid.NewGuid().ToByteArray();
                    Result[0] = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
                    Result[1] = BitConverter.ToUInt64(buffer2, 8).ToString().Substring(0, 8);
                    temp_0 = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
                }
                Result[0] = Result[0].Insert(4, "-");
                Result[0] = Result[0].Insert(9, "-");
                Result[0] = Result[0].Insert(14, "-");

                bool again = false;

                if (Codes1_10Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_10Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
                if (Codes1_5Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_5Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
                if (Codes1_2Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_2Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
                if (Codes1_1Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_1Score.Any(m => m == Result[1]))
                {
                    again = true;
                }

                if (again == false)
                {
                    Codes1_10Score.Add(Result[0]);
                    Codes2_10Score.Add(Result[1]);
                    CodesList.Add(Result);
                }
                else
                {
                    i--;
                }


            }

            ///////******************15

            //List<string> Codes1_15Score = new List<string>();
            //List<string> Codes2_15Score = new List<string>();
            //for (int i = 0; i < NumberOf15Score; i++)
            //{
            //    string[] Result = new string[3];
            //    byte[] buffer1 = Guid.NewGuid().ToByteArray();
            //    byte[] buffer2 = Guid.NewGuid().ToByteArray();
            //    Result[0] = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
            //    Result[1] = BitConverter.ToUInt64(buffer2, 8).ToString().Substring(0, 8);
            //    Result[2] = "15";
            //    string temp_0 = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
            //    while (temp_0 == Result[1])
            //    {
            //        buffer1 = Guid.NewGuid().ToByteArray();
            //        buffer2 = Guid.NewGuid().ToByteArray();
            //        Result[0] = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
            //        Result[1] = BitConverter.ToUInt64(buffer2, 8).ToString().Substring(0, 8);
            //        temp_0 = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
            //    }
            //    Result[0] = Result[0].Insert(4, "-");
            //    Result[0] = Result[0].Insert(9, "-");
            //    Result[0] = Result[0].Insert(14, "-");

            //    bool again = false;

            //    if (Codes1_15Score.Any(m => m == Result[0]))
            //    {
            //        again = true;
            //    }

            //    if (Codes2_15Score.Any(m => m == Result[1]))
            //    {
            //        again = true;
            //    }
            //    if (Codes1_10Score.Any(m => m == Result[0]))
            //    {
            //        again = true;
            //    }

            //    if (Codes2_10Score.Any(m => m == Result[1]))
            //    {
            //        again = true;
            //    }
            //    if (Codes1_5Score.Any(m => m == Result[0]))
            //    {
            //        again = true;
            //    }

            //    if (Codes2_5Score.Any(m => m == Result[1]))
            //    {
            //        again = true;
            //    }
            //    if (Codes1_2Score.Any(m => m == Result[0]))
            //    {
            //        again = true;
            //    }

            //    if (Codes2_2Score.Any(m => m == Result[1]))
            //    {
            //        again = true;
            //    }
            //    if (Codes1_1Score.Any(m => m == Result[0]))
            //    {
            //        again = true;
            //    }

            //    if (Codes2_1Score.Any(m => m == Result[1]))
            //    {
            //        again = true;
            //    }

            //    if (again == false)
            //    {
            //        Codes1_15Score.Add(Result[0]);
            //        Codes2_15Score.Add(Result[1]);
            //        CodesList.Add(Result);
            //    }
            //    else
            //    {
            //        i--;
            //    }


            //}

            ///////////////////20
            List<string> Codes1_20Score = new List<string>();
            List<string> Codes2_20Score = new List<string>();
            for (int i = 0; i < NumberOf20Score; i++)
            {
                string[] Result = new string[3];
                byte[] buffer1 = Guid.NewGuid().ToByteArray();
                byte[] buffer2 = Guid.NewGuid().ToByteArray();
                Result[0] = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
                Result[1] = BitConverter.ToUInt64(buffer2, 8).ToString().Substring(0, 8);
                Result[2] = "20";
                string temp_0 = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
                while (temp_0 == Result[1])
                {
                    buffer1 = Guid.NewGuid().ToByteArray();
                    buffer2 = Guid.NewGuid().ToByteArray();
                    Result[0] = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
                    Result[1] = BitConverter.ToUInt64(buffer2, 8).ToString().Substring(0, 8);
                    temp_0 = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
                }
                Result[0] = Result[0].Insert(4, "-");
                Result[0] = Result[0].Insert(9, "-");
                Result[0] = Result[0].Insert(14, "-");

                bool again = false;

                if (Codes1_20Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_20Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
            
                if (Codes1_10Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_10Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
                if (Codes1_5Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_5Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
                if (Codes1_2Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_2Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
                if (Codes1_1Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_1Score.Any(m => m == Result[1]))
                {
                    again = true;
                }

                if (again == false)
                {
                    Codes1_20Score.Add(Result[0]);
                    Codes2_20Score.Add(Result[1]);
                    CodesList.Add(Result);
                }
                else
                {
                    i--;
                }


            }

            /////////////25
            List<string> Codes1_25Score = new List<string>();
            List<string> Codes2_25Score = new List<string>();
            for (int i = 0; i < NumberOf25Score; i++)
            {
                string[] Result = new string[3];
                byte[] buffer1 = Guid.NewGuid().ToByteArray();
                byte[] buffer2 = Guid.NewGuid().ToByteArray();
                Result[0] = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
                Result[1] = BitConverter.ToUInt64(buffer2, 8).ToString().Substring(0, 8);
                Result[2] = "25";
                string temp_0 = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
                while (temp_0 == Result[1])
                {
                    buffer1 = Guid.NewGuid().ToByteArray();
                    buffer2 = Guid.NewGuid().ToByteArray();
                    Result[0] = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
                    Result[1] = BitConverter.ToUInt64(buffer2, 8).ToString().Substring(0, 8);
                    temp_0 = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
                }
                Result[0] = Result[0].Insert(4, "-");
                Result[0] = Result[0].Insert(9, "-");
                Result[0] = Result[0].Insert(14, "-");

                bool again = false;

                if (Codes1_25Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_25Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
                if (Codes1_20Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_20Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
               
                if (Codes1_10Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_10Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
                if (Codes1_5Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_5Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
                if (Codes1_2Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_2Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
                if (Codes1_1Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_1Score.Any(m => m == Result[1]))
                {
                    again = true;
                }

                if (again == false)
                {
                    Codes1_25Score.Add(Result[0]);
                    Codes2_25Score.Add(Result[1]);
                    CodesList.Add(Result);
                }
                else
                {
                    i--;
                }


            }


            //*****************50
            List<string> Codes1_50Score = new List<string>();
            List<string> Codes2_50Score = new List<string>();
            for (int i = 0; i < NumberOf50Score; i++)
            {
                string[] Result = new string[3];
                byte[] buffer1 = Guid.NewGuid().ToByteArray();
                byte[] buffer2 = Guid.NewGuid().ToByteArray();
                Result[0] = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
                Result[1] = BitConverter.ToUInt64(buffer2, 8).ToString().Substring(0, 8);
                Result[2] = "50";
                string temp_0 = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
                while (temp_0 == Result[1])
                {
                    buffer1 = Guid.NewGuid().ToByteArray();
                    buffer2 = Guid.NewGuid().ToByteArray();
                    Result[0] = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
                    Result[1] = BitConverter.ToUInt64(buffer2, 8).ToString().Substring(0, 8);
                    temp_0 = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
                }
                Result[0] = Result[0].Insert(4, "-");
                Result[0] = Result[0].Insert(9, "-");
                Result[0] = Result[0].Insert(14, "-");

                bool again = false;

                if (Codes1_50Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_50Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
                if (Codes1_25Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_25Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
                if (Codes1_20Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_20Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
               
                if (Codes1_10Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_10Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
                if (Codes1_5Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_5Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
                if (Codes1_2Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_2Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
                if (Codes1_1Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_1Score.Any(m => m == Result[1]))
                {
                    again = true;
                }


                if (again == false)
                {
                    Codes1_50Score.Add(Result[0]);
                    Codes2_50Score.Add(Result[1]);
                    CodesList.Add(Result);
                }
                else
                {
                    i--;
                }


            }

            //////////////100
            List<string> Codes1_100Score = new List<string>();
            List<string> Codes2_100Score = new List<string>();
            for (int i = 0; i < NumberOf100Score; i++)
            {
                string[] Result = new string[3];
                byte[] buffer1 = Guid.NewGuid().ToByteArray();
                byte[] buffer2 = Guid.NewGuid().ToByteArray();
                Result[0] = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
                Result[1] = BitConverter.ToUInt64(buffer2, 8).ToString().Substring(0, 8);
                Result[2] = "100";
                string temp_0 = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
                while (temp_0 == Result[1])
                {
                    buffer1 = Guid.NewGuid().ToByteArray();
                    buffer2 = Guid.NewGuid().ToByteArray();
                    Result[0] = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
                    Result[1] = BitConverter.ToUInt64(buffer2, 8).ToString().Substring(0, 8);
                    temp_0 = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
                }
                Result[0] = Result[0].Insert(4, "-");
                Result[0] = Result[0].Insert(9, "-");
                Result[0] = Result[0].Insert(14, "-");

                bool again = false;

                if (Codes1_100Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_100Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
                if (Codes1_50Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_50Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
                if (Codes1_25Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_25Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
                if (Codes1_20Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_20Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
             
                if (Codes1_10Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_10Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
                if (Codes1_5Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_5Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
                if (Codes1_2Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_2Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
                if (Codes1_1Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_1Score.Any(m => m == Result[1]))
                {
                    again = true;
                }

                if (again == false)
                {
                    Codes1_100Score.Add(Result[0]);
                    Codes2_100Score.Add(Result[1]);
                    CodesList.Add(Result);
                }
                else
                {
                    i--;
                }


            }

            /////////////////200
            List<string> Codes1_200Score = new List<string>();
            List<string> Codes2_200Score = new List<string>();
            for (int i = 0; i < NumberOf200Score; i++)
            {
                string[] Result = new string[3];
                byte[] buffer1 = Guid.NewGuid().ToByteArray();
                byte[] buffer2 = Guid.NewGuid().ToByteArray();
                Result[0] = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
                Result[1] = BitConverter.ToUInt64(buffer2, 8).ToString().Substring(0, 8);
                Result[2] = "200";
                string temp_0 = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
                while (temp_0 == Result[1])
                {
                    buffer1 = Guid.NewGuid().ToByteArray();
                    buffer2 = Guid.NewGuid().ToByteArray();
                    Result[0] = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
                    Result[1] = BitConverter.ToUInt64(buffer2, 8).ToString().Substring(0, 8);
                    temp_0 = BitConverter.ToUInt64(buffer1, 8).ToString().Substring(0, 16);
                }
                Result[0] = Result[0].Insert(4, "-");
                Result[0] = Result[0].Insert(9, "-");
                Result[0] = Result[0].Insert(14, "-");

                bool again = false;

                if (Codes1_200Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_200Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
                if (Codes1_100Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_100Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
                if (Codes1_50Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_50Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
                if (Codes1_25Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_25Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
                if (Codes1_20Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_20Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
              
                if (Codes1_10Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_10Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
                if (Codes1_5Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_5Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
                if (Codes1_2Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_2Score.Any(m => m == Result[1]))
                {
                    again = true;
                }
                if (Codes1_1Score.Any(m => m == Result[0]))
                {
                    again = true;
                }

                if (Codes2_1Score.Any(m => m == Result[1]))
                {
                    again = true;
                }

                if (again == false)
                {
                    Codes1_200Score.Add(Result[0]);
                    Codes2_200Score.Add(Result[1]);
                    CodesList.Add(Result);
                }
                else
                {
                    i--;
                }


            }*/


            return CodesList;

        }




    }


    public static class StringCipher
    {
        // This constant string is used as a "salt" value for the PasswordDeriveBytes function calls.
        // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
        private static readonly byte[] initVectorBytes = Encoding.ASCII.GetBytes("tu89geji340t89u2");

        // This constant is used to determine the keysize of the encryption algorithm.
        private const int keysize = 256;

        public static string Encrypt(string plainText, string passPhrase)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null))
            {
                byte[] keyBytes = password.GetBytes(keysize / 8);
                using (RijndaelManaged symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.Mode = CipherMode.CBC;
                    using (ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes))
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                byte[] cipherTextBytes = memoryStream.ToArray();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public static string Decrypt(string cipherText, string passPhrase)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            using (PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null))
            {
                byte[] keyBytes = password.GetBytes(keysize / 8);
                using (RijndaelManaged symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.Mode = CipherMode.CBC;
                    using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes))
                    {
                        using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }
    }
}