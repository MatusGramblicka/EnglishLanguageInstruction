using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Provider;

namespace EnglishLanguageInstruction;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                           ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    private bool _requestedStorageManagerAccess;

    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        RequestStorageManagerAccessIfNeeded();
    }

    protected override void OnResume()
    {
        base.OnResume();

        RequestStorageManagerAccessIfNeeded();
    }

    private void RequestStorageManagerAccessIfNeeded()
    {
        if (_requestedStorageManagerAccess || Android.OS.Environment.IsExternalStorageManager)
        {
            return;
        }

        _requestedStorageManagerAccess = true;

        try
        {
            var intent = new Intent(Settings.ActionManageAppAllFilesAccessPermission);
            var uri = Android.Net.Uri.FromParts("package", PackageName, null);
            intent.SetData(uri);
            StartActivity(intent);
        }
        catch (ActivityNotFoundException)
        {
            StartActivity(new Intent(Settings.ActionManageAllFilesAccessPermission));
        }
    }
}