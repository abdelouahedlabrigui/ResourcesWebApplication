# ResourcesWebApplication

## Description
ResourcesWebApplication is an ASP.NET Core web API designed to manage and provide access to diverse resources, with a strong emphasis on machine learning, natural language processing (NLP), aviation datasets, and biographical data analysis. The MachineLearningController is a key component, offering endpoints for entity recognition, aviation pioneer biographies, linguistic feature extraction, and statistical modeling, among others. This project serves as a backend for applications requiring advanced data processing and resource management.

## Features

**Machine Learning Capabilities:** Supports NLP (entity recognition, parse trees, noun chunks), aviation data analysis, and statistical modeling (e.g., regression, feature importance).
**RESTful API:** Provides GET and POST endpoints for data retrieval and storage.
**Aviation Focus:** Includes datasets and tools for analyzing aviation pioneers and aircraft structures.
**Modular Design:** Built with extensibility in mind, leveraging Entity Framework Core for database interactions.
**Highlighted Resources:** Integrates Documentations, Documents, Marks, Readings, Prompts, Plaintexts, and Chronometers as key components (see below).

## Prerequisites

**.NET Core SDK:** Version 5.0 or later.
**IDE:** Visual Studio, VS Code, or any C# development environment.
**Database:** SQL Server or compatible database with Entity Framework Core (configured via ApplicationDbContext).
**Dependencies:** Requires models from namespaces like ResourcesWebApplication.Models.MachineLearning, ResourcesWebApplication.Models.Aviation, etc.

## Installation

### **1. Clone the Repository:**
```bash
    git clone https://github.com/<your-username>/ResourcesWebApplication.git
    cd ResourcesWebApplication
```
### **2. Restore Dependencies:**
```bash
    dotnet restore
```
### **3. Configure the Application:**

    * Update appsettings.json with your database connection string (e.g., for SQL Server).
    * Ensure the database is migrated:
```bash
        dotnet ef migrations add InitialCreate
        dotnet ef database update
```
### **4. Build and Run:**
```bash
    dotnet build
    dotnet run

    Access the API at http://localhost:5000 (or your configured port).
```
## Usage

## MachineLearningController Overview

The MachineLearningController provides endpoints for machine learning and data analysis tasks, routed under [controller] (i.e., /MachineLearning). Below are key functionalities:

### **1. Health Check Endpoints**

- **Returns a 200 OK status to verify API health.**
    ```http
    * GET /MachineLearning/Get200OKSatusTest
    ```

### **2. Aircraft Structures**

- **Retrieves the latest aircraft structure concept.**
    ```http
    * GET /MachineLearning/GetTopDescConcept
    ```
- **Fetches the latest concept definition, cleaning up invalid console messages.**
    ```http
    * GET /MachineLearning/GetTopDescConceptDefinition
    ```
- **Adds a new aircraft structure definition if it doesn’t exist.**
    ```http
    * GET /MachineLearning/PostAircraftStructureGeneratedDefinition?concept={concept}&definition={definition}
    ```
- **Adds a new aircraft structure concept if unique.**
    ```http
    * GET /MachineLearning/PostAircraftStructureConcept?concept={concept}
    ```

### **3. Aviation Pioneers**

- **Lists distinct dataset names for aviation pioneers (IDs 79351–121740).**
    ```http
    * GET /MachineLearning/GetAviationPioneersDatasetNames
    ```
- **Retrieves entity data for a person’s life by dataset name.**
    ```http
    * GET /MachineLearning/GetPersonLifeFromEntitiesByDatasetName?datasetName={datasetName}
    ```
- **Fetches life events by year for a dataset.**
    ```http
    * GET /MachineLearning/GetPersonLifeByYearsByDatasetName?datasetName={datasetName}
    ```
- **Adds birth and death dates for notable aviation pioneers (e.g., Amelia Earhart, Orville Wright).**
    ```http
    * GET /MachineLearning/PostPersonBirthAndDeathDates
    ```
- **Automates posting life events by year for all persons in PersonLifespans.**
    ```http
    * GET /MachineLearning/RequestAutomatePostingPersonsLivesByYears
    ```

### **4. NLP and Linguistic Features**

- **Extracts linguistic features (e.g., Token, Entity, ParseTree) based on query parameters.**
    ```http
    * GET /MachineLearning/GetLinguisticFeatures?feature={feature}&queryType={queryType}&filename={filename}&label={label}
    ```
- **Saves entity recognition data (body: Entity).**
    ```http
    * POST /MachineLearning/POSTEntityRecognition
    ```
- **Stores parse tree data (body: ParseTree).**
    ```http
    * POST /MachineLearning/POSTParseTree
    ```
- **Adds noun chunk data (body: NounChunk).**
    ```http
    * POST /MachineLearning/POSTNounChunks
    ```

### **5. Biographical Data**

- **Retrieves biography questions for analysis.**
    ```http
    * GET /MachineLearning/SearchBiographyQuestionsAsync
    ```
- **Saves answers to biography questions (body: BiographyQuestionAnswer).**
    ```http
    * POST /MachineLearning/PostBiographyQuestionAnswer
    ```
- **Adds a person’s short biography (body: Person).**
    ```http
    * POST /MachineLearning/PostPersonShortBiography
    ```

### **6. Statistical Modeling**

- **Stores feature importance data (body: FeatureImportance).**
    ```http
    * POST /MachineLearning/PostFeatureImportances
    ```
- **Retrieves feature importance by dataset.**
    ```http
    * GET /MachineLearning/GetFeatureImportancesByDatasetName?datasetName={datasetName}
    ```
- **Saves best hyperparameters (body: BestHyperParameter).**
    ```http
    * POST /MachineLearning/PostBestHyperParameters
    ```
- **Fetches plot data by dataset.**
    ```http
    * GET /MachineLearning/GetPlotsByDatasetName?datasetName={datasetName}
    ```

## Highlighted Controllers Integration

While not directly in MachineLearningController, the following controllers align with the project’s scope and are emphasized here:

- **Documentations:** Likely stores API or system documentation related to machine learning processes.
    * Example Use: Document endpoint usage (GET /MachineLearning/GetLinguisticFeatures).
- **Documents:** Manages raw files (e.g., Amelia_Earhart.txt) processed by NLP endpoints.
    * Example: GET /MachineLearning/GetEntities?filename=Amelia_Earhart.txt retrieves entities from a document.
- **Marks:** Could tag or score machine learning outputs (e.g., accuracy of FeatureImportance).
- **Readings:** Tracks reading or processing times for datasets, potentially linked to Chronometers.
    * Example: Time taken to process GetPersonLifeByYearsByDatasetName.
- **Prompts:** Manages input prompts for biography questions or NLP tasks.
    * Example: POST /MachineLearning/PostPersonBiographyQuestions uses prompts.
- **Plaintexts:** Stores unformatted text inputs for analysis (e.g., biography data).
    * Example: Source for LocalPostPersonLifeByYears.
- **Chronometers:** Measures execution time for tasks like RequestAutomatePostingPersonsLivesByYears.
    * Example API Calls

### **Fetch aviation pioneer dataset names:**
```bash
curl -X GET "http://localhost:5000/MachineLearning/GetAviationPioneersDatasetNames"
```
### **Post a person’s biography:**
```bash
curl -X POST "http://localhost:5000/MachineLearning/PostPersonShortBiography" \
-H "Content-Type: application/json" \
-d '{"FullName": "Amelia Earhart", "Biography": "First woman to fly solo across the Atlantic"}'
```
### **Get linguistic features:**
```bash
curl -X GET "http://localhost:5000/MachineLearning/GetLinguisticFeatures?feature=Entity&queryType=Count%20Entity%20Labels&filename=Amelia_Earhart.txt"
```
## API Documentation

Full endpoint details are in the controller code (e.g., MachineLearningController.cs).
Use Postman or Swagger (if integrated) for interactive testing.

## Acknowledgments

Built with ASP.NET Core and Entity Framework Core.
Inspired by aviation history and machine learning advancements.
