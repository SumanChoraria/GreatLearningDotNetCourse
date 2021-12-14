using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SumanChoraria_Sprint1.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ProjectModel:BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedOn { get; set; }
    }
}
