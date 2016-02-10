// =================================================
// <copyright file="SquareReader.cs">
//     Copyright (c) 2016 seb!
// </copyright>
// <author>seb!</author>
// =================================================

using System.Xml;
using FuncPatterns.Functional.ChainOfResponsibility;

namespace FuncPatterns.Tests.ShapeReader
{
    sealed class SquareReader : MonadicLink<XmlReader, string>
    {
        protected override string ProcessCore()
            => Input.Name == "Square" ? Input.GetAttribute("length") : null;
    }
}
