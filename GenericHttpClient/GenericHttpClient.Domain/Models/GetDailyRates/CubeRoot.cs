using System.Xml.Serialization;

namespace GenericHttpClient.Domain.Models.GetDailyRates;

public class CubeRoot
{
    [XmlElement(ElementName = "Cube")]
    public List<CubeTime> TimeCubes { get; set; }
}