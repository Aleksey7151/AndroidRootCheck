# RootCheck Plugin

This component provides functionality for checking if Android device was Rooted or iOS device was jail broken. 

## Current Status

This package is released and considered as stable.

## Authors

* Aliaksei Safonau, https://github.com/Aleksey7151

## Requirements

* Android 4.3+
* iOS 9.3+

## Quick Start

```C#

// ANDROID PLATFORM
public class MainActivity : Activity
{
	protected override void OnCreate(Bundle bundle)
	{
		base.OnCreate(bundle);

		if (RootChecker.IsRooted())
		{
			// Android device was Rooted
		}
	}
}

// iOS PLATFORM
public partial class AppDelegate
{
	public override bool FinishedLaunching(UIApplication app, NSDictionary options)
	{
		if (JailBrakeChecker.IsJailBroken())
		{
			// iOS device was jail broken
		}

		return base.FinishedLaunching(app, options);
	}
}

// =================== USAGE IN CORE PROJECT

```

## Solution Structure
* Android specific code: Android class library.
* iOS specific code: iOS class library.

## Installation
You need to add the **Xamarin.Plugin.RootCheck** package to your iOS / Android projects projects

## Package dependencies
No.
