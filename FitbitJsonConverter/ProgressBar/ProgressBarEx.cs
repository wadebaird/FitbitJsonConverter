using Sunbreak.WinForms.ProgressBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sunbreak.WinForms.ProgressBar
{
	/// <summary>
	/// Version of progress bar with additional features, such as displaying text over progress
	/// </summary>
	/// <remarks>
	/// Created by Arlen Feldman. see article at https://cowthulu.com/winforms-progress-bar-with-text
	/// Freely distributable, modifiable, collagable, inflatable. Use any way you like. Attribution/blame not required.
	/// </remarks>
	public class ProgressBarEx : System.Windows.Forms.ProgressBar
	{
		// ------------------------------------------------------------------------------------------------------
		// Constants and fields

		public const int WM_PAINT = 0xF;
		public const int WS_EX_COMPOSITED = 0x2000_000;

		private TextDisplayType _style = TextDisplayType.Percent;
		private string _manualText = "";

		// ------------------------------------------------------------------------------------------------------
		// Construction

		/// <summary>Constructor</summary>
		public ProgressBarEx()
		{
		}

		// ------------------------------------------------------------------------------------------------------
		// Properties

		/// <summary>What text to display</summary>
		[Category("Appearance")]
		[Description("What type of text to display on the progress bar.")]
		public TextDisplayType DisplayType
		{
			get { return _style; }
			set
			{
				_style = value;
				this.Invalidate();
			}
		}

		/// <summary>If the TextStyle is set to Manual, the text to display</summary>
		[Category("Appearance")]
		[Description("If DisplayType is Manual, the text to display.")]
		public string ManualText
		{
			get { return _manualText; }
			set
			{
				_manualText = value;
                this.Invalidate();
			}
		}

		/// <summary>Color for the text on the bar</summary>
		/// <remarks>Can't use the ForeColor because it <i>technically</i> is the bar color, although this is ignored when VisualStyles are enabled</remarks>
		[Category("Appearance")]
		[Description("Color of text on bar.")]
		public Color TextColor { get; set; } = SystemColors.ControlText;

		/// <summary>The font for the text</summary>
		/// <remarks>Have to override this just to restore the Browsable flags so that it will show up in the designer. ProgressBar hides it.</remarks>
		[Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public override Font Font { get => base.Font; set => base.Font = value; }

		// ------------------------------------------------------------------------------------------------------
		// Implementation

		/// <summary>Windows-control creation parameters</summary>
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams parms = base.CreateParams;

				// Force control to double-buffer painting
				parms.ExStyle |= WS_EX_COMPOSITED;

				return parms;
			}
		}

		/// <summary>Handle Windows messages</summary>
		/// <param name="m"></param>
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);

			if (m.Msg == WM_PAINT)
			{
                this.AdditionalPaint();
			}
		}

		/// <summary>Does the actual painting of the text</summary>
		/// <param name="m"></param>
		private void AdditionalPaint()
		{
			if (DisplayType == TextDisplayType.None)
				return;

			string text = this.GetDisplayText();

			using Graphics g = Graphics.FromHwnd(Handle);
			Rectangle rect = new(0, 0, Width, Height);
			StringFormat format = new(StringFormatFlags.NoWrap)
			{
				Alignment = StringAlignment.Center,
				LineAlignment = StringAlignment.Center
			};

			using Brush textBrush = new SolidBrush(TextColor);
			g.DrawString(text, Font, textBrush, rect, format);
		}

		private string GetDisplayText()
		{
			string result = "";

			switch (DisplayType)
			{
				case TextDisplayType.Percent:
					if (Maximum != 0)
						result = ((int)(((float)Value / (float)Maximum) * 100)).ToString() + " %";
					break;

				case TextDisplayType.Count:
					result = Value.ToString() + " / " + Maximum.ToString();
					break;

				case TextDisplayType.Manual:
					result = ManualText;
					break;
			}

			return result;
		}

		// ------------------------------------------------------------------------------------------------------
		// Nested classes and enums

		/// <summary>Supported display options</summary>
		public enum TextDisplayType
		{
			/// <summary>No display text</summary>
			None,
			/// <summary>Display percentage of progress</summary>
			Percent,
			/// <summary>Show x / y of progress</summary>
			Count,
			/// <summary>Display a manually-provided string</summary>
			Manual
		}
	}
}

namespace System.Windows.Forms
{ 
	public static class FormExtension
	{
		public static ProgressBarEx CreateProgressBar(this Form hostingForm, string text)
		{
			var width = 280;

			ProgressBarEx progressBar = new()
			{
				Style = ProgressBarStyle.Marquee,
				Visible = true,
				Enabled = true,
				Location = new Drawing.Point(Math.Max(0, (hostingForm.Width - width) / 2), Math.Max(0, (hostingForm.Height - 20) / 2)),
				Size = new Drawing.Size(width, 20),
				Minimum = 1,
				Maximum = 100,
				Step = 1,
				ManualText = hostingForm.Text = text,
				DisplayType = ProgressBarEx.TextDisplayType.Manual,
			};

			hostingForm.Controls.Add(progressBar);
			progressBar.BringToFront();

			hostingForm.Enabled = false;

			return progressBar;
		}

		public static void DestroyProgressBar(this Form hostingForm, ProgressBarEx progressBar)
		{
			hostingForm.Controls.Remove(progressBar);
			hostingForm.Enabled = true;
		}
	}
}