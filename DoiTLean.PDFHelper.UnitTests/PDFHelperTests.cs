using NUnit.Framework;
using OutSystems.ExternalLibraries.SDK;
using Argument = DoiTLean.PDFHelper.Structures.Argument;

namespace DoiTLean.PDFHelper.UnitTests;

public class PDFHelperTests {

    /// <summary>
    /// Tests if the JSONPair constructor correctly creates the JSONPair struct
    /// </summary>
    [Test]
    public void ArgumentStructureIsCorrectlyCreatedWhenGivenPair() {
        var pairStruct = new Argument("A1");
        Assert.That(pairStruct.argument, Is.EqualTo("A1"));
    }


}