using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForexCalc_v1
{
    public partial class Form1 : Form
    {
        public static float CAD;
        public static float CHF;
        public static float JPY;
        public static float NZD;
        public static float USD;
        public static float AUD;
        public static float GBP;
        public static float XAU;
        public static float PipPrice;

        public Form1()
        {
            InitializeComponent();
            cb_Pairs.SelectedIndex = 19;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try //Try to get prices
            {
                GetPipPriceCadChf(lbl_Cad, lbl_Chf);
                GetPipPriceJpyNzd(lbl_Jpy, lbl_Nzd);
                GetPipPriceUsdAud(lbl_Usd, lbl_Aud);
                GetPipPriceGbpXau(lbl_Gbp, lbl_Xau);

            }
            catch (System.Net.WebException) //No connection exception
            {
                var result = MessageBox.Show("Cannot connect to data server!" + Environment.NewLine + "Would you like to try connect again?", "Connection error!", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.Yes)
                {
                    Application.Restart();
                    Environment.Exit(0);
                }
                else
                {
                    Environment.Exit(0);
                }
            }
        }

        private static void GetPipPriceCadChf(Label lbl_Cad, Label lbl_Chf)
        {
            var json1 = new WebClient().DownloadString("https://free.currconv.com/api/v7/convert?q=CAD_CZK,CHF_CZK&compact=ultra&apiKey=_YOUR_API_KEY_");
            string[] currency = json1.Split(',');
            string currencyCut1 = currency[0].Replace("{\"CAD_CZK\":", "");
            string currencyCut2Step = currency[1].Replace("\"CHF_CZK\":", "");
            string currencyCut2 = currencyCut2Step.Replace("}", "");
            CAD = float.Parse(currencyCut1, System.Globalization.CultureInfo.InvariantCulture) * 10;
            CHF = float.Parse(currencyCut2, System.Globalization.CultureInfo.InvariantCulture) * 10;
            lbl_Cad.Text = CAD.ToString();
            lbl_Chf.Text = CHF.ToString();
        }

        private static void GetPipPriceJpyNzd(Label lbl_Jpy, Label lbl_Nzd)
        {
            var json1 = new WebClient().DownloadString("https://free.currconv.com/api/v7/convert?q=JPY_CZK,NZD_CZK&compact=ultra&apiKey=_YOUR_API_KEY_");
            string[] currency = json1.Split(',');
            string currencyCut1 = currency[0].Replace("{\"JPY_CZK\":", "");
            string currencyCut2Step = currency[1].Replace("\"NZD_CZK\":", "");
            string currencyCut2 = currencyCut2Step.Replace("}", "");
            JPY = float.Parse(currencyCut1, System.Globalization.CultureInfo.InvariantCulture) * 1000;
            NZD = float.Parse(currencyCut2, System.Globalization.CultureInfo.InvariantCulture) * 10;
            lbl_Jpy.Text = JPY.ToString();
            lbl_Nzd.Text = NZD.ToString();
        }

        private static void GetPipPriceUsdAud(Label lbl_Usd, Label lbl_Aud)
        {
            var json1 = new WebClient().DownloadString("https://free.currconv.com/api/v7/convert?q=USD_CZK,AUD_CZK&compact=ultra&apiKey=_YOUR_API_KEY_");
            string[] currency = json1.Split(',');
            string currencyCut1 = currency[0].Replace("{\"USD_CZK\":", "");
            string currencyCut2Step = currency[1].Replace("\"AUD_CZK\":", "");
            string currencyCut2 = currencyCut2Step.Replace("}", "");
            USD = float.Parse(currencyCut1, System.Globalization.CultureInfo.InvariantCulture) * 10;
            AUD = float.Parse(currencyCut2, System.Globalization.CultureInfo.InvariantCulture) * 10;
            lbl_Usd.Text = USD.ToString();
            lbl_Aud.Text = AUD.ToString();
        }

        private static void GetPipPriceGbpXau(Label lbl_Gbp, Label lbl_Xau)
        {
            var json1 = new WebClient().DownloadString("https://free.currconv.com/api/v7/convert?q=GBP_CZK,USD_CZK&compact=ultra&apiKey=_YOUR_API_KEY_");
            string[] currency = json1.Split(',');
            string currencyCut1 = currency[0].Replace("{\"GBP_CZK\":", "");
            string currencyCut2Step = currency[1].Replace("\"USD_CZK\":", "");
            string currencyCut2 = currencyCut2Step.Replace("}", "");
            GBP = float.Parse(currencyCut1, System.Globalization.CultureInfo.InvariantCulture) * 10;
            XAU = float.Parse(currencyCut2, System.Globalization.CultureInfo.InvariantCulture) / 10;
            lbl_Gbp.Text = GBP.ToString();
            lbl_Xau.Text = XAU.ToString();
        }

        private void cb_Pairs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_Pairs.Text == "- AUD -" || cb_Pairs.Text == "- CAD -" || cb_Pairs.Text == "- CHF -" || cb_Pairs.Text == "- EUR -" || cb_Pairs.Text == "- GBP -" || cb_Pairs.Text == "- NZD -" || cb_Pairs.Text == "- USD -" || cb_Pairs.Text == "- OTHERS -")
            {
                cb_Pairs.SelectedIndex++;
            }
        }

        private void btn_Calculate_Click(object sender, EventArgs e)
        {
            FX calculate = new FX();
 
            if (cb_Pairs.SelectedIndex == 1 || cb_Pairs.SelectedIndex == 13 || cb_Pairs.SelectedIndex == 22 || cb_Pairs.SelectedIndex == 28 || cb_Pairs.SelectedIndex == 33)
            {
                calculate.Calculate(cb_Pairs, tb_Size05, tb_Size1, tb_Size2, tb_Capital, tb_StopLoss, CAD, chb_Fee, tb_Fee);
            }
            else if (cb_Pairs.SelectedIndex == 2 || cb_Pairs.SelectedIndex == 7 || cb_Pairs.SelectedIndex == 14 || cb_Pairs.SelectedIndex == 23 || cb_Pairs.SelectedIndex == 29 || cb_Pairs.SelectedIndex == 34)
            {
                calculate.Calculate(cb_Pairs, tb_Size05, tb_Size1, tb_Size2, tb_Capital, tb_StopLoss, CHF, chb_Fee, tb_Fee);
            }
            else if (cb_Pairs.SelectedIndex == 3 || cb_Pairs.SelectedIndex == 8 || cb_Pairs.SelectedIndex == 10 || cb_Pairs.SelectedIndex == 17 || cb_Pairs.SelectedIndex == 24 || cb_Pairs.SelectedIndex == 30 || cb_Pairs.SelectedIndex == 35)
            {
                calculate.Calculate(cb_Pairs, tb_Size05, tb_Size1, tb_Size2, tb_Capital, tb_StopLoss, JPY, chb_Fee, tb_Fee);
            }
            else if (cb_Pairs.SelectedIndex == 4 || cb_Pairs.SelectedIndex == 18 || cb_Pairs.SelectedIndex == 25)
            {
                calculate.Calculate(cb_Pairs, tb_Size05, tb_Size1, tb_Size2, tb_Capital, tb_StopLoss, NZD, chb_Fee, tb_Fee);
            }
            else if (cb_Pairs.SelectedIndex == 5 || cb_Pairs.SelectedIndex == 19 || cb_Pairs.SelectedIndex == 26 || cb_Pairs.SelectedIndex == 31)
            {
                calculate.Calculate(cb_Pairs, tb_Size05, tb_Size1, tb_Size2, tb_Capital, tb_StopLoss, USD, chb_Fee, tb_Fee);
            }
            else if (cb_Pairs.SelectedIndex == 12 || cb_Pairs.SelectedIndex == 21)
            {
                calculate.Calculate(cb_Pairs, tb_Size05, tb_Size1, tb_Size2, tb_Capital, tb_StopLoss, AUD, chb_Fee, tb_Fee);
            }
            else if (cb_Pairs.SelectedIndex == 16)
            {
                calculate.Calculate(cb_Pairs, tb_Size05, tb_Size1, tb_Size2, tb_Capital, tb_StopLoss, GBP, chb_Fee, tb_Fee);
            }
            else if (cb_Pairs.SelectedIndex == 15)
            {
                calculate.Calculate(cb_Pairs, tb_Size05, tb_Size1, tb_Size2, tb_Capital, tb_StopLoss, 100, chb_Fee, tb_Fee);
            }
            else if (cb_Pairs.SelectedIndex == 37)
            {
                calculate.Calculate(cb_Pairs, tb_Size05, tb_Size1, tb_Size2, tb_Capital, tb_StopLoss, XAU, chb_Fee, tb_Fee);
            }
        }

        public class FX
        {
            public void Calculate(ComboBox cb_Pairs, TextBox tb_Size05, TextBox tb_Size1, TextBox tb_Size2, TextBox tb_Capital, TextBox tb_StopLoss, float Currency, CheckBox chb_Fee, TextBox tb_Fee)
            {
                try //INPUT FORMAT EXCEPTION
                {
                    float lotSize05 = ((float.Parse(tb_Capital.Text) / float.Parse(tb_StopLoss.Text) / Currency / 100) / 2);
                    float lotSize1 = (float.Parse(tb_Capital.Text) / float.Parse(tb_StopLoss.Text) / Currency / 100);
                    float lotSize2 = ((float.Parse(tb_Capital.Text) / float.Parse(tb_StopLoss.Text) / Currency / 100) * 2);

                    if (chb_Fee.Checked == false) //CALCULATED WITHOUT FEE
                    {
                        if (lotSize1 < 0.001)
                        {
                            tb_Size1.Text = "0";
                        }
                        else
                        {
                            tb_Size1.Text = lotSize1.ToString();
                        }
                        if (lotSize2 < 0.001)
                        {
                            tb_Size2.Text = "0";
                        }
                        else
                        {
                            tb_Size2.Text = lotSize2.ToString();
                        }
                        if (lotSize05 < 0.001)
                        {
                            tb_Size05.Text = "0";
                        }
                        else
                        {
                            tb_Size05.Text = lotSize05.ToString();
                        }

                    }

                    else if (chb_Fee.Checked == true) //CALCULATING WITH FEE
                    {
                        int lotSize05i = (int)lotSize05;
                        int lotSize1i = (int)lotSize1;
                        int lotSize2i = (int)lotSize2;
                        float fee05 = lotSize05i * (float.Parse(tb_Fee.Text));
                        float fee1 = lotSize1i * (float.Parse(tb_Fee.Text));
                        float fee2 = lotSize2i * (float.Parse(tb_Fee.Text));
                        if (lotSize05 < 0.001)
                        {
                            tb_Size05.Text = "0";
                        }
                        else
                        {
                            tb_Size05.Text = (((float.Parse(tb_Capital.Text) / 100 - fee05) / float.Parse(tb_StopLoss.Text) / Currency) / 2).ToString();
                        }
                        if (lotSize1 < 0.001)
                        {
                            tb_Size1.Text = "0";
                        }
                        else
                        {
                            tb_Size1.Text = ((float.Parse(tb_Capital.Text) / 100 - fee1) / float.Parse(tb_StopLoss.Text) / Currency).ToString();
                        }
                        if (lotSize2 < 0.001)
                        {
                            tb_Size2.Text = "0";
                        }
                        else
                        {
                            tb_Size2.Text = (((float.Parse(tb_Capital.Text) / 100 - fee2) / float.Parse(tb_StopLoss.Text) / Currency) * 2).ToString();
                        }
                        //tb_Size05.Text = (((float.Parse(tb_Capital.Text) / 100 - fee05) / float.Parse(tb_StopLoss.Text) / Currency) / 2).ToString();
                        //tb_Size1.Text = ((float.Parse(tb_Capital.Text) / 100 - fee1) / float.Parse(tb_StopLoss.Text) / Currency).ToString();
                        //tb_Size2.Text = (((float.Parse(tb_Capital.Text) / 100 - fee2) / float.Parse(tb_StopLoss.Text) / Currency) * 2).ToString();
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid input format. Please insert numbers only.", "Invalid input format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
