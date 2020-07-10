using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace CSharpFilters
{
	/// <summary>
	/// Summary description for KanjiInput.
	/// </summary>
	public class KanjiInput : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button OK;
		private System.Windows.Forms.Button Cancel;
		private System.Windows.Forms.TextBox Value;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public string sValue
		{
			get 
			{
				return Value.Text;
			}
			set{Value.Text = value;}
		}

		public KanjiInput()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			OK.DialogResult = System.Windows.Forms.DialogResult.OK;
			Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.OK = new System.Windows.Forms.Button();
			this.Cancel = new System.Windows.Forms.Button();
			this.Value = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// OK
			// 
			this.OK.Location = new System.Drawing.Point(22, 89);
			this.OK.Name = "OK";
			this.OK.Size = new System.Drawing.Size(90, 26);
			this.OK.TabIndex = 0;
			this.OK.Text = "OK";
			// 
			// Cancel
			// 
			this.Cancel.Location = new System.Drawing.Point(127, 89);
			this.Cancel.Name = "Cancel";
			this.Cancel.Size = new System.Drawing.Size(90, 26);
			this.Cancel.TabIndex = 1;
			this.Cancel.Text = "Cancel";
			// 
			// Value
			// 
			this.Value.Font = new System.Drawing.Font("MS Gothic", 18F);
			this.Value.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
			this.Value.Location = new System.Drawing.Point(96, 19);
			this.Value.Multiline = true;
			this.Value.Name = "Value";
			this.Value.Size = new System.Drawing.Size(120, 46);
			this.Value.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("MS Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(19, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(67, 26);
			this.label1.TabIndex = 3;
			this.label1.Text = "漢字";
			// 
			// KanjiInput
			// 
			this.AcceptButton = this.OK;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
			this.ClientSize = new System.Drawing.Size(200, 104);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.Value);
			this.Controls.Add(this.Cancel);
			this.Controls.Add(this.OK);
			this.Name = "KanjiInput";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Parameter";
			this.Load += new System.EventHandler(this.KanjiInput_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void KanjiInput_Load(object sender, System.EventArgs e)
		{
		
		}
	}
}
