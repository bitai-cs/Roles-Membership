using System;
using System.Windows.Forms;

namespace NetSqlAzMan.SnapIn.Forms
{
	/// <summary>
	/// Base class for all Windows Forms
	/// </summary>
	public partial class frmBase : Form
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:frmBase"/> class.
		/// </summary>
		public frmBase()
			: this(true) {

		}

		public frmBase(bool localized) {
			if (localized) {
				this.Load += new EventHandler(frmBase_Load);
			}
		}

		/// <summary>
		/// Handles the Load event of the frmBase control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
		private void frmBase_Load(object sender, EventArgs e) {
			NetSqlAzMan.SnapIn.Globalization.ResourcesManager.ManageResource(this);
		}
	}
}
