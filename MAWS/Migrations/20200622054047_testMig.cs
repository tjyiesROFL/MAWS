using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MAWS.Migrations
{
    public partial class testMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademicStaff",
                columns: table => new
                {
                    StaffID = table.Column<string>(nullable: false),
                    AcademicStaffID = table.Column<string>(type: "VARCHAR(8)", nullable: false),
                    FirstName = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    Surname = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    EmployeeType = table.Column<string>(type: "VARCHAR(3)", nullable: false),
                    Area = table.Column<string>(type: "VARCHAR(3)", nullable: false),
                    ClassCode = table.Column<string>(type: "VARCHAR(6)", nullable: false),
                    ClassName = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    FTBaseHrs = table.Column<decimal>(type: "NUMERIC(4,0)", nullable: false),
                    WorkFraction = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: false),
                    EmployeeStatus = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    ContractExpiryDate = table.Column<string>(type: "VARCHAR(10)", nullable: true),
                    WorkMax_Pc = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: false),
                    WorkHrs = table.Column<decimal>(type: "Numeric(6,2)", nullable: false, computedColumnSql: "\"WorkFraction\"*\"FTBaseHrs\""),
                    TeachingMax_Pc = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: false),
                    Create_User = table.Column<string>(type: "text", nullable: true),
                    Create_DateTime = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    Update_User = table.Column<string>(type: "text", nullable: true),
                    Update_DateTime = table.Column<DateTime>(type: "Timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicStaff", x => x.StaffID);
                    table.UniqueConstraint("AcademicStaffID", x => x.AcademicStaffID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    RoleTitle = table.Column<string>(nullable: true),
                    AcademicStaffID = table.Column<string>(type: "VARCHAR(8)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MiscTeachingActivity_audit",
                columns: table => new
                {
                    stamp = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    operation = table.Column<char>(type: "Char(1)", nullable: false),
                    MiscTeachingActivityID = table.Column<int>(nullable: false),
                    Year = table.Column<decimal>(type: "NUMERIC(4,0)", nullable: false),
                    UnitCode = table.Column<string>(type: "VARCHAR(12)", nullable: true),
                    TeachingPeriod = table.Column<string>(type: "VARCHAR(12)", nullable: true),
                    MiscName = table.Column<string>(type: "VARCHAR(55)", nullable: true),
                    Hours = table.Column<decimal>(type: "NUMERIC(6,2)", nullable: false),
                    Comments = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    UnitOfferingID = table.Column<string>(type: "VARCHAR(32)", nullable: true),
                    AcademicStaffID = table.Column<string>(type: "VARCHAR(8)", nullable: true),
                    Create_User = table.Column<string>(type: "text", nullable: true),
                    Create_DateTime = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    Update_User = table.Column<string>(type: "text", nullable: true),
                    Update_DateTime = table.Column<DateTime>(type: "Timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MiscTeachingActivity_audit", x => new { x.operation, x.stamp });
                });

            migrationBuilder.CreateTable(
                name: "Research_audit",
                columns: table => new
                {
                    stamp = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    operation = table.Column<char>(type: "Char(1)", nullable: false),
                    ResearchID = table.Column<string>(type: "text", nullable: true),
                    Year = table.Column<decimal>(type: "NUMERIC(4,0)", nullable: false),
                    Fifteen_Pc = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: false),
                    ECR_Pc = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: false),
                    Income_Pc = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: false),
                    Completions_Pc = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: false),
                    Pubs_Pc = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: false),
                    RCI_Pc = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: false),
                    Discretionary_Pc = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: true),
                    Discretionary_Comments = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Percentage = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: true),
                    IS_CURRENT = table.Column<bool>(nullable: false),
                    AcademicStaffID = table.Column<string>(type: "VARCHAR(8)", nullable: true),
                    Create_User = table.Column<string>(type: "text", nullable: true),
                    Create_DateTime = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    Update_User = table.Column<string>(type: "text", nullable: true),
                    Update_DateTime = table.Column<DateTime>(type: "Timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Research_audit", x => new { x.operation, x.stamp });
                });

            migrationBuilder.CreateTable(
                name: "Service_audit",
                columns: table => new
                {
                    stamp = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    operation = table.Column<char>(type: "Char(1)", nullable: false),
                    ServiceID = table.Column<int>(nullable: false),
                    Year = table.Column<decimal>(type: "NUMERIC(4,0)", nullable: false),
                    Hours = table.Column<decimal>(type: "NUMERIC(6,2)", nullable: false),
                    Type = table.Column<string>(type: "VARCHAR(32)", nullable: true),
                    Comments = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    IS_CURRENT = table.Column<bool>(nullable: false),
                    AcademicStaffID = table.Column<string>(type: "VARCHAR(8)", nullable: true),
                    Create_User = table.Column<string>(type: "text", nullable: true),
                    Create_DateTime = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    Update_User = table.Column<string>(type: "text", nullable: true),
                    Update_DateTime = table.Column<DateTime>(type: "Timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service_audit", x => new { x.operation, x.stamp });
                });

            migrationBuilder.CreateTable(
                name: "Supervision_audit",
                columns: table => new
                {
                    stamp = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    operation = table.Column<char>(type: "Char(1)", nullable: false),
                    SupervisionID = table.Column<int>(nullable: false),
                    Year = table.Column<decimal>(type: "NUMERIC(4,0)", nullable: false),
                    Hours = table.Column<decimal>(type: "NUMERIC(6,2)", nullable: false),
                    Type = table.Column<string>(type: "VARCHAR(3)", nullable: true),
                    Comments = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    IS_CURRENT = table.Column<bool>(nullable: false),
                    AcademicStaffID = table.Column<string>(type: "VARCHAR(8)", nullable: true),
                    Create_User = table.Column<string>(type: "text", nullable: true),
                    Create_DateTime = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    Update_User = table.Column<string>(type: "text", nullable: true),
                    Update_DateTime = table.Column<DateTime>(type: "Timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supervision_audit", x => new { x.operation, x.stamp });
                });

            migrationBuilder.CreateTable(
                name: "TeachingActivityAssignment_audit",
                columns: table => new
                {
                    stamp = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    operation = table.Column<char>(type: "Char(1)", nullable: false),
                    AssignmentID = table.Column<int>(nullable: false),
                    ActivityHrs = table.Column<decimal>(type: "NUMERIC(6,2)", nullable: false),
                    WorkloadHrs = table.Column<decimal>(type: "NUMERIC(6,2)", nullable: false),
                    ActiveFlag = table.Column<bool>(nullable: false),
                    TeachingActivityID = table.Column<int>(nullable: false),
                    StaffID = table.Column<string>(nullable: true),
                    Create_User = table.Column<string>(type: "text", nullable: true),
                    Create_DateTime = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    Update_User = table.Column<string>(type: "text", nullable: true),
                    Update_DateTime = table.Column<DateTime>(type: "Timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachingActivityAssignment_audit", x => new { x.operation, x.stamp });
                });

            migrationBuilder.CreateTable(
                name: "Discipline",
                columns: table => new
                {
                    DisciplineID = table.Column<string>(type: "VARCHAR(6)", nullable: false),
                    DisciplineName = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    School = table.Column<string>(type: "VARCHAR(6)", nullable: false),
                    ActiveFlag = table.Column<bool>(nullable: false),
                    WOCT_FirstHrsPH = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: false),
                    WOCT_RepeatHRsPH = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: false),
                    SGT_FirstSessionCount = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: false),
                    SGT_FirstHrsPH = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: false),
                    SGT_SubsequentHrsPH = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: false),
                    SGT_MarkingHrsPS = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: false),
                    Marking_ExamHrsPS = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: false),
                    OUAE_AttentionHrsPS = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: false),
                    OUAE_MarkingHrsPS = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: false),
                    CP_3ptRatio = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: false),
                    CP_6ptRatio = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: false),
                    CP_9ptRatio = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: false),
                    CP_12ptRatio = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: false),
                    U_BaseHrs_Tier1 = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: false),
                    U_BaseHrs_Tier2 = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: false),
                    U_BaseHrs_Tier3 = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: false),
                    UCM_ExternalHrs = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: false),
                    UCM_BaseStudents = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: false),
                    UCM_AdditionalHrsPerStudent = table.Column<decimal>(type: "NUMERIC(6,5)", nullable: false),
                    UCM_NewUCHrs = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: false),
                    UCM_UpdateHrs_Minor = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: false),
                    UCM_UpdateHrs_Major = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: false),
                    UCM_DevelopNewUnitBaseHrs = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: false),
                    UCM_DevelopNewUnitDiscretionHrsMax = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: false),
                    UCM_DevelopNewUnitDigitallyEnhancedHrs = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: false),
                    PU_BaseHrs = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: false),
                    PU_AdditionalClassHrs = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: false),
                    PU_BaseHrsTNE = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: true),
                    PU_BaseHrsExtra = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: false),
                    PU_OtherTeaching = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: false),
                    PU_SupervisorHrsPP = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: false),
                    PU_StaffAsClientHrs = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: false),
                    PU_StaffAsClientHrsTNE = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: false),
                    UCM_TNE_SetupHrs = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: false),
                    UCM_TNE_CMBaseHrs = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: false),
                    UCM_TNE_CMBaseStudents = table.Column<decimal>(type: "NUMERIC(6,2)", nullable: false),
                    UCM_TNE_CMBaseAffiliates = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: false),
                    UCM_TNE_CMAdditionalHrsPerAffiliate = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: false),
                    HeadOfDiscipline = table.Column<string>(type: "VARCHAR(8)", nullable: true),
                    Create_User = table.Column<string>(type: "text", nullable: true),
                    Create_DateTime = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    Update_User = table.Column<string>(type: "text", nullable: true),
                    Update_DateTime = table.Column<DateTime>(type: "Timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discipline", x => x.DisciplineID);
                    table.ForeignKey(
                        name: "FK_Discipline_AcademicStaff_HeadOfDiscipline",
                        column: x => x.HeadOfDiscipline,
                        principalTable: "AcademicStaff",
                        principalColumn: "AcademicStaffID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Research",
                columns: table => new
                {
                    ResearchID = table.Column<string>(nullable: false),
                    Year = table.Column<decimal>(type: "NUMERIC(4,0)", nullable: false),
                    Fifteen_Pc = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: false),
                    ECR_Pc = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: false),
                    Income_Pc = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: false),
                    Completions_Pc = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: false),
                    Pubs_Pc = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: false),
                    RCI_Pc = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: false),
                    Discretionary_Pc = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: true),
                    Discretionary_Comments = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Percentage = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: true, computedColumnSql: "\"Fifteen_Pc\"+\"ECR_Pc\"+\"Income_Pc\"+\"Completions_Pc\"+\"Pubs_Pc\"+\"RCI_Pc\"+\"Discretionary_Pc\""),
                    IS_CURRENT = table.Column<bool>(nullable: false),
                    AcademicStaffID = table.Column<string>(type: "VARCHAR(8)", nullable: true),
                    Create_User = table.Column<string>(type: "text", nullable: true),
                    Create_DateTime = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    Update_User = table.Column<string>(type: "text", nullable: true),
                    Update_DateTime = table.Column<DateTime>(type: "Timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Research", x => x.ResearchID);
                    table.ForeignKey(
                        name: "FK_Research_AcademicStaff_AcademicStaffID",
                        column: x => x.AcademicStaffID,
                        principalTable: "AcademicStaff",
                        principalColumn: "AcademicStaffID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    ServiceID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Year = table.Column<decimal>(type: "NUMERIC(4,0)", nullable: false),
                    Hours = table.Column<decimal>(type: "NUMERIC(6,2)", nullable: false),
                    Type = table.Column<string>(type: "VARCHAR(32)", nullable: false),
                    Comments = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    IS_CURRENT = table.Column<bool>(nullable: false),
                    AcademicStaffID = table.Column<string>(type: "VARCHAR(8)", nullable: true),
                    Create_User = table.Column<string>(type: "text", nullable: true),
                    Create_DateTime = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    Update_User = table.Column<string>(type: "text", nullable: true),
                    Update_DateTime = table.Column<DateTime>(type: "Timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.ServiceID);
                    table.ForeignKey(
                        name: "FK_Service_AcademicStaff_AcademicStaffID",
                        column: x => x.AcademicStaffID,
                        principalTable: "AcademicStaff",
                        principalColumn: "AcademicStaffID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Supervision",
                columns: table => new
                {
                    SupervisionID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Year = table.Column<decimal>(type: "NUMERIC(4,0)", nullable: false),
                    Hours = table.Column<decimal>(type: "NUMERIC(6,2)", nullable: false),
                    Type = table.Column<string>(type: "VARCHAR(3)", nullable: false),
                    Comments = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    IS_CURRENT = table.Column<bool>(nullable: false),
                    AcademicStaffID = table.Column<string>(type: "VARCHAR(8)", nullable: true),
                    Create_User = table.Column<string>(type: "text", nullable: true),
                    Create_DateTime = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    Update_User = table.Column<string>(type: "text", nullable: true),
                    Update_DateTime = table.Column<DateTime>(type: "Timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supervision", x => x.SupervisionID);
                    table.ForeignKey(
                        name: "FK_Supervision_AcademicStaff_AcademicStaffID",
                        column: x => x.AcademicStaffID,
                        principalTable: "AcademicStaff",
                        principalColumn: "AcademicStaffID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserAcademicStaff",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    StaffID = table.Column<string>(type: "text", nullable: true),
                    Create_User = table.Column<string>(type: "text", nullable: true),
                    Create_DateTime = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    Update_User = table.Column<string>(type: "text", nullable: true),
                    Update_DateTime = table.Column<DateTime>(type: "Timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserAcademicStaff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserAcademicStaff_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserAcademicStaff_AcademicStaff_StaffID",
                        column: x => x.StaffID,
                        principalTable: "AcademicStaff",
                        principalColumn: "AcademicStaffID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    UnitCode = table.Column<string>(type: "VARCHAR(12)", nullable: false),
                    UnitName = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    Area = table.Column<string>(type: "VARCHAR(6)", nullable: false),
                    PU_BaseHrsExtraFlag = table.Column<bool>(nullable: false),
                    PU_OtherTeachingFlag = table.Column<bool>(nullable: false),
                    ClientFlag = table.Column<bool>(nullable: false),
                    ExamFlag = table.Column<bool>(nullable: false),
                    LabFlag = table.Column<bool>(nullable: false),
                    FieldworkFlag = table.Column<bool>(nullable: false),
                    Tier = table.Column<decimal>(type: "NUMERIC(1,0)", nullable: false),
                    UCMTierBaseHrs = table.Column<decimal>(type: "NUMERIC(5,2)", nullable: false),
                    ActiveFlag = table.Column<bool>(nullable: false),
                    ProjectFlag = table.Column<bool>(nullable: false),
                    CreditPoints = table.Column<decimal>(type: "NUMERIC(2)", nullable: false),
                    CreditPointsRatio = table.Column<decimal>(type: "NUMERIC(3,2)", nullable: false),
                    TeachingPattern = table.Column<string>(type: "VARCHAR(6)", nullable: true),
                    DisciplineID = table.Column<string>(nullable: true),
                    Create_User = table.Column<string>(type: "text", nullable: true),
                    Create_DateTime = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    Update_User = table.Column<string>(type: "text", nullable: true),
                    Update_DateTime = table.Column<DateTime>(type: "Timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.UnitCode);
                    table.ForeignKey(
                        name: "FK_Unit_Discipline_DisciplineID",
                        column: x => x.DisciplineID,
                        principalTable: "Discipline",
                        principalColumn: "DisciplineID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UnitOffering",
                columns: table => new
                {
                    UnitOfferingID = table.Column<string>(type: "VARCHAR(32)", nullable: false),
                    Year = table.Column<decimal>(type: "NUMERIC(4,0)", nullable: false),
                    TeachingPeriod = table.Column<string>(type: "VARCHAR(6)", nullable: false),
                    Location = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Mode = table.Column<string>(type: "VARCHAR(2)", nullable: false),
                    OUAE_Flag = table.Column<bool>(nullable: false),
                    WOCT_Flag = table.Column<bool>(nullable: false),
                    SGT_Flag = table.Column<bool>(nullable: false),
                    OfferingType = table.Column<string>(type: "VARCHAR(1)", nullable: false),
                    ActiveFlag = table.Column<bool>(nullable: false),
                    AcademicStaffID = table.Column<string>(type: "VARCHAR(8)", nullable: true),
                    UnitCode = table.Column<string>(nullable: true),
                    Create_User = table.Column<string>(type: "text", nullable: true),
                    Create_DateTime = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    Update_User = table.Column<string>(type: "text", nullable: true),
                    Update_DateTime = table.Column<DateTime>(type: "Timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOffering", x => x.UnitOfferingID);
                    table.ForeignKey(
                        name: "FK_UnitOffering_AcademicStaff_AcademicStaffID",
                        column: x => x.AcademicStaffID,
                        principalTable: "AcademicStaff",
                        principalColumn: "AcademicStaffID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnitOffering_Unit_UnitCode",
                        column: x => x.UnitCode,
                        principalTable: "Unit",
                        principalColumn: "UnitCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MiscTeachingActivity",
                columns: table => new
                {
                    MiscTeachingActivityID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Year = table.Column<decimal>(type: "NUMERIC(4,0)", nullable: false),
                    UnitCode = table.Column<string>(type: "VARCHAR(12)", nullable: false),
                    TeachingPeriod = table.Column<string>(type: "VARCHAR(12)", nullable: false),
                    MiscName = table.Column<string>(type: "VARCHAR(55)", nullable: false),
                    Hours = table.Column<decimal>(type: "NUMERIC(6,2)", nullable: false),
                    Comments = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    UnitOfferingID = table.Column<string>(nullable: true),
                    AcademicStaffID = table.Column<string>(type: "VARCHAR(8)", nullable: true),
                    Create_User = table.Column<string>(type: "text", nullable: true),
                    Create_DateTime = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    Update_User = table.Column<string>(type: "text", nullable: true),
                    Update_DateTime = table.Column<DateTime>(type: "Timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MiscTeachingActivity", x => x.MiscTeachingActivityID);
                    table.ForeignKey(
                        name: "FK_MiscTeachingActivity_AcademicStaff_AcademicStaffID",
                        column: x => x.AcademicStaffID,
                        principalTable: "AcademicStaff",
                        principalColumn: "AcademicStaffID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MiscTeachingActivity_UnitOffering_UnitOfferingID",
                        column: x => x.UnitOfferingID,
                        principalTable: "UnitOffering",
                        principalColumn: "UnitOfferingID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeachingActivity",
                columns: table => new
                {
                    TeachingActivityID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UnitCode = table.Column<string>(type: "VARCHAR(12)", nullable: false),
                    Year = table.Column<decimal>(type: "NUMERIC(4,0)", nullable: false),
                    TeachingPeriod = table.Column<string>(type: "VARCHAR(12)", nullable: false),
                    Transferable = table.Column<bool>(nullable: false),
                    Activity = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    ActivityWeeks = table.Column<decimal>(type: "NUMERIC(2)", nullable: false),
                    ActivityQty = table.Column<decimal>(type: "NUMERIC(3)", nullable: false),
                    ActivityHrs = table.Column<decimal>(type: "NUMERIC(6,2)", nullable: false),
                    ActiveFlag = table.Column<bool>(nullable: false),
                    UnitOfferingID = table.Column<string>(nullable: true),
                    Create_User = table.Column<string>(type: "text", nullable: true),
                    Create_DateTime = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    Update_User = table.Column<string>(type: "text", nullable: true),
                    Update_DateTime = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    AcademicStaffStaffID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachingActivity", x => x.TeachingActivityID);
                    table.ForeignKey(
                        name: "FK_TeachingActivity_AcademicStaff_AcademicStaffStaffID",
                        column: x => x.AcademicStaffStaffID,
                        principalTable: "AcademicStaff",
                        principalColumn: "StaffID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeachingActivity_UnitOffering_UnitOfferingID",
                        column: x => x.UnitOfferingID,
                        principalTable: "UnitOffering",
                        principalColumn: "UnitOfferingID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeachingPattern",
                columns: table => new
                {
                    TeachingPatternID = table.Column<string>(type: "VARCHAR(32)", nullable: false),
                    UnitCode = table.Column<string>(type: "VARCHAR(12)", nullable: false),
                    Year = table.Column<decimal>(type: "NUMERIC(4)", nullable: false),
                    TeachingPeriod = table.Column<string>(type: "VARCHAR(12)", nullable: false),
                    OfferingType = table.Column<string>(type: "VARCHAR(1)", nullable: false),
                    TotalEnrolments = table.Column<decimal>(type: "NUMERIC(4)", nullable: false),
                    ExternalEnrolments = table.Column<decimal>(type: "NUMERIC(4)", nullable: false),
                    EnrolmentStatus = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    WOCT_HrsPerSessionFIRST = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: false),
                    WOCT_SessionsPerWeekFIRST = table.Column<decimal>(type: "NUMERIC(2)", nullable: false),
                    WOCT_HrsPerSessionREPEAT = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: false),
                    WOCT_SessionsPerWeekREPEAT = table.Column<decimal>(type: "NUMERIC(2)", nullable: false),
                    SGT_ClassSize = table.Column<decimal>(type: "NUMERIC(2)", nullable: false),
                    SGT_HrsPerSession = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: false),
                    SGT_SessionsPerWeek = table.Column<decimal>(type: "NUMERIC(2)", nullable: false),
                    UC_TNE_Affiliates = table.Column<decimal>(type: "NUMERIC(1)", nullable: false),
                    PU_GroupSize = table.Column<decimal>(type: "NUMERIC(2)", nullable: false),
                    PU_StaffAsClientQty = table.Column<decimal>(type: "NUMERIC(2)", nullable: false),
                    PU_StaffAsClientTNE_Qty = table.Column<decimal>(type: "NUMERIC(2)", nullable: false),
                    UC_New = table.Column<bool>(nullable: false),
                    UU_Type = table.Column<string>(type: "VARCHAR(5)", nullable: true),
                    UD_Type = table.Column<string>(type: "VARCHAR(1)", nullable: true),
                    UD_DiscretionHrs = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: false),
                    UD_DiscretionHrsComment = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    NoTeachingWeeks = table.Column<decimal>(type: "NUMERIC(2)", nullable: false),
                    ActiveFlag = table.Column<bool>(nullable: false),
                    UnitOfferingID = table.Column<string>(nullable: true),
                    AcademicStaffID = table.Column<string>(type: "VARCHAR(8)", nullable: true),
                    Create_User = table.Column<string>(type: "text", nullable: true),
                    Create_DateTime = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    Update_User = table.Column<string>(type: "text", nullable: true),
                    Update_DateTime = table.Column<DateTime>(type: "Timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachingPattern", x => x.TeachingPatternID);
                    table.ForeignKey(
                        name: "FK_TeachingPattern_AcademicStaff_AcademicStaffID",
                        column: x => x.AcademicStaffID,
                        principalTable: "AcademicStaff",
                        principalColumn: "AcademicStaffID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeachingPattern_UnitOffering_UnitOfferingID",
                        column: x => x.UnitOfferingID,
                        principalTable: "UnitOffering",
                        principalColumn: "UnitOfferingID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeachingActivityAssignment",
                columns: table => new
                {
                    AssignmentID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActivityHrs = table.Column<decimal>(type: "NUMERIC(6,2)", nullable: false),
                    WorkloadHrs = table.Column<decimal>(type: "NUMERIC(6,2)", nullable: false),
                    ActiveFlag = table.Column<bool>(nullable: false),
                    TeachingActivityID = table.Column<int>(nullable: true),
                    AcademicStaffStaffID = table.Column<string>(nullable: true),
                    Create_User = table.Column<string>(type: "text", nullable: true),
                    Create_DateTime = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    Update_User = table.Column<string>(type: "text", nullable: true),
                    Update_DateTime = table.Column<DateTime>(type: "Timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachingActivityAssignment", x => x.AssignmentID);
                    table.ForeignKey(
                        name: "FK_TeachingActivityAssignment_AcademicStaff_AcademicStaffStaff~",
                        column: x => x.AcademicStaffStaffID,
                        principalTable: "AcademicStaff",
                        principalColumn: "StaffID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeachingActivityAssignment_TeachingActivity_TeachingActivit~",
                        column: x => x.TeachingActivityID,
                        principalTable: "TeachingActivity",
                        principalColumn: "TeachingActivityID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserAcademicStaff_StaffID",
                table: "AspNetUserAcademicStaff",
                column: "StaffID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Discipline_HeadOfDiscipline",
                table: "Discipline",
                column: "HeadOfDiscipline",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MiscTeachingActivity_AcademicStaffID",
                table: "MiscTeachingActivity",
                column: "AcademicStaffID");

            migrationBuilder.CreateIndex(
                name: "IX_MiscTeachingActivity_UnitOfferingID",
                table: "MiscTeachingActivity",
                column: "UnitOfferingID");

            migrationBuilder.CreateIndex(
                name: "IX_Research_AcademicStaffID",
                table: "Research",
                column: "AcademicStaffID");

            migrationBuilder.CreateIndex(
                name: "IX_Service_AcademicStaffID",
                table: "Service",
                column: "AcademicStaffID");

            migrationBuilder.CreateIndex(
                name: "IX_Supervision_AcademicStaffID",
                table: "Supervision",
                column: "AcademicStaffID");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingActivity_AcademicStaffStaffID",
                table: "TeachingActivity",
                column: "AcademicStaffStaffID");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingActivity_UnitOfferingID",
                table: "TeachingActivity",
                column: "UnitOfferingID");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingActivityAssignment_AcademicStaffStaffID",
                table: "TeachingActivityAssignment",
                column: "AcademicStaffStaffID");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingActivityAssignment_TeachingActivityID",
                table: "TeachingActivityAssignment",
                column: "TeachingActivityID");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingPattern_AcademicStaffID",
                table: "TeachingPattern",
                column: "AcademicStaffID");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingPattern_UnitOfferingID",
                table: "TeachingPattern",
                column: "UnitOfferingID");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_DisciplineID",
                table: "Unit",
                column: "DisciplineID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitOffering_AcademicStaffID",
                table: "UnitOffering",
                column: "AcademicStaffID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitOffering_UnitCode",
                table: "UnitOffering",
                column: "UnitCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserAcademicStaff");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "MiscTeachingActivity");

            migrationBuilder.DropTable(
                name: "MiscTeachingActivity_audit");

            migrationBuilder.DropTable(
                name: "Research");

            migrationBuilder.DropTable(
                name: "Research_audit");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "Service_audit");

            migrationBuilder.DropTable(
                name: "Supervision");

            migrationBuilder.DropTable(
                name: "Supervision_audit");

            migrationBuilder.DropTable(
                name: "TeachingActivityAssignment");

            migrationBuilder.DropTable(
                name: "TeachingActivityAssignment_audit");

            migrationBuilder.DropTable(
                name: "TeachingPattern");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "TeachingActivity");

            migrationBuilder.DropTable(
                name: "UnitOffering");

            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropTable(
                name: "Discipline");

            migrationBuilder.DropTable(
                name: "AcademicStaff");
        }
    }
}
