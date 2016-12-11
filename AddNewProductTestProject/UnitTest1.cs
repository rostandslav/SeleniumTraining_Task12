using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using System.Linq;
using System.IO;


namespace AddNewProductTestProject
{
    [TestClass]
    public class UnitTest1
    {
        private IWebDriver driver;
        private WebDriverWait wait;


        [TestInitialize]
        public void Init()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
        }


        private const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnoprstuvwxyz";
        private const string numbers = "0123456789";
        private const string alphanumbers = alphabet + numbers;
        private static Random random = new Random();

        private static string RandomString(string str, int length)
        {
            return new string(Enumerable.Repeat(str, length).Select(s => s[random.Next(s.Length)]).ToArray());

        }

        private static string RandomAlphabetString(int length)
        {
            return RandomString(alphabet, length);
        }

        private static string RandomNumbersString(int length)
        {
            return RandomString(numbers, length);
        }

        private static string RandomAlphanumbersString(int length)
        {
            return RandomString(alphanumbers, length);
        }


        private void SelectElementByText(By by, string element)
        {
            SelectElement select = new SelectElement(driver.FindElement(by));
            select.SelectByText(element);
        }


        private void SetCheckBoxState(IWebElement checkBox, bool needState)
        {
            string currentState = checkBox.GetAttribute("checked");
            if (Convert.ToBoolean(currentState) != needState)
            {
                checkBox.Click();
            }
        }


        [TestMethod]
        public void AddNewProduct()
        {
            driver.Url = "http://litecart/admin/";

            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            string catalogSelector = "a[href='http://litecart/admin/?app=catalog&doc=catalog']";
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(catalogSelector)));
            driver.FindElement(By.CssSelector(catalogSelector)).Click();

            string newProductSelector = "a[href='http://litecart/admin/?category_id=0&app=catalog&doc=edit_product']";
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(newProductSelector)));
            driver.FindElement(By.CssSelector(newProductSelector)).Click();



            string generalTabSelector = "#content a[href='#tab-general']";
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(generalTabSelector)));
            driver.FindElement(By.CssSelector(generalTabSelector)).Click();

            string generalSelector = "#content #tab-general table";
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(generalSelector)));


            const int statusDisabled = 0;
            const int statusEnabled = 1;
            int currentStatus = statusEnabled;

            string name = RandomAlphabetString(9);

            string code = RandomNumbersString(7);

            bool isRoot = false;
            bool isRubberDucks = true;
            bool isSubcategory = false;
            string defaultCategory = "Rubber Ducks";

            bool isFemale = false;
            bool isMale = true;
            bool isUnisex = false;

            int quantity = 55;
            string quantityUnit = "pcs";
            string deliveryStatus = "3-5 days";
            string soldOutStatus = "Sold out";

            //string pathToImage = AppDomain.CurrentDomain.BaseDirectory + "\\..\\..\\images\\goose.jpg";
            string pathToImage = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\images\\goose.jpg";

            string dateValidFrom = "22082016";
            string dateValidTo = "05122016";


            driver.FindElement(By.CssSelector("input[name='status'][value='" + currentStatus.ToString() + "']")).Click();

            driver.FindElement(By.CssSelector("input[name='name[en]']")).SendKeys(name);
            driver.FindElement(By.CssSelector("input[name='code']")).SendKeys(code);

            IWebElement rootCheckBox = driver.FindElement(By.CssSelector("input[data-name='Root']"));
            SetCheckBoxState(rootCheckBox, isRoot);
            IWebElement rubberDucksCheckBox = driver.FindElement(By.CssSelector("input[data-name='Rubber Ducks']"));
            SetCheckBoxState(rubberDucksCheckBox, isRubberDucks);
            IWebElement subcategoryCheckBox = driver.FindElement(By.CssSelector("input[data-name='Subcategory']"));
            SetCheckBoxState(subcategoryCheckBox, isSubcategory);
            SelectElementByText(By.CssSelector("select[name='default_category_id']"), defaultCategory);

            IWebElement femaleCheckBox = driver.FindElement(By.CssSelector("input[name='product_groups[]'][value='1-2']"));
            SetCheckBoxState(femaleCheckBox, isFemale);
            IWebElement maleCheckBox = driver.FindElement(By.CssSelector("input[name='product_groups[]'][value='1-1']"));
            SetCheckBoxState(maleCheckBox, isMale);
            IWebElement unisexCheckBox = driver.FindElement(By.CssSelector("input[name='product_groups[]'][value='1-3']"));
            SetCheckBoxState(unisexCheckBox, isUnisex);

            driver.FindElement(By.CssSelector("input[name='quantity']")).Clear();
            driver.FindElement(By.CssSelector("input[name='quantity']")).SendKeys(quantity.ToString());

            SelectElementByText(By.CssSelector("select[name='quantity_unit_id']"), quantityUnit);
            SelectElementByText(By.CssSelector("select[name='delivery_status_id']"), deliveryStatus);
            SelectElementByText(By.CssSelector("select[name='sold_out_status_id']"), soldOutStatus);

            driver.FindElement(By.CssSelector("input[name='new_images[]']")).SendKeys(pathToImage);

            driver.FindElement(By.CssSelector("input[name='date_valid_from']")).SendKeys(dateValidFrom);
            driver.FindElement(By.CssSelector("input[name='date_valid_to']")).SendKeys(dateValidTo);



            string informationTabSelector = "#content a[href='#tab-information']";
            driver.FindElement(By.CssSelector(informationTabSelector)).Click();

            string informationSelector = "#content #tab-information table";
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(informationSelector)));


            string manufacturer = "ACME Corp.";
            string supplier = "NewSupplier";

            string keywords = RandomAlphabetString(6) + ", " + RandomAlphabetString(6);
            string shortDescription = RandomAlphabetString(8) + " " + RandomAlphabetString(8);
            string description = RandomAlphabetString(5) + " " + RandomAlphabetString(5) + " " + RandomAlphabetString(5);

            string headTitle = RandomAlphabetString(7);
            string metaDescription = RandomAlphabetString(5);


            SelectElementByText(By.CssSelector("select[name='manufacturer_id']"), manufacturer);
            SelectElementByText(By.CssSelector("select[name='supplier_id']"), supplier);

            driver.FindElement(By.CssSelector("input[name='keywords']")).SendKeys(keywords);
            driver.FindElement(By.CssSelector("input[name='short_description[en]']")).SendKeys(shortDescription);
            driver.FindElement(By.CssSelector("div.trumbowyg-editor")).SendKeys(description);

            driver.FindElement(By.CssSelector("input[name='head_title[en]']")).SendKeys(headTitle);
            driver.FindElement(By.CssSelector("input[name='meta_description[en]']")).SendKeys(metaDescription);



            string pricesTabSelector = "#content a[href='#tab-prices']";
            driver.FindElement(By.CssSelector(pricesTabSelector)).Click();

            string pricesSelector = "#content #tab-prices table";
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(pricesSelector)));


            double purchasePrice = 1.23;
            string priceCurrency = "Euros";

            //string taxClasss = "";

            double priceUSD = 1.01;
            double priceEUR = purchasePrice;


            driver.FindElement(By.CssSelector("input[name='purchase_price'")).Clear();
            driver.FindElement(By.CssSelector("input[name='purchase_price'")).SendKeys(purchasePrice.ToString());
            SelectElementByText(By.CssSelector("select[name='purchase_price_currency_code']"), priceCurrency);

            //SelectElementByText(By.CssSelector("select[name='tax_class_id']"), taxClasss);

            driver.FindElement(By.CssSelector("input[name='prices[USD]'")).SendKeys(priceUSD.ToString());
            driver.FindElement(By.CssSelector("input[name='prices[EUR]'")).SendKeys(priceEUR.ToString());



            driver.FindElement(By.CssSelector("button[name='save'")).Click(); // [Save]



            string catalogTableSelector = "form[name=catalog_form] table.dataTable";
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(catalogTableSelector)));

            var products = driver.FindElements(By.CssSelector(catalogTableSelector + " tr.row td:nth-child(3)"));
            int productsCount = products.Count;

            bool isAddedProductExists = false;
            for (int i = 0; i < productsCount; i++)
            {
                if ( products[i].Text == name )
                {
                    isAddedProductExists = true;
                    break;
                }
            }

            Assert.IsTrue(isAddedProductExists); 
        }


        [TestCleanup]
        public void Finish()
        {
            driver.Quit();
            //driver = null;
        }
    }
}
