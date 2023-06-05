using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

class Program
{
    static void Main(string[] args)
    {
        IWebDriver driver = new FirefoxDriver();
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        driver.Navigate().GoToUrl("http://svyatoslav.biz/testlab/wt/");


        Console.WriteLine();
        bool menu = driver.PageSource.Contains("menu");
        bool banner = driver.PageSource.Contains("banners");
        Console.WriteLine("Check a. The main page contains the words 'menu' and 'banners':");
        Console.WriteLine("Result: " + (menu && banner));
        Console.WriteLine();


        string pageSource = driver.PageSource;
        string searchText = "© CoolSoft by Somebody";
        bool text = pageSource.Contains(searchText);
        Console.WriteLine("Check b. The lower table cell contains the text 'CoolSoft by Somebody':");
        Console.WriteLine("Result: " + text);


        IWebElement height = driver.FindElement(By.Name("height"));
        IWebElement weight = driver.FindElement(By.Name("weight"));
        IWebElement name = driver.FindElement(By.Name("name"));
        IWebElement gender = driver.FindElement(By.Name("gender"));


        bool areTextFieldsEmpty = string.IsNullOrEmpty(height.GetAttribute("value"))
            && string.IsNullOrEmpty(weight.GetAttribute("value"));
        bool isGenderNotSelected = !gender.Selected;
        Console.WriteLine("Check c. By default, all text fields are empty, and the 'Gender' field is not selected:");
        Console.WriteLine("Result: " + (areTextFieldsEmpty && isGenderNotSelected));
        Console.WriteLine();


        height.SendKeys("50");
        weight.SendKeys("3");
        name.SendKeys("Mikita Dubrovin");
        gender.Click();
        IWebElement submitButton = driver.FindElement(By.XPath("//input[@type='submit']"));
        submitButton.Click();


        bool isErrorMessageDisplayed = driver.PageSource.Contains("Слишком большая масса тела");
        Console.WriteLine("Check d. After filling the 'Height' field with '50' and the 'Weight' field with '3' and submitting the form, " +
            "the form disappears, and the message 'Слишком большая масса тела':");
        Console.WriteLine("Result: " + isErrorMessageDisplayed);
        driver.Navigate().Back();
        Console.WriteLine();


        bool isFormPresent = driver.FindElement(By.TagName("form")).Displayed;
        Console.WriteLine("Check e. The main page of the application immediately after opening contains a form with three text fields, " +
            "a group of two radio buttons, and a button:");
        Console.WriteLine("Result: " + isFormPresent);
        driver.Navigate().Refresh();
        height = driver.FindElement(By.Name("height"));
        weight = driver.FindElement(By.Name("weight"));
        submitButton = driver.FindElement(By.XPath("//input[@type='submit']"));
        Console.WriteLine();


        height.Clear();
        weight.Clear();
        submitButton.Click();
        bool isHeightErrorMessageDisplayed = driver.PageSource.Contains("Рост должен быть в диапазоне 50-300 см.");
        bool isWeightErrorMessageDisplayed = driver.PageSource.Contains("Вес должен быть в диапазоне 3-500 кг.");
        Console.WriteLine("Check f. When entering invalid weight and/or height values, error messages appear stating " +
        "that the height should be in the range of '50-300 cm' and the weight should be in the range of '3-500 kg':");
        Console.WriteLine("Результат: " + (isHeightErrorMessageDisplayed && isWeightErrorMessageDisplayed));
        Console.WriteLine();


        string currentDate = DateTime.Now.ToString("dd.MM.yyyy");
        bool isDatePresent = driver.PageSource.Contains(currentDate);
        Console.WriteLine($"Check g. The main page contains the current date in the format {currentDate}:\"");
        Console.WriteLine("Result: " + isDatePresent);
    }
}