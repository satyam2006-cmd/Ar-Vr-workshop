# AR Book

This Unity project reads its Vuforia credentials from environment variables instead of storing them in the project asset.

On Windows, set both user environment variables before opening Unity:

```powershell
setx VUFORIA_LICENSE_KEY "your-vuforia-license-key"
setx VUFORIA_UFO_LICENSE_KEY "your-ufo-license-key"
```

Restart Unity after setting or changing the variables. The Unity Editor loads them for Play mode, and builds fail with a clear message if either variable is missing.

Vuforia embeds its license data in the built application. Environment variables keep the values out of the project source, but do not hide them from someone who can inspect the built app.
