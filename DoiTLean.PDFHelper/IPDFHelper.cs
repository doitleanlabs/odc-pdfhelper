using System.Collections.Generic;
using OutSystems.ExternalLibraries.SDK;
using DoiTLean.PDFHelper.Structures;

namespace DoiTLean.PDFHelper
{
    /// <summary>
    /// This extension contains very commonly used actions for PDF
    /// </summary>
    [OSInterface(Description = "This extension contains very commonly used actions for PDF.", IconResourceName = "DoiTLean.PDFHelper.resources.pdfhelper.png", Name = "PDFHelper")]
    public interface IPDFHelper
    {


        /// <summary>
        /// Read text from Existing PDF
        /// </summary>
        /// <param name="PDFBytes">PDF File Data Binary</param>
        /// <param name="PDFText">Extracted text from PDF</param>
        /// <param name="ErrorMessage">Error Message if something goes wrong</param>
        [OSAction(Description = "Read text from Existing PDF")] 
        void ReadTextFromPDF(byte[] PDFBytes, out string PDFText, out string ErrorMessage);

        /// <summary>
        /// Add Text WaterMark In Existing PDF using a temporary directory path
        /// </summary>
        /// <param name="PDF">Binary Data of the pdf file.</param>
        /// <param name="Watermark_Text">Text Needs to be printed as water mark.</param>
        /// <param name="Text_Size">Size of Text</param>
        /// <param name="Temp_Path">Path at which the new watermarked PDF will be generated.</param>
        /// <param name="OutPDF">PDF Bytes with water mark text.</param>
        /// <param name="ErrorMessage">Error message if something goes wrong</param>
        [OSAction(Description = "Add Text WaterMark In Existing PDF using a temporary directory path")] 
        void AddTextWatermarkInPDF_TempPath(byte[] PDF, string Watermark_Text, int Text_Size, string Temp_Path, out byte[] OutPDF, out string ErrorMessage);

        /// <summary>
        /// Print Page Number To existing PDF
        /// </summary>
        /// <param name="PdfByte">PDF Filedata</param>
        /// <param name="XAxis"></param>
        /// <param name="YAxis"></param>
        /// <param name="PaginationText">A custom text can be passed to format the output text of pagination.
        /// 
        /// Default: Page {page_number} of {total_pages}
        /// 
        /// Available tags:
        /// - {page_number}
        /// - {total_pages}</param>
        /// <param name="ErrorMsg"></param>
        /// <param name="PdfOutByte">PDF Byte with Page Number</param>
        [OSAction(Description = "Print Page Number To existing PDF")] 
        void PrintPageNumber(byte[] PdfByte, int XAxis, int YAxis, string PaginationText, out string ErrorMsg, out byte[] PdfOutByte);

        /// <summary>
        /// Join Multiple PDF  files in single file
        /// </summary>
        /// <param name="PDFFileList">PDF Binary list</param>
        /// <param name="JoinedFile">Output merged file </param>
        /// <param name="ErrorMessage">Error Message if something goes wrong</param>
        [OSAction(Description = "Join Multiple PDF  files in single file")] 
        void JoinPDF(List<PDFFile> PDFFileList, out byte[] JoinedFile, out string ErrorMessage);

        /// <summary>
        /// Add Text WaterMark In Existing PDF using server memory
        /// </summary>
        /// <param name="PDF">Binary data of the PDF we want to write the watermark</param>
        /// <param name="Watermark_Text">Text Needs to be printed as watermark.</param>
        /// <param name="Text_Size">Size of Text of the watermark</param>
        /// <param name="OutPDF">PDF Binary Data with water mark text printed.</param>
        /// <param name="ErrorMessage">Error Message if something goes wrong</param>
        [OSAction(Description = "Add Text WaterMark In Existing PDF using server memory")] 
        void AddTextWatermarkInPDF_Memory(byte[] PDF, string Watermark_Text, int Text_Size, out byte[] OutPDF, out string ErrorMessage);

        /// <summary>
        /// Get a list of comments for a PDF Binary
        /// </summary>
        /// <param name="PDFBytes">PDF File Data Binary</param>
        /// <param name="ErrorMessage">Return a list of comments</param>
        /// <param name="Comments">Error Message if something goes wrong</param>
        [OSAction(Description = "Get a list of comments for a PDF Binary")] 
        void GetPDFAnnotation(byte[] PDFBytes, out string ErrorMessage, out List<PDFComment> Comments);

        /// <summary>
        /// Compress a PDF file using Ghostscript (https://www.ghostscript.com/)
        /// </summary>
        /// <param name="PDF"></param>
        /// <param name="Arguments">Default arguments:
        /// 
        /// &quot;-sDEVICE=pdfwrite&quot;
        /// &quot;-dNOPAUSE&quot;
        /// &quot;-dSAFER&quot;
        /// &quot;-dBATCH&quot;
        /// &quot;-dCompatibilityLevel=1.5&quot;
        /// &quot;-dPDFSETTINGS=/screen&quot;
        /// &quot;-dDownsampleColorImages=true&quot;
        /// &quot;-dDownsampleGrayImages=true&quot;
        /// &quot;-dDownsampleMonoImages=true&quot;
        /// &quot;-dDOINTERPOLATE&quot;
        /// &quot;-dColorImageDownsampleThreshold=1.0&quot;
        /// &quot;-dGrayImageDownsampleThreshold=1.0&quot;
        /// &quot;-dMonoImageDownsampleThreshold=1.0&quot;
        /// &quot;-dColorImageResolution=95&quot;
        /// &quot;-dGrayImageResolution=95&quot;
        /// &quot;-dMonoImageResolution=95&quot;
        /// 
        /// Read more:
        /// https://www.ghostscript.com/doc/current/Use.htm
        /// https://askubuntu.com/a/256449</param>
        /// <param name="CompressedPDF"></param>
        /// <param name="isSuccess"></param>
        /// <param name="Message"></param>
        [OSAction(Description = "Compress a PDF file using Ghostscript")]
        void CompressPDF(byte[] PDF, List<Argument> Arguments, out byte[] CompressedPDF, out bool isSuccess, out string Message);

        /// <summary>
        /// Removes existing JavaScript code (e.g.: macros) from a PDF file
        /// </summary>
        /// <param name="PDF">Binary data of the PDF</param>
        /// <param name="OutPDF">PDF Binary Data with JS removed.</param>
        /// <param name="ErrorMessage">Error Message if something goes wrong</param>
        [OSAction(Description = "Removes existing JavaScript code (e.g.: macros) from a PDF file")]
        void RemoveJavascript(byte[] PDF, out byte[] OutPDF, out string ErrorMessage);

        /// <summary>
        /// Extracts images from a PDF file
        /// </summary>
        /// <param name="PDF"></param>
        /// <param name="Images"></param>
        /// <param name="ErrorMessage"></param>
        [OSAction(Description = "Extracts images from a PDF file")]
        void ExtractImagesFromPDF(byte[] PDF, out List<Image> Images, out string ErrorMessage);


    }
}