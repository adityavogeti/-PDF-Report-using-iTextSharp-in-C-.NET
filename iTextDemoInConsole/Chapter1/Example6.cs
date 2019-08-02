using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace iTextSharp.tutorial.Chapter1
{
	/// <summary>
	/// Example 6 shows you the Following:
	/// 1. Adding watermark to each page during creation
	/// </summary>
	public class Example6
	{
		public Example6()
		{
			string appRootDir = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName;

			using (FileStream fs = new FileStream(appRootDir + "/PDFs/" + "Chapter1_Example6.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
			using (Document doc = new Document(PageSize.LETTER))
			using (PdfWriter writer = PdfWriter.GetInstance(doc, fs))
			{
				writer.PageEvent = new PDFWriterEvents("This is a Test");
				doc.Open();
				doc.Add(new Paragraph("This is a page 1"));
				doc.Close();
			}
		}
	}

	// Implementing IPdfPageEvent interface which contains all the PdfWriteEvents
	// i.e. Events that are occured from the Openning & to Closing the PDF Document. The Events are following:
	 //1. public void OnOpenDocument(PdfWriter writer, Document document)
	 //2. public void OnCloseDocument(PdfWriter writer, Document document)
	 //3. public void OnStartPage(PdfWriter writer, Document document)
	 //4. public void OnEndPage(PdfWriter writer, Document document)
	 //5. public void OnParagraph(PdfWriter writer, Document document, float paragraphPosition)
	 //6. public void OnParagraphEnd(PdfWriter writer, Document document, float paragraphPosition)
	 //7. public void OnChapter(PdfWriter writer, Document document, float paragraphPosition, Paragraph title)
	 //8. public void OnChapterEnd(PdfWriter writer, Document document, float paragraphPosition)
	 //9. public void OnSection(PdfWriter writer, Document document, float paragraphPosition, int depth, Paragraph title)
	 //10. public void OnSectionEnd(PdfWriter writer, Document document, float paragraphPosition)
	 //11. public void OnGenericTag(PdfWriter writer, Document document, Rectangle rect, String text)

	// Creating Watermark inside OnStartPage Event by implementing IPdfPageEvent interface
	// So that, dusring Page Creation, Watermark will be create
	class PDFWriterEvents : IPdfPageEvent
	{
		string watermarkText;
		float fontSize = 80f;
		float xPosition = 300f;
		float yPosition = 800f;
		float angle = 45f;

		public PDFWriterEvents(string watermarkText, float fontSize = 80f, float xPosition = 300f, float yPosition = 400f, float angle = 45f)
		{
			this.watermarkText = watermarkText;
			this.xPosition = xPosition;
			this.yPosition = yPosition;
			this.angle = angle;
		}

		public void OnOpenDocument(PdfWriter writer, Document document) { }
		public void OnCloseDocument(PdfWriter writer, Document document) { }
		public void OnStartPage(PdfWriter writer, Document document)
		{
			try
			{
				PdfContentByte cb = writer.DirectContentUnder;
				BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.EMBEDDED);
				cb.BeginText();
				cb.SetColorFill(BaseColor.LIGHT_GRAY);
				cb.SetFontAndSize(baseFont, fontSize);
				cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, watermarkText, xPosition, yPosition, angle);
				cb.EndText();
			}
			catch (DocumentException docEx)
			{
				throw docEx;
			}
		}
		public void OnEndPage(PdfWriter writer, Document document) { }
		public void OnParagraph(PdfWriter writer, Document document, float paragraphPosition) { }
		public void OnParagraphEnd(PdfWriter writer, Document document, float paragraphPosition) { }
		public void OnChapter(PdfWriter writer, Document document, float paragraphPosition, Paragraph title) { }
		public void OnChapterEnd(PdfWriter writer, Document document, float paragraphPosition) { }
		public void OnSection(PdfWriter writer, Document document, float paragraphPosition, int depth, Paragraph title) { }
		public void OnSectionEnd(PdfWriter writer, Document document, float paragraphPosition) { }
		public void OnGenericTag(PdfWriter writer, Document document, Rectangle rect, String text) { }

	}
}