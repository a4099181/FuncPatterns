// =================================================
// <copyright file="WhenShapeReaderReadsSquareElement.cs">
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
    sealed class WhenShapeReaderReadsSquareElement
    {
        Establish _context = () => _xmlReader = new XmlTextReader(new StringReader("<Square length='25' />"));

        Because _of = () =>
        {
            _xmlReader.Read();
            var chain = MonadicLink.Create(new SquareReader {Input = _xmlReader})
                .BindTo(l => MonadicLink.Create(new CircleReader {Input = l.Input}));
            _length = chain().Process();
        };

        It _shouldReadLengthOf25 = () => _length.ShouldEqual("25");

        static XmlReader _xmlReader;
        static string _length;
    }
}
