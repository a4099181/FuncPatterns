// =================================================
// <copyright file="WhenShapeReaderReadsCircleElement.cs">
//     Copyright (c) 2016 seb!
// </copyright>
// <author>seb!</author>
// =================================================

using System.IO;
using System.Xml;
using FuncPatterns.Functional.ChainOfResponsibility;
using FuncPatterns.Tests.ShapeReader;
using Machine.Specifications;

namespace FuncPatterns.Tests
{
    [Subject("Shape reader")]
    sealed class WhenShapeReaderReadsCircleElement
    {
        static XmlReader _xmlReader;
        static string _radius;
        Establish _context = () => _xmlReader = new XmlTextReader(new StringReader("<Circle radius='75' />"));

        Because _of = () =>
        {
            _xmlReader.Read();
            var chain = MonadicLink.Create(new SquareReader {Input = _xmlReader})
                .BindTo(l => MonadicLink.Create(new CircleReader {Input = l.Input}));
            _radius = chain().Process();
        };

        It _shouldReadRadiusOf75 = () => _radius.ShouldEqual("75");
    }
}