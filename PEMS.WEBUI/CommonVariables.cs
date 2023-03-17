using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PEMS.WEBUI
{
    public static class CommonVariables
    {
        public static string superAdminRole = "SUPER ADMIN";
        public static string adminRole = "ADMIN";
        public static string bankUser = "BANK USER";
        public static string surveyEngineer = "SURVEY ENGINEER";
        public static string inspectionEngineer = "INSPECTION ENGINEER";

        public static string SuperAdminRole { get { return superAdminRole; } set { superAdminRole = value; } }
        public static string AdminRole { get { return adminRole; } set { adminRole = value; } }
        public static string BankUser { get { return bankUser; } set { bankUser = value; } }
        public static string SurveyEngineer { get { return surveyEngineer; } set { surveyEngineer = value; } }
        public static string InspectionEngineer { get { return inspectionEngineer; } set { inspectionEngineer = value; } }

    }
}