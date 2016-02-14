// =================================================
// <copyright file="WhenShapeReaderReadsCircleElement.cs" company="seb!">
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
    sealed class WhenShapeReaderReadsCircleElement
    {
        Establish _context = () =>
        {
            _xmlReader = new XmlTextReader(new StringReader("<Circle radius='75' />"));
            _chain = MonadicReader.Create();
        };

        Because _of = () =>
        {
            _xmlReader.Read();
            _radius = _chain.Process(_xmlReader);
        };

        It _shouldReadRadiusOf75 = () => _radius.ShouldEqual("75");
        static MonadicLink<XmlReader, string> _chain;
        static string _radius;

        static XmlReader _xmlReader;
    }
}