using CodingTracker;
using System.Globalization;

Database.CreateTable();
bool doesAppContinue = true;
do
{
    DisplayMenuScreen();
    int userInput;
    do
    {
        userInput = GetIntegerInput("\nPlease enter your selection(0 - 5): ");

    } while (userInput > 5 || userInput < 0);

    //perform the operation
    switch (userInput)
    {
        case 0:
            ExitProgram();
            break;
        case 1:
            AppController.GetTable();
            doesAppContinue = true;
            break;
        case 2:
            AppController.GetTable();
            ProcessDataForInsertion();
            doesAppContinue = true;
            break;
        case 3:
            AppController.GetTable();
            ProcessDataForUpdate();
            break;
        case 4:
            AppController.GetTable();
            ProcessDataForDeletion();
            break;
        case 5:
            AppController.GetHours();
            break;

            default:
            Console.WriteLine("\nInvalid Input.. The input needs to be between 0 and 5. Try again.");
            break;
    }

} while (doesAppContinue == true);



void ProcessDataForDeletion()
{
    TableColumnHeaders headers = new TableColumnHeaders();
    bool doesIdExist;
    headers.Id = GetIntegerInput("\nPlease enter the ID number to delete associated records: ");
    doesIdExist = AppController.ValidateIDNumberExist(headers);
    if (doesIdExist == false)
    {
        Console.WriteLine("\n\nSelected ID does not exist..");
    }
    else
    {
        AppController.Delete(headers);
        Console.WriteLine("\nRecord succesfully deleted..");
    }
}

void ProcessDataForUpdate()
{
    TableColumnHeaders headers = new();
    bool doesIdExist;
    headers.Id = GetIntegerInput("\nPlease enter the ID number to update associated records: ");
    doesIdExist = AppController.ValidateIDNumberExist(headers);

    if (doesIdExist == false)
    {
        Console.WriteLine("\n\nSelected ID does not exist..");
    }
    else
    {
        string date = GetDateInput("\nPlease Enter the date {format: dd-mm-yy}: ");
        double time = GetHourInput("\nPlease Enter how much time you have spent on coding: ");

        headers.Date = date;
        headers.Hours = time;
        AppController.Update(headers);

        Console.WriteLine("\nRecord Sucessfully Updated..");
    }
    
}

void ProcessDataForInsertion()
{
    TableColumnHeaders headers = new();
    var date = GetDateInput("Please Enter the date {format: dd-mm-yy}: ");
    var time = GetHourInput("Please Enter how much time you have spent on coding: ");

    headers.Date = date;
    headers.Hours = time;

    AppController.Insert(headers);
    Console.WriteLine("\nRecord Successfully Inserted...");
}

static double GetHourInput(string selection)
{
    string? input;
    bool isValidDouble = false;
    double output = 0;
    do
    {
        Console.Write(selection);
        input = Console.ReadLine();

        if (!double.TryParse(input, out output))
        {
            isValidDouble = false;
        }
        else
        {
            isValidDouble = true;
        }

    } while (isValidDouble == false && output > 0);

    return output;
}

static string GetDateInput(string selection)
{
    string? input;
    bool isValidDate = false;
    do
    {
        Console.Write(selection);
        input = Console.ReadLine();
        if (!DateTime.TryParseExact(input, "dd-MM-yy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
        {
            isValidDate = false;
        }
        else
        {
            isValidDate = true;
        }

    } while (isValidDate == false);

    return input;
}

static void ExitProgram()
{
    Console.Write("\n\nHave a great day my friend!");
    Console.ReadLine();
    Environment.Exit(0);
}

int GetIntegerInput(string selection)
{
    int output;
    string? input;
    do
    {
        Console.Write(selection);
        input = Console.ReadLine();

    } while (int.TryParse(input, out output) == false);

    return output;
}

void DisplayMenuScreen()
{
    Console.WriteLine("\nMAIN MENU");
    Console.WriteLine("Type 0 to Close Application\n" +
                      "Type 1 to View All Records.\n" +
                      "Type 2 to Insert Record.\n" +
                      "Type 3 to Update Record.\n" +
                      "Type 4 to Delete Record.\n" +
                      "Type 5 to Show Total Hours.");
}






