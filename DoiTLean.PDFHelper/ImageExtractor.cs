using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using iText.Kernel.Pdf.Canvas.Parser.Data;
using iText.Kernel.Pdf;

namespace DoiTLean.PDFHelper
{
    /// <summary>
    /// Wrapper class for iTextsharp to easily dump all images from a PDF into separate files in a folder.
    /// </summary>
    /// <remarks>
    /// Adapted from article at:
    /// http://www.thevalvepage.com/swmonkey/2014/11/26/extract-images-from-pdf-files-using-itextsharp/
    /// </remarks>
    public class ImageExtractor
    {
        /// <summary>
        /// Current page in document.
        /// </summary>
        public int CurrentPage { get; private set; }

        /// <summary>
        /// Current count of images for entire document up to this point.
        /// </summary>
        public int ImageCount { get; private set; }

        private readonly string _OutputFilePrefix;
        private readonly string _OutputPath;
        private readonly bool _OverwriteExistingFiles;

        /// <summary>
        /// Helper class to dump all images from a PDF into separate files.
        /// </summary>
        /// <param name="outputFilePrefix">Prefix to use for files created. Uses source file name if null.</param>
        /// <param name="outputPath">Output files will be written to this path.</param>
        /// <param name="overwriteExistingFiles">Should existing files be overwritten?</param>
        private ImageExtractor(string outputFilePrefix, string outputPath, bool overwriteExistingFiles)
        {
            CurrentPage = 1;
            ImageCount = 0;

            _OutputFilePrefix = outputFilePrefix;
            _OutputPath = outputPath;
            _OverwriteExistingFiles = overwriteExistingFiles;
        }

        /// <summary>
        /// Extract all images from a PDF file. Images will be extracted in their native format.
        /// </summary>
        /// <param name="pdfPath">Full path and file name of PDF file</param>
        /// <param name="outputFilePrefix">
        /// Basic name of exported files. If null then uses same name as PDF file.
        /// </param>
        /// <param name="outputPath">
        /// Images will be saved to this path. If null or empty then uses same folder as PDF file.
        /// </param>
        /// <param name="overwriteExistingFiles">
        /// True to overwrite existing image files, false to skip past them
        /// </param>
        /// <returns>
        /// Count of total number of images extracted.
        /// </returns>
        public static int ExtractImagesFromFile(
            string pdfPath,
            string outputFilePrefix,
            string outputPath,
            bool overwriteExistingFiles)
        {
            // Handle setting of default values
            outputFilePrefix = outputFilePrefix ?? System.IO.Path.GetFileNameWithoutExtension(pdfPath);
            outputPath = String.IsNullOrEmpty(outputPath) ? System.IO.Path.GetDirectoryName(pdfPath) : outputPath;

            var extractor = new ImageExtractor(outputFilePrefix, outputPath, overwriteExistingFiles);


            return extractor.ImageCount;
        }

        #region Implementation of IRenderListener

        public void BeginTextBlock() { }
        public void EndTextBlock() { }
        public void RenderText(TextRenderInfo renderInfo) { }

        public void RenderImage(ImageRenderInfo renderInfo)
        {
            var imageObject = renderInfo.GetImage();
          

            ImageCount++;
        }

        #endregion

    }
}