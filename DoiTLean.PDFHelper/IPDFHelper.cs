using System.Collections.Generic;
using OutSystems.ExternalLibraries.SDK;

namespace DoiTLean.PDFHelper
{
    /// <summary>
    /// The DiffJSON interface defines the methods for parsing two json and returns a list of JSONPairs with the previous and new values for each difference found.
    /// </summary>
    [OSInterface(Description = "Parsing two json and returns a list of JSONPairs with the previous and new values for each difference found.", IconResourceName = "DoiTLean.PDFHelper.resources.pdfhelper.png", Name = "PDFHelper")]
    public interface IPDFHelper
    {
        /// <summary>
        /// Parses Left and Right JSON and returns a list of JSONPairs with the previous and new values for each difference found
        /// </summary>
        [OSAction(Description = "Parses Left and Right JSON to find differences.", IconResourceName = "DoiTLean.PDFHelper.resources.pdfhelper.png", ReturnName = "DiffList")]
        List<Structures.JSONPair> Diff(
            [OSParameter(DataType = OSDataType.Text, Description = "LeftJSON")]
            string LeftJSON,
            [OSParameter(DataType = OSDataType.Text, Description = "RightJSON")]
            string RightJSON);


        /// <summary>
        /// Replaces an object identified by Path with a list of name/value pairs.
        /// 
        /// This is helpful to pre-process JSON with &quot;dynamic&quot; property names to something that can be translated into the OutSystems Platform types
        /// </summary>
        /// <param name="JSONIn">JSON to process</param>
        /// <param name="Path">Path to the point where processing should happen (where your object is)</param>
        [OSAction(Description = "Replaces an object identified by Path with a list of name/value pairs", IconResourceName = "DoiTLean.PDFHelper.resources.pdfhelper.png", ReturnName = "JSONOut")]
        string JSON_Listify(
            [OSParameter(DataType = OSDataType.Text, Description = "JSONIn")]
            string JSONIn,
            [OSParameter(DataType = OSDataType.Text, Description = "Path")]
            string Path);

    }
}