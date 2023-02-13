using System;
using System.Drawing;
using System.Windows.Forms;

namespace AzManLoginWinForms
{
	public partial class BorderlessForm : Form
	{
		#region Private fields
		private Point _lastPoint;
		private bool _isMouseDown;
		#endregion

		#region Constructor
		protected BorderlessForm() {
			InitializeComponent();
		}
		#endregion

		#region Protected methods
		protected void control_DoubleClick(object sender, EventArgs e) {
			switch (this.WindowState) {
				case FormWindowState.Maximized:
					this.WindowState = FormWindowState.Normal;
					break;
				case FormWindowState.Normal:
					this.WindowState = FormWindowState.Maximized;
					break;
			}
		}

		protected void control_MouseDown(object sender, MouseEventArgs e) {
			if (this.WindowState == FormWindowState.Maximized)
				return;

			this._isMouseDown = true;
			this._lastPoint = new Point(e.X, e.Y);
		}

		protected void control_MouseMove(object sender, MouseEventArgs e) {
			if (this._isMouseDown)
				this.Location = new Point(this.Left - (this._lastPoint.X - e.X), this.Top - (this._lastPoint.Y - e.Y));
		}

		protected void control_MouseUp(object sender, MouseEventArgs e) {
			this._isMouseDown = false;
		}
		#endregion
	}
}
