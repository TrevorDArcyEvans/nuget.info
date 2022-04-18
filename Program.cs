namespace nuget.info;

using NuGet.Configuration;
using NuGet.Frameworks;
using NuGet.Packaging;

internal static class Program
{
  public static void Main(string[] args)
  {
    var settings = Settings.LoadDefaultSettings(null);

    // /home/trevorde/.nuget/packages/
    var globPackDir = SettingsUtility.GetGlobalPackagesFolder(settings);
    Console.WriteLine($"GlobalPackagesFolder = {globPackDir}");
    Console.WriteLine("-------------------------");

    // [blank]
    var repoPath = SettingsUtility.GetRepositoryPath(settings);

    //var nuspecPath = Path.Combine(globPackDir, "autofac", "6.0.0", "autofac.nuspec");
    var nuspecPath = "/home/trevorde/.nuget/packages/quikgraph/2.3.0/quikgraph.nuspec";
    //var nuspecPath = "/home/trevorde/.nuget/packages/blazored.modal/7.0.0-preiew.2/blazored.modal.nuspec";
    //var nuspecPath = "/home/trevorde/.nuget/packages/blazor.extensions.canvas/1.1.1/blazor.extensions.canvas.nuspec";
    var nuspecRdr = new NuspecReader(nuspecPath);
    var isSpec = PackageHelper.IsNuspec(nuspecPath);
    Console.WriteLine($"{nuspecPath} --> {isSpec}");

    Console.WriteLine($"GetDependencyGroups:");
    Dump(nuspecRdr.GetDependencyGroups());
    Console.WriteLine("-------------------------");


    //var archivePath = "/home/trevorde/.nuget/packages/blazorise/0.9.3.6/blazorise.0.9.3.6.nupkg";
    var archivePath = "/home/trevorde/.nuget/packages/quikgraph/2.3.0/quikgraph.2.3.0.nupkg";
    var isPkg = PackageHelper.IsPackageFile(archivePath, PackageSaveMode.Defaultv3);
    Console.WriteLine($"{archivePath} --> {isPkg}");
    var par = new PackageArchiveReader(archivePath);
    Dump(par.GetLibItems());
    Console.WriteLine();
  }




  private static void Dump(IEnumerable<FrameworkSpecificGroup> groups)
  {
    foreach (var grp in groups)
    {
      Console.WriteLine($"  {grp.TargetFramework}");
      foreach (var item in grp.Items)
      {
        var isAssy = PackageHelper.IsAssembly(item);
        Console.WriteLine($"    {item} --> {isAssy}");
      }
    }
  }

  private static void Dump(IEnumerable<PackageDependencyGroup> groups)
  {
    groups.ToList().ForEach(g => Dump(g.TargetFramework));
  }

  private static void Dump(NuGetFramework ngf)
  {
    Console.WriteLine($"  {ngf.Framework} : {ngf.Version.ToString()}");
  }
}
