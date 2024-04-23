namespace PwSaveController
{
	/// <summary>
	/// Service that provides message handling.
	/// </summary>
	public interface IMessageService
	{
		/// <summary>
		/// Shows the message.
		/// </summary>
		void ShowMessage(string message, string caption = null);

		/// <summary>
		/// Shows the message as warning.
		/// </summary>
		void ShowWarning(string message, string caption = null);

		/// <summary>
		/// Shows the message as error.
		/// </summary>
		void ShowError(string message, string caption = null);

		/// <summary>
		/// Shows the specified question.
		/// </summary>
		/// <returns><c>true</c> for yes, <c>false</c> for no and <c>null</c> for cancel.</returns>
		bool? ShowQuestion(string message, string caption = null);

		/// <summary>
		/// Shows the specified yes/no question.
		/// </summary>
		/// <returns><c>true</c> for yes and <c>false</c> for no.</returns>
		bool ShowYesNoQuestion(string message, string caption = null);
	}
}