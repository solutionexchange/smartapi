﻿// SmartAPI - .Net programmatic access to RedDot servers
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

using System.Xml;
using erminas.SmartAPI.Utils;

namespace erminas.SmartAPI.CMS.Project.Workflows
{
    public interface INote : IRedDotObject, IProjectObject
    {
        NoteType Type { get; }
        string Value { get; }
    }

    internal class Note : RedDotProjectObject, INote
    {
        public readonly IWorkflow Workflow;

        internal Note(IWorkflow workflow, XmlElement xmlElement) : base(workflow.Project, xmlElement)
        {
            Workflow = workflow;
            LoadXml();
        }

        public NoteType Type { get; private set; }
        public string Value { get; private set; }

        private void LoadXml()
        {
            Value = _xmlElement.GetAttributeValue("value");
            Type = (NoteType) _xmlElement.GetIntAttributeValue("type").GetValueOrDefault();
        }
    }

    public enum NoteType
    {
        Line = 1,
        TextField = 2
    }
}