using System;
using System.Collections.Generic;
using System.IO;

namespace DepurginatorWeb
{
    public partial class depurgeit : System.Web.UI.Page
    {
        public string filename = "";
        public string dateTimeStamp = ""; 
        public string outFile = ""; 
        public string outFileReverse = "";
        public string outFileLink = "";
        public string outFileLinkReverse = "";
        public string host = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            host = Request.Url.AbsoluteUri;
            ErrorLabel.Text = "";

            if (Request.Params["showLinks"] != "True")
            {
                form1.Action = host + "?showLinks=True";
            }
            else
            {
                form1.Action = host;
                dateTimeStamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                outFile =  "depurginated_numbers_" + dateTimeStamp + ".txt";
                outFileReverse = "depurginated_numbers_reverse_" + dateTimeStamp + ".txt"; 
                outFileLink = "outfile/" + outFile;
                outFileLinkReverse = "outfile/" + outFileReverse;
                LinkPanel.Visible = true;     
            }

            DataBind();
        }

        protected void DepurginateIt_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Server.MapPath("~/infile/")))
            {
                try
                {
                    Directory.CreateDirectory(Server.MapPath("~/infile/"));
                }
                catch (Exception ex)
                {
                    ErrorLabel.Text = "Could not create infile directory. The following error occured: " + ex.Message;
                }
            }

            if(!Directory.Exists(Server.MapPath("~/outfile/")))
            {
                try
                {
                    Directory.CreateDirectory(Server.MapPath("~/outfile/"));
                }
                catch (Exception ex)
                {
                    ErrorLabel.Text = "Could not create outfile directory. The following error occured: " + ex.Message;
                }
            }

            if (NumbersIn.HasFile)
            {
                if (NumbersIn.PostedFile.ContentType == "text/plain")
                    filename = Path.GetFileName(NumbersIn.FileName);

                try
                {
                    NumbersIn.SaveAs(Server.MapPath("~/infile/") + filename);
                }
                catch (Exception ex)
                {
                    ErrorLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }

            if (!File.Exists(Server.MapPath("~/infile/") + filename))     
            {
                ViewState["error"] = "Could not find a numbers file to depurginate.";
                ErrorLabel.Text = ViewState["error"].ToString();
                ViewState["linksVisible"] = false;
                LinkPanel.Visible = (bool)ViewState["linksVisible"];
                return;
            }

            double result = 0;
            double revResult = 0;
            Permutator permutator = new Permutator();
            int[] arr1 = new int[] { 1, 2, 3, 4, 5, 6, 7 };

            List<double> numberList = new List<double>();
            List<double> permutedList = new List<double>();
            List<double> resultList = new List<double>();
            List<double> reverseResultList = new List<double>();

            if (!ReadFile(numberList))
            {
                ViewState["error"] = "Error reading the numbers file. Check format.";
                ErrorLabel.Text = ViewState["error"].ToString();
                ViewState["linksVisible"] = false;                
                LinkPanel.Visible = (bool)ViewState["linksVisible"];
                return;
            }

            permutator.Permutate(arr1, 0, 6, permutedList);

            foreach (double d in numberList)
            {
                int p = 0;
                result = d;
                revResult = d;

                foreach (var number in permutedList)
                {
                    p++;
                    string num = number.ToString();

                    foreach (char c in num)
                    {
                        switch (c)
                        {
                            case '1':
                                result = result - 49;
                                revResult = revResult + 49;
                                break;
                            case '2':
                                result = result - 49;
                                revResult = revResult + 49;
                                break;
                            case '3':
                                result = result * .2;
                                revResult = revResult * 5;
                                break;
                            case '4':
                                result = result - 35;
                                revResult = revResult + 35;
                                break;
                            case '5':
                                result = result - 38;
                                revResult = revResult + 38;
                                break;
                            case '6':
                                result = result / 6;
                                revResult = revResult * 6;
                                break;
                            case '7':
                                result = result / 13;
                                revResult = revResult * 13;
                                break;
                        }
                    }

                    if (p % 7 == 0)
                    {
                        double key = 0;
                        string skey = "";

                        for (int i = p - 7; i < p; i++)
                        {
                            skey += permutedList[i].ToString();
                        }

                        //if (!resultList.Exists(e => e.Equals(result)))
                        //{
                        key = double.Parse(skey);
                        resultList.Add(key);
                        resultList.Add(result);
                        reverseResultList.Add(key);
                        reverseResultList.Add(revResult);
                        //}
                        result = d;
                        revResult = d;
                    }
                }
            }

            try
            {
                using (StreamWriter outputFile = new StreamWriter(Server.MapPath("~/outfile/") + outFile))
                {
                    int x = 1;
                    int y = 1;
                    string outString = "";
                    string oNumber = numberList[0].ToString();

                    foreach (double line in resultList)
                    {
                        if (x % 5040 == 0 && y <= numberList.Count)
                        {
                            oNumber = numberList[y - 1].ToString();
                            y++;
                        }

                        if (x % 2 == 0)
                        {
                            outString += line.ToString();
                            outputFile.WriteLine(outString);
                        }
                        else
                        {
                            outString = oNumber + "," + line.ToString() + ",";
                        }
                        x++;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLabel.Text = "The following error occured: " + ex.Message;
            }

            try
            {
                using (StreamWriter outputFile = new StreamWriter(Server.MapPath("~/outfile/") + outFileReverse))
                {
                    int x = 1;
                    int y = 1;
                    string outString = "";
                    string oNumber = numberList[0].ToString();

                    foreach (double line in reverseResultList)
                    {
                        if (x % 5040 == 0 && y <= numberList.Count)
                        {
                            oNumber = numberList[y - 1].ToString();
                            y++;
                        }

                        if (x % 2 == 0)
                        {
                            outString += line.ToString();
                            outputFile.WriteLine(outString);
                        }
                        else
                        {
                            outString = oNumber + "," + line.ToString() + ",";
                        }
                        x++;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLabel.Text = "The following error occured: " + ex.Message;
            }
                        
            LinkPanel.Visible = true;
        }

        public bool ReadFile(List<double> numberList)
        {
            bool isGood = true;
            string dir = System.IO.Directory.GetCurrentDirectory();
            try
            {
                using (StreamReader reader = new StreamReader(Server.MapPath("~/infile/") + filename))  
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        numberList.Add(double.Parse(line));
                    }
                }
            }
            catch (Exception)
            {
                isGood = false;
            }
            return isGood;
        }
    }

    class Permutator
    {
        public void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }
        public void Permutate(int[] list, int k, int m, List<double> numList)
        {
            int i;

            if (k == m)
            {
                for (i = 0; i <= m; i++)
                {
                    numList.Add(list[i]);
                }
            }
            else
            {
                for (i = k; i <= m; i++)
                {
                    Swap(ref list[k], ref list[i]);
                    Permutate(list, k + 1, m, numList);
                    Swap(ref list[k], ref list[i]);
                }
            }
        }
    }
}
