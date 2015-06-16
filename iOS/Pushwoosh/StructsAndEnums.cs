using System;
using System.Drawing;
using ObjCRuntime;
using Foundation;
using UIKit;
using CoreLocation;

namespace Pushwoosh
{
	public enum PWSupportedOrientations {
		OrientationPortrait = 1 << 0,
		OrientationPortraitUpsideDown = 1 << 1,
		OrientationLandscapeLeft = 1 << 2,
		OrientationLandscapeRight = 1 << 3
	}

}

