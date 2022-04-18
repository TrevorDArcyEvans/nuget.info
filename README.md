# nuget.info
Using local _nuget_ programmatically

# Background
Almost all sample code related using _nuget_ is for a remote _nuget_ server.
There is very little sample code on how to use a file based _nuget_ repository.

# Prerequisites
* .NET Core 6

# Getting started
```bash
$ git clone https://github.com/TrevorDArcyEvans/nuget.info.git
$ cd nuget.info
$ dotnet build
$ ./bin/Debug/net6.0/nuget.info nuget.packaging
```

# Further information
* https://devblogs.microsoft.com/nuget/play-with-packages/
* https://stackoverflow.com/questions/42174699/how-to-download-a-nupkg-package-from-nuget-programmatically-in-net-core
* https://www.meziantou.net/exploring-the-nuget-client-libraries.htm
* https://github.com/paraspatidar/NugetDownloader
* https://www.nuget.org/packages/nugetdownloader/
* https://www.nuget.org/packages?q=nuget

## Download pkg + extract
* https://stackoverflow.com/questions/39569318/how-to-download-and-unzip-packages-using-nuget-v3-api
* https://stackoverflow.com/questions/59415619/extract-a-dll-from-a-nuget-package-using-nuget-client
* https://docs.microsoft.com/en-us/nuget/reference/nuget-client-sdk
