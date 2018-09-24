using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            List<string> logs = new List<string>();
            List<string> crs = new List<string>();

            string[] lines = System.IO.File.ReadAllLines(@"C:\crs_pagamento_pendente.txt");

            foreach (string line in lines)
            {
                try
                {
                    using (IWebDriver driver = new ChromeDriver())
                    {
                        driver.Navigate().GoToUrl("https://www2.correios.com.br/sistemas/rastreamento/");

                        IWebElement query = driver.FindElement(By.Name("objetos"));

                        query.SendKeys(line);
                        query.Submit();

                        driver.FindElement(By.Id("formTelaPgto")).Submit();

                        IWebElement username = driver.FindElement(By.Name("username"));
                        username.SendKeys("mundofast");

                        IWebElement password = driver.FindElement(By.Name("password"));
                        password.SendKeys("220100ra");

                        driver.FindElement(By.ClassName("botao-principal")).Submit();


                        //string num = "5162306322396965";
                        //IWebElement numCartao = driver.FindElement(By.Name("numCartao"));
                        //((IJavaScriptExecutor)driver).ExecuteScript(string.Format("document.getElementById('numCartao').value='{0}';", num));
                        //numCartao.Click();

                        IWebElement nomeCartao = driver.FindElement(By.Name("nomeCartao"));
                        nomeCartao.SendKeys("ALEXANDRE AUGUSTO MOSSATO");

                        string data = "06/2019";
                        ((IJavaScriptExecutor)driver).ExecuteScript(string.Format("document.getElementById('expiraCartao').value='{0}';", data));

                        //IWebElement expiraCartao = driver.FindElement(By.Name("expiraCartao"));
                        //expiraCartao.SendKeys("06/2019");

                        IWebElement cvcCartao = driver.FindElement(By.Name("cvcCartao"));
                        cvcCartao.SendKeys("365");

                        driver.FindElement(By.Id("formPayment")).Submit();
                        

                        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(100000));
                    }

                    crs.Add(line);

                }
                catch (WebDriverException ex)
                {
                    logs.Add(ex.Message);
                }
            }

            File.AppendAllLines(@"C:\crs_logs.txt", logs);
            File.AppendAllLines(@"C:\crs_feitos.txt", crs);

            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Form1";
        }

        #endregion
    }
}

