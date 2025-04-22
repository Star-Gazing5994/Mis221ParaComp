// 💰 BUDGET TRACKER WITH FINANCIAL COACHING

using System;
using System.Collections.Generic;
using System.Threading;

const int maxExpenses = 100;
string[] expenseLabels = new string[maxExpenses];
double[] expenseAmounts = new double[maxExpenses];
int count = 0;


string userName = "";
double income = 0;
double totalExpenses = 0;
string payPeriod = "monthly ";
double amountPaid = 0;
double loanAmount = 0;

Console.Clear();
Console.WriteLine("Welcome to the Budget Tracker 💸");
Console.Write("What’s your name? ");
userName = Console.ReadLine();

string userInput = GetMenuChoice();
while (userInput != "2")
{
    Route(ref expenseLabels, ref expenseAmounts, userInput, ref count, ref userName, ref amountPaid, ref totalExpenses, ref income, ref payPeriod, ref loanAmount);
    userInput = GetMenuChoice();
}

Goodbye(userName);

// MENU SYSTEM
static void DisplayMenu()
{
    Console.Clear();
    Console.WriteLine("📋 Budget Tracker Menu");
    Console.WriteLine("==============================");
    Console.WriteLine("Enter 1 to start.");
    Console.WriteLine("Enter 2 to exit.");
    Console.WriteLine("==============================");
}

static string GetMenuChoice()
{
    DisplayMenu();
    Console.Write("Enter your choice (1-2): ");
    string choice = Console.ReadLine();
    while (!int.TryParse(choice, out int c) || c < 1 || c > 2)
    {
        Console.WriteLine("❌ Invalid menu choice. Please enter a number from 1 to 2.");
        Console.Write("Try again: ");
        choice = Console.ReadLine();
    }
    return choice;
}

static void Route(ref string[] labels,ref double[] amounts,string userInput, ref int count, ref string userName, ref double amountPaid, ref double totalExpenses,ref double income,ref string payPeriod,ref double loanAmount)
{
    Console.Clear();
    switch (userInput)
    {
        case "1":
            GetIncome(ref amountPaid, ref payPeriod, ref totalExpenses, ref labels, ref amounts, ref count, ref userName, ref income, ref loanAmount);
            break;
    break;
    }
}

// GET INCOME
static void GetIncome(ref double amountPaid, ref string payPeriod, ref double totalExpenses, ref string[] labels, ref double[]amounts, ref int count, ref string userName, ref double income, ref double loanAmount)
{
    Console.Clear();
    Console.Write("Enter your total monthly income: $");
    while (!double.TryParse(Console.ReadLine(), out amountPaid) || amountPaid <= 0)
    {
        Console.Write("❌ Invalid entry. Enter a positive number: $");
    }
    income = amountPaid;
    Console.WriteLine("Income recorded.");
    Thread.Sleep(4000);
    GetPayPeriod(ref payPeriod, ref totalExpenses, ref labels, ref amounts, ref count, ref userName, ref income, ref loanAmount);

}

// GET PAY PERIOD
static void GetPayPeriod(ref string payPeriod, ref double totalExpenses, ref string[] labels, ref double[]amounts, ref int count, ref string userName, ref double income, ref double loanAmount)
{
    Console.Write("How often are you paid? (monthly/bi-weekly/weekly): ");
    string input = Console.ReadLine().ToLower();
    if (input == "monthly" || input == "bi-weekly" || input == "weekly")
        payPeriod = input;
    else
    {
        Console.WriteLine("Invalid entry. Defaulting to monthly.");
        payPeriod = "monthly";
    }
    Thread.Sleep(4000);
    TrackExpenses(ref totalExpenses, ref labels, ref amounts, ref count,  ref userName, ref income, ref loanAmount, ref payPeriod);
}

// TRACK EXPENSES
static void TrackExpenses(ref double totalExpenses, ref string[] labels, ref double[] amounts, ref int count, ref string userName, ref double income, ref double loanAmount, ref string payPeriod)
{
    string more = "y";
    while (more.ToLower() == "y" && count < labels.Length)
    {
        Console.Write("Enter an expense label (Ex: Rent, Utilities,etc.): ");
        labels[count] = Console.ReadLine();

        Console.Write("Enter amount for this expense: $");
        amounts[count] = double.Parse(Console.ReadLine());

        count++;

        Console.Clear();
        Console.Write("Add another expense? (y/n): ");
        more = Console.ReadLine();
    }
    
    Thread.Sleep(5000);
    ShowReport(ref labels, ref amounts, count, ref totalExpenses, ref userName, ref income, ref loanAmount, ref payPeriod);

}

//********************* SHOW REPORT *********************//
static void ShowReport(ref string[] labels, ref double[] amounts, int count, ref double totalExpenses, ref string userName, ref double income, ref double loanAmount, ref string payPeriod)
{
    Console.Clear();
    totalExpenses = 0;
    Console.WriteLine($"🧾 Budget Summary for {userName}");
    Console.WriteLine("---------------------------------");
    for (int i = 0; i < count; i++)
    {
    int bars = (int)(amounts[i] / 10);
    Console.Write($"{labels[i]}: ${amounts[i],6} ");
    totalExpenses += amounts[i];
    Console.WriteLine(new string('█', bars));
    }

    Console.WriteLine("---------------------------------");
    Console.WriteLine($"Total Expenses: ${totalExpenses}");
    Console.WriteLine($"Remaining Balance: ${income - totalExpenses}");
    Thread.Sleep(5000);
    Console.Clear();

    OfferAdvice(ref totalExpenses, ref income, ref payPeriod, ref loanAmount, ref labels, ref amounts, ref count);
}

// *******************************OFFER ADVICE**************************//
static void OfferAdvice(ref double totalExpenses, ref double income, ref string payPeriod, ref double loanAmount, ref string[] labels,ref double[] amounts,ref int count)
{
    Console.WriteLine("💡 Coaching Tips:");
    double ratio = totalExpenses / income;
    if (ratio > 0.9)
        Console.WriteLine("You’re spending over 90% of your income. Consider cutting discretionary costs.");
    else if (ratio > 0.7)
        Console.WriteLine("You have some breathing room. Try allocating more toward savings.");
    else
        Console.WriteLine("Great job! You’re saving a healthy portion of your income.");

    double emergencyFund = income * 3;
    Console.WriteLine($"Aim to build an emergency fund of ${emergencyFund} (3x your income).");

    Console.WriteLine("🗓️ Recommended Savings per Paycheck:");
    double saveRate = 0.20;
    if (payPeriod == "weekly")
        Console.WriteLine($"Weekly: Save around ${income * saveRate / 4} per week.");
    else if (payPeriod == "bi-weekly")
        Console.WriteLine($"Bi-Weekly: Save around ${income * saveRate / 2} every two weeks.");
    else
        Console.WriteLine($"Monthly: Save at least ${income * saveRate} each month.");

    Console.WriteLine("📈 Smart Account Goals:");
    Console.WriteLine($"High-Yield Savings: Deposit ${income * 0.10} into a HYSA.");
    Console.WriteLine("Roth IRA: Contribute up to $500/month if possible.");

    System.Console.WriteLine("Would you like to save your budget or route to other options? Type : 'y' for Yes or 'n' for No");
    string userInput = Console.ReadLine();
    switch (userInput)
    {
        case "y":
        SaveBudget(ref totalExpenses, ref loanAmount,ref labels,ref amounts,ref count,ref income, ref payPeriod);
        break;
        case "n":
        Thread.Sleep(5000);
        RerouteMenu(ref totalExpenses, ref income, ref loanAmount);
        break;
        default:
        System.Console.WriteLine("Invalid entry, try again.");
        break;
    }
}

// ***************************SAVINGS PROJECTION**********************//
static void SavingsProjection(ref double totalExpenses, ref double income, ref double loanAmount)
{
    Console.Write("What age are you starting your savings? ");
    int startAge = int.Parse(Console.ReadLine());
    Console.Write("What age would you like to stop saving? ");
    int endAge = int.Parse(Console.ReadLine());

    int years = endAge - startAge;
    if (years <= 0)
    {
        Console.WriteLine("⛔ Invalid range. Must be saving over time.");
        return;
    }

    double totalSaved = income * 0.20 * 12 * years;
    Console.WriteLine($"If you save 20% of your income annually for {years} years...");
    Console.WriteLine($"You could save: ${totalSaved} by age {endAge}.");

    Thread.Sleep(5000);
    RerouteMenu(ref totalExpenses, ref income, ref loanAmount);
}
//**************** SAVING BUDGET *******************//
static void SaveBudget(ref double totalExpenses, ref double loanAmount,ref string[] labels,ref double[] amounts,ref int count,ref double income,ref string payPeriod)
{
    using (StreamWriter writer = new StreamWriter("budget.txt"))
    {
        writer.WriteLine(income);
        writer.WriteLine(payPeriod);
        writer.WriteLine(count);
        for (int i = 0; i < count; i++)
        {
            writer.WriteLine($"{labels[i]}|{amounts[i]}");
        }
    }
    Console.WriteLine("✅ Budget saved to 'budget.txt'.");
    Thread.Sleep(4000);
    LoadBudget(ref totalExpenses, ref loanAmount,ref labels, ref amounts, ref count, ref income, ref payPeriod);

}

static void LoadBudget(ref double totalExpenses, ref double loanAmount, ref string[] labels, ref double[] amounts, ref int count, ref double income, ref string payPeriod)
{
    if (!File.Exists("budget.txt"))
    {
        Console.WriteLine("⚠️ No saved file found.");
        return;
    }

    using (StreamReader reader = new StreamReader("budget.txt"))
    {
        income = double.Parse(reader.ReadLine());
        payPeriod = reader.ReadLine();
        count = int.Parse(reader.ReadLine());

        for (int i = 0; i < count; i++)
        {
            string[] parts = reader.ReadLine().Split('|');
            labels[i] = parts[0];
            amounts[i] = double.Parse(parts[1]);
        }
    }

    Console.WriteLine("✅ Budget loaded from file.");
    Thread.Sleep(4000);
    RerouteMenu(ref totalExpenses, ref income, ref loanAmount);
}

//************************* RETIREMENT PLANNING******************/
static void RetirementPlanning(ref double totalExpenses, ref double income, ref double loanAmount)
{
    System.Console.WriteLine($"You spend {totalExpenses} in monthly expenses.");
    Console.Write("What is your average monthly spending for leisure: $");
    double monthly = double.Parse(Console.ReadLine());
    double total = totalExpenses + monthly;
    double needed = total * 12 * 25;
    Console.WriteLine($"To retire, you should aim for: ${needed} saved.");
    System.Console.WriteLine("You will have enough money to cover your monthly expenses for a total of 25 years after you retire.");
    Thread.Sleep(5000);
    RerouteMenu(ref totalExpenses, ref income, ref loanAmount);
}

//*************************** INVESTMENT ADVICE**********************//
static void InvestmentAdvice(ref double totalExpenses, ref double income, ref double loanAmount)
{
    string filePath = "market_data.txt";
    List<double> rates = new List<double>();    
    double averageRate = 0;

    try
    {
        string[] lines = File.ReadAllLines(filePath);
        foreach (string line in lines)
        {
            if (line.StartsWith("Year")) continue; // Skip header
            string[] parts = line.Split('#');
            if (parts.Length == 2 && double.TryParse(parts[1], out double rate))
            {
                rates.Add(rate);
                System.Console.WriteLine(rate);

            }
        }

        if (rates.Count > 0)
        {
            averageRate = rates.Average();
        }
        else
        {
            Console.WriteLine("⚠️ No valid data found. Defaulting to 10% average return.");
            averageRate = 0.10;
        }
    }
    catch (Exception ex)
     {
        Console.WriteLine($"⚠️ Error reading market data: {ex.Message}");
        Console.WriteLine("Using default 10% projection rate.");
        averageRate = 0.10;
    }

    Console.WriteLine($"\n📊 Based on market data, the average annual return over 20 years is: {averageRate * 100}%");

    Console.Write("How much can you invest monthly in SPY or another index fund? $");
    double monthlyInvestment = double.Parse(Console.ReadLine());

    Console.Write("How many years do you want to invest for? ");
    double years = double.Parse(Console.ReadLine());

    double futureValue = 0;
    for (int i = 1; i <= years * 12; i++)
    {
        futureValue = (futureValue + monthlyInvestment) * (1 + averageRate / 12);
    }

    Console.WriteLine($"\n📈 If you invest ${monthlyInvestment}/month for {years} years at an average annual return of {averageRate * 100}%:");
    Console.WriteLine($"💰 Your investments could grow to about: ${futureValue}\n");

    Thread.Sleep(5000);
    RerouteMenu(ref totalExpenses, ref income, ref loanAmount);
}



// SAVINGS GOAL PLANNING
static void SavingsGoalPlanning(ref double totalExpenses, ref double income, ref double loanAmount)
{
    Console.Write("What are you saving for? ");
    string goal = Console.ReadLine();
    Console.Write("Goal amount: $");
    double amount = double.Parse(Console.ReadLine());
    Console.Write("How many months do you plan to save for? ");
    int months = int.Parse(Console.ReadLine());

    double surplus = income - totalExpenses;
    double monthly = amount / months;

    if (monthly > surplus)
    {
        Console.WriteLine("⚠️ Not enough surplus. Consider reducing timeline or expenses.");
    }
    else
    {
        Console.WriteLine($"✅ Save ${monthly}/month to reach your goal.");
    }

    Thread.Sleep(5000);
    RerouteMenu(ref totalExpenses, ref income, ref loanAmount);
}

// LOAN ELIGIBILITY
static void AnalyzeLoanEligibility(ref double totalExpenses, ref double loanAmount, ref double income)
{
    Console.Write("What is your Credit score: ");
    double credit = double.Parse(Console.ReadLine());
    Console.Write("Annual income: $");
    double annual = double.Parse(Console.ReadLine());
    Console.Write("Interest rate (enter a whole number to calculate the percent): ");
    double rate = double.Parse(Console.ReadLine()) / 100;

    Console.Write("What is your desired loan amount: $");
    loanAmount = double.Parse(Console.ReadLine());

    double monthly = (loanAmount * (rate / 12)) / (1 - Math.Pow(1 + (rate / 12), -60));
    double maxPayment = (income - totalExpenses) * 0.4;

    Console.Write($"Estimated monthly payment: ${monthly}");
    if (credit >= 650 && annual >= 25000 && monthly <= maxPayment){
        Console.Clear();
        System.Console.WriteLine("✅ You may be eligible for a loan.");
    }
    else{
        Console.Clear();
        Console.WriteLine("❌ Loan approval may be difficult. Consider improving your score or income.");
        }

    Thread.Sleep(5000);
    RerouteMenu(ref totalExpenses, ref income, ref loanAmount);
}

// *********************** REROUTING MENU **************************//

static void RerouteMenu(ref double totalExpenses, ref double income, ref double loanAmount){
Console.Clear();
    System.Console.WriteLine("Would you like to \n1.) Monitor your savings projection.\n2.) Start retirement planning.\n3.) Get investment advice.\n4.) Start savings goal planning. \n5.) Consider getting a loan?\n6.) To return to the Main Menu and start all over again.");
            int reroute = int.Parse(Console.ReadLine());
            switch(reroute){
        case 1:
            Console.WriteLine("Redirecting to Savings Projection...");
            Thread.Sleep(2500);
            Console.Clear();
            SavingsProjection(ref totalExpenses, ref income, ref loanAmount);
            break;
        case 2:
            Console.WriteLine("Redirecting to Retirement Planning...");
            Thread.Sleep(2500);
            Console.Clear();
            RetirementPlanning(ref totalExpenses, ref income, ref loanAmount);
            break;
        case 3:
            Console.WriteLine("Redirecting to Investment Advice...");
            Thread.Sleep(2500);
            Console.Clear();
            InvestmentAdvice(ref totalExpenses, ref income, ref loanAmount);
            break;
        case 4:
            Console.WriteLine("Redirecting to Savings Goal Setup...");
            Thread.Sleep(2500);
            Console.Clear();
            SavingsGoalPlanning(ref totalExpenses, ref income, ref loanAmount);
            break;
        case 5:
            Console.WriteLine("Redirecting to Loan Eligibility...");
            Thread.Sleep(2500);
            Console.Clear();
            AnalyzeLoanEligibility(ref totalExpenses, ref loanAmount, ref income);
            break;
        case 6:
            Console.WriteLine("Redirecting to Main Menu...");
            Thread.Sleep(2500);
            Console.Clear();
            DisplayMenu();
            break;
        default:
                System.Console.WriteLine("Invalid entry, try again.");
                break;
            }
}

static void Goodbye(string name)
{
    Console.Clear();
    Console.WriteLine($"Thanks for using the Budget Tracker, {name}! 💼");
    string[] quotes = {
    "“Don’t save what is left after spending, but spend what is left after saving.” – Warren Buffett",
    "“A budget is telling your money where to go instead of wondering where it went.” – Dave Ramsey",
    "“Money looks better in the bank than on your feet.” – Sophia Amoruso"
    };
    Console.WriteLine("💬 Money Tip: " + quotes[new Random().Next(quotes.Length)]);

}
