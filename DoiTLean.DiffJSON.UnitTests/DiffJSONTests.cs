using NUnit.Framework;
using OutSystems.ExternalLibraries.SDK;
using JSONPair = DoiTLean.DiffJSON.Structures.JSONPair;

namespace DoiTLean.DiffJSON.UnitTests;

public class DiffJSONTests {

    /// <summary>
    /// Tests if the JSONPair constructor correctly creates the JSONPair struct
    /// </summary>
    [Test]
    public void JSONPairStructureIsCorrectlyCreatedWhenGivenPair() {
        var pairStruct = new JSONPair("A1","1","2");
        Assert.That(pairStruct.Attribute, Is.EqualTo("A1"));
        Assert.That(pairStruct.PreviousValue, Is.EqualTo("1"));
        Assert.That(pairStruct.NewValue, Is.EqualTo("2"));
    }


}