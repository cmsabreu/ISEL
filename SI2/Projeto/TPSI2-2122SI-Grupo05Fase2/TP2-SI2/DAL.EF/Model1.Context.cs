﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.EF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class L51DG5Entities : DbContext
    {
        public L51DG5Entities()
            : base("name=L51DG5Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ASSET> ASSETs { get; set; }
        public virtual DbSet<EMPLOYEE> EMPLOYEEs { get; set; }
        public virtual DbSet<INTERVENTION> INTERVENTIONs { get; set; }
        public virtual DbSet<MAINTENANCE_TEAM> MAINTENANCE_TEAM { get; set; }
        public virtual DbSet<NON_PERIODIC> NON_PERIODIC { get; set; }
        public virtual DbSet<PERIODIC> PERIODICs { get; set; }
        public virtual DbSet<REGISTER> REGISTERs { get; set; }
        public virtual DbSet<SCHEDULING> SCHEDULINGs { get; set; }
        public virtual DbSet<SKILL> SKILLs { get; set; }
        public virtual DbSet<TYPE> TYPEs { get; set; }
    
        [DbFunction("L51DG5Entities", "interByYear")]
        public virtual IQueryable<interByYear_Result> interByYear(Nullable<int> year)
        {
            var yearParameter = year.HasValue ?
                new ObjectParameter("year", year) :
                new ObjectParameter("year", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<interByYear_Result>("[L51DG5Entities].[interByYear](@year)", yearParameter);
        }
    
        public virtual int insertEmployee(Nullable<int> ssn, string f_name, string l_name, Nullable<System.DateTime> birth_date, string address, string postal_code, string city, string job, Nullable<int> phone_number, string mail)
        {
            var ssnParameter = ssn.HasValue ?
                new ObjectParameter("ssn", ssn) :
                new ObjectParameter("ssn", typeof(int));
    
            var f_nameParameter = f_name != null ?
                new ObjectParameter("f_name", f_name) :
                new ObjectParameter("f_name", typeof(string));
    
            var l_nameParameter = l_name != null ?
                new ObjectParameter("l_name", l_name) :
                new ObjectParameter("l_name", typeof(string));
    
            var birth_dateParameter = birth_date.HasValue ?
                new ObjectParameter("birth_date", birth_date) :
                new ObjectParameter("birth_date", typeof(System.DateTime));
    
            var addressParameter = address != null ?
                new ObjectParameter("address", address) :
                new ObjectParameter("address", typeof(string));
    
            var postal_codeParameter = postal_code != null ?
                new ObjectParameter("postal_code", postal_code) :
                new ObjectParameter("postal_code", typeof(string));
    
            var cityParameter = city != null ?
                new ObjectParameter("city", city) :
                new ObjectParameter("city", typeof(string));
    
            var jobParameter = job != null ?
                new ObjectParameter("job", job) :
                new ObjectParameter("job", typeof(string));
    
            var phone_numberParameter = phone_number.HasValue ?
                new ObjectParameter("phone_number", phone_number) :
                new ObjectParameter("phone_number", typeof(int));
    
            var mailParameter = mail != null ?
                new ObjectParameter("mail", mail) :
                new ObjectParameter("mail", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("insertEmployee", ssnParameter, f_nameParameter, l_nameParameter, birth_dateParameter, addressParameter, postal_codeParameter, cityParameter, jobParameter, phone_numberParameter, mailParameter);
        }
    
        public virtual int p_create_team(string location, Nullable<int> ssn_supervisor)
        {
            var locationParameter = location != null ?
                new ObjectParameter("location", location) :
                new ObjectParameter("location", typeof(string));
    
            var ssn_supervisorParameter = ssn_supervisor.HasValue ?
                new ObjectParameter("ssn_supervisor", ssn_supervisor) :
                new ObjectParameter("ssn_supervisor", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("p_create_team", locationParameter, ssn_supervisorParameter);
        }
    
        public virtual int p_criaInter(string description, Nullable<decimal> price, Nullable<System.DateTime> startDate, Nullable<System.DateTime> endDate, Nullable<int> frequency, Nullable<int> asset_id, string skillDescription)
        {
            var descriptionParameter = description != null ?
                new ObjectParameter("description", description) :
                new ObjectParameter("description", typeof(string));
    
            var priceParameter = price.HasValue ?
                new ObjectParameter("price", price) :
                new ObjectParameter("price", typeof(decimal));
    
            var startDateParameter = startDate.HasValue ?
                new ObjectParameter("startDate", startDate) :
                new ObjectParameter("startDate", typeof(System.DateTime));
    
            var endDateParameter = endDate.HasValue ?
                new ObjectParameter("endDate", endDate) :
                new ObjectParameter("endDate", typeof(System.DateTime));
    
            var frequencyParameter = frequency.HasValue ?
                new ObjectParameter("frequency", frequency) :
                new ObjectParameter("frequency", typeof(int));
    
            var asset_idParameter = asset_id.HasValue ?
                new ObjectParameter("asset_id", asset_id) :
                new ObjectParameter("asset_id", typeof(int));
    
            var skillDescriptionParameter = skillDescription != null ?
                new ObjectParameter("skillDescription", skillDescription) :
                new ObjectParameter("skillDescription", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("p_criaInter", descriptionParameter, priceParameter, startDateParameter, endDateParameter, frequencyParameter, asset_idParameter, skillDescriptionParameter);
        }
    
        public virtual int removeEmployee(Nullable<int> ssn)
        {
            var ssnParameter = ssn.HasValue ?
                new ObjectParameter("ssn", ssn) :
                new ObjectParameter("ssn", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("removeEmployee", ssnParameter);
        }
    
        public virtual int update_interventation_state(string state)
        {
            var stateParameter = state != null ?
                new ObjectParameter("state", state) :
                new ObjectParameter("state", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("update_interventation_state", stateParameter);
        }
    
        public virtual int update_team_members(Nullable<int> team_code, Nullable<bool> toDelete)
        {
            var team_codeParameter = team_code.HasValue ?
                new ObjectParameter("team_code", team_code) :
                new ObjectParameter("team_code", typeof(int));
    
            var toDeleteParameter = toDelete.HasValue ?
                new ObjectParameter("toDelete", toDelete) :
                new ObjectParameter("toDelete", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("update_team_members", team_codeParameter, toDeleteParameter);
        }
    
        public virtual int updateEmployee(Nullable<int> ssn, string f_name, string l_name, Nullable<System.DateTime> birth_date, string address, string postal_code, string city, string job, Nullable<int> phone_number, string mail)
        {
            var ssnParameter = ssn.HasValue ?
                new ObjectParameter("ssn", ssn) :
                new ObjectParameter("ssn", typeof(int));
    
            var f_nameParameter = f_name != null ?
                new ObjectParameter("f_name", f_name) :
                new ObjectParameter("f_name", typeof(string));
    
            var l_nameParameter = l_name != null ?
                new ObjectParameter("l_name", l_name) :
                new ObjectParameter("l_name", typeof(string));
    
            var birth_dateParameter = birth_date.HasValue ?
                new ObjectParameter("birth_date", birth_date) :
                new ObjectParameter("birth_date", typeof(System.DateTime));
    
            var addressParameter = address != null ?
                new ObjectParameter("address", address) :
                new ObjectParameter("address", typeof(string));
    
            var postal_codeParameter = postal_code != null ?
                new ObjectParameter("postal_code", postal_code) :
                new ObjectParameter("postal_code", typeof(string));
    
            var cityParameter = city != null ?
                new ObjectParameter("city", city) :
                new ObjectParameter("city", typeof(string));
    
            var jobParameter = job != null ?
                new ObjectParameter("job", job) :
                new ObjectParameter("job", typeof(string));
    
            var phone_numberParameter = phone_number.HasValue ?
                new ObjectParameter("phone_number", phone_number) :
                new ObjectParameter("phone_number", typeof(int));
    
            var mailParameter = mail != null ?
                new ObjectParameter("mail", mail) :
                new ObjectParameter("mail", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("updateEmployee", ssnParameter, f_nameParameter, l_nameParameter, birth_dateParameter, addressParameter, postal_codeParameter, cityParameter, jobParameter, phone_numberParameter, mailParameter);
        }
    
        public virtual int updateStateIntervention(Nullable<int> intervention_code, string state, Nullable<System.DateTime> endDate)
        {
            var intervention_codeParameter = intervention_code.HasValue ?
                new ObjectParameter("intervention_code", intervention_code) :
                new ObjectParameter("intervention_code", typeof(int));
    
            var stateParameter = state != null ?
                new ObjectParameter("state", state) :
                new ObjectParameter("state", typeof(string));
    
            var endDateParameter = endDate.HasValue ?
                new ObjectParameter("endDate", endDate) :
                new ObjectParameter("endDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("updateStateIntervention", intervention_codeParameter, stateParameter, endDateParameter);
        }
    
        [DbFunction("L51DG5Entities", "get_code_from_team")]
        public virtual IQueryable<get_code_from_team_Result> get_code_from_team(string description)
        {
            var descriptionParameter = description != null ?
                new ObjectParameter("description", description) :
                new ObjectParameter("description", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<get_code_from_team_Result>("[L51DG5Entities].[get_code_from_team](@description)", descriptionParameter);
        }
    }
}