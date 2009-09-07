using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XStreamer.Data.Exception;

namespace XStreamer.Data.Interface
{
    /// <summary>
    /// Provide facilities for managing shares for XStreamer
    /// </summary>
    interface IShareDataProvider
    {
        /// <summary>
        /// Adds the share.
        /// </summary>
        /// <param name="name">The name of the share.</param>
        /// <param name="path">The filesystem path for the share.</param>
        /// <exception cref="ShareExistsException">
        /// If a share with the specified name already exists
        /// </exception>
        void AddShare(string name, string path);

        /// <summary>
        /// Deletes the share with the specified name.
        /// </summary>
        /// <param name="name">The name of the share to delete.</param>
        /// <exception cref="ShareNotFoundException">
        /// When the specified share does not exist
        /// </exception>
        void DeleteShare(string name);

        /// <summary>
        /// Updates the path for the specified share.
        /// </summary>
        /// <param name="name">The name of the share.</param>
        /// <param name="path">The new path.</param>
        /// 
        /// <exception cref="ShareNotFoundException">
        /// When the specified share does not exist
        /// </exception>
        void UpdatePathForShare(string name, string path);

        /// <summary>
        /// Updates the name for share.
        /// </summary>
        /// <param name="oldName">The old name.</param>
        /// <param name="newName">The new name.</param>
        /// 
        /// <exception cref="ShareNotFoundException">
        /// When the specified share does not exist
        /// </exception>
        void UpdateNameForShare(string oldName, string newName);

        /// <summary>
        /// Return an <see cref="IEnumerable{T}"/> containing all the
        /// share names.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> containing all the
        /// share names</returns>
        IEnumerable<string> ShareNames();

        /// <summary>
        /// Returns the root path for the specified share.
        /// </summary>
        /// <param name="name">The name of the share.</param>
        /// <returns>The root path for the specified share.</returns>
        /// <exception cref="ShareNotFoundException">
        /// When the specified share does not exist
        /// </exception>
        string PathForShare(string name);


    }
}
