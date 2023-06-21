using OutSystems.ExternalLibraries.SDK;

namespace DoiTLean.PDFHelper.Structures
{
    /// <summary>
    /// Argument
    /// </summary>
    [OSStructure(Description = "Argument")]
    public struct Argument
    {
        [OSStructureField(DataType = OSDataType.Text, Description = "Argument", IsMandatory = true)]
        public string argument;

        public Argument(string inputArgument) : this()
        {
            argument = inputArgument ?? string.Empty;
        }
    }

}