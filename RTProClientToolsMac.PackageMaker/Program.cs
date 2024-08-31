﻿using System.Diagnostics;

namespace RTProRTProClientToolsMac.PackageMaker;

internal class Program
{
    private static void Main(string[] args)
    {
        var productName = nameof(RTProRTProClientToolsMac);
        var version = "1.0.0";

        var projectDir1 = @"..\..\..\..\RTProClientToolsMac";
        //var installerDir = @"..\..\..\..\installer";
        //var projectDir = @"C:\Users\Vahid\Desktop\RTProClientToolsMac\RTProClientToolsMac\RTProClientToolsMac.csproj";
        var currentDir = AppDomain.CurrentDomain.BaseDirectory;
        //var publishFolder = Path.Combine(currentDir, "publish");

        //var targetInstaller = Path.Combine(currentDir, "installer");
        //CopyFilesRecursively(installerDir, targetInstaller);

        var applicationDir = Path.Combine(currentDir, "installer", "macOS-x64", "application");
        var commands = new List<string>()
        {
            $"cd {projectDir1}",
            $"dotnet publish -o {applicationDir}"
        };
        var ars = string.Join(" & ", commands);

        Process.Start("cmd.exe", "/C " + ars).WaitForExit();

        var s = Path.Combine(currentDir, "installer", "macOS-x64", "darwin");
        foreach (var file in Directory.GetFiles(s, "*", SearchOption.AllDirectories))
        {
            var content = File.ReadAllText(file);
            var content1 = content.Replace("__PRODUCT__", productName)
                .Replace("__VERSION__", version);
            File.WriteAllText(file, content1);
        }
        Console.ReadLine();
    }

    private static void CopyFilesRecursively(string sourcePath, string targetPath)
    {
        foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
        {
            Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
        }
        foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
        {
            File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
        }
    }
}