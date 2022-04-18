namespace nuget.info;

using NuGet.Configuration;
using NuGet.Frameworks;
using NuGet.Packaging;
using NuGet.Packaging.Core;

internal static class Program
{
  public static void Main(string[] args)
  {
    var target = args[0].ToLowerInvariant();
    Console.WriteLine($"Looking for:  {target}");
    Console.WriteLine("-------------------------");

    var settings = Settings.LoadDefaultSettings(null);

    // /home/trevorde/.nuget/packages/
    var globPackDir = SettingsUtility.GetGlobalPackagesFolder(settings);
    Console.WriteLine($"GlobalPackagesFolder = {globPackDir}");
    Console.WriteLine("-------------------------");

    // [blank]
    var repoPath = SettingsUtility.GetRepositoryPath(settings);

    var nuspecDir = Path.Combine(globPackDir, target);
    var nuspecVerDirs = Directory.EnumerateDirectories(nuspecDir);
    foreach (var nuspecVerDir in nuspecVerDirs)
    {
      var nuspecFilePath = Path.Combine(nuspecVerDir, $"{target}{PackagingCoreConstants.NuspecExtension}");
      var nuspecPath = Path.Combine(globPackDir, target, nuspecVerDir, nuspecFilePath);
      var nuspecRdr = new NuspecReader(nuspecPath);
      var isSpec = PackageHelper.IsNuspec(nuspecPath);
      Console.WriteLine($"{nuspecPath} --> {isSpec}");

      Console.WriteLine($"DependencyGroups:");
      var depGrps = nuspecRdr.GetDependencyGroups();
      Dump(depGrps);
      Console.WriteLine("-------------------------");

      var nuspecVer = Path.GetFileName(nuspecVerDir);
      var archiveFilePath = Path.Combine(nuspecVerDir, $"{target}.{nuspecVer}{PackagingCoreConstants.NupkgExtension}");
      var isPkg = PackageHelper.IsPackageFile(archiveFilePath, PackageSaveMode.Defaultv3);
      Console.WriteLine($"{archiveFilePath} --> {isPkg}");
      var par = new PackageArchiveReader(archiveFilePath);
      Dump(par.GetLibItems());
      Console.WriteLine();
    }
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
