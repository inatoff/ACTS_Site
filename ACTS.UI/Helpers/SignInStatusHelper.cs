using Microsoft.AspNet.Identity.Owin;

namespace ACTS.UI.Helpers
{
	public static class SignInStatusHelper
	{
		public static bool Succeeded(this SignInStatus signInStatus)
		{
			return signInStatus == SignInStatus.Success;
		}

		public static bool IsLockedOut(this SignInStatus signInStatus)
		{
			return signInStatus == SignInStatus.LockedOut;
		}


		public static bool Failed(this SignInStatus signInStatus)
		{
			return signInStatus == SignInStatus.Failure;
		}
	}
}
