namespace FuncPatterns.Tests.ShapeReader
{
    using System.Xml;
    using Functional.ChainOfResponsibility;

    sealed class SquareReader : MonadicLink<XmlTextReader, string>
    {
        public override string Process()
            => Input.Name == "Square" ? Input.GetAttribute("length") : null;
    }
}
