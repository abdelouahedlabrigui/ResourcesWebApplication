using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Documents;
using ResourcesWebApplication.Models.Chronometers;
using ResourcesWebApplication.Models.Commands;
using ResourcesWebApplication.Models.Prompts;
using ResourcesWebApplication.Models.Dictionaries;
using ResourcesWebApplication.Models.Astrology;
using ResourcesWebApplication.Models.Tenses;
using ResourcesWebApplication.Models.Knowledge;
using ResourcesWebApplication.Models.Networking.IPv4;
using ResourcesWebApplication.Models.Servers.Users;
using ResourcesWebApplication.Models.Servers.Groups;
using ResourcesWebApplication.Models.Servers.Backups;
using ResourcesWebApplication.Models.Cisco;
using ResourcesWebApplication.Models.Games.TwoPlayers;
using ResourcesWebApplication.Models.Games;
using ResourcesWebApplication.Models.Politics;
using ResourcesWebApplication.Models.MachineLearning;
using ResourcesWebApplication.Models.MachineLearning.Visualizations;
using ResourcesWebApplication.Models.Endpoints;
using ResourcesWebApplication.Models.MachineLearning.Datasets;
using ResourcesWebApplication.Models.FinancialFormulas.BankingFormulas;
using ResourcesWebApplication.Models.Resume;
using ResourcesWebApplication.Models.Forensics;
using ResourcesWebApplication.Models.Accounting;
using ResourcesWebApplication.Models.Infrastractures;
using ResourcesWebApplication.Models.MachineLearning.Astronomy;
using ResourcesWebApplication.Models.MachineLearning.Astronomy.Problems;
using ResourcesWebApplication.Models.LanguageProcessing;
using ResourcesWebApplication.Models.Exceptions;
using ResourcesWebApplication.Models.LanguageProcessing.QueryProcessing;
using ResourcesWebApplication.Models.LanguageProcessing.Visualization;
using ResourcesWebApplication.Models.Secrets;
using ResourcesWebApplication.Models.Jira;
using ResourcesWebApplication.Models.News;
using ResourcesWebApplication.Models.Devops;
using ResourcesWebApplication.Models.GenerativeAI;
using ResourcesWebApplication.Models.Politics.Law;
using ResourcesWebApplication.Models.Politics.Law.Bankruptcy;
using ResourcesWebApplication.Models.Devops.Wazuh;
using ResourcesWebApplication.Models.Requests;
using ResourcesWebApplication.Models.Accounting.DeterminationDesCoutsEtDesResultatsCasParticulier;
using ResourcesWebApplication.Models.Trees;
using ResourcesWebApplication.Models.Documents.Summary;
using ResourcesWebApplication.Models.Careers;
using ResourcesWebApplication.Models.Accounting.RegularisationDesStocks;
using ResourcesWebApplication.Models.Accounting.Travaux;
using ResourcesWebApplication.Models.MachineLearning.Weather;
using ResourcesWebApplication.Models.Prompts.MachineLearning;
using ResourcesWebApplication.Models.MachineLearning.Aviation;
using ResourcesWebApplication.Models.MachineLearning.Aviation.Datasets;
using ResourcesWebApplication.Models.GenerativeAI.Biography;
using ResourcesWebApplication.Models.SourceCodes.AtmosphericSciences;
using ResourcesWebApplication.Models.SourceCodes.QuantumMechanics;
using ResourcesWebApplication.Models.SourceCodes.Aerodynamics;
using ResourcesWebApplication.Models.SourceCodes.AircraftPropulsions;
using ResourcesWebApplication.Models.SourceCodes.Seismology;
using ResourcesWebApplication.Models.Aerospace;
using ResourcesWebApplication.Models.Finance;

namespace ResourcesWebApplication.Models.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {}
        public DbSet<Plaintext> Plaintexts { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Chronometer> Chronometers { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<Code> Codes { get; set; }
        public DbSet<Cli> Commands { get; set; }
        public DbSet<Prompt> Prompts { get; set; }
        public DbSet<Dictionary> Dictionaries { get; set; }
        public DbSet<Zodiac> Zodiacs { get; set; }
        public DbSet<Reading> Readings { get; set; }
        public DbSet<Tense> Tenses { get; set; }
        public DbSet<Conjugation> Conjugations { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<Category> Categories { get; set; }

        private DbSet<Address> addresses;

        public DbSet<Address> GetAddresses()
        {
            return addresses;
        }

        public void SetAddresses(DbSet<Address> value)
        {
            addresses = value;
        }
        public DbSet<User> RockyUsers { get; set; }
        public DbSet<Group> RockyGroups { get; set; }
        public DbSet<Backup> RockyBackup { get; set; }
        public DbSet<ResourcesWebApplication.Models.Servers.Users.Hash> Hashes { get; set; }
        public DbSet<IPAddress> IPAddresses { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Port> Ports { get; set; }
        public DbSet<CiscoCommand> CiscoCommands { get; set; }
        public DbSet<Container> Containers { get; set; }
        public DbSet<Choice> Choices {get;set;}
        public DbSet<Player> Players { get; set; }
        public DbSet<Preference> Preferences { get; set; }
        public DbSet<Strategy> Strategies { get; set; }
        public DbSet<Utility> Utilities { get; set; }
        public DbSet<DualPlayers> DualPlayers { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<PlayersDataset> PlayersDatasets { get; set; }
        public DbSet<USPresident> USPresidents { get; set; }
        public DbSet<Info> Infos { get; set; }
        public DbSet<Describe> Describes { get; set; }
        public DbSet<Columns> Columns { get; set; }
        public DbSet<Plot> Plots { get; set; }
        public DbSet<PlotByColumn> PlotByColumns { get; set; }
        public DbSet<USVicePresident> USVicePresidents { get; set; }
        public DbSet<Correlation> Correlations { get; set; }
        public DbSet<Coefficient> Coefficients { get; set; }
        public DbSet<EndPoint> EndPoints { get; set; }
        public DbSet<EndpointMetadata> UrlEndPoints { get; set; }
        public DbSet<LinearRegression> LinearRegressions { get; set; }
        public DbSet<LogisticRegression> LogisticRegressions { get; set; }
        public DbSet<ClassificationReport> ClassificationReports { get; set; }
        public DbSet<Dataset> Datasets { get; set; }
        public DbSet<Mean> Means { get; set; }

        
        public DbSet<AnnualPercentageYield> AnnualPercentageYields { get; set; }
        public DbSet<BalloonBalanceOfLoan> BalloonBalanceOfLoans { get; set; }
        public DbSet<BalloonLoanPayment> BalloonLoanPayments { get; set; }
        public DbSet<CompoundInterest> CompoundInterests { get; set; }
        public DbSet<DebtToIncomeRation> DebtToIncomeRations { get; set; }
        public DbSet<LoanPayment> LoanPayments { get; set; }
        public DbSet<LoanToDepositRatio> LoanToDepositRatios { get; set; }
        public DbSet<LoanToValueRatio> LoanToValueRatios { get; set; }
        public DbSet<RemainingBalanceOnLoan> RemainingBalanceOnLoans { get; set; }
        public DbSet<SimpleInterest> SimpleInterests { get; set; }
        public DbSet<SimpleInterestPrincipal> SimpleInterestPrincipals { get; set; }
        public DbSet<SimpleInterestRate> SimpleInterestRates { get; set; }
        public DbSet<ArrayCoefficient> ArrayCoefficients { get; set; }
        public DbSet<EstimationWithConfusionMatrix> EstimationWithConfusionMatrices { get; set; }
        public DbSet<Intercept> Intercepts { get; set; }
        public DbSet<Missing> Missings { get; set; }
        public DbSet<RegressionResult> RegressionResults { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<Shape> Shapes { get; set; }
        public DbSet<PredictProbabilityDecision> PredictProbabilityDecisions { get; set; }
        public DbSet<EstimationWithConfusionMatrix> EstimationWithConfusionMatrixs { get; set; }
        public DbSet<GeneralizedLinearModelRegressionResult> GeneralizedLinearModelRegressionResults { get; set; }
        public DbSet<ScikitLearnStatsmodelsPrediction> ScikitLearnStatsmodelsPredictions { get; set; }
        public DbSet<OLSSummaryResult> OLSSummaryResults { get; set; }
        public DbSet<Education> Educations {get;set;}
        public DbSet<PersonalInformation> PersonalInformations {get;set;}
        public DbSet<Language> Languages {get;set;}
        // public DbSet<Experience> Experiences {get;set;}
        public DbSet<SkillModel> SkillModels {get;set;}
        public DbSet<Project> Projects {get;set;}
        public DbSet<SARAMIXSummaryResult> SARAMIXSummaryResults { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Data> DataAddresses { get; set; }
        public DbSet<PlotMetadata> PlotMetadataDetails { get; set; }
        public DbSet<PlanComptable> PlanComptableMarocain { get; set; }
        public DbSet<Figure> Figures { get; set; }
        public DbSet<NoComptesMontant> NoComptesMontants { get; set; }
        public DbSet<TravailAFaire> TravauxAFaire { get; set; }
        public DbSet<Enancee> Enancees { get; set; }
        public DbSet<CriticalInfrastructure> Infrastractures { get; set; }
        public DbSet<RegressionPlot> RegressionPlots { get; set; }
        public DbSet<PhysicalConstant> PhysicalConstants { get; set; }
        public DbSet<AstrophysicalQuantity> AstrophysicalQuantities { get; set; }
        public DbSet<GreekAlphabet> GreekAlphabets { get; set; }
        public DbSet<SIPrefix> SIPrefixes { get; set; }
        public DbSet<PlasmaFrequency> PlasmaFrequencies { get; set; }
        public DbSet<Indicator> Indicators { get; set; }
        public DbSet<WebPage> WebDocuments { get; set; }

        public DbSet<QueryData> QueryDataStores { get; set; }
        public DbSet<ColumnDescription> EntityDescriptions { get; set; }
        public DbSet<EntityRecognition> EntityRecognitions { get; set; }

        public DbSet<Entity> Entities { get; set; }
        public DbSet<ParseTree> ParseTrees { get; set; }
        public DbSet<LocalTree> LocalTrees { get; set; }
        public DbSet<NounChunk> NounChunks { get; set; }
        public DbSet<Pos> PartOfSpeech { get; set; }
        public DbSet<NLPSentence> NLPSentences { get; set; }
        // public DbSet<PosCount> PosCounts { get; set; }
        public DbSet<DictionnaireManagementProjet> DictionnaireManagementProjets { get; set; }
        public DbSet<VocabulaireDroit> VocabulaireDroits { get; set; }
        public DbSet<LogException> LogExceptions { get; set; }
        public DbSet<YearDifference> YearDifferences { get; set; }
        public DbSet<MainCentury> MainCenturies { get; set; }
        public DbSet<SentimentPlot> SentimentPlots { get; set; }
        public DbSet<PregnancyOutcome> PregnancyOutcomes { get; set; }
        public DbSet<Documentation> Documentations { get; set; }
        public DbSet<Password> Passwords { get; set; }
        public DbSet<Error> Errors { get; set; }
        public DbSet<ForgeHelp> ForgeHelps { get; set; }
        public DbSet<NewsArticle> NewsArticles { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<AnswerSentiment> AnswerSentiments { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<PingResult> PingResults { get; set; }
        public DbSet<Auth> Auths { get; set; }
        public DbSet<ServiceStatus> ServiceStatus { get; set; }
        public DbSet<ServiceCheck> ServiceChecks { get; set; }
        public DbSet<NlpQuestion> NlpQuestions { get; set; }
        public DbSet<ActiveConfiguration> ActiveConfigurations { get; set; }
        public DbSet<LinuxAgent> LinuxAgents { get; set; }
        public DbSet<NewsTitleDescription> NewsTitleDescriptions { get; set; }
        public DbSet<NewsQuestion> NewsQuestions { get; set; }
        public DbSet<Profession> Professions {get;set;} // Bankruptcy

        public DbSet<BankruptcyEntity> BankruptcyEntities {get; set;} 
        public DbSet<BankruptcyParseTree> BankruptcyParseTrees {get; set;} 
        public DbSet<BankruptcyLocalTree> BankruptcyLocalTrees {get; set;} 
        public DbSet<BankruptcyNounChunk> BankruptcyNounChunks {get; set;} 
        public DbSet<BankruptcyPos> BankruptcyPartOfSpeech {get; set;} 
        public DbSet<BankruptcyNLPSentence> BankruptcyNLPSentences {get; set;} 
        public DbSet<WazuhEndPoint> WazuhEndPoints { get; set; }
        public DbSet<CodeGen> CodeGenerators { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<NewsDocument> NewsDocuments { get; set; }

        public DbSet<PlainTextNewsDocument> PlainTextNewsDocuments { get; set; }
        public DbSet<LinguisticFeatureEndpoint> LinguisticFeatureEndpoints { get; set; }
        public DbSet<Connect> NetworkAutomationConnections { get; set; }
        public DbSet<Exemple> Exemples { get; set; }
        public DbSet<Objet> Objets { get; set; }
        public DbSet<Condition> Conditions { get; set; }
        public DbSet<Enoncee> Enoncees { get; set; }
        public DbSet<Instruction> Instructions { get; set; }
        public DbSet<ExempleSerie> ExempleSeries { get; set; }
        public DbSet<PSDrive> PSDrives { get; set; }
        public DbSet<PSProvider> PSProviders { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Root> Roots { get; set; }
        public DbSet<InitialMetadata> InitialMetadata { get; set; }
        public DbSet<InitialMetadatDetail> initialMetadatDetails { get; set; }
        public DbSet<VersionInfo> VersionInfos { get; set; }
        public DbSet<CodeGenInterpretion> CodeGenInterpretions { get; set; }
        public DbSet<RequestSuccess> RequestSuccesses { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Subsection> Subsections { get; set; }
        public DbSet<Summary> Summaries { get; set; }
        public DbSet<Definition> Definitions { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Responsibility> Responsibilities { get; set; }
        public DbSet<Requirement> Requirements { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<NgComponent> NgComponents { get; set; }
        public DbSet<Information_ex1> Information_Ex1s { get; set; }
        public DbSet<TravauxAFaire> TravauxAFaires { get; set; }
        public DbSet<CommandGenInterpretation> CommandGenInterpretations { get; set; }
        public DbSet<TerraformPrompts> TerraformPromptsRequests { get; set; }
        public DbSet<WeatherCorrelation> WeatherCorrelations { get; set; }
        public DbSet<GenInfoInterpretation> GenInfoInterpretations { get; set; }
        public DbSet<MLConcept> MLConcepts { get; set; }
        public DbSet<ADFTest> ADFTests { get; set; }
        public DbSet<MeanError> MeanErrors { get; set; }
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
        public DbSet<WeatherSummary> WeatherSummaries { get; set; }
        public DbSet<FeatureImportance> FeatureImportances { get; set; }
        public DbSet<BestHyperParameter> BestHyperParameters { get; set; }
        public DbSet<ClassificationReporting> ClassificationReportings { get; set; }
        public DbSet<SVMBestHyperParameter> SVMBestHyperParameters { get; set; }
        public DbSet<Accident> Accidents { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Life> Lives { get; set; }
        public DbSet<NounChunkByYear> NounChunkByYears { get; set; }
        public DbSet<ParseTreeByYear> ParseTreeByYears { get; set; }
        public DbSet<BiographyQuestion> BiographyQuestions { get; set; }
        public DbSet<LifeExperience> LifeExperiences { get; set; }
        public DbSet<BiographyQuestionAnswer> BiographyQuestionAnswers { get; set; }
        public DbSet<EventYear> EventYears { get; set; }
        public DbSet<PersonLifespan> PersonLifespans { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleDetail> RoleDetails { get; set; }
        public DbSet<AtmosphericFileInfo> AtmosphericFileInfos { get; set; }
        public DbSet<QuantumMechanicFileInfo> QuantumMechanicFileInfos { get; set; }
        public DbSet<AerodynamicFileInfo> AerodynamicFileInfos { get; set; }
        public DbSet<AircraftPropulsionFileInfo> AircraftPropulsionFileInfos { get; set; }
        public DbSet<SeismologyFileInfo> SeismologyFileInfos { get; set; }
        public DbSet<AtmosphericCodeLine> AtmosphericCodeLines { get; set; }
        public DbSet<AtmosphericCodeLinePrompt> AtmosphericCodeLinePrompts { get; set; }
        public DbSet<AircraftStructure> AircraftStructures { get; set; }
        public DbSet<AircraftStructuresConceptDefinition> AircraftStructuresConceptDefinitions { get; set; }
        public DbSet<TestResult> TestResults { get; set; }

    }
}