// =================================================
// <copyright file="CircleReader.cs" company="seb!">
//     Copyright (c) 2016
// </copyright>
// <author>s.mach</author>
// =================================================

using System.Xml;
using FuncPatterns.ChainOfResponsibility;

namespace FuncPatterns.Tests.ShapeReader
{
    sealed class CircleReader : MonadicLink<XmlReader, string>
    {
        protected internal override bool IsApplicableFor(XmlReader input) => input.Name == "Circle";

        protected internal override string Resolve(XmlReader input) => input.GetAttribute("radius");
    }
}