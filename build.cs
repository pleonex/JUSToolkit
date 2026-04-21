#!/usr/bin/env dotnet run
#:property PublishAot=false
#:package Cake.Frosting.PleOps.Recipe@1.0.4-preview.33

using System.Diagnostics.CodeAnalysis;
using Cake.Common.IO;
using Cake.Common.Net;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Frosting;
using Cake.Frosting.PleOps.Recipe;
using Cake.Frosting.PleOps.Recipe.Dotnet;

return new CakeHost()
    .AddAssembly(typeof(PleOpsBuildContext).Assembly)
    .UseContext<BuildContext>()
    .UseLifetime<BuildLifetime>()
    .Run(args);

public sealed class BuildContext : PleOpsBuildContext
{
    public BuildContext(ICakeContext context)
        : base(context)
    {
    }

    public string? TestResourceUri { get; private set; }

    public string? TestResourceName { get; private set; }

    [LogIgnore]
    public string? TestResourceUsername { get; private set; }

    [LogIgnore]
    public string? TestResourcePassword { get; private set; }

    public override void ReadArguments()
    {
        base.ReadArguments();

        Arguments.SetIfPresent("resource-uri", x => TestResourceUri = x);
        Arguments.SetIfPresent("resource-name", x => TestResourceName = x);
        Arguments.SetIfPresent("resource-usr", x => TestResourceUsername = x);
        Arguments.SetIfPresent("resource-pwd", x => TestResourcePassword = x);
    }
}

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
public sealed class BuildLifetime : FrostingLifetime<BuildContext>
{
    public override void Setup(BuildContext context, ISetupContext info)
    {
        context.WarningsAsErrors = false;
        context.DotNetContext.CoverageTarget = 0;

        context.DotNetContext.ApplicationProjects.Add(
            new ProjectPublicationInfo(
                "./src/JUS.CLI",
                [ "win-x64", "linux-x64", "osx-x64" ],
                "net8.0"));

        context.ReadArguments();

        context.DotNetContext.PreviewNuGetFeed = "https://pkgs.dev.azure.com/SceneGate/SceneGate/_packaging/SceneGate-Preview/nuget/v3/index.json";

        context.Print();
    }

    public override void Teardown(BuildContext context, ITeardownContext info)
    {
        context.DeliveriesContext.Save();
    }
}

[TaskName("Default")]
[IsDependentOn(typeof(Cake.Frosting.PleOps.Recipe.Common.SetGitVersionTask))]
[IsDependentOn(typeof(Cake.Frosting.PleOps.Recipe.Common.CleanArtifactsTask))]
[IsDependentOn(typeof(Cake.Frosting.PleOps.Recipe.Dotnet.DotnetTasks.BuildProjectTask))]
public sealed class DefaultTask : FrostingTask
{
}

[TaskName("Bundle")]
[IsDependentOn(typeof(Cake.Frosting.PleOps.Recipe.Common.SetGitVersionTask))]
[IsDependentOn(typeof(Cake.Frosting.PleOps.Recipe.GitHub.ExportReleaseNotesTask))]
[IsDependentOn(typeof(Cake.Frosting.PleOps.Recipe.Dotnet.DotnetTasks.BundleProjectTask))]
[IsDependentOn(typeof(Cake.Frosting.PleOps.Recipe.DocFx.BuildTask))]
public sealed class BundleTask : FrostingTask
{
}

[TaskName("Deploy")]
[IsDependentOn(typeof(Cake.Frosting.PleOps.Recipe.Common.SetGitVersionTask))]
[IsDependentOn(typeof(Cake.Frosting.PleOps.Recipe.Dotnet.DotnetTasks.DeployProjectTask))]
[IsDependentOn(typeof(Cake.Frosting.PleOps.Recipe.GitHub.UploadReleaseBinariesTask))]
public sealed class DeployTask : FrostingTask
{
}

[TaskName("Download-TestFiles")]
[TaskDescription("Download the test resource files")]
[IsDependeeOf(typeof(Cake.Frosting.PleOps.Recipe.Dotnet.TestTask))]
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
public class DownloadTestFilesTask : FrostingTask<BuildContext>
{
    public override bool ShouldRun(BuildContext context) =>
        !string.IsNullOrEmpty(context.TestResourceUri);

    public override void Run(BuildContext context)
    {
        string resourcesPath = Path.GetFullPath(Path.Combine("resources", "tests"));
        if (Directory.Exists(resourcesPath)) {
            context.Log.Information("Test files already exists, skipping download.");
            return;
        }

        string resourceUri = string.Format(context.TestResourceUri!, context.TestResourceName);
        var downloadSettings = new DownloadFileSettings {
            Username = context.TestResourceUsername,
            Password = context.TestResourcePassword,
            UseDefaultCredentials = string.IsNullOrWhiteSpace(context.TestResourceUsername) || string.IsNullOrWhiteSpace(context.TestResourcePassword),
        };
        context.Log.Information(downloadSettings.UseDefaultCredentials
            ? "Download without credentials"
            : "Download will use provided password");

        context.Log.Information("Downloading resource");
        var compressedResources = context.DownloadFile(resourceUri, downloadSettings);

        context.Log.Debug("Unzipping resource");
        context.Unzip(compressedResources, resourcesPath);
    }
}
