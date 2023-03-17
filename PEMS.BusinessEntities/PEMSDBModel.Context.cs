﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PEMS.BusinessEntities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class PEMSDBEntities : DbContext
    {
        public PEMSDBEntities()
            : base("name=PEMSDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<BankBranch> BankBranches { get; set; }
        public virtual DbSet<BeneficiaryBankMapping> BeneficiaryBankMappings { get; set; }
        public virtual DbSet<BeneficiaryDetail> BeneficiaryDetails { get; set; }
        public virtual DbSet<Evaluation> Evaluations { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Survey> Surveys { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<PostCode> PostCodes { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<SurveyFilesInfo> SurveyFilesInfoes { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<PaymentFilesInfo> PaymentFilesInfoes { get; set; }
        public virtual DbSet<Donor> Donors { get; set; }
        public virtual DbSet<Inspection> Inspections { get; set; }
        public virtual DbSet<InspectionFilesInfo> InspectionFilesInfoes { get; set; }
        public virtual DbSet<EmailsForAlertMessage> EmailsForAlertMessages { get; set; }
    
        public virtual ObjectResult<Sp_GetSurveyList_Result> Sp_GetSurveyList(Nullable<int> surveyId)
        {
            var surveyIdParameter = surveyId.HasValue ?
                new ObjectParameter("SurveyId", surveyId) :
                new ObjectParameter("SurveyId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sp_GetSurveyList_Result>("Sp_GetSurveyList", surveyIdParameter);
        }
    }
}
