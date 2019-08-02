using System;
using System.IO;
// Importing necessary Library to work with iTextSharp 5.4.4
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace iTextSharp.tutorial.Chapter1
{
	/// <summary>
	/// Exmaple 7: Viewer Preferences
	/// </summary>
	public class Example7
	{
		public Example7()
		{
			string appRootDir = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName;
			try
			{
				// Creating System.IO.FileStream object
				using (FileStream fs = new FileStream(appRootDir + "/PDFs/" + "Chapter1_Example7.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
				// Creating iTextSharp.text.Document object
				using (Document doc = new Document())
				// Creating iTextSharp.text.pdf.PdfWriter object
				// It helps to write the Document to the Specified FileStream
				using (PdfWriter writer = PdfWriter.GetInstance(doc, fs))
				{
					// Setting Viewer Preferences by calling method
					writer.AddViewerPreference(PdfName.HIDEMENUBAR, new PdfBoolean(true));

					// Setting View Preferences by setting properties
					writer.ViewerPreferences = PdfWriter.HideMenubar;

					// Openning the Document
					doc.Open();

					// Adding a paragraph
					// NOTE: When we want to insert text, then we've to do it through creating paragraph
					doc.NewPage();
					doc.Add(new Paragraph("First Page"));
					doc.NewPage();
					doc.Add(new Paragraph("Second Page"));

					// Closing the Document
					doc.Close();
				}
			}
			// Catching iTextSharp.text.DocumentException if any
			catch (DocumentException de)
			{
				throw de;
			}
			// Catching System.IO.IOException if any
			catch (IOException ioe)
			{
				throw ioe;
			}
		}
	}
}
