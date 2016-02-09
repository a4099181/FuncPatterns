namespace FuncPatterns.Tests
{
    using System.IO;
    using System.Xml;
    using Machine.Specifications;
    using ShapeReader;
    using Functional.ChainOfResponsibility;
    using static Functional.ChainOfResponsibility.MonadicLink<System.Xml.XmlReader, string>;

    [Subject("Shape reader")]
    sealed class WhenShapeReaderReadsSquareElement
    {
        Establish _context = () => xmlReader = new XmlTextReader(new StringReader("<Square length='25' />"));

        Because _of = () =>
        {
            xmlReader.Read();
            var chain = Create<SquareReader>(xmlReader)
                .BindTo(Create<CircleReader>);
            length = chain().Process();
        };

        It _shouldReadLengthOf25 = () => length.ShouldEqual("25");

        static XmlReader xmlReader;
        static string length;
    }
}

