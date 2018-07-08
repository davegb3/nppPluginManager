Notepad++ PluginManager MSI setup solution
----------------------------------------------------

The solution is based on the WixSharp (C# binding for WiX/MSI).

The solution allows building both x64 and x86 MSIs. Though since this repository is for the x64 plugin only x64 build is enabled.

The setup expects the target Notepad++ to be installed in the default `%ProgramFiles%\Notepad++` location though user can specify the custom location if required.

Currently it is implemented as a C# build script.

Build instructions:
1. Run build.cmd

Note: the script engine (cscs.exe), WixSharp assembly, and MS installer interface assembly (Microsoft.Deployment.WindowsInstaller) and PluginManager v1.4.11.0 are included in the solution.

However WiX will need to be installed manually (e.g. "choco install wixtoolset" from https://chocolatey.org/packages/wixtoolset).

---

If VS solution is preferred I can share VS project for the setup.cs. With VS solution it is also possible to get all dependencied (even WiX binaries) as a NuGet package.
