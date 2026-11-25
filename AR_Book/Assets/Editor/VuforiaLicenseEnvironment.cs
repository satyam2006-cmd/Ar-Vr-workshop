using System;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using Vuforia;

[InitializeOnLoad]
internal sealed class VuforiaLicenseEnvironment : IPreprocessBuildWithReport
{
    private const string LicenseKeyVariable = "VUFORIA_LICENSE_KEY";
    private const string UfoLicenseKeyVariable = "VUFORIA_UFO_LICENSE_KEY";

    static VuforiaLicenseEnvironment()
    {
        EditorApplication.delayCall += LoadForEditor;
    }

    public int callbackOrder => 0;

    public void OnPreprocessBuild(BuildReport report)
    {
        if (!TryLoad(out var error))
        {
            throw new BuildFailedException(error);
        }
    }

    private static void LoadForEditor()
    {
        if (!TryLoad(out var error))
        {
            Debug.LogWarning(error);
        }
    }

    private static bool TryLoad(out string error)
    {
        var licenseKey = Environment.GetEnvironmentVariable(LicenseKeyVariable);
        var ufoLicenseKey = Environment.GetEnvironmentVariable(UfoLicenseKeyVariable);

        if (string.IsNullOrWhiteSpace(licenseKey) || string.IsNullOrWhiteSpace(ufoLicenseKey))
        {
            error = $"Set both {LicenseKeyVariable} and {UfoLicenseKeyVariable} in the environment.";
            return false;
        }

        var configuration = VuforiaConfiguration.Instance.Vuforia;
        configuration.LicenseKey = licenseKey;
        configuration.UfoLicenseKey = ufoLicenseKey;
        error = null;
        return true;
    }
}
