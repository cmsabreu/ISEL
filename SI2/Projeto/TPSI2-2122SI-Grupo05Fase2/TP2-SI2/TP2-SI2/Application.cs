using System;
using System.Collections.Generic;
using TP2SI2;
using DAL.EF;
class Application
{
    private enum Option
    {
        Unknown = -1,
        Exit,
        GetCodeFromTeam,
        CreateInterventionWithProcedure,
        CreateTeam,
        UpdateTeamMembers,
        GetInterByYear,
        CreateIntervention,
        swapSkill
    }
private static Application instance;
    public static Application Instance
    {
        get
        {
            if (instance == null)
                instance = new Application();
            return instance;
        }
        private set { }
    }
    private delegate void DBMethod();
    private Dictionary<Option, DBMethod> routes;
    private Application()
    {
        routes = new Dictionary<Option, DBMethod>();
        Console.WriteLine("Choose the framework.");
        Console.WriteLine("1 - ADO.NET");
        Console.WriteLine("2 - EF");
        var result = Console.ReadLine();

        if(result == "1")
        {
            Services services = new Services();
            routes.Add(Option.GetCodeFromTeam, services.GetCodeFromTeam);
            routes.Add(Option.CreateTeam, services.CreateTeam);
            routes.Add(Option.CreateInterventionWithProcedure, services.CreateInterventionProc);
            routes.Add(Option.GetInterByYear, services.GetInterventionByYear);
            routes.Add(Option.CreateIntervention, services.CreateIntervention);
            routes.Add(Option.UpdateTeamMembers, services.UpdateTeamElems);
        }
        else
        {
            ServicesEF services = new ServicesEF();
            routes.Add(Option.GetCodeFromTeam, services.GetCodeFromTeam);
            routes.Add(Option.CreateTeam, services.CreateTeam);
            routes.Add(Option.CreateInterventionWithProcedure, services.CreateInterventionProc);
            routes.Add(Option.GetInterByYear, services.GetInterventionByYear);
            routes.Add(Option.CreateIntervention, services.CreateIntervention);
            routes.Add(Option.UpdateTeamMembers, services.UpdateTeamElems);
            routes.Add(Option.swapSkill, services.swapSkill);
        }
    }
    private Option DisplayMenu()
    {
        Option option = Option.Unknown;
        try
        {
            Console.WriteLine("Maintain4ver management app");
            Console.WriteLine();
            Console.WriteLine("1. Obtain code of free team"); //1.a.e
            Console.WriteLine("2. Create Intervention with procedure"); //1.a.f
            Console.WriteLine("3. Create Team"); //1.a.g
            Console.WriteLine("4. Update Team Members"); //1.a.h
            Console.WriteLine("5. Get Interventions of Specific Year"); //1.a.i
            Console.WriteLine("6. Create Intervention"); //1.b
            Console.WriteLine("7. Swap Skill - only available if you choosed EF"); //1.b
            Console.WriteLine("0. Exit");
            var result = Console.ReadLine();
            option = (Option)Enum.Parse(typeof(Option), result);
        }
        catch (ArgumentException ex)
        {
            //nothing to do. User select unknown option and press enter.
        }

        return option;

    }
    public void Run()
    {
        Option userInput = Option.Unknown;
        do
        {
            Console.Clear();
            userInput = DisplayMenu();
            Console.Clear();
            try
            {
                routes[userInput]();
                Console.WriteLine("Press any key to return.");
                Console.ReadKey();
            }
            catch (KeyNotFoundException ex)
            {
                //Nothing to do. The option was not a valid one. Read another.
            }
        } while (userInput != Option.Exit);
    }
}
class MainClass
{
    public static void Main(string[] args)
    {
        Application.Instance.Run();
    }
}