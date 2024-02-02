using System.Collections.Generic;

namespace Constructorio_NET.Models
{
    public interface IUserDetails
    {
        /// <summary>
        /// Gets or sets user test cells.
        /// </summary>
        Dictionary<string, string> TestCells { get; set; }

        /// <summary>
        /// Gets or sets collection of user related data.
        /// </summary>
        UserInfo UserInfo { get; set; }
    }
}
