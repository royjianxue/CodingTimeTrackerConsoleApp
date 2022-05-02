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
            break;
        case 2:
            AppController.GetTable();
            ProcessDataForInsertion();
            Console.Clear();
            break;
        case 3:
            AppController.GetTable();
            ProcessDataForUpdate();
            Console.Clear();
            break;
        case 4:
            AppController.GetTable();
            ProcessDataForDeletion();
            Console.Clear();
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
    CodingSession headers = new CodingSession();
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
    CodingSession headers = new();
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
        TimeSpan duration = GetDuration();
        headers.Date = date;
        headers.Hours = Math.Round(duration.TotalHours, 2);
        AppController.Update(headers);

        Console.WriteLine("\nRecord Sucessfully Updated..");
    }
    
}

void ProcessDataForInsertion()
{
    CodingSession headers = new();
    var date = GetDateInput("\nPlease Enter the date {format: dd-mm-yy}: ");
    var duration = GetDuration();

    headers.Date = date;
    headers.Hours = Math.Round(duration.TotalHours, 2);

    AppController.Insert(headers);
    Console.WriteLine("\nRecord Successfully Inserted...");
}

TimeSpan GetDuration()
{

    TimeSpan duration;
    string? startTime;
    string? endTime;
    bool allConditionChecked = false;
    do
    {
        do
        {
            Console.Write("\nPlease enter the start time: Format: {hh:mm}: ");
            startTime = Console.ReadLine();

        } while (!IsTimeSpan(startTime));

        do
        {
            Console.Write("\nPlease enter the end time: Format: {hh:mm}: ");
            endTime = Console.ReadLine();

        } while (!IsTimeSpan(endTime));

        duration = DateTime.Parse(endTime).Subtract(DateTime.Parse(startTime));

        if (duration.TotalHours >=0)
        {
            allConditionChecked = true;
        }
        else
        {
            Console.WriteLine("\nEnd Time must be Greater than Start Time. Try Again!");
            allConditionChecked =false;
        }

    } while (allConditionChecked == false);

    return duration;
}

bool IsTimeSpan(string time)
{
    if (TimeSpan.TryParseExact(time, "hh\\:mm", CultureInfo.InvariantCulture, out _))  
    {
        return true;
    }
    else
    {
        Console.WriteLine("\nTime entry format is invalid. Please try again!");
        return false;
    }

}

static string GetDateInput(string selection)
{
    string? input;
    DateTime date;
    bool isValidDateFormat = false;
    bool isValidDate = false;
    do
    {
        do
        {
            Console.Write(selection);
            input = Console.ReadLine();
            if (!DateTime.TryParseExact(input, "dd-MM-yy", new CultureInfo("en-US"), DateTimeStyles.None, out date))
            {
                isValidDateFormat = false;
            }
            else
            {
                isValidDateFormat = true;
            }

        } while (isValidDateFormat == false);

        if (DateTime.Compare(date, DateTime.Now) > 0)
        {
            Console.WriteLine("\nDate entered must not be in the future. Please try again!");
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






