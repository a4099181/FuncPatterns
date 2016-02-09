namespace FuncPatterns.Tests.ShapeReader
{
    using System.Xml;
    using Functional.ChainOfResponsibility;

    sealed class CircleReader : MonadicLink<XmlTextReader, string>
    {
        public override string Process()
            => Input.Name == "Circle" ? Input.GetAttribute("radius") : null;
    }
}
