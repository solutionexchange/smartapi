// Smart API - .Net programmatic access to RedDot servers
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

using System.Collections.Generic;
using System.Linq;
using System.Xml;
using erminas.SmartAPI.CMS.Project.ContentClasses;
using erminas.SmartAPI.Utils;
using erminas.SmartAPI.Utils.CachedCollections;

namespace erminas.SmartAPI.CMS.Project.Filesystem
{
    public interface IContentClassFolder : IRedDotObject, IProjectObject
    {
        /// <summary>
        ///     All content classes contained in this folder, indexed by name. The list is cached by default.
        /// </summary>
        IIndexedRDList<string, IContentClass> ContentClasses { get; }
    }

    /// <summary>
    ///     A folder containing content classes.
    /// </summary>
    internal class ContentClassFolder : RedDotProjectObject, IContentClassFolder
    {
        private readonly IProject _project;

        internal ContentClassFolder(IProject project, XmlElement xmlElement) : base(project, xmlElement)
        {
            ContentClasses = new NameIndexedRDList<IContentClass>(GetContentClasses, Caching.Enabled);
            _project = project;
        }

        /// <summary>
        ///     All content classes contained in this folder, indexed by name. The list is cached by default.
        /// </summary>
        public IIndexedRDList<string, IContentClass> ContentClasses { get; private set; }

        private List<IContentClass> GetContentClasses()
        {
            // RQL for listing all content classes of a folder. 
            // One Parameter: Folder Guid: {0}
            const string LIST_CC_OF_FOLDER = @"<TEMPLATES folderguid=""{0}"" action=""list""/>";

            XMLDoc = _project.ExecuteRQL(string.Format(LIST_CC_OF_FOLDER, Guid.ToRQLString()));

            return
                (from XmlElement curNode in XMLDoc.GetElementsByTagName("TEMPLATE")
                 select (IContentClass) new ContentClass(_project, curNode)).ToList();
        }
    }
}