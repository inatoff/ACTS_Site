namespace ACTS.UI.Helpers
{
	public static class MailHelper
	{
		public static bool IsValidEmail(string email)
		{
			try
			{
				var addr = new System.Net.Mail.MailAddress(email);
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
