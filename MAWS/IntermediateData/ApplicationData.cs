using System.Collections.Generic;

namespace MAWS.IntermediateData
{
    
    public static class ApplicationData
    {

        public static readonly int fullTimeBaseHours = 1725;
        public static readonly double teachingMaxPercent = 0.8;

        public static readonly List<string> Options = new List<string>() { "Administrator", "Contracts Administrator", "Head Of Discipline", "Unit Coordinator", "Academic Staff" };
        public static readonly List<string> AcademicRoleOptions = new List<string>() { "Head Of Discipline", "Unit Coordinator", "Academic Staff" };

        //--- Other Role Definitions ---//

        public static readonly string ContractsAdministrator = "Contracts Administrator";
        public static readonly string AcademicStaff = "AcademicStaff";

        //------ Role Authorization for Pages ------//

        //--- View Workload Alerts ---//
        //--- Manage Misc Teaching Activity ---//
        //--- Manage Academic Service ---// 
        //--- Manage Academic Teaching Pattern ---//
        //--- Manage Unit Coordinator ---//
        public static readonly string HeadOfDiscipline = "Head Of Discipline";

        //--- Manage Users ---//
        //--- Manage Academic Supervision ---//
        //--- Manage Units ---//
        //--- Manage UnitOfferings ---//
        public static readonly string Administrator = "Administrator";

        //--- View Misc Teaching Activity ---//
        //--- Manage Teaching Assignment ---//
        public static readonly string UnitCoordinator = "Unit Coordinator";

        //--- View Units ---//
        //--- View Unit Offerings ---//
        public static readonly string AdministratorOrHeadOfDisciplineOrUnitCoordinatorOrAcademicStaff = Administrator + ", " + HeadOfDiscipline + ", " + UnitCoordinator + ", " + AcademicStaff;

        //--- View Teaching Activities ---//
        //--- View Teaching Assignments ---//
        public static readonly string ContractsAdministratorOrHeadOfDisciplineOrUnitCoordinatorOrAcademicStaff = ContractsAdministrator + ", " + HeadOfDiscipline + ", " + UnitCoordinator + ", " + AcademicStaff;

        //--- View Academic Staff ---//
        public static readonly string AdministratorOrContractsAdministratorOrHeadOfDisciplineOrUnitCoordinator = Administrator + ", " + ContractsAdministrator + ", " + HeadOfDiscipline + ", " + UnitCoordinator;

        //--- Manage Academic Staff ---//
        public static readonly string AdministratorOrContractsAdministratorOrHeadOfDiscipline = Administrator + ", " + ContractsAdministrator + ", " + HeadOfDiscipline;

        //--- Manage Academic Research ---//
        public static readonly string AdministratorOrHeadOfDiscipline = Administrator + ", " + HeadOfDiscipline;

        //--- View Teaching Pattern ---//
        public static readonly string HeadOfDisciplineOrUnitCoordinatorOrAcademicStaff =  HeadOfDiscipline + ", " + UnitCoordinator + ", " + AcademicStaff;

        //--- Compare ---//
        public static readonly string AdministratorOrContractsAdministratorOrHeadOfDisciplineOrUnitCoordinatorOrAcademicStaff = Administrator + ", " + ContractsAdministrator + ", " + HeadOfDiscipline + ", " + UnitCoordinator + ", " + AcademicStaff;

    }
}