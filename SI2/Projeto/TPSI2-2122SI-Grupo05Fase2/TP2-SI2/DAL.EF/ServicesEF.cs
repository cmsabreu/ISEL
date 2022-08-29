using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace DAL.EF
{
    //alinea 2
    public class ServicesEF
    {
        public ServicesEF() { }
        //2.1.a
        public void GetCodeFromTeam()
        {
            Console.WriteLine("Get Intervention based on skill");
            Console.WriteLine("Please Enter the skill");
            var skill = Console.ReadLine();

            try
            {
                using (var ctx = new L51DG5Entities())
                {

                    IQueryable<get_code_from_team_Result> team_result = ctx.get_code_from_team(skill);
                    var list = team_result.Select(team => new
                    {
                        team.team_code,
                        team.location,
                        team.n_elements,
                        team.supervisor
                    }).ToList();
                    int? team_code = null;
                    list.ForEach(team =>
                    {
                        Console.WriteLine("------------------------------");
                        Console.WriteLine("team_code: " + team.team_code);
                        Console.WriteLine("location: " + team.location);
                        Console.WriteLine("n_elements: " + team.n_elements);
                        Console.WriteLine("supervisor: " + team.supervisor);
                        Console.WriteLine("------------------------------");

                    });
                    ctx.SaveChanges();
                }
            }
            catch
            {
                Console.WriteLine("Could not find team with skill " + skill);
            }


        }
        //Este metodo cria uma intervencao e atribui uma equipa livre 
        //2.1.a
        public void CreateInterventionProc()
        {
            Console.WriteLine("Create an Intervention");
            Console.WriteLine("");
            Console.Write("Description? ");
            var description = Console.ReadLine();
            Console.Write("Price? ");
            var price = Console.ReadLine();
            Console.Write("Start date? ");
            var startDate = Console.ReadLine();
            Console.Write("End date? ");
            var endDate = Console.ReadLine();
            Console.Write("Frequency? ");
            var frequency = Console.ReadLine();
            Console.Write("Asset Id? ");
            var asset_id = Console.ReadLine();
            Console.Write("Skill Description? ");
            var skill_description = Console.ReadLine();
            try
            {
                using (var ctx = new L51DG5Entities())
                {
                    ctx.p_criaInter(Convert.ToString(description), Convert.ToDecimal(price),
                                    Convert.ToDateTime(startDate), String.IsNullOrEmpty(endDate) ? null : Convert.ToDateTime(endDate),
                                     String.IsNullOrEmpty(frequency) ? null : Convert.ToInt32(frequency), Convert.ToInt32(asset_id),
                                    Convert.ToString(skill_description));
                    Console.WriteLine("Intervention create with sucess");
                    ctx.SaveChanges();
                    Console.WriteLine("Intervention created sucessfully and scheduled.");
                }
            }
            catch
            {
                Console.WriteLine("Something went wrong on CreateIntervention using procedure.");
            }
        }
        //ATENÇAO: este metodo nao chama a procedure porque a nossa procedure recebe uma tabela (melhorar depois de perguntar ao stor)
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

            MAINTENANCE_TEAM maintencance_team = new MAINTENANCE_TEAM();
            using (var ctx = new L51DG5Entities())
            {
                ICollection<EMPLOYEE> listOfEmployees = new List<EMPLOYEE>();
                foreach (EMPLOYEE emp in ctx.EMPLOYEEs)
                {
                    if (ssn_list.Contains(emp.ssn))
                    {
                        listOfEmployees.Add(emp);
                    }
                }
                int teamCode = 0;
                foreach (var aux in ctx.MAINTENANCE_TEAM)
                {
                    teamCode++;
                }
                maintencance_team.team_code = teamCode;
                if (listOfEmployees.Count < 2)
                {
                    throw new Exception("Few elements");
                }
                maintencance_team.n_elements = listOfEmployees.Count;
                maintencance_team.location = location;
                //Assumimos que o supervisor existe (para evitar fazer mais um acesso de dados)
                maintencance_team.supervisor = Convert.ToInt32(supervisor);
                foreach (EMPLOYEE e in listOfEmployees)
                {
                    maintencance_team.EMPLOYEEs.Add(e);
                }


                MAINTENANCE_TEAM m = ctx.MAINTENANCE_TEAM.Add(maintencance_team);
                ctx.SaveChanges();

                Console.WriteLine(string.Format("Your team id is {0}", m.team_code));
            }
        }
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

            using (var ctx = new L51DG5Entities())
            {
                //TODO:
                //ctx.update_team_members(ssn_list, toDelete.Equals( "delete"));
                ctx.SaveChanges();
            }
        }
        //2.1.a
        public void GetInterventionByYear()
        {
            Console.WriteLine("Get Intervention By Year");
            Console.WriteLine("Please Enter the year");
            var year = Console.ReadLine();
            try
            {
                using (var ctx = new L51DG5Entities())
                {
                    foreach (var intervention in ctx.interByYear(Convert.ToInt32(year)))
                    {
                        Console.WriteLine("-------------------------------");
                        Console.WriteLine("intervention_code: " + intervention.intervention_code);
                        Console.WriteLine("price: " + intervention.price);
                        Console.WriteLine("description: " + intervention.description);
                        Console.WriteLine("-------------------------------");
                    }
                    ctx.SaveChanges();
                }
            }
            catch
            {
                Console.WriteLine("Something went wrong in Get Intervention By Year");
            }

        }
        //Este metodo cria uma intervencao e atribui uma equipa livre 
        //2.1.b e 2.1.c
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
            try
            {
                using (var ctx = new L51DG5Entities())
                {

                    IQueryable<get_code_from_team_Result> team_result = ctx.get_code_from_team(skill);
                    var list = team_result.Select(team => new
                    {
                        team.team_code,
                        team.location,
                        team.n_elements,
                        team.supervisor
                    }).ToList();
                    int? team_code = null;
                    list.ForEach(team =>
                    {
                        team_code = team.team_code;
                    });
                    INTERVENTION i = ctx.INTERVENTIONs.Add(new INTERVENTION()
                    {
                        description = description,
                        state = team_code == null ? "por atribuir" : "em analise",
                        price = Convert.ToDecimal(price),
                        start_date = Convert.ToDateTime(startDate),
                        end_date = String.IsNullOrEmpty(endDate) ? null : Convert.ToDateTime(endDate),
                        asset_id = Convert.ToInt32(assetId)
                    });
                    if (team_code != null)
                    {

                        ctx.SCHEDULINGs.Add(new SCHEDULING()
                        {
                            team_code = team_code.Value,
                            intervention_code = i.intervention_code,
                            scheduling_date = Convert.ToDateTime(startDate)
                        });
                    }
                    ctx.SaveChanges();
                    Console.WriteLine("Intervention created sucessfully and scheduled.");
                }
            }
            catch
            {
                Console.WriteLine("Something went wrong on CreateIntervention");
            }

        }
        //4
        //usando optimistic locking
        public void swapSkill()
        {
            Console.WriteLine("Exchange skill of two employees.");
            Console.WriteLine("Insert first ssn.");
            var ssn1 = Console.ReadLine();
            Console.WriteLine("Insert second ssn.");
            var ssn2 = Console.ReadLine();


            using (var ctx = new L51DG5Entities())
            {
               
                EMPLOYEE employee1 = ctx.EMPLOYEEs.Find(Convert.ToInt32(ssn1));
                EMPLOYEE employee2 = ctx.EMPLOYEEs.Find(Convert.ToInt32(ssn2));

                ICollection<SKILL> s1 = employee1.SKILLs;
                ICollection<SKILL> s2 = employee2.SKILLs;
                //swap
                employee1.SKILLs = s2;
                employee2.SKILLs = s1;

                bool fail;
                do
                {
                    fail = false;
                    try
                    {
                        ctx.Entry(employee1).State = EntityState.Modified;
                        ctx.Entry(employee2).State = EntityState.Modified;
                        ctx.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException e)
                    {
                        fail = true;
                        Console.WriteLine(e.Message);
                        // esmagar as alterações na BD
                        var entry = e.Entries.Single();
                        var dbValues = entry.GetDatabaseValues();
                        entry.OriginalValues.SetValues(dbValues);
                        ctx.SaveChanges();
                    }
                } while (fail);

            }
        }
    }
}