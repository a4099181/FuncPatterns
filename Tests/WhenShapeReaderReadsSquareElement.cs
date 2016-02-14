// =================================================
// <copyright file="WhenShapeReaderReadsSquareElement.cs" company="seb!">
//     Copyright (c) 2016
// </copyright>
// <author>s.mach</author>
// =================================================

using System.IO;
using System.Xml;
using FuncPatterns.ChainOfResponsibility;
using FuncPatterns.Tests.ShapeReader;
using Machine.Specifications;

namespace FuncPatterns.Tests
{
    [Subject("Shape reader")]
    sealed class WhenShapeReaderReadsSquareElement
    {
        Establish _context = () =>
        {
            _xmlReader = new XmlTextReader(new StringReader("<Square length='25' />"));
            _chain = MonadicReader.Create();
        };

        Because _of = () =>
        {
            _xmlReader.Read();
            _length = _chain.Process(_xmlReader);
        };

        It _shouldReadLengthOf25 = () => _length.ShouldEqual("25");
        static MonadicLink<XmlReader, string> _chain;
        static string _length;

        static XmlReader _xmlReader;
    }
}