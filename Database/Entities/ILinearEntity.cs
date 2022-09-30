using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    /// <summary>
    /// The stuff that all Linear entities need to have
    /// </summary>
    public interface ILinearEntity
    {
        public string Id { get; set; }

        public DateTime ModifiedTime { get; set; }

        public DateTime CreatedTime { get; set; }
    }
}
