using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using TP2SI2.concrete;
using TP2SI2.model;

namespace TP2SI2
{
    public class Services
    {
        public Services() { }
        //para usar local BD
        //private static Context ctx = new Context(ConfigurationManager.ConnectionStrings["localhost"].ConnectionString);
        private static Context ctx = new Context(ConfigurationManager.ConnectionStrings["SI2ServerADO"].ConnectionString);

        //1.a.e
        public void GetCodeFromTeam()
        {
            Console.WriteLine("Get a code of an available team");
            Console.WriteLine("");
            Console.Write("Please Enter a description.");
            var description = Console.ReadLine();
            InterventionMapper mapper = new InterventionMapper(ctx);
            int? intervention_code = mapper.GetCodeFromTeam(Convert.ToString(description));
            Console.WriteLine("Intervention code: " + intervention_code);
        }
        //1.a.f
        public void CreateInterventionProc()
        {

            Console.WriteLine("Create Intervention");
            Console.WriteLine("Insert intervention description");
            var description = Console.ReadLine();
            Console.WriteLine("Insert intervention price");
            var price = Console.ReadLine();
            Console.WriteLine("Insert intervention start date");
            var startDate = Console.ReadLine();
            Console.WriteLine("Insert intervention end date");
            var endDate = Console.ReadLine();
            Console.WriteLine("Insert intervention frequency");
            var frequency = Console.ReadLine();
            Console.WriteLine("Insert intervention asset");
            var assetId = Console.ReadLine();
            Console.WriteLine("Insert skill required for intervention");
            var skill = Console.ReadLine();

            Intervention intervention = new Intervention();
            intervention.Description = Convert.ToString(description);
            intervention.Price = Convert.ToDecimal(price);
            intervention.StartDate = Convert.ToDateTime(startDate);
            intervention.EndDate = String.IsNullOrEmpty(endDate) ? null : Convert.ToDateTime(endDate);

            AssetMapper assetMapper = new AssetMapper(ctx);
            Asset asset = assetMapper.Read(Convert.ToInt32(assetId));
            if (asset == null)
            {
                Console.WriteLine("Please enter valid asset");
            }
            intervention.AssetId = asset;

            InterventionMapper interventionMapper = new InterventionMapper(ctx);


            interventionMapper.CreateWithProcedure(intervention,
                String.IsNullOrEmpty(frequency) ? null : Convert.ToInt32(frequency), skill);

            Console.WriteLine("Intervention created successfully");


        }
        //1.a.g
        public void CreateTeam()
        {
            Console.WriteLine("Create Team");
            Console.WriteLine("Enter Team Location.");
            var location = Console.ReadLine();
            Console.WriteLine("Enter ssn of supervisor.");
            var supervisor = Console.ReadLine();

            Console.WriteLine("Nº elements to add:");

            int n_elements = Convert.ToInt32(Console.ReadLine());

            List<int> ssn_list = new List<int>();
            while (n_elements > 0)
            {
                Console.WriteLine("Enter Team Members ssn.");
                var teamMembers = Console.ReadLine();
                ssn_list.Add(Convert.ToInt32(teamMembers));
                n_elements--;
            }
            MaintenanceTeamMapper maintenanceTeamMapper = new MaintenanceTeamMapper(ctx);
            maintenanceTeamMapper.CreateTeam(Convert.ToString(location), Convert.ToInt32(supervisor), ssn_list);

        }

        //1.a.h
        public void UpdateTeamElems()
        {
            Console.WriteLine("Update elements from a team");
            Console.WriteLine("Insert team code");
            var teamCode = Console.ReadLine();
            Console.WriteLine("It is to delete or to update? Please insert delete or update");
            var toDelete = Console.ReadLine();
            if (!(toDelete != "update" || toDelete != "delete"))
            {
                throw new Exception("Invalid option");
            }
            Console.WriteLine("Nº elements to update information:");

            int n_elements = Convert.ToInt32(Console.ReadLine());

            List<int> ssn_list = new List<int>();
            while (n_elements > 0)
            {
                Console.WriteLine("Enter Team Members ssn.");
                var teamMembers = Console.ReadLine();
                ssn_list.Add(Convert.ToInt32(teamMembers));
                n_elements--;
            }

            TeamMemberMapper mapper = new TeamMemberMapper(ctx);

            mapper.UpdateTeam(Convert.ToInt32(teamCode), toDelete == "delete" ? 1 : 0, ssn_list);
            Console.WriteLine("Update done successfully!");


        }
        //1.a.i
        public void GetInterventionByYear()
        {
            Console.Write("Please Enter Year.");
            var year = Console.ReadLine();
            InterventionMapper mapper = new InterventionMapper(ctx);
            List<Intervention> list = mapper.ListInterventionYear(Convert.ToInt32(year));

            foreach (Intervention intervention in list)
            {
                Console.WriteLine("-------------------------------");
                Console.WriteLine("intervention_code: " + intervention.InterventionCode);
                Console.WriteLine("price: " + intervention.Price);
                Console.WriteLine("description: " + intervention.Description);
                Console.WriteLine("-------------------------------");
            }
        }
        //1.b
        public void CreateIntervention()
        {
            Console.WriteLine("Create Intervention");
            Console.WriteLine("Insert intervention description");
            var description = Console.ReadLine();
            Console.WriteLine("Insert intervention price");
            var price = Console.ReadLine();
            Console.WriteLine("Insert intervention start date");
            var startDate = Console.ReadLine();
            Console.WriteLine("Insert intervention end date");
            var endDate = Console.ReadLine();
            Console.WriteLine("Insert intervention frequency");
            var frequency = Console.ReadLine();
            Console.WriteLine("Insert intervention asset");
            var assetId = Console.ReadLine();
            Console.WriteLine("Insert skill required for intervention");
            var skill = Console.ReadLine();


            Intervention intervention = new Intervention();
            intervention.Description = Convert.ToString(description);
            intervention.Price = Convert.ToDecimal(price);
            intervention.StartDate = Convert.ToDateTime(startDate);
            intervention.EndDate = String.IsNullOrEmpty(endDate) ? null : Convert.ToDateTime(endDate);

            AssetMapper assetMapper = new AssetMapper(ctx);
            Asset asset = assetMapper.Read(Convert.ToInt32(assetId));
            if (asset == null)
            {
                Console.WriteLine("Please enter valid asset");
            }
            intervention.AssetId = asset;
            InterventionMapper interventionMapper = new InterventionMapper(ctx);
            try
            {
                intervention = interventionMapper.Create(intervention,
                String.IsNullOrEmpty(frequency) ? null : Convert.ToInt32(frequency), skill);
                Console.WriteLine("Intervention created successfully with intervention id: " + intervention.InterventionCode);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}