using ConsoleTableExt;
using System.Data.SQLite;
using CodingTracker;

public class TableVisualization
{
    internal static void ShowTable(List<CodingSession> tableData)
    {
        Console.WriteLine("\n");
        ConsoleTableBuilder
        .From(tableData)
        .WithTitle("CodingTracker")
        .ExportAndWriteLine(TableAligntment.Left);
        Console.WriteLine(" ");
    }
}