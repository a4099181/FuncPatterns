// =================================================
// <copyright file="CircleReader.cs">
//     Copyright (c) 2016 seb!
// </copyright>
// <author>seb!</author>
// =================================================

using System.Xml;
using FuncPatterns.Functional.ChainOfResponsibility;

namespace FuncPatterns.Tests.ShapeReader
{
    sealed class CircleReader : MonadicLink<XmlReader, string>
    {
        protected override string ProcessCore()
            => Input.Name == "Circle" ? Input.GetAttribute("radius") : null;
    }
}
