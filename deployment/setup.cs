using System.Diagnostics;
using System.Reflection;
using IO = System.IO;
using System;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Forms;
using Microsoft.Deployment.WindowsInstaller;
using WixSharp;
using WixSharp.CommonTasks;

class Script
{
    static void Main(string[] args)
    {
        BuidMsi(is64: true, setupId: "6f930b47-2277-411d-9095-12214525889b", sourceDir: @".\bin");
        // BuidMsi(is64: false, guid: "6f930b47-2277-411d-9095-12214525889c", @".\binx86");
    }

    static void BuidMsi(bool is64, string setupId, string sourceDir)
    {
        var cpu = is64 ? "x64" : "x86";

        string pluginFile = IO.Path.GetFullPath(@"bin\plugins\PluginManager.dll");

        // remove the revision part as MSI update works corectly only with 3-part versions 
        var version = FileVersionInfo.GetVersionInfo(pluginFile).ClearRevision();

        var msiName = "PluginManager." + version + "." + cpu + ".msi";

        var project =
            new Project("PluginManager for Notepad++ (" + cpu + ")",

                new Dir(@"%ProgramFiles%\Notepad++",
                    new Dir("plugins",
                        new File(@"bin\plugins\PluginManager.dll") { OverwriteOnInstall = true }),
                    new Dir("updater",
                        new File(@"bin\updater\gpup.exe") { OverwriteOnInstall = true })),

                new CloseApplication("notepad++.exe", true, false) { Timeout = 5 });

        project.SourceBaseDir = sourceDir;

        project.ControlPanelInfo.UrlInfoAbout = "https://github.com/bruderstein/nppPluginManager/";
        project.ControlPanelInfo.Contact = "Product owner";
        project.ControlPanelInfo.Manufacturer = "Dave Brotherstone";

        project.GUID = new Guid(setupId);

        project.Version = version;
        project.Platform = is64 ? Platform.x64 : Platform.x86;
        project.MajorUpgradeStrategy = MajorUpgradeStrategy.Default;
        project.LicenceFile = "license.rtf";

        project.UI = WUI.WixUI_InstallDir;

        project.PreserveTempFiles = true;
        project.EmitConsistentPackageId = true;

        Compiler.BuildMsi(project, @"..\" + msiName);
    }
}