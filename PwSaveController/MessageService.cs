using System.Net.Mime;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace PwSaveController
{
	/// <summary>
	/// Service to show winforms based message box with text.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public class MessageService : IMessageService // Singleton<IMessageService, MessageService> //--wird erst bei Tests interessant
    {
        private const string lab = "Art & Image";
		/// <summary>
		/// Shows the message.
		/// </summary>
		public void ShowMessage(string message, string caption = null)
		{
			caption = caption ?? lab; //MediaTypeNames.Application.ProductName;
			MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.None);
		}

		/// <summary>
		/// Shows the message as warning.
		/// </summary>
		public void ShowWarning(string message, string caption = null)
        {
            caption = caption ?? lab; //MediaTypeNames.Application.ProductName;
			MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		/// <summary>
		/// Shows the message as error.
		/// </summary>
		public void ShowError(string message, string caption = null)
		{
			caption = caption ?? lab; //MediaTypeNames.Application.ProductName;
			MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		/// <summary>
		/// Shows the specified question.
		/// </summary>
		/// <returns><c>true</c> for yes, <c>false</c> for no and <c>null</c> for cancel.</returns>
		public bool? ShowQuestion(string message, string caption = null)
		{
			caption = caption ?? lab; //MediaTypeNames.Application.ProductName;
			DialogResult result = MessageBox.Show(message,
												  caption,
												  MessageBoxButtons.YesNoCancel,
												  MessageBoxIcon.Question);

			if (result == DialogResult.Yes)
			{
				return true;
			}
			if (result == DialogResult.No)
			{
				return false;
			}
			return null;
		}

		/// <summary>
		/// Shows the specified yes/no question.
		/// </summary>
		/// <returns><c>true</c> for yes and <c>false</c> for no.</returns>
		public bool ShowYesNoQuestion(string message, string caption = null)
		{
			caption = caption ?? lab; //MediaTypeNames.Application.ProductName;
			DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			return result == DialogResult.Yes;
		}
	}
}