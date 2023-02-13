using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Basgosoft.Reflection;

namespace AzManWinUI
{
	public partial class SplashUI : Form
	{
		public SplashUI()
		{
			InitializeComponent();

			lablTitle.Text = ClaimantAssembly.AssemblyTitle;
			lablProduct.Text = ClaimantAssembly.AssemblyProduct;
			lablVersion.Text = String.Format("Versión: {0}", ClaimantAssembly.AssemblyFileVersion);
			lablCopyright.Text = ClaimantAssembly.AssemblyCopyright;
		}
	}
}
