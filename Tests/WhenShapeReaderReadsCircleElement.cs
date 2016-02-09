namespace FuncPatterns.Tests
{
    using System.IO;
    using System.Xml;
    using Machine.Specifications;
    using ShapeReader;
    using Functional.ChainOfResponsibility;
    using static Functional.ChainOfResponsibility.MonadicLink<System.Xml.XmlReader, string>;

    [Subject("Shape reader")]
    sealed class WhenShapeReaderReadsCircleElement
    {
        Establish _context = () => xmlReader = new XmlTextReader(new StringReader("<Circle radius='75' />"));

        Because _of = () =>
        {
            xmlReader.Read();
            var chain = Create<SquareReader>(xmlReader)
                .BindTo(Create<CircleReader>);
            radius = chain().Process();
        };

        It _shouldReadRadiusOf75 = () => radius.ShouldEqual("75");

        static XmlReader xmlReader;
        static string radius;
    }
}

