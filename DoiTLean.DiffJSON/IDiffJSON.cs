using System.Collections.Generic;
using OutSystems.ExternalLibraries.SDK;

namespace DoiTLean.DiffJSON
{
    /// <summary>
    /// The DiffJSON interface defines the methods for parsing two json and returns a list of JSONPairs with the previous and new values for each difference found.
    /// </summary>
    [OSInterface(Description = "Parsing two json and returns a list of JSONPairs with the previous and new values for each difference found.", IconResourceName = "DoiTLean.DiffJSON.resources.diffjson.png", Name = "DiffJSON")]
    public interface IDiffJSON
    {
        /// <summary>
        /// Parses Left and Right JSON and returns a list of JSONPairs with the previous and new values for each difference found
        /// </summary>
        [OSAction(Description = "Parses Left and Right JSON to find differences.", IconResourceName = "DoiTLean.DiffJSON.resources.diff.png", ReturnName = "DiffList")]
        List<Structures.JSONPair> Diff(
            [OSParameter(DataType = OSDataType.Text, Description = "LeftJSON")]
            string LeftJSON,
            [OSParameter(DataType = OSDataType.Text, Description = "RightJSON")]
            string RightJSON);

    }
}