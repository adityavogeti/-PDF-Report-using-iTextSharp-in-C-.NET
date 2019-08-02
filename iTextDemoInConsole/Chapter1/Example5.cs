using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace iTextSharp.tutorial.Chapter1
{
	/// <summary>
	/// Example 5 shows you the Following:
	/// 1. Creating a 5 paged PDF Document
	/// 2. Creating another PDF Document from Existing Document(Produced by 1 no) and Adding Watermark to it
	/// 3. Creating another PDF Document from Existing Document(Produced by 2 no) and Removing Watermark from it
	/// </summary>
	public class Example5
	{
		public Example5()
		{
			string appRootDir = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName;

			string startFile = appRootDir + "/PDFs/" + "Chapter1_Example5.pdf";
			string watermarkedFile = appRootDir + "/PDFs/" + "Chapter1_Example5_Watermarked.pdf";
			string unwatermarkedFile = appRootDir + "/PDFs/" + "Chapter1_Example5_Un-Watermarked.pdf";

			string watermarkText = "This is a Test";

			// Creating a Five paged PDF
			using (FileStream fs = new FileStream(startFile, FileMode.Create, FileAccess.Write, FileShare.None))
			using (Document doc = new Document(PageSize.LETTER))
			using (PdfWriter writer = PdfWriter.GetInstance(doc, fs))
			{
				doc.Open();
				for (int i = 1; i <= 5; i++)
				{
					doc.NewPage();
					doc.Add(new Paragraph(string.Format("This is a page {0}", i)));
				}
				doc.Close();
			}

			// Creating watermark on a separate layer
			// Creating iTextSharp.text.pdf.PdfReader object to read the Existing PDF Document produced by 1 no.
			PdfReader reader1 = new PdfReader(startFile);
			using (FileStream fs = new FileStream(watermarkedFile, FileMode.Create, FileAccess.Write, FileShare.None))
			// Creating iTextSharp.text.pdf.PdfStamper object to write Data from iTextSharp.text.pdf.PdfReader object to FileStream object
			using (PdfStamper stamper = new PdfStamper(reader1, fs))
			{
				// Getting total number of pages of the Existing Document
				int pageCount = reader1.NumberOfPages;

				// Create New Layer for Watermark
				PdfLayer layer = new PdfLayer("WatermarkLayer", stamper.Writer);
				// Loop through each Page
				for (int i = 1; i <= pageCount; i++)
				{
					// Getting the Page Size
					Rectangle rect = reader1.GetPageSize(i);

					// Get the ContentByte object
					PdfContentByte cb = stamper.GetUnderContent(i);

					// Tell the cb that the next commands should be "bound" to this new layer
					cb.BeginLayer(layer);
					cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 50);

					PdfGState gState = new PdfGState();
					gState.FillOpacity = 0.25f;
					cb.SetGState(gState);

					cb.SetColorFill(BaseColor.BLACK);
					
					cb.BeginText();
					
					cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, watermarkText, rect.Width / 2, rect.Height / 2, 45f);
					
					cb.EndText();

					// Close the layer
					cb.EndLayer();
				}
			}

			// Removing the layer created above
			// 1. First we bind a reader to the watermarked file
			// 2. Then strip out a branch of things
			// 3. Finally use a simple stamper to write out the edited reader
			PdfReader reader2 = new PdfReader(watermarkedFile);

			// NOTE: This will destroy all layers in the Document, only use if you don't have any addtional layers
			// Remove the OCG group completely from the Document: reader2.Catalog.Remove(PdfName.OCPROPERTIES);

			// Clean up the reader, optional
			reader2.RemoveUnusedObjects();

			// Placeholder variables
			PRStream stream;
			string content;
			PdfDictionary page;
			PdfArray contentArray;

			// Get the number of pages
			int pageCount2 = reader2.NumberOfPages;

			// Loop through each page
			for (int i = 1; i <= pageCount2; i++)
			{
				// Get the page
				page = reader2.GetPageN(i);

				// Get the raw content
				contentArray = page.GetAsArray(PdfName.CONTENTS);

				if (contentArray != null)
				{
					// Loop through content
					for (int j = 0; j < contentArray.Size; j++)
					{
						stream = (PRStream)contentArray.GetAsStream(j);

						// Convert to a String, NOTE: you might need a different encoding here
						content = System.Text.Encoding.ASCII.GetString(PdfReader.GetStreamBytes(stream));

						//Look for the OCG token in the stream as well as our watermarked text
						if (content.IndexOf("/OC") >= 0 && content.IndexOf(watermarkText) >= 0)
						{
							//Remove it by giving it zero length and zero data
							stream.Put(PdfName.LENGTH, new PdfNumber(0));
							stream.SetData(new byte[0]);
						}
					}
				}
			}

			// Write the content out
			using (FileStream fs = new FileStream(unwatermarkedFile, FileMode.Create, FileAccess.Write, FileShare.None))
			using (PdfStamper stamper = new PdfStamper(reader2, fs)) { }
		}
	}
}
