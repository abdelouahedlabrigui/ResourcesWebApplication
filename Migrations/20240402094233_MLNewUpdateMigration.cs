using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourcesWebApplication.Migrations
{
    public partial class MLNewUpdateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClassificationReportIdentifier",
                table: "ClassificationReports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AnnualPercentageYields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatedAnnualInterestRate = table.Column<float>(type: "real", nullable: false),
                    NumberOfTimesCompounded = table.Column<float>(type: "real", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnualPercentageYields", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArrayCoefficients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatasetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArrayCoefficientIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Coefficients = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArrayCoefficients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BalloonBalanceOfLoans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PresentValue = table.Column<float>(type: "real", nullable: false),
                    Payment = table.Column<float>(type: "real", nullable: false),
                    RatePerPayment = table.Column<float>(type: "real", nullable: false),
                    NumberOfPayments = table.Column<float>(type: "real", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalloonBalanceOfLoans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BalloonLoanPayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PresentValue = table.Column<float>(type: "real", nullable: false),
                    BalloonAmount = table.Column<float>(type: "real", nullable: false),
                    RatePerPeriod = table.Column<float>(type: "real", nullable: false),
                    NumberOfPeriods = table.Column<float>(type: "real", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalloonLoanPayments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompoundInterests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Principal = table.Column<float>(type: "real", nullable: false),
                    RatePerPeriod = table.Column<float>(type: "real", nullable: false),
                    NumberOfPeriods = table.Column<float>(type: "real", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompoundInterests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DebtToIncomeRations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MonthlyDebtPayments = table.Column<float>(type: "real", nullable: false),
                    GrossMonthlyIncome = table.Column<float>(type: "real", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DebtToIncomeRations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstimationWithConfusionMatrix",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatasetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstimationWithConfusionMatrixIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PredictYES = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PredictNO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstimationWithConfusionMatrix", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeneralizedLinearModelRegressionResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatasetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneralizedLinearModelRegressionResultsIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResultName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResultValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralizedLinearModelRegressionResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Intercepts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatasetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InterceptIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MLModelType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MLModelIntercept = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intercepts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoanPayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PresentValue = table.Column<float>(type: "real", nullable: false),
                    RatePerPeriod = table.Column<float>(type: "real", nullable: false),
                    NumberOfPeriods = table.Column<float>(type: "real", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanPayments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoanToDepositRatios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Loans = table.Column<float>(type: "real", nullable: false),
                    Deposits = table.Column<float>(type: "real", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanToDepositRatios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoanToValueRatios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanAmount = table.Column<float>(type: "real", nullable: false),
                    ValueOfCollateral = table.Column<float>(type: "real", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanToValueRatios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Missings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatasetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Column = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MissingData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Missings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OLSSummaryResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatasetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OLSRegressionResultsIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResultName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResultValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OLSSummaryResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PredictProbabilityDecisions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatasetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PredictProbabilityDecisionIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MLModelType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProbOfNO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProbOfYes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Decision = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PredictProbabilityDecisions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegressionResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatasetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegressionResultIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Variable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Coefficient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StdError = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CILower = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CIUpper = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegressionResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RemainingBalanceOnLoans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PresentValue = table.Column<float>(type: "real", nullable: false),
                    Payment = table.Column<float>(type: "real", nullable: false),
                    RatePerPayment = table.Column<float>(type: "real", nullable: false),
                    NumberOfPayments = table.Column<float>(type: "real", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemainingBalanceOnLoans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScikitLearnStatsmodelsPredictions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatasetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScikitLearnStatsmodelsPredictionIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScikitLearnProbability = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatsmodelsProbability = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScikitLearnStatsmodelsPredictions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatasetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScoreIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScoreValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shapes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatasetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatasetType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatasetShape = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shapes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SimpleInterestPrincipals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Interest = table.Column<float>(type: "real", nullable: false),
                    Rate = table.Column<float>(type: "real", nullable: false),
                    Time = table.Column<float>(type: "real", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimpleInterestPrincipals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SimpleInterestRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Principal = table.Column<float>(type: "real", nullable: false),
                    Interest = table.Column<float>(type: "real", nullable: false),
                    Time = table.Column<float>(type: "real", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimpleInterestRates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SimpleInterests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Principal = table.Column<float>(type: "real", nullable: false),
                    Rate = table.Column<float>(type: "real", nullable: false),
                    Time = table.Column<float>(type: "real", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimpleInterests", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnualPercentageYields");

            migrationBuilder.DropTable(
                name: "ArrayCoefficients");

            migrationBuilder.DropTable(
                name: "BalloonBalanceOfLoans");

            migrationBuilder.DropTable(
                name: "BalloonLoanPayments");

            migrationBuilder.DropTable(
                name: "CompoundInterests");

            migrationBuilder.DropTable(
                name: "DebtToIncomeRations");

            migrationBuilder.DropTable(
                name: "EstimationWithConfusionMatrix");

            migrationBuilder.DropTable(
                name: "GeneralizedLinearModelRegressionResults");

            migrationBuilder.DropTable(
                name: "Intercepts");

            migrationBuilder.DropTable(
                name: "LoanPayments");

            migrationBuilder.DropTable(
                name: "LoanToDepositRatios");

            migrationBuilder.DropTable(
                name: "LoanToValueRatios");

            migrationBuilder.DropTable(
                name: "Missings");

            migrationBuilder.DropTable(
                name: "OLSSummaryResults");

            migrationBuilder.DropTable(
                name: "PredictProbabilityDecisions");

            migrationBuilder.DropTable(
                name: "RegressionResults");

            migrationBuilder.DropTable(
                name: "RemainingBalanceOnLoans");

            migrationBuilder.DropTable(
                name: "ScikitLearnStatsmodelsPredictions");

            migrationBuilder.DropTable(
                name: "Scores");

            migrationBuilder.DropTable(
                name: "Shapes");

            migrationBuilder.DropTable(
                name: "SimpleInterestPrincipals");

            migrationBuilder.DropTable(
                name: "SimpleInterestRates");

            migrationBuilder.DropTable(
                name: "SimpleInterests");

            migrationBuilder.DropColumn(
                name: "ClassificationReportIdentifier",
                table: "ClassificationReports");
        }
    }
}
