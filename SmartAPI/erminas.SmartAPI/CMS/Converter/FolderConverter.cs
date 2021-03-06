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

using System;
using System.Linq;
using System.Xml;
using erminas.SmartAPI.CMS.Project;
using erminas.SmartAPI.CMS.Project.Folder;
using erminas.SmartAPI.Exceptions;

namespace erminas.SmartAPI.CMS.Converter
{
    internal class FolderConverter : AbstractGuidElementConverter<IFolder>
    {
        protected override IFolder GetFromGuid(IProjectObject parent, XmlElement element, RedDotAttribute attribute,
                                               Guid guid)
        {
            return parent.Project.Folders.AllIncludingSubFolders.First(folder => folder.Guid == guid);
        }

        protected override IFolder GetFromName(IProjectObject parent, IXmlReadWriteWrapper element,
                                               RedDotAttribute attribute, IFolder value)
        {
            var folders = parent.Project.Folders.AllIncludingSubFolders;
            var folder = folders.FirstOrDefault(folder1 => folder1.Name == value.Name);
            if (folder == null)
            {
                throw new SmartAPIException(parent.Session.ServerLogin,
                                            string.Format("Could not find a folder with name {0} in project {1}",
                                                          value.Name, parent.Project));
            }
            return folder;
        }
    }
}