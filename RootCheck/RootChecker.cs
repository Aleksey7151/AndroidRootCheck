using Android.OS;
using Java.IO;
using Java.Lang;
using Process = Java.Lang.Process;

namespace RootCheck
{
    /// <summary>
    /// 
    /// </summary>
    public static class RootChecker
    {
        private const string TestKeys = "test-keys";

        private static readonly string[] SuperUserPaths =
        {
            "/system/app/Superuser.apk",
            "/sbin/su",
            "/system/bin/su",
            "/system/xbin/su",
            "/data/local/xbin/su",
            "/data/local/bin/su",
            "/system/sd/xbin/su",
            "/system/bin/failsafe/su",
            "/data/local/su",
            "/su/bin/su"
        };

        /// <summary>
        /// Checks if device was rooted
        /// </summary>
        /// <returns>TRUE - if device was rooted, else - FALSE.</returns>
        public static bool IsRooted()
        {
            return CheckUsingTestKeys() || CheckUsingSuperUserAvailability() || CheckUsingProcessPermissions();
        }

        private static bool CheckUsingTestKeys()
        {
            var buildTags = Build.Tags;

            return buildTags != null && buildTags.Contains(TestKeys);
        }

        private static bool CheckUsingSuperUserAvailability()
        {
            foreach (var superUserPath in SuperUserPaths)
            {
                if (new File(superUserPath).Exists())
                {
                    return true;
                }
            }

            return false;
        }

        private static bool CheckUsingProcessPermissions()
        {
            Process process = null;

            try
            {
                process = Runtime.GetRuntime().Exec(new[] { "/system/xbin/which", "su" });

                using (var input = new BufferedReader(new InputStreamReader(process.InputStream)))
                {
                    return input.ReadLine() != null;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                process?.Destroy();
            }
        }
    }
}
