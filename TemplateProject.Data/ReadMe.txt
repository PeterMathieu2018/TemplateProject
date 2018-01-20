Step 1: Add connection strings to App.config.
Step 2: Use reverse code first reverse generator (Rverse POCO generator) to generate entity framework classes.
Step 3: Use Repository generator to generate Repositories and unit of work.
Step 4: Copy files into new folder named for project. Example: ProjectName.Data.Core
Step 5: Delete generated filesd in Code Generation folder.

Notes: 
Use Settings.DbContextInterfaceName = "IDbContext"; in CodeFirstGenerator.tt to use IdbContext interface.
Be sure to specify namespace, context and prefix in repository generator.
Suggestion: Re-generate classes in code generation then edit in "Core" or other specified folder.
Suggestion: Use services when using multiple databases.

