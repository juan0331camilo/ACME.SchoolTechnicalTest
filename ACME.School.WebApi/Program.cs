using ACME.School.WebApi.Bootstrapper;
using ACME.School.WebApi.Endpoints;

// Get WebApplication instance
WebApplication app = AppBuilder.GetApp(args);

// Configure Request Pipeline
RequestPipelineBuilder.Configure(app);

// Configure APIs 
CommonEndpoint.RegisterApis(app);
AccountEndpoint.RegisterApis(app);
CourseEndpoint.RegisterApis(app);
StudentEndpoint.RegisterApis(app);
EnrollmentEndpoint.RegisterApis(app);

// Start the app
app.Run();