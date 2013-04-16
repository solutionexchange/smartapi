﻿// Smart API - .Net programmatic access to RedDot servers
//  
// Copyright (C) 2013 erminas GbR
// 
// This program is free software: you can redistribute it and/or modify it 
// under the terms of the GNU General Public License as published by the Free Software Foundation,
// either version 3 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License along with this program.
// If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Xml;
using erminas.SmartAPI.Utils;

namespace erminas.SmartAPI.CMS.Project.ContentClasses.Elements
{
    public interface IInfoAttribute
    {
        int Id { get; }
        string Name { get; }
        InfoType Type { get; }
    }

    internal class InfoAttribute : IInfoAttribute
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public InfoType Type { get; private set; }

        internal InfoAttribute(XmlElement xmlElement)
        {
            Type = (InfoType) Enum.Parse(typeof (InfoType), xmlElement.Name, true);
            Id = int.Parse(xmlElement.GetAttributeValue("id"));
            Name = xmlElement.GetAttributeValue("name");
        }
    }

    public enum InfoType
    {
        PageInfo,
        ProjectInfo,
        SessionObject
    }
}