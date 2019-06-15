#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0

var target = Argument("target", "Default");

var directory = @".\src\bin";

var bin = Directory(directory);

var configuration = Argument("Configuration", "Release");
Task("clean-build-directory")
    .Does(() =>
{
    
    Information($"Cleaning {directory}....");
    CleanDirectory(bin);
});
Task("building-Kalev.Framework.Cqrs.EventSourcing")
    .IsDependentOn("clean-build-directory")
    .Does(() =>
{
    Information("Building Project....");    
    DotNetCoreBuild(@".\src\Kalev.Framework.Cqrs.EventSourcing.csproj",
    new DotNetCoreBuildSettings() {
        Configuration = configuration,
    }
    );

});
Task("Default")
  .IsDependentOn("building-Kalev.Framework.Cqrs.EventSourcing")
  .Does(() =>
{
  Information("build Complete");
});

RunTarget(target);