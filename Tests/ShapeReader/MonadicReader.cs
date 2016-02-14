// =================================================
// <copyright file="MonadicReader.cs" company="seb!">
//     Copyright (c) 2016
// </copyright>
// <author>s.mach</author>
// =================================================

using System.Xml;
using FuncPatterns.ChainOfResponsibility;
using static FuncPatterns.ChainOfResponsibility.MonadicLink<System.Xml.XmlReader, string>;

namespace FuncPatterns.Tests.ShapeReader
{
    static class MonadicReader
    {
        internal static MonadicLink<XmlReader, string> Create()
            => Unit<SquareReader>()
                .Bind(Unit<CircleReader>)();
    }
}