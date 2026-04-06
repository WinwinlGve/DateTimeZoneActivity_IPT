using System;
using System.Collections.Generic;

namespace EmployeeTimeTracker
{
    // --- 1. ABSTRACTION & ENCAPSULATION ---
    // This handles the math rules for every shift
    public abstract class ShiftCalculator
    {
        protected const double StandardShift = 9.0;

        public abstract double CalculateHours(DateTime start, DateTime end);

        public string GenerateNote(double actualHours)
        {
            if (actualHours < StandardShift)
                return $"Early Out. Hours left: {StandardShift - actualHours} hours";
            
            if (actualHours > StandardShift)
                return $"Overtime. Hours extended: {actualHours - StandardShift} hours";

            return "Standard Shift Completed";
        }
    }

    // --- 2. INHERITANCE ---
    // This class "inherits" from the calculator to do the actual math
    public class LogManager : ShiftCalculator
    {
        public override double CalculateHours(DateTime start, DateTime end)
        {
            return (end - start).TotalHours;
        }
    }

    // --- 3. DATA STRUCTURE ---
    public class EmployeeRecord
    {
        public string EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public string OfficeLocation { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public double TotalHours { get; set; }
        public string Note { get; set; }
    }

    class Program
    {
        // Use the exact Dictionary name requested
        static Dictionary<string, EmployeeRecord> EmployeeTimeinTimeOutRecord = new Dictionary<string, EmployeeRecord>();
        static LogManager manager = new LogManager();

        static void Main(string[] args)
        {
            // --- INPUT ---
            Console.Write("Employee Number: ");
            string empId = Console.ReadLine();

            Console.Write("Office Location (Philippines/United States/India): ");
            string location = Console.ReadLine();

            // --- LOGIC ---
            // Using DateTime.Now is simpler than manual conversion for a basic tracker
            DateTime currentTime = DateTime.Now;

            EmployeeRecord newRecord = new EmployeeRecord
            {
                EmployeeNumber = empId,
                EmployeeName = "John Doe", // Sample Name
                OfficeLocation = location,
                TimeIn = currentTime,
                // Simulate a 7-hour shift for the example
                TimeOut = currentTime.AddHours(7) 
            };

            // Calculate hours and notes using our classes
            newRecord.TotalHours = manager.CalculateHours(newRecord.TimeIn, newRecord.TimeOut);
            newRecord.Note = manager.GenerateNote(newRecord.TotalHours);

            // --- SAVE TO DICTIONARY ---
            EmployeeTimeinTimeOutRecord[empId] = newRecord;

            // --- OUTPUT ---
            Console.WriteLine("\n--- Employee Log ---");
            Console.WriteLine($"ID:       {newRecord.EmployeeNumber}");
            Console.WriteLine($"Location: {newRecord.OfficeLocation}");
            Console.WriteLine($"Time-In:  {newRecord.TimeIn:MM/dd/yy hh:mm tt}");
            Console.WriteLine($"Note:     {newRecord.Note}");

            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }
    }
}