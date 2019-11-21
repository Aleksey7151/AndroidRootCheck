using System.IO;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace RootCheck
{
    public static class JailBrakeChecker
    {
        private static readonly string[] Paths =
        {
            @"/Applications/Cydia.app",
            @"/Library/MobileSubstrate/MobileSubstrate.dylib",
            @"/bin/bash",
            @"/usr/sbin/sshd",
            @"/etc/apt",
            @"/usr/bin/ssh"
        };

        public static bool IsJailBroken()
        {
            if (Runtime.Arch == Arch.SIMULATOR)
            {
                return false;
            }

            return CheckUsingFileManager() || CheckUsingFileAccess() || CheckUsingApplicationPermissions();
        }

        private static bool CheckUsingFileManager()
        {
            var fileManager = NSFileManager.DefaultManager;

            foreach (var path in Paths)
            {
                if (fileManager.FileExists(path))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool CheckUsingFileAccess()
        {
            foreach (var path in Paths)
            {
                if (File.Exists(path))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool CheckUsingApplicationPermissions()
        {
            return UIApplication.SharedApplication.CanOpenUrl(new NSUrl(@"cydia://package/com.example.package"));
        }
    }
}
