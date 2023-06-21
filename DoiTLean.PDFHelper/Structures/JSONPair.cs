using OutSystems.ExternalLibraries.SDK;

namespace DoiTLean.PDFHelper.Structures
{
    /// <summary>
    /// The JSON Pair struct represents a record of attribute, previous and new value for each attribute compared by the DiffJSON class
    /// </summary>
    [OSStructure(Description = "Represents an JSONPair (Attribute,Previous,New).")]
    public struct JSONPair
    {
        [OSStructureField(DataType = OSDataType.Text, Description = "The attribute in the left json", IsMandatory = true)]
        /// <summary>
        /// Represents the attribute in the original json
        /// </summary>
        public string Attribute;


        [OSStructureField(DataType = OSDataType.Text, Description = "The previous value of the attribute in the left json", IsMandatory = true)]
        /// <summary>
        /// Represents the value of attribute in the left json
        /// </summary>
        public string PreviousValue;


        [OSStructureField(DataType = OSDataType.Text, Description = "The new value of the attribute in the right json", IsMandatory = true)]
        /// <summary>
        /// Represents the value of the attribute in the right json
        /// </summary>
        public string NewValue;

        /// <summary>
        /// Constructs an JSONPair struct.
        /// </summary>
        public JSONPair(string inputAttribute,string inputLeftValue,string inputRightValue) : this()
        {
            Attribute = inputAttribute ?? string.Empty;
            PreviousValue = inputLeftValue ?? string.Empty;
            NewValue = inputRightValue ?? string.Empty;
        }
    }

}